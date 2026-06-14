using Domain;

namespace Services;

public class SnapshotService
{
    private readonly ISnapshotRepository _snapshotRepository;

    public SnapshotService(ISnapshotRepository snapshotRepository)
    {
        _snapshotRepository = snapshotRepository;
    }

    public async Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot)
    {
        await _snapshotRepository.CreatePortfolioSnapshot(snapshot);
    }

    public async Task<PortfolioItemSnapshot[]> GetPortfolioItemSnapshots(int portfolioItemId)
    {
        return await _snapshotRepository.GetPortfolioItemSnapshots(portfolioItemId);
    }
}
