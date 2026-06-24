using Domain;

namespace Services;

public class PortfolioItemUnitService
{
    private readonly IPortfolioItemUnitRepository _repository;
    private readonly IPortfolioItemRepository _portfolioItemRepository;
    private readonly ISnapshotRepository _snapshotRepository;

    public PortfolioItemUnitService(IPortfolioItemUnitRepository repository, IPortfolioItemRepository portfolioItemRepository, ISnapshotRepository snapshotRepository)
    {
        _repository = repository;
        _portfolioItemRepository = portfolioItemRepository;
        _snapshotRepository = snapshotRepository;
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
        var portfolioItems = await _portfolioItemRepository.GetPortfolioItems();
        var snapshots = await _snapshotRepository.GetExchangeRateSnapshots(portfolioItemUnit.Id);

        return !(portfolioItems.Any(p => p.Unit.Id == portfolioItemUnit.Id) || snapshots.Length > 0);
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
