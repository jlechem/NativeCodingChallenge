using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        public async Task<IActionResult> GetCurrentServerTime(bool simulateError = false)
        {
            if( simulateError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return Ok(DateTime.UtcNow);
            }
        }

    }
}