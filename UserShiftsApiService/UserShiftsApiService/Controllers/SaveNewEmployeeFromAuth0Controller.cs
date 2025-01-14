using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UserShiftsApiService.ActionFilters;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;


[ApiController]
[Route("/save-new-employee")]
public class SaveNewEmployeeFromAuth0Controller : ControllerBase
{
    private readonly ISaveNewEmployeeFromAuth0Service _saveNewEmployeeFromAuth0Service;

    public SaveNewEmployeeFromAuth0Controller(ISaveNewEmployeeFromAuth0Service saveNewEmployeeFromAuth0Service)
    {
        _saveNewEmployeeFromAuth0Service = saveNewEmployeeFromAuth0Service;
    }
    

    [HttpPost]
    [ServiceFilter<RequireHmacSignatureFilter>]
    public async Task<IActionResult> SaveNewEmployee(Auth0EmployeeModel auth0Employee)
    {
        await _saveNewEmployeeFromAuth0Service.SaveNewEmployeeAsync(auth0Employee);
        
        return Ok("Employee Saved!");
    }
}