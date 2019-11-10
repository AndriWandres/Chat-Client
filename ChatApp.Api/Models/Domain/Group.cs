using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Models.Domain
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        public int? GroupImageId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(GroupImageId))]
        public DisplayImage GroupImage { get; set; }

        public ICollection<GroupMembership> GroupMemberships { get; set; }

        #endregion
    }
}
