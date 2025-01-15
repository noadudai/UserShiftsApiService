using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IManageUserFromAuth0Service
{ 
    Task SaveNewEmployeeAsync(Auth0UserModel auth0UserModel);
}