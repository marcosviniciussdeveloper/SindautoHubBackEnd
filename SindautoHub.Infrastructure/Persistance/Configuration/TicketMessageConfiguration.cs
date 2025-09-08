using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities;

namespace SindautoHub.Infrastructure.Persistence.Configuration;

public class TicketMessageConfiguration : IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
        builder.ToTable("ticket_messages");

        builder.HasKey(tm => tm.Id);
        builder.Property(tm => tm.Id).ValueGeneratedOnAdd();

        builder.Property(tm => tm.MessageText)
            .IsRequired();
            
        builder.HasOne(tm => tm.Ticket)
            .WithMany(t => t.Messages)
            .HasForeignKey(tm => tm.TicketId)
            .IsRequired();
            
        // Relacionamento com User (Remetente)
        builder.HasOne(tm => tm.Sender)
            .WithMany(u => u.TicketMessages)
            .HasForeignKey(tm => tm.SenderId)
            .IsRequired();
    }
}
