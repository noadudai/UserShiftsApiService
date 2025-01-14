using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UserShiftsApiService.Models;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;


[ApiController]
[Route("/save-new-employee")]
public class SaveNewEmployeeFromAuth0Controller : ControllerBase
{
    private readonly ISaveNewEmployeeFromAuth0Service _saveNewEmployeeFromAuth0Service;
    private readonly IConfiguration _configuration;

    public SaveNewEmployeeFromAuth0Controller(ISaveNewEmployeeFromAuth0Service saveNewEmployeeFromAuth0Service,
        IConfiguration configuration)
    {
        _saveNewEmployeeFromAuth0Service = saveNewEmployeeFromAuth0Service;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> SaveNewEmployee(Auth0EmployeeModel auth0Employee)
    {
        if (auth0Employee.HostName != _configuration["Auth0:Domain"])
        {
            throw new Exception(HttpStatusCode.Unauthorized.ToString());
        }
        
        await _saveNewEmployeeFromAuth0Service.SaveNewEmployeeAsync(auth0Employee);
        
        return Ok("Employee Saved!");
    }
}