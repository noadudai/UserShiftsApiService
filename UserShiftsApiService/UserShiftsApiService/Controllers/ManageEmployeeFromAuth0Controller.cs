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
[Route("/auth0-maintenance/")]
public class ManageEmployeeFromAuth0Controller : ControllerBase
{
    private readonly IManageUserFromAuth0Service _manageUserFromAuth0Service;

    public ManageEmployeeFromAuth0Controller(IManageUserFromAuth0Service manageUserFromAuth0Service)
    {
        _manageUserFromAuth0Service = manageUserFromAuth0Service;
    }
    

    [HttpPost]
    [Route("save-new-user")]
    [ServiceFilter<RequireHmacSignatureFilter>]
    public async Task<IActionResult> SaveNewEmployee(Auth0UserModel auth0User)
    {
        await _manageUserFromAuth0Service.SaveNewEmployeeAsync(auth0User);
        
        return Ok("User Saved!");
    }
}