using System;
using System.Linq;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class SaveNewEmployeeService : ISaveNewEmployeeService
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public SaveNewEmployeeService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveNewEmployee(EmployeeItem employee)
    {
        var user = _dbContext.Users.Any(u => u.AuthSub == employee.user_id);
        if (!user)
        {
            _dbContext.Add(new UserEntity
            {
                AuthSub = employee.user_id,
                Email = employee.user_email,
                Id = Guid.NewGuid().ToString(),
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}