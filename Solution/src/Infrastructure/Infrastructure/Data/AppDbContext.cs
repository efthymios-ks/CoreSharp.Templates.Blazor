using CoreSharp.EntityFramework.DbContexts.Abstracts;
using CoreSharp.Templates.Blazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreSharp.Templates.Blazor.Infrastructure.Data;

internal sealed class AppDbContext : AuditableDbContextBase
{
    // Constructors
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Properties
    public DbSet<Product> Products { get; set; }

    // Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Always call base method 
        base.OnModelCreating(modelBuilder);

        ConfigureEnums(modelBuilder);
        ConfigureModels(modelBuilder);
    }

    private static void ConfigureEnums(ModelBuilder modelBuilder)
        => ArgumentNullException.ThrowIfNull(modelBuilder);

    private static void ConfigureModels(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
