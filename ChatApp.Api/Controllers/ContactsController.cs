using AutoMapper;
using ChatApp.Api.Database;
using ChatApp.Api.Models.Domain;
using ChatApp.Api.Models.ViewModel.Contact;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ChatContext _context;
        private readonly UserService _userService;

        public ContactsController(ChatContext context, UserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("GetContacts")]
        public async Task<ActionResult<IEnumerable<ContactViewModel>>> GetContacts()
        {
            AppUser user = await _userService.GetUser(User);

            // todo expand for groups (no union)
            IEnumerable<ContactViewModel> userContacts = _context.MessageRecipients
                .AsNoTracking()
                .Include(mr => mr.RecipientUser)
                .Include(mr => mr.Message)
                    .ThenInclude(m => m.Author)

                // Non group chats
                .Where(mr => mr.RecipientGroupId == null)

                // Relevant to the currently logged in user 
                .Where(mr => mr.RecipientUserId == user.UserId || mr.Message.AuthorId == user.UserId)

                // Group by the others user id, not the id of the currently logged in user
                .GroupBy(mr => mr.RecipientUserId == user.UserId ? mr.Message.AuthorId : mr.RecipientUserId)

                // Get the last message with the user and map it to viewmodel
                .Select(grouping => MapToViewModel(grouping, user.UserId));
            
            return Ok(userContacts);
        }
        
        private ContactViewModel MapToViewModel(IGrouping<int?, MessageRecipient> grouping, int userId)
        {
            MessageRecipient latestMessage = grouping.OrderByDescending(mr => mr.Message.CreatedAt).First();

            int unreadMessagesCount = grouping.Count(mr => mr.RecipientUserId == userId && !mr.IsRead);

            return _mapper.Map<MessageRecipient, ContactViewModel>(latestMessage, options =>
            {
                options.Items["UnreadMessagesCount"] = unreadMessagesCount;
            });
        }
    }
}
