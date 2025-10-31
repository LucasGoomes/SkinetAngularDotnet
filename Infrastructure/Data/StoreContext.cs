using Core.Entities;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; } // DbSet for Product entity - represents Products table in the database
    public DbSet<Address> Addresses { get; set; } // DbSet for Address entity - represents Addresses table in the database
    // each DbSet corresponds to a table in the database - EF Core uses these to track and query entities - 
    // need to run 'dotnet ef migrations add <MigrationName - AdressAdded> -s API -p Infrastructure' to create migration after adding new DbSet 
    // -s specifies startup project, -p specifies project containing the DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly); // Apply all configurations from the current assembly
    }
}
