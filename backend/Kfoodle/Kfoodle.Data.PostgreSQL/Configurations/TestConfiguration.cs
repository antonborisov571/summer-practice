using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kfoodle.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(y => y.AuthorTests);

        builder.HasMany(x => x.Questions)
            .WithOne(y => y.Test);
    }
}