using System;

namespace ChatApp.Api.Models.ViewModel.Contact
{
    public class ContactViewModel
    {
        public int MessageRecipientId { get; set; }
        public bool IsRead { get; set; }
        public int UnreadMessagesCount { get; set; }

        public LatestMessage Message { get; set; }

        public ContactRecipientGroup RecipientGroup { get; set; }
        public ContactRecipientUser RecipientUser { get; set; }

        #region Nested Classes

        public class LatestMessage
        {
            public int MessageId { get; set; }
            public string TextContent { get; set; }
            public DateTime CreatedAt { get; set; }
            public string AuthorName { get; set; }
        }

        public class ContactRecipientGroup
        {
            public int GroupId { get; set; }
            public string Name { get; set; }
        }

        public class ContactRecipientUser
        {
            public int UserId { get; set; }
            public string DisplayName { get; set; }
            public bool IsOnline { get; set; }
        }

        #endregion Nested Classes
    }
}
