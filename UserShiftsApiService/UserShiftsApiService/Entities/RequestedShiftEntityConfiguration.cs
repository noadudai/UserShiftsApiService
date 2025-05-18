using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class RequestedShiftEntityConfiguration : IEntityTypeConfiguration<RequestedShiftEntity>
{
    public void Configure(EntityTypeBuilder<RequestedShiftEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ShiftId).IsRequired();
        builder.Property(p => p.UserShiftsPreferenceRequestId).IsRequired();
        
        builder.HasOne(p => p.Shift).WithMany().HasForeignKey(p => p.ShiftId);
    }
}
