using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IAnnouncementsRepository
    {
        Task CreateAsync (Announcement announcement );

        Task DeleteAsync(Announcement  announcement);
        Task  UpdateAsync (Announcement announcement);
        Task<Announcement> GetByIdAsync (Guid annoucement);

        Task<IEnumerable<Announcement>>GetAllAsync();
    }
}
