using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftsSchedulingContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserDateRangePreferenceRequestEntity> UserDateRangeScheduleRequests { get; set; }
    public DbSet<UserShiftPreferenceRequestEntity> UserShiftPreferenceRequests { get; set; }
    public DbSet<ShiftEntity> Shifts { get; set; }

    public ShiftsSchedulingContext(DbContextOptions<ShiftsSchedulingContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShiftsSchedulingContext).Assembly);
    }    
}