using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    public class SetorConfiguration : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder
                .ToTable("setores");
            builder
            .Property(f => f.Id).ValueGeneratedOnAdd();
            builder
                .HasKey(c => c.Id);
            builder
                  .Property(c => c.NomeSetor)
                  .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(s => s.HorarioFuncionamento)
                .HasMaxLength(120)
                .IsRequired();
        }
    }
}
