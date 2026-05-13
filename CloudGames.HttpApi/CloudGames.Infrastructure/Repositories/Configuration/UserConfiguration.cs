using CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudGames.Infrastructure.Repositories.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Email).HasColumnType("VARCHAR(60)").IsRequired().HasMaxLength(60);
        builder.Property(e => e.Password).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(e => e.Administrator).HasColumnType("BOOL").IsRequired();
        builder.HasMany(u => u.Libraries)
               .WithOne(l => l.User)
               .HasForeignKey(l => l.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}