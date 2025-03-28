using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacoBuilt.Entities;

namespace VacoBuilt.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasData(
                new Category { Id = 1, Name = "General" },
                new Category { Id = 2, Name = "Technology" },
                new Category { Id = 3, Name = "Random" }
            );
        }
    }
}
