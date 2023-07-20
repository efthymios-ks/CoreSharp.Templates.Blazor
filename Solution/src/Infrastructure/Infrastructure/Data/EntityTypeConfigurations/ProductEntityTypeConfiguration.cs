using CoreSharp.Templates.Blazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSharp.Templates.Blazor.Infrastructure.Data.EntityTypeConfigurations;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    // Methods
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.Description)
               .HasMaxLength(100);
    }
}
