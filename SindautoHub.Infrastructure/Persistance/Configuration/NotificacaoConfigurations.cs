using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    internal class NotificacaoConfigurations : IEntityTypeConfiguration<announcements>
    {
        public void Configure(EntityTypeBuilder<announcements> builder)
        {
            builder
                .ToTable("notificacoes");
            builder
            .Property(f => f.Id).ValueGeneratedOnAdd();

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Mensagem)
                .HasMaxLength(255)
                .IsRequired();

            builder

                .HasOne(f => f.Usuario)
                .WithMany( f => f.Notificacoes)
                .HasForeignKey(f => f.UsuarioId)
                .IsRequired();
        }
    }
}
