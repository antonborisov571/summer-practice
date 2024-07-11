using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kfoodle.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class TestAttemptConfiguration : IEntityTypeConfiguration<TestAttempt>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TestAttempt> builder)
    {
        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.HasOne(x => x.Test)
            .WithMany(y => y.TestAttempts);

        builder.HasMany(x => x.Answers)
            .WithOne(y => y.TestAttempt);

        builder.HasOne(x => x.User);
    }
}