using ChatApp.Api.Models.Dto.User;
using ChatApp.Api.Models.ViewModel.User;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool emailTaken = await _userService.IsEmailTaken(model.Email);

            if (emailTaken)
            {
                return BadRequest($"Email ({model.Email}) is already taken");
            }

            await _userService.Register(model);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticatedUser>> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthenticatedUser user = await _userService.Login(model);

            if (user == null)
            {
                return BadRequest("Email or password is incorrect");
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("IsEmailTaken")]
        public async Task<ActionResult<bool>> IsEmailTaken([FromQuery(Name = "email")] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isTaken = await _userService.IsEmailTaken(email);

            return Ok(isTaken);
        }
    }
}
