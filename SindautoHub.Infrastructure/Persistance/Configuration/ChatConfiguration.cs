using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{


    namespace SindautoHub.Infrastructure.Persistance.Configuration
    {
        public class ChatConfiguration : IEntityTypeConfiguration<Chat>
        {
            public void Configure(EntityTypeBuilder<Chat> builder)
            {
                builder.ToTable("Chats");

                builder.HasKey(c => c.Id);
                builder.Property(c => c.Id).ValueGeneratedOnAdd();

                builder.Property(c => c.CreatedAt)
                       .IsRequired();

                builder.Property(c => c.LastMessageAt);

                builder.Property(c => c.StatusChat)
          .HasConversion<int>()
          .HasDefaultValue(StatusChat.Ativo)
          .IsRequired();


                builder.HasMany(c => c.Messages)
                       .WithOne(m => m.Chat)
                       .HasForeignKey(m => m.ChatId)
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(c => c.ChatUsers)
                       .WithOne(cu => cu.Chat)
                       .HasForeignKey(cu => cu.ChatId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
