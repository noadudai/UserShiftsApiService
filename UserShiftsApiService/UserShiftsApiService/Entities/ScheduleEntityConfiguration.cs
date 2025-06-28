using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class ScheduleEntityConfiguration :IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.Property(p => p.CreationDate).IsRequired();
        builder.Property(p => p.CreatedByManagerId).IsRequired();
    }
}