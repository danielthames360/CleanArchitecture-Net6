using CleanArchitecture.Services;
using CleanArchitecture.WebApi.Configurations;
using CleanArchitecture.WebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHanldeService _service;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHanldeService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser is not null) return BadRequest("THe user already exists");


            var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = user.Email, UserName = user.Email, }, user.Password);
            if (!isCreated.Succeeded) return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserLoginResponseDto() { Login = false, Errors = new List<string>() { "user or account invalid" } });

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser is null)
                return BadRequest(new UserLoginResponseDto() { Login = false, Errors = new List<string>() { "user or account invalid" } });

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!isCorrect)
                return BadRequest(new UserLoginResponseDto() { Login = false, Errors = new List<string>() { "user or account invalid" } });

            var parms = new TokenParameters()
            {
                Id = existingUser.Id,
                PasswordHash = existingUser.PasswordHash,
                UserName = existingUser.UserName
            };

            var token = await _service.GenerateJwtToken(parms);

            return Ok(new UserLoginResponseDto()
            {
                Login = true,
                Token = token
            });


        }

    }
}
