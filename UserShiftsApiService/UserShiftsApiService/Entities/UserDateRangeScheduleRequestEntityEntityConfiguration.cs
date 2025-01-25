using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;


public class UserDateRangeScheduleRequestEntityTypeConfiguration : IEntityTypeConfiguration<UserDateRangeScheduleRequestEntity>
{
    public void Configure(EntityTypeBuilder<UserDateRangeScheduleRequestEntity> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.RequestType).IsRequired();
        
        builder.HasOne(vac => vac.User)
            .WithMany(u => u.vacations)
            .HasForeignKey(vac => vac.UserId);
    }
}