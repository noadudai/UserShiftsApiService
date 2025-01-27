using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;

namespace UserShiftsApiService.Models;

public class ShiftsSchedulingContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserDateRangeScheduleRequestEntity> UserDateRangeScheduleRequests { get; set; }

    public ShiftsSchedulingContext(DbContextOptions<ShiftsSchedulingContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShiftsSchedulingContext).Assembly);
    }    
}