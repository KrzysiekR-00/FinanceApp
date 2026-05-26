using Domain;
using Services;

namespace Data;

public class PortfolioItemRepository : IPortfolioItemRepository
{
    public async Task<PortfolioItem[]> GetPortfolioItems()
    {
        return Array.Empty<PortfolioItem>();
    }

    public async Task CreatePortfolioItem(PortfolioItem portfolioItem)
    {

    }

    public async Task UpdatePortfolioItem(PortfolioItem portfolioItem)
    {

    }

    public async Task DeletePortfolioItem(PortfolioItem portfolioItem)
    {

    }
}
