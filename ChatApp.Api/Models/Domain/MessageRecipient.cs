using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Models.Domain
{
    public class MessageRecipient
    {
        [Key]
        public int MessageRecipientId { get; set; }

        public int MessageId { get; set; }
        public int? RecipientUserId { get; set; }
        public int? RecipientGroupId { get; set; }

        [Required]
        public bool IsRead { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }

        [ForeignKey(nameof(RecipientUserId))]
        public AppUser RecipientUser { get; set; }

        [ForeignKey(nameof(RecipientGroupId))]
        public GroupMembership RecipientGroup { get; set; }

        #endregion
    }
}
