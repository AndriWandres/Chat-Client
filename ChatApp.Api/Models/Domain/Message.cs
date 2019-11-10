using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Models.Domain
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        
        public int AuthorId { get; set; }
        public int? ParentId { get; set; }

        public string TextContent { get; set; }
        public byte[] ImageContent { get; set; }
        public byte[] VideoContent { get; set; }
        public byte[] FileContent { get; set; }
        
        [Required]
        public bool IsEdited { get; set; }

        [Required]
        public bool IsForwarded { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(AuthorId))]
        public AppUser Author { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Message Parent { get; set; }

        public ICollection<MessageRecipient> MessageRecipients { get; set; }

        #endregion
    }
}
