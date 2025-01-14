using System;
using System.Linq;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class ManageEmployeeFromAuth0Service : IManageEmployeeFromAuth0Service
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public ManageEmployeeFromAuth0Service(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveNewEmployeeAsync(Auth0EmployeeModel auth0EmployeeModel)
    {
        var user = _dbContext.Users.Any(u => u.AuthSub == auth0EmployeeModel.UserId);
        if (!user)
        {
            _dbContext.Add(new UserEntity
            {
                AuthSub = auth0EmployeeModel.UserId,
                Email = auth0EmployeeModel.UserEmail,
                Id = Guid.NewGuid().ToString(),
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}