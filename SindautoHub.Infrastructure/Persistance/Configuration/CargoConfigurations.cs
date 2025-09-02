
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    public class CargoConfigurations : IEntityTypeConfiguration<Cargo>
    {
     
  

        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder
                .ToTable("cargos");
            builder
                   .HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
    }

