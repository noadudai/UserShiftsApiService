using System;
using Microsoft.EntityFrameworkCore;
using ShiftsUsersApi.Entities;

namespace ShiftsUsersApi;

public class ShiftsSchedulingContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    
    public ShiftsSchedulingContext(DbContextOptions<ShiftsSchedulingContext> options)
        : base(options)
    {
    }
}