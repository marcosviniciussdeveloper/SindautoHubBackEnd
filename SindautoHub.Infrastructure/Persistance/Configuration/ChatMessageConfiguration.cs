using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("chat_messages");

        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Id).ValueGeneratedOnAdd();

        builder.Property(cm => cm.MessageText)
               .IsRequired();

        builder.HasOne(cm => cm.Chat)
               .WithMany(c => c.Messages)
               .HasForeignKey(cm => cm.ChatId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Sender)
               .WithMany(u => u.ChatMessages)
               .HasForeignKey(cm => cm.SenderId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);


        builder.Property(cm => cm.DeliveryStatus)
         .HasConversion<int>()
         .HasDefaultValue(DeliveryStatus.Enviado)
         .IsRequired();



        builder.Property(cm => cm.IsRead)
               .HasDefaultValue(false)
               .IsRequired();
    }
}
