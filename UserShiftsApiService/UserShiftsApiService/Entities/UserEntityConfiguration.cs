using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;


public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(p => p.AuthSub).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        
        builder.HasMany(u => u.vacations)
            .WithOne(u => u.User)
            .HasForeignKey(u=>u.UserId);
    }
}