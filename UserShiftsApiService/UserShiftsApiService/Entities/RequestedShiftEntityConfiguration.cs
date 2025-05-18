using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserShiftsApiService.Entities;

public class RequestedShiftEntityConfiguration : IEntityTypeConfiguration<RequestedShiftEntity>
{
    public void Configure(EntityTypeBuilder<RequestedShiftEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ShiftId).IsRequired();
        builder.Property(p => p.UserShiftsRequestId).IsRequired();
        
        builder.HasOne(RequestedShift => RequestedShift.UserShiftsRequest)
            .WithMany(req => req.RequestedShifts)
            .HasForeignKey(RequestedShift => RequestedShift.UserShiftsRequestId);
        
        builder.HasOne(RequestedShift => RequestedShift.Shift)
            .WithMany(s =>  s.ShiftRequests)
            .HasForeignKey(RequestedShift => RequestedShift.ShiftId);
    }
}
