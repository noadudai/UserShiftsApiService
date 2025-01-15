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
public class Auth0MaintenanceController : ControllerBase
{
    private readonly IAuth0UserManagementService _auth0UserManagementService;

    public Auth0MaintenanceController(IAuth0UserManagementService auth0UserManagementService)
    {
        _auth0UserManagementService = auth0UserManagementService;
    }
    

    [HttpPost]
    [Route("save-new-user")]
    [ServiceFilter<RequireHmacSignatureFilter>]
    public async Task<IActionResult> SaveNewUserAsync(Auth0UserModel auth0User)
    {
        await _auth0UserManagementService.SaveNewUserAsync(auth0User);
        
        return Ok("User Saved!");
    }
}