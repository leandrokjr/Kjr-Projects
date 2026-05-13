using CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudGames.Infrastructure.Repositories.Configuration;

public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
               .WithMany(u => u.Libraries)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Game)
               .WithMany(g => g.Libraries)
               .HasForeignKey(e => e.GameId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
