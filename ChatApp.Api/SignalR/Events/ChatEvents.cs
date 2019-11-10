using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.SignalR.Events
{
    public static class ChatEvents
    {
        public const string ChatMessage = "ChatMessage";
        public const string StartTyping = "StartTyping";
        public const string StopTyping = "StopTyping";
    }
}
