using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    { private readonly SindautoHubContext _context;

        public AnnouncementsRepository (SindautoHubContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
        }

        public Task DeleteAsync(Announcement announcement)
        {
          _context.Announcements.Remove(announcement);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {   return await _context.Announcements
                .Include(a => a.Author)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Announcement> GetByIdAsync(Guid annoucement)
        {  
            return await _context.Announcements.FindAsync(annoucement);
        }

        public Task UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            return Task.CompletedTask;
        }
    }
    }


