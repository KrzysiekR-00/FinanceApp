using Domain;

namespace Services;

public interface IPortfolioItemUnitRepository
{
    Task<PortfolioItemUnit[]> GetPortfolioItemUnits();
    Task CreatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit);
    Task UpdatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit);
    Task DeletePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit);
    Task<int> GetMainUnitId();
    Task SaveMainUnitId(int id);
}
