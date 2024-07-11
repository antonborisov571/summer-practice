using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kfoodle.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(x => x.QuestionText)
            .IsRequired();

        builder.HasOne(x => x.Test)
            .WithMany(y => y.Questions);

        builder.HasMany(x => x.Choices)
            .WithOne(y => y.Question);
    }
}