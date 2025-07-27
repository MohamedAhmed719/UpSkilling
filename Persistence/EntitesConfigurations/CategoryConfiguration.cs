
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Api.Persistence.EntitesConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}
