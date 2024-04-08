using Microsoft.EntityFrameworkCore;

namespace SignalR_Chat
{
    public class ChatContext : DbContext
    {
        public DbSet<Messages> Messages { get; set; }

        
        public ChatContext(DbContextOptions<ChatContext> options)
               : base(options)
        {
            Database.EnsureCreated();
        }

        public ChatContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add initial records here
            modelBuilder.Entity<Messages>().HasData(
                new Messages { Id = 1, Message = "Initial Message 1" },
                new Messages { Id = 2, Message = "Initial Message 2" }
                // Add more initial records as needed
            );
        }
    }
}
