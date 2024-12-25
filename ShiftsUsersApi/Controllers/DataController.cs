using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftsUsersApi.Services;

namespace ShiftsUsersApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IOutputDataService _outputDataService;

        public DataController(IOutputDataService outputDataService)
        {
            _outputDataService = outputDataService;
        }
        
        [HttpGet("{data}")]
        [Authorize]
        public IActionResult PrintData(string data)
        {
            _outputDataService.OutputData(data);
            
            return Ok($"Data printed: {data}");
        }
    }
}
