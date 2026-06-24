using Domain;

namespace Services;

public class PortfolioItemService
{
    private readonly IPortfolioItemRepository _repository;
    private readonly ISnapshotRepository _snapshotRepository;

    public PortfolioItemService(IPortfolioItemRepository repository, ISnapshotRepository snapshotRepository)
    {
        _repository = repository;
        _snapshotRepository = snapshotRepository;
    }

    public async Task<PortfolioItem[]> GetPortfolioItems()
    {
        return await _repository.GetPortfolioItems();
    }

    public async Task<bool> CanEditPortfolioItem(PortfolioItem portfolioItem)
    {
        var snapshots = await _snapshotRepository.GetPortfolioItemSnapshots(portfolioItem.Id);

        return snapshots.Length <= 0;
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
