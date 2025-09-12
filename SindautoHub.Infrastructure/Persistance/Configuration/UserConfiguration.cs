using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Cpf)
            .HasMaxLength(14);

        builder.Property(u => u.WhatsappNumber)
            .HasMaxLength(15);

        builder.Property(u => u.UserName)
                .HasMaxLength(50)
                     .IsRequired();

        builder.HasIndex(u => u.UserName).IsUnique();

        builder.Property(u => u.Status);


        builder.Property(u => u.SectorId)
            .HasColumnName("sector_id");

        builder.Property(u => u.PositionId)
            .HasColumnName("position_id");


        builder.HasOne(u => u.Sector)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.SectorId)
            .IsRequired(false);

        builder.HasOne(u => u.Position)
            .WithMany(p => p.Users)
            .HasForeignKey(u => u.PositionId)
            .IsRequired(false);

        builder.HasIndex(u => u.WhatsappNumber).IsUnique();

        builder.Property(u => u.CreatedAt)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("timezone('utc', now())")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("timezone('utc', now())")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(u => u.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(u => u.UserName).IsUnique();
    }
}

