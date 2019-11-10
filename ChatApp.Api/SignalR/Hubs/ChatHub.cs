using ChatApp.Api.Models.Domain;
using ChatApp.Api.SignalR.Events;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Api.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(MessageRecipient message)
        {
            string userId = message.RecipientUserId.ToString();
            IClientProxy user = Clients.User(userId);

            if (user != null)
            {
                await user.SendAsync(ChatEvents.ChatMessage, message);
            }
        }

        public async Task SendMessages(Message message)
        {
            foreach (MessageRecipient recipient in message.MessageRecipients)
            {
                string id = recipient.RecipientUserId.ToString();
                IClientProxy user = Clients.User(id);

                if (user != null)
                {
                    await user.SendAsync(ChatEvents.ChatMessage, recipient);
                }
            }
        }
    }
}
