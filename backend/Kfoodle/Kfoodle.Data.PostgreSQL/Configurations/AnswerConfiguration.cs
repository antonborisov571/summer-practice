using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kfoodle.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasOne(x => x.TestAttempt)
            .WithMany(y => y.Answers);

        builder.HasOne(x => x.Question);

        builder.HasOne(x => x.SingleChoice)
            .WithMany() 
            .OnDelete(DeleteBehavior.Cascade);
    }
}