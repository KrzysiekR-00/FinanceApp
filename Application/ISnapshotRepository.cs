using Domain;

namespace Services;

public interface ISnapshotRepository
{
    Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot);
    Task<PortfolioItemSnapshot[]> GetPortfolioItemSnapshots(int? portfolioItemId = null);
    Task CreatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
    Task UpdatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
    Task DeletePortfolioItemSnapshot(PortfolioItemSnapshot snapshot);
    Task<ExchangeRateSnapshot[]> GetExchangeRateSnapshots(int? portfolioItemUnitId = null);
    Task CreateExchangeRateSnapshot(ExchangeRateSnapshot snapshot);
    Task UpdateExchangeRateSnapshot(ExchangeRateSnapshot snapshot);
    Task DeleteExchangeRateSnapshot(ExchangeRateSnapshot snapshot);
}
