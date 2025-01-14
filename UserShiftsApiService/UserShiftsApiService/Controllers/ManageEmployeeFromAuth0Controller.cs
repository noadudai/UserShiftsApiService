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
[Route("/auth0-maintenance")]
public class ManageEmployeeFromAuth0Controller : ControllerBase
{
    private readonly IManageEmployeeFromAuth0Service _manageEmployeeFromAuth0Service;

    public ManageEmployeeFromAuth0Controller(IManageEmployeeFromAuth0Service manageEmployeeFromAuth0Service)
    {
        _manageEmployeeFromAuth0Service = manageEmployeeFromAuth0Service;
    }
    

    [HttpPost]
    [ServiceFilter<RequireHmacSignatureFilter>]
    public async Task<IActionResult> SaveNewEmployee(Auth0EmployeeModel auth0Employee)
    {
        await _manageEmployeeFromAuth0Service.SaveNewEmployeeAsync(auth0Employee);
        
        return Ok("Employee Saved!");
    }
}