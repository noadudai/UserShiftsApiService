using System;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftsSchedulingContext _dbContext;

    public ShiftService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateNewShiftAsync(ShiftModel shiftModel)
    {
        _dbContext.Add(new ShiftEntity
        {
            Id = Guid.NewGuid().ToString(),
            ShiftType = shiftModel.ShiftType,
            EndDate = shiftModel.EndDate,
            StartDate = shiftModel.StartDate,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}