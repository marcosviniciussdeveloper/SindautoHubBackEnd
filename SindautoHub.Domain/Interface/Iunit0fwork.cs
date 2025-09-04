using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Interface
{
    public interface IunitOfwork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
