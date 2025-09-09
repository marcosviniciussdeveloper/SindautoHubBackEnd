using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Infrastructure.Persistance.Configuration;
using SindautoHub.Infrastructure.Persistence.Configuration;

namespace SindautoHub.Infrastructure.Persistance.Database
{
    public class SindautoHubContext : DbContext
    {
        public SindautoHubContext(DbContextOptions<SindautoHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new ChatUserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SindautoHubContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Sector> Sectors { get; set; }
    }
}
