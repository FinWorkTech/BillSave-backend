using BillSave.API.IAM.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Application database context for the Learning Center Platform 
/// </summary>
/// <param name="options">
/// The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   /// <summary>
   /// On configuring the database context 
   /// </summary>
   /// <remarks>
   /// This method is used to configure the database context.
   /// It also adds the created and updated date interceptor to the database context.
   /// </remarks>
   /// <param name="builder">
   /// The options builder for the database context
   /// </param>
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
   {
      builder.AddCreatedUpdatedInterceptor();
      base.OnConfiguring(builder);
   }

   /// <summary>
   /// On creating the database model 
   /// </summary>
   /// <remarks>
   /// This method is used to create the database model for the application.
   /// </remarks>
   /// <param name="builder">
   /// The model builder for the database context
   /// </param>
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
      
      // Profiles Context
      builder.Entity<Profile>().HasKey(p => p.Id);
      builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<Profile>().Property(p => p.FullName).IsRequired();
      
      // IAM Context
      builder.Entity<User>().HasKey(u => u.Id);
      builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<User>().Property(u => u.Username).IsRequired();
      builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
      
      // Portfolio Context
      builder.Entity<Pack>().HasKey(p => p.Id);
      builder.Entity<Pack>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<Pack>().Property(p => p.Name).IsRequired();
      builder.Entity<Pack>().Property(p => p.DiscountDate).IsRequired();
      builder.Entity<Pack>().Property(p => p.UserId).IsRequired();
      builder.Entity<Pack>().Property(p => p.TotalDocuments).IsRequired();
      builder.Entity<Pack>().OwnsOne(p => p.EffectiveAnnualCostRate,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Value).HasColumnName("EffectiveAnnualCostRate");
         });
      
      // Sales Context
      builder.Entity<Document>().HasKey(d => d.Id);
      builder.Entity<Document>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<Document>().Property(d => d.PortfolioId).IsRequired();
      builder.Entity<Document>().Property(d => d.Code).IsRequired();
      builder.Entity<Document>().OwnsOne(d => d.IssueDate,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Value).HasColumnName("IssueDate");
         });
      builder.Entity<Document>().OwnsOne(d => d.DueDate,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Value).HasColumnName("DueDate");
         });
      builder.Entity<Document>().OwnsOne(d => d.Rate,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Type).HasColumnName("RateType");
         });
      builder.Entity<Document>().OwnsOne(d => d.Rate,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Value).HasColumnName("RateValue");
         });
      builder.Entity<Document>().OwnsOne(d => d.Currency,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(p => p.Code).HasColumnName("Currency");
         });
      
      // Bounded Context
      //...
      
      // Profiles Context
        
      /*builder.Entity<Profile>().HasKey(p => p.Id);
      builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<Profile>().OwnsOne(p => p.Name,
         n =>
         {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
         });

      builder.Entity<Profile>().OwnsOne(p => p.Email,
         e =>
         {
            e.WithOwner().HasForeignKey("Id");
            e.Property(a => a.Address).HasColumnName("EmailAddress");
         });

      builder.Entity<Profile>().OwnsOne(p => p.Address,
         a =>
         {
            a.WithOwner().HasForeignKey("Id");
            a.Property(s => s.Street).HasColumnName("AddressStreet");
            a.Property(s => s.Number).HasColumnName("AddressNumber");
            a.Property(s => s.City).HasColumnName("AddressCity");
            a.Property(s => s.PostalCode).HasColumnName("AddressPostalCode");
            a.Property(s => s.Country).HasColumnName("AddressCountry");
         });
      
      // IAM Context
      builder.Entity<User>().HasKey(u => u.Id);
      builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<User>().Property(u => u.Username).IsRequired();
      builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
      */
      
      
      // Apply snake case naming convention
      builder.UseSnakeCaseNamingConvention();
   }
}