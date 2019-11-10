using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Models.Domain
{
    public class GroupMembership
    {
        [Key]
        public int GroupMembershipId { get; set; }

        public int GroupId { get; set; }
        public int UserId { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        #endregion
    }
}
