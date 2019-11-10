using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Models.Domain
{
    public class DisplayImage
    {
        [Key]
        public int DisplayImageId { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}

