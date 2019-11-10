using ChatApp.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Database
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<DisplayImage> ProfileImages { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<DisplayImage> GroupImages { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
    }
}
