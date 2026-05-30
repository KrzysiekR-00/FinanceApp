using Domain;

namespace Services;

public class PortfolioItemUnitService
{
    private readonly IPortfolioItemUnitRepository _repository;

    public PortfolioItemUnitService(IPortfolioItemUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<PortfolioItemUnit[]> GetPortfolioItemUnits()
    {
        return await _repository.GetPortfolioItemUnits();
    }

    public async Task CreatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        await _repository.CreatePortfolioItemUnit(portfolioItemUnit);
    }

    public async Task<bool> CanEditPortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        return true;
    }

    public async Task UpdatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        if (!await CanEditPortfolioItemUnit(portfolioItemUnit)) return;

        await _repository.UpdatePortfolioItemUnit(portfolioItemUnit);
    }

    public async Task DeletePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        if (!await CanEditPortfolioItemUnit(portfolioItemUnit)) return;

        await _repository.DeletePortfolioItemUnit(portfolioItemUnit);
    }

    public async Task<int> GetMainUnitId()
    {
        return await _repository.GetMainUnitId();
    }

    public async Task SaveMainUnitId(int id)
    {
        await _repository.SaveMainUnitId(id);
    }
}
