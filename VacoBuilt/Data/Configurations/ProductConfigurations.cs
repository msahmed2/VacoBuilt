using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacoBuilt.Data;
using VacoBuilt.Data;

namespace VacoBuilt.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Fluent API configuration for the Product entity
        builder.ToTable("Products"); // Optional: define the table name

        builder.HasKey(p => p.Id); // Define the primary key

        builder.Property(p => p.Name)
               .IsRequired()  // Make the Name property required
               .HasMaxLength(100); // Set max length for Name

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)"); // Specify decimal precision
    }
}
