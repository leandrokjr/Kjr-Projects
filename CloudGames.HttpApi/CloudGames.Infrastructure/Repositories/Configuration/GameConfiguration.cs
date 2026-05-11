using CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudGames.Infrastructure.Repositories.Configuration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Title).IsRequired().HasColumnType("VARCHAR(60)").HasMaxLength(60);
        builder.Property(e => e.Price).HasColumnType("DECIMAL(14,2)").IsRequired();
        builder.Property(e => e.CurrentPrice).HasColumnType("DECIMAL(14,2)");
    }
}
