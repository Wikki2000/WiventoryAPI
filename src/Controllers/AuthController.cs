using Microsoft.AspNetCore.Mvc;
using WiventoryAPI.Services;
using WiventoryAPI.Errors; // For HttpError
using System.Collections.Generic;
using System.Threading.Tasks;
using WiventoryAPI.DTOs;


namespace WiventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto data)
        {
		/*
            if (data == null || data.Count == 0)
                return BadRequest(new { error = "Request body cannot be empty." });
		*/

            try
            {
                await _authService.LoginAsync(data);
                return Ok(new { message = "OTP sent successfully" });
            }
            catch (HttpError ex)
            {
                return StatusCode(ex.Status, new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                // Unexpected errors
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
