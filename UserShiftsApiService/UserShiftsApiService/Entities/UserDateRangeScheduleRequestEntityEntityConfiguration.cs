using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;


public class UserDateRangeScheduleRequestEntityTypeConfiguration : IEntityTypeConfiguration<UserDateRangePreferenceRequestEntity>
{
    public void Configure(EntityTypeBuilder<UserDateRangePreferenceRequestEntity> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.RequestType).IsRequired();
        
        builder.HasOne(vac => vac.UserId);
    }
}