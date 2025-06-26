using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class ScheduleEntityConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.HasMany(p => p.ShiftsInSchedule)
            .WithOne()
            .HasForeignKey(p => p.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}