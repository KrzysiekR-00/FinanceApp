using Domain;

namespace Services;

public class PortfolioItemService
{
    private readonly IPortfolioItemRepository _repository;

    public PortfolioItemService(IPortfolioItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<PortfolioItem[]> GetPortfolioItems()
    {
        return await _repository.GetPortfolioItems();
    }

    public async Task<bool> CanEditPortfolioItem(PortfolioItem portfolioItem)
    {
        return true;
    }

    public async Task CreatePortfolioItem(PortfolioItem portfolioItem)
    {
        await _repository.CreatePortfolioItem(portfolioItem);
    }

    public async Task UpdatePortfolioItem(PortfolioItem portfolioItem)
    {
        await _repository.UpdatePortfolioItem(portfolioItem);
    }

    public async Task DeletePortfolioItem(PortfolioItem portfolioItem)
    {
        await _repository.DeletePortfolioItem(portfolioItem);
    }
}
