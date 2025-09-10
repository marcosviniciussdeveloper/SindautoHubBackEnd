

using SindautoHub.Domain.Entities.Enums;

namespace SindautoHub.Domain.Entities.Models
{
    public class Sector
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string NameSector { get; set; }  

        public string Description { get; set; }

        public string  OpeningsHours { get; set; }

        public DateTime? StatusChangedAt { get; set; }

         public ICollection<User> Users { get; set; } = new List<User>();
    }
}
