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
                // New Auth0 users are created as employees by default.
                // Manager access is assigned manually in Auth0 for now, and can later
                // be updated from the future employee-management UI.
                Role = UserRole.Employee,
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}