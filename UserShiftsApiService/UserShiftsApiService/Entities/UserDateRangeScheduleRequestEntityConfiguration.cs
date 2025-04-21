using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserShiftsApiService.Entities;


public class UserDateRangeScheduleRequestEntityTypeConfiguration : IEntityTypeConfiguration<UserDateRangePreferenceRequestEntity>
{
    public void Configure(EntityTypeBuilder<UserDateRangePreferenceRequestEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.StartingDate).IsRequired();
        builder.Property(p => p.EndingDate).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        
        builder.HasOne(dateRangePrefs => dateRangePrefs.User)
            .WithMany(u => u.DateRangePreferences)
            .HasForeignKey(dateRangePrefs => dateRangePrefs.UserId);
        
        builder.Property(p => p.RequestType)
            .HasConversion(new EnumToStringConverter<ShiftRequestType>())
            .IsRequired();
    }
}