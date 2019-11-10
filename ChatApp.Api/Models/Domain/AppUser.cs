using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Models.Domain
{
    public class AppUser
    {
        public AppUser()
        {
            GroupMemberships = new HashSet<GroupMembership>();
        }

        [Key]
        public int UserId { get; set; }

        public int? ProfileImageId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(ProfileImageId))]
        public DisplayImage ProfileImage { get; set; }

        public ICollection<GroupMembership> GroupMemberships { get; set; }

        #endregion
    }
}
