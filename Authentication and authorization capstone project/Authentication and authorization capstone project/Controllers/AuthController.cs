using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication_and_authorization_capstone_project.Models;
using Authentication_and_authorization_capstone_project.Services;
using Microsoft.AspNetCore.RateLimiting;

namespace Authentication_and_authorization_capstone_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly JwtService _jwtService;

            public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtService jwtService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtService = jwtService;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register(RegisterDto model)
            {
                model.Email = model.Email.Trim();
                model.FirstName = model.FirstName.Trim();
                model.LastName = model.LastName.Trim();

                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    return Conflict(new { Message = "Email already exists." });
                }

                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result =
                await _userManager.CreateAsync(
                user,
                model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.Select(error => new { error.Code, error.Description }));
                }

                return Ok(new
                {
                    Message = "User Registered Successfully"
                });
            }

            [HttpPost("login")]
            [EnableRateLimiting("auth")]
            public async Task<IActionResult> Login(
            LoginDto model)
            {
                model.Email = model.Email.Trim();

                var user =
                await _userManager
                .FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Invalid Credentials"
                    });
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
                if (signInResult.IsLockedOut || !signInResult.Succeeded)
                    return Unauthorized(new
                    {
                        Message = "Invalid Credentials"
                    });

                var token =
                _jwtService.GenerateToken(user);

            return Ok(new
            {
                Token = token
            });
        }
    }
}

