using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SindautoHub.Domain.Entities.Enums;
using SindautoHub.Domain.Entities.Models;

public class SectorConfiguration : IEntityTypeConfiguration<Sector>
{
    public void Configure(EntityTypeBuilder<Sector> builder)
    {
        builder.ToTable("sectors"); 
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();



        builder.Property(s => s.NameSector).HasMaxLength(100).IsRequired(); 
        builder.Property(s => s.OpeningsHours).HasMaxLength(100).IsRequired();

      

    }
}