using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Database
{
    public class SindautoHubContext : DbContext
    {
        public SindautoHubContext(DbContextOptions<SindautoHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SindautoHubContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

        public DbSet<ChatMessages> ChatMessages { get; set; }

        public DbSet<TicketMessages> TicketMessages { get; set; }

        public DbSet<Chats> Chats { get; set; }
        public DbSet<Setor> Setores { get; set; }
    }
}