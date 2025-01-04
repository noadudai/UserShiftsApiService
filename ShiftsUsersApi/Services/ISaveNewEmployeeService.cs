using System.Threading.Tasks;
using ShiftsUsersApi.Models;

namespace ShiftsUsersApi.Services
{
    public interface ISaveNewEmployeeService
    {
        public Task SaveNewEmployee(EmployeeItem employee);
    }
}

