using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface ISaveNewEmployeeFromAuth0Service
{ 
    Task SaveNewEmployeeAsync(Auth0EmployeeModel auth0EmployeeModel);
}