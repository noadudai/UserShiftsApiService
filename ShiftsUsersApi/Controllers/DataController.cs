using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftsUsersApi;
using ShiftsUsersApi.Entities;
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
        // Authorization: Bearer <token>: extract the token & validate against the options.Authority options.Audience 
        public async Task<IActionResult> PrintData(string data)
        {
            _outputDataService.OutputData(data);
            
            return Ok($"Data printed: {data}");
        }
    }
}
