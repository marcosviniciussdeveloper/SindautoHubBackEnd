using SindautoHub.Application.Interface;
using SindautoHub.Domain.Interfaces;
using SindautoHub.Infrastructure.Persistance.Database;

public class UnitOfWork : IunitOfwork
{
    private readonly SindautoHubContext _context;

    public IUserRepository UserRepository { get; }
    public IPositionRepository PositionRepository { get; }
    public ISectorRepository SectorRepository { get; }

    public UnitOfWork(SindautoHubContext context,
                      IUserRepository userRepository,
                      IPositionRepository positionRepository,
                      ISectorRepository sectorRepository)
    {
        _context = context;
        UserRepository = userRepository;
        PositionRepository = positionRepository;
        SectorRepository = sectorRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
