using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IAuth0UserManagementService
{ 
    Task SaveNewUserAsync(Auth0UserModel auth0UserModel);
}