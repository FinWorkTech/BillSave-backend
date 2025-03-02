using BillSave.API.IAM.Application.CommandServices;
using BillSave.API.IAM.Application.OutboundServices;
using BillSave.API.IAM.Application.QueryServices;
using BillSave.API.IAM.Domain.Repositories;
using BillSave.API.IAM.Domain.Services;
using BillSave.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using BillSave.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using BillSave.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using BillSave.API.IAM.Infrastructure.Tokens.JWT.Services;
using BillSave.API.Portfolio.Application.ACL.InboundServices;
using BillSave.API.Portfolio.Application.ACL.OutboundServices;
using BillSave.API.Portfolio.Application.Internal.CommandServices;
using BillSave.API.Portfolio.Application.Internal.QueryServices;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Portfolio.Domain.Services;
using BillSave.API.Portfolio.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Portfolio.Interfaces.ACL;
using BillSave.API.Profiles.Application.ACL;
using BillSave.API.Profiles.Application.Internal.CommandServices;
using BillSave.API.Profiles.Application.Internal.QueryServices;
using BillSave.API.Profiles.Domain.Repositories;
using BillSave.API.Profiles.Domain.Services;
using BillSave.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Profiles.Interfaces.ACL;
using BillSave.API.Sales.Application.ACL.InboundServices;
using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Application.Internal.CommandServices;
using BillSave.API.Sales.Application.Internal.EventHandlers;
using BillSave.API.Sales.Application.Internal.QueryServices;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Sales.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Sales.Interfaces.ACL;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BillSave.API.Shared.Infrastructure.Pipeline.Middleware.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
    }
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BillSave.API",
        Version = "v1",
        Description = "Bill Save API",
        TermsOfService = new Uri("https://billsave.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "",
            Email = ""
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://apache.org/licenses/LICENSE-2.0.html")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// Add CORS policy
builder.Services.AddCors(options =>
    options.AddPolicy(
        "AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Portfolio Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IPackRepository, PackRepository>();
builder.Services.AddScoped<IPackCommandService, PackCommandService>();
builder.Services.AddScoped<IPackQueryService, PackQueryService>();
builder.Services.AddScoped<IPortfoliosContextFacade, PortfoliosContextFacade>();

// Sales Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentCommandService, DocumentCommandService>();
builder.Services.AddScoped<IDocumentQueryService, DocumentQueryService>();
builder.Services.AddScoped<ISalesContextFacade, SalesContextFacade>();
builder.Services.AddScoped<ExternalPortfolioService>();

// Profiles Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
builder.Services.AddScoped<ExternalSalesService>();

// IAM Bounded Context Dependency Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();

builder.Services.AddScoped<ExternalProfileService>();

// Common Exception Handling Middleware
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddProblemDetails();

// Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly, 
    typeof(DocumentChangedEventHandler).Assembly
));


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

// Enable Documentation Generation
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS
app.UseCors("AllowAllPolicy");

// Enable Request Authorization Middleware
app.UseRequestAuthorization();
 
// Enable Exception Handling Middleware
app.UseExceptionHandler();

// Other Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();