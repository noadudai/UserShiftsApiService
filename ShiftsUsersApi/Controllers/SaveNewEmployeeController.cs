using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShiftsUsersApi.Models;
using ShiftsUsersApi.Services;

namespace ShiftsUsersApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    
    public class SaveNewEmployeeController : ControllerBase
    {
        private readonly ISaveNewEmployeeService _saveNewEmployeeService;

        public SaveNewEmployeeController(ISaveNewEmployeeService saveNewEmployeeService)
        {
            _saveNewEmployeeService = saveNewEmployeeService;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> SaveEmployee(EmployeeItem employee)
        {
            await _saveNewEmployeeService.SaveNewEmployee(employee);
            
            return Ok("Employee Saved!");
        }
    }
}
