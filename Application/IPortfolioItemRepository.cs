using Domain;

namespace Services;

public interface IPortfolioItemRepository
{
    Task<PortfolioItem[]> GetPortfolioItems();
    Task CreatePortfolioItem(PortfolioItem portfolioItem);
    Task UpdatePortfolioItem(PortfolioItem portfolioItem);
    Task DeletePortfolioItem(PortfolioItem portfolioItem);
}
