using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Middlewares;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("/session/")]
public class SessionController : ControllerBase
{
    private readonly IUserContextProvider _userContextProvider;

    public SessionController(IUserContextProvider userContextProvider)
    {
        _userContextProvider = userContextProvider;
    }

    [HttpGet]
    [Route("me")]
    [Authorize]
    [ServiceFilter<UserContextProviderMiddleware>]
    public ActionResult<CurrentUserResponseModel> GetCurrentUser()
    {
        var userContext = _userContextProvider.GetUserContext();
        if (userContext == null)
        {
            return Unauthorized();
        }

        return Ok(new CurrentUserResponseModel
        {
            Name = userContext.DisplayName,
            Email = userContext.Email,
            Role = userContext.Role,
        });
    }
}
