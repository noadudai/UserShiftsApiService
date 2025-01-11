using Microsoft.AspNetCore.Mvc;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.Controllers;

[ApiController]
[Route("hello")]
public class GreetingController
{
    private readonly IGreetingService _greetingService;

    public GreetingController(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }

    [HttpGet]
    public ActionResult<string> GetGreeting()
    {
        return _greetingService.GetGreeting();
    }
}
