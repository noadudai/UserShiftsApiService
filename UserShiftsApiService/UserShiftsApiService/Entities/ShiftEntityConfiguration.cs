using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserShiftsApiService.Entities;

public class ShiftEntityConfiguration : IEntityTypeConfiguration<ShiftEntity>
{
    public void Configure(EntityTypeBuilder<ShiftEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        
        builder.Property(p => p.ShiftType)
            .HasConversion(new EnumToStringConverter<ShiftType>())
            .IsRequired();
    }
}
