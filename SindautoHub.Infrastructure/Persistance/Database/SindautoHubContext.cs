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
        public SindautoHubContext(DbContextOptions<SindautoHubContext> options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SindautoHubContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet <Cargo>  Cargo { get; set; }

        public DbSet<Funcionario> Funcionario { get; set; }

        public DbSet<Notificacao> Notificacao { get; set; }

        public DbSet<Postagens> Postagens { get; set; }

        public DbSet<Setor>  Setor { get; set; }
    }
}
