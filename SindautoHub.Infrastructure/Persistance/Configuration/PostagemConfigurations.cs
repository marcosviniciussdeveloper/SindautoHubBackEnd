using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    internal class PostagemConfigurations : IEntityTypeConfiguration<Postagens>
    {
        public void Configure(EntityTypeBuilder<Postagens> builder)
        {
            builder
                 .ToTable("postagens");
            builder
                .HasKey(x => x.Id);
            builder
               .Property(x => x.Id)
              .HasMaxLength(100)
              .IsRequired();



            builder
                .HasOne(f => f.Autor)
               .WithMany(s => s.postagens)
               .HasForeignKey(f => f.AutorId)
               .IsRequired();


        }
    }
}
