using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserShiftsApiService.Entities;

public class ScheduleEntityConfiguration :IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.Property(p => p.CreationDate).IsRequired();
        
        builder.Property(p => p.Status)
            .HasConversion(new EnumToStringConverter<ScheduleStatus>())
            .IsRequired()
            .HasDefaultValue(ScheduleStatus.Draft);

        builder.HasOne(p => p.Manager)
            .WithMany()
            .HasForeignKey(p => p.CreatedByManagerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}