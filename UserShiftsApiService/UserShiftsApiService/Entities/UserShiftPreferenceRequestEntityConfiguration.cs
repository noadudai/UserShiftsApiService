using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class UserShiftPreferenceRequestEntityConfiguration : IEntityTypeConfiguration<UserShiftPreferenceRequestEntity>
{
    public void Configure(EntityTypeBuilder<UserShiftPreferenceRequestEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ShiftIds).IsRequired();
        builder.Property(p => p.ShiftRequestType).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        
        builder.HasOne(shiftPref => shiftPref.User)
            .WithMany(u => u.ShiftPreferences)
            .HasForeignKey(shiftPref => shiftPref.UserId);
    }
}
