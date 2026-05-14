using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService.Services.AuthService _authService;

        public AuthenticationController(AuthService.Services.AuthService authService)
        {
            _authService = authService;
        }

        // ── POST /api/Authentication/login ─────────────────────────────
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);

            if (result == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(result);
        }

        // ── POST /api/Authentication/customer-register ─────────────────
        [HttpPost("customer-register")]
        public async Task<IActionResult> CustomerRegister([FromBody] CustomerRegisterModel model)
        {
            var success = await _authService.RegisterCustomerAsync(model);

            if (!success)
                return BadRequest(new { message = "Username or email already exists" });

            return Ok(new { message = "Customer registered successfully" });
        }

        // ── POST /api/Authentication/register-admin ────────────────────
        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CustomerRegisterModel model)
        {
            var success = await _authService.RegisterAdminAsync(model);

            if (!success)
                return BadRequest(new { message = "Username or email already exists" });

            return Ok(new { message = "Admin registered successfully" });
        }

        // ── POST /api/Authentication/update ────────────────────────────
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateModel model)
        {
            // Read user id from JWT claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            int userId = int.Parse(userIdClaim.Value);
            var success = await _authService.UpdateUserAsync(userId, model);

            if (!success)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "Profile updated successfully" });
        }

        // ── DELETE /api/Authentication/Delete ──────────────────────────
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            int userId = int.Parse(userIdClaim.Value);
            var success = await _authService.DeleteUserAsync(userId);

            if (!success)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "Account deleted successfully" });
        }

        // ── GET /api/Authentication/GetUser ────────────────────────────
        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid token" });

            int userId = int.Parse(userIdClaim.Value);
            var user = await _authService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // ── GET /api/Authentication/GetAllUsers ────────────────────────
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationFilter filter)
        {
            var users = await _authService.GetAllUsersAsync(filter);
            return Ok(users);
        }
    }
}