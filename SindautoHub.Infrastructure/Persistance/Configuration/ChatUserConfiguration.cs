using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities;

namespace SindautoHub.Infrastructure.Persistance.Configuration
{
    public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> b)
        {
            b.ToTable("ChatUsers");

            b.HasKey(x => new { x.ChatId, x.UserId });

            b.HasOne(x => x.Chat)
                .WithMany(c => c.ChatUsers)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
                .WithMany(u => u.ChatUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
