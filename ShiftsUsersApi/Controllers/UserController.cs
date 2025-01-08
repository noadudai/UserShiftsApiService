using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsUsersApi.Services;


namespace ShiftsUsersApi.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        private readonly ShiftsSchedulingContext _dbContext;

        public UserController(ShiftsSchedulingContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Authorize] 
        // Authorization: Bearer <token>: extract the token & validate against the options.Authority options.Audience 
        public async Task<IActionResult> GetUser()
        {
            var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.Users.SingleAsync(u => u.AuthSub == sub);
            
            Console.WriteLine(user.Id);
            return Ok(user.Id);

        }
    }
}