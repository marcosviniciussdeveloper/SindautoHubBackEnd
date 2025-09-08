using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SindautoHub.Application.Interface
{
    public interface IunitOfwork
    {
          IPositionRepository PositionRepository { get; }
        ISectorRepository SectorRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
