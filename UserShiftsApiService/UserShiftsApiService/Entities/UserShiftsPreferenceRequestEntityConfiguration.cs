using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserShiftsApiService.Entities;

public class UserShiftsPreferenceRequestEntityConfiguration : IEntityTypeConfiguration<UserShiftsPreferenceRequestEntity>
{
    public void Configure(EntityTypeBuilder<UserShiftsPreferenceRequestEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.UserId).IsRequired();
        
        builder.HasOne(p => p.User)
            .WithMany(u => u.ShiftsByIdPreferences)
            .HasForeignKey(shiftPref => shiftPref.UserId);

        builder.Property(p => p.ShiftRequestType)
            .HasConversion(new EnumToStringConverter<ShiftRequestType>())
            .IsRequired();

        builder.HasMany(p => p.RequestedShifts)
            .WithOne()
            .HasForeignKey(p => p.UserShiftsPreferenceRequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
