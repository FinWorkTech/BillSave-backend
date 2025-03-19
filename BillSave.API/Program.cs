using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using BillSave.API.IAM.Infrastructure.Config;
using BillSave.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using BillSave.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;

using BillSave.API.Portfolio.Application.Internal.EventHandlers;

using BillSave.API.Sales.Infrastructure.Config;
using BillSave.API.Profiles.Infrastructure.Config;
using BillSave.API.Portfolio.Infrastructure.Config;

using BillSave.API.Shared.Domain.Services;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Shared.Infrastructure.Services;
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
builder.Services.AddPortfolioServices();

// Sales Bounded Context Dependency Injection Configuration
builder.Services.AddSalesServices();

// Profiles Bounded Context Dependency Injection Configuration
builder.Services.AddSProfilesServices();

// IAM Bounded Context Dependency Injection and TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddIamServices();

// Common Exception Handling Middleware
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddProblemDetails();

// External Services
builder.Services.AddHttpClient<ISunatService, SunatService>();

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