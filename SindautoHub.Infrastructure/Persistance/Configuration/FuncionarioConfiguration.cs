using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    internal class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder
                .ToTable("funcionarios");
            builder .HasKey(x => x.Id);

            builder
             .Property(p => p.Nome)
             .HasMaxLength(100)
               .IsRequired();

            builder
               .HasOne(f => f.setor)
               .WithMany(s => s.Funcionarios)
               .HasForeignKey(f => f.SetorId)
               .IsRequired();


            builder
                .HasOne(f => f.cargo)
                .WithMany(c => c.Funcionarios)
                .HasForeignKey(f => f.CargoId)
                .IsRequired();

        }
    }
}
