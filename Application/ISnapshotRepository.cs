using Domain;

namespace Services;

public interface ISnapshotRepository
{
    Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot);
    Task<PortfolioItemSnapshot[]> GetPortfolioItemSnapshots(int portfolioItemId);
    Task CreatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
    Task UpdatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
    Task DeletePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
}
