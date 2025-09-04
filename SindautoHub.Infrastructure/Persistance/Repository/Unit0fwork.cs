using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Application.Interface;
using SindautoHub.Infrastructure.Persistance.Database;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
    public class UnitOfwork : IunitOfwork
    {
        private readonly SindautoHubContext _context;

        public UnitOfwork(SindautoHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
              return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
