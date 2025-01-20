using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/users/")]
public class UserController : ControllerBase
{
    private readonly ShiftsSchedulingContext _dbContext;

    public UserController(ShiftsSchedulingContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [Authorize]
    [HttpGet]
    [Route("get-user")]
    public async Task<IActionResult> GetUser()
    {
        var authSub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _dbContext.Users.SingleAsync(u => u.AuthSub == authSub);
        
        return Ok(user.Id);
    }
}