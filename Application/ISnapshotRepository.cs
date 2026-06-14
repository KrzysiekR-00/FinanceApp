using Domain;

namespace Services;

public interface ISnapshotRepository
{
    Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot);

    Task<PortfolioItemSnapshot[]> GetPortfolioItemSnapshots(int portfolioItemId);
}
