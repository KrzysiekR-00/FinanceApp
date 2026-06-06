using Domain;

namespace Services;

public interface ISnapshotRepository
{
    Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot);
}
