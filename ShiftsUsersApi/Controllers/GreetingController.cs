using Microsoft.AspNetCore.Mvc;
using ShiftsUsersApi.Services;

namespace ShiftsUsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
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
}

