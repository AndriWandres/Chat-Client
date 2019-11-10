using ChatApp.Api.Database;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly UserService _userService;

        public ChatController(ChatContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
    }
}
