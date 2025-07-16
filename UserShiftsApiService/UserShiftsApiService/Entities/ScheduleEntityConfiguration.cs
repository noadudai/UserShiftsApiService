using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class ScheduleEntityConfiguration :IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.Property(p => p.CreationDate).IsRequired();

        builder.HasOne(p => p.Manager)
            .WithMany()
            .HasForeignKey(p => p.CreatedByManagerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}