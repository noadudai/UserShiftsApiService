using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface ISaveNewEmployeeService
{
    public Task SaveNewEmployee(EmployeeItem employee);
}