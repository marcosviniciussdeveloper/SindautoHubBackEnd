
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("users");
            builder.HasKey(x => x.Id);

            builder
             .Property(f => f.Id).ValueGeneratedOnAdd();
            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();



             builder.Property(u => u.Cpf)
              .HasMaxLength(14); // 000.000.000-00

            builder.Property(u => u.WhatsappNumber)
                 .HasMaxLength(15); // (00) 00000-0000


            builder
                .HasOne(u => u.Sector)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SectorId)
                .IsRequired(false);

            builder
                .HasOne(u => u.Position)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.PositionId)
                .IsRequired(false);

            builder.HasIndex(u => u.WhatsappNumber).IsUnique();

            builder.HasIndex(u => u.Cpf).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.CreatedAt)
                 .HasColumnType("timestampz")
                    .HasDefaultValueSql("now()");

            builder.Property(u => u.UpdatedAt)
                   .HasColumnType("timestampz")
                   .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(u => u.UserName)
                .HasMaxLength (50)
                .IsRequired();

            builder.HasIndex(u => u.UserName).IsUnique();


        }
    }
}
