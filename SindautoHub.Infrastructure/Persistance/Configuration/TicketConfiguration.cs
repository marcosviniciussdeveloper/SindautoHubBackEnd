using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Infrastructure.Persistence.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Subject).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Status).IsRequired();
        builder.Property(t => t.Priority).HasConversion<string>()
               .HasColumnName("Priority").IsRequired();

        builder.Property(t => t.IsInternal).IsRequired();


        builder.HasOne(t => t.Cliente)
               .WithMany(u => u.TicketsAsCliente)
               .HasForeignKey(t => t.ClienteId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Agent)
               .WithMany(u => u.TicketsAsAgente)
               .HasForeignKey(t => t.AgenteId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

    }
}