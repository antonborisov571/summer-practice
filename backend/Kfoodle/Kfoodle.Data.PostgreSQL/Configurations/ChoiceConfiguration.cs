using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kfoodle.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.Property(x => x.ChoiceText)
            .IsRequired();

        builder.HasOne(x => x.Question)
            .WithMany(y => y.Choices);
    }
}