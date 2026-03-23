using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserShiftsApiService.Entities;


public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(p => p.AuthSub).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Role)
            .HasConversion(new EnumToStringConverter<UserRole>())
            .IsRequired()
            .HasDefaultValue(UserRole.Employee);
    }
}