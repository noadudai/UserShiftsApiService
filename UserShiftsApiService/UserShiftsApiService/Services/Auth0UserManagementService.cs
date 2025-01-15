using System;
using System.Linq;
using System.Threading.Tasks;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public class Auth0UserManagementService : IAuth0UserManagementService
{
    private readonly ShiftsSchedulingContext _dbContext;
    
    public Auth0UserManagementService(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveNewUserAsync(Auth0UserModel auth0UserModel)
    {
        var user = _dbContext.Users.Any(u => u.AuthSub == auth0UserModel.UserId);
        if (!user)
        {
            _dbContext.Add(new UserEntity
            {
                AuthSub = auth0UserModel.UserId,
                Email = auth0UserModel.UserEmail,
                Id = Guid.NewGuid().ToString(),
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}