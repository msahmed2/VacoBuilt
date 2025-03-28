using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacoBuilt.Data;
using VacoBuilt.Data;
using VacoBuilt.Entities;

namespace VacoBuilt.Data.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(bp => bp.Id);
        builder.Property(bp => bp.Title).IsRequired().HasMaxLength(255);
        builder.Property(bp => bp.Contents).IsRequired().HasMaxLength(1000);
        builder.Property(bp => bp.Timestamp).IsRequired();
        builder.HasOne(bp => bp.Category)
               .WithMany(c => c.BlogPosts)
               .HasForeignKey(bp => bp.CategoryId);
    }
}
