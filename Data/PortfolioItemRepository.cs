using Domain;
using Services;

namespace Data;

public class PortfolioItemRepository : IPortfolioItemRepository
{
    public async Task<PortfolioItem[]> GetPortfolioItems()
    {
        //return Array.Empty<PortfolioItem>();

        return new PortfolioItem[]
        {
            new PortfolioItem()
            {
                Id = 1,
                Name = "Asset",
                Type = PortfolioItemType.Asset,
                Unit = new PortfolioItemUnit() { Id = 1, Symbol = "USD1", UnitModifier = 1}
            },
            new PortfolioItem()
            {
                Id = 2,
                Name = "Liability",
                Type = PortfolioItemType.Liability,
                Unit = new PortfolioItemUnit() { Id = 2, Symbol = "USD2", UnitModifier = 1}
            }
        };
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
