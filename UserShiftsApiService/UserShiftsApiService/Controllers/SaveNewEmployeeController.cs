using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/save-new-employee")]
public class SaveNewEmployeeController : ControllerBase
{
    private readonly ISaveNewEmployeeService _saveNewEmployeeService;

    public SaveNewEmployeeController(ISaveNewEmployeeService saveNewEmployeeService)
    {
        _saveNewEmployeeService = saveNewEmployeeService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveNewEmployee(EmployeeItem employee)
    {
        await _saveNewEmployeeService.SaveNewEmployee(employee);
        
        return Ok("Employee Saved!");
    }
}