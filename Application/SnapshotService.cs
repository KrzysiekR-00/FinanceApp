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

    public async Task CreatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        await _snapshotRepository.CreatePortfolioItemSnapshot(snapshot);
    }

    public async Task UpdatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        await _snapshotRepository.UpdatePortfolioItemSnapshot(snapshot);
    }

    public async Task DeletePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        await _snapshotRepository.DeletePortfolioItemSnapshot(snapshot);
    }

    public async Task<ExchangeRateSnapshot[]> GetExchangeRateSnapshots(int portfolioItemUnitId)
    {
        return await _snapshotRepository.GetExchangeRateSnapshots(portfolioItemUnitId);
    }

    public async Task CreateExchangeRateSnapshot(ExchangeRateSnapshot snapshot)
    {
        await _snapshotRepository.CreateExchangeRateSnapshot(snapshot);
    }

    public async Task UpdateExchangeRateSnapshot(ExchangeRateSnapshot snapshot)
    {
        await _snapshotRepository.UpdateExchangeRateSnapshot(snapshot);
    }

    public async Task DeleteExchangeRateSnapshot(ExchangeRateSnapshot snapshot)
    {
        await _snapshotRepository.DeleteExchangeRateSnapshot(snapshot);
    }
}
