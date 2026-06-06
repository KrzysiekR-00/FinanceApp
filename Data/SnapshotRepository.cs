using Data.Entities;
using Domain;
using Services;

namespace Data;

public class SnapshotRepository : ISnapshotRepository
{
    private readonly AppDbContext _dbContext;

    public SnapshotRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreatePortfolioSnapshot(PortfolioSnapshot snapshot)
    {
        foreach (var u in snapshot.ExchangeRates)
        {
            _dbContext.ExchangeRateSnapshots.Add(new ExchangeRateSnapshotEntity()
            {
                Date = u.Date,
                Value = u.Value,
                UnitId = u.PortfolioItemUnit.Id
            });
        }

        foreach (var p in snapshot.PortfolioItems)
        {
            _dbContext.PortfolioItemSnapshots.Add(new PortfolioItemSnapshotEntity()
            {
                Date = p.Date,
                Quantity = p.Quantity,
                PortfolioItemId = p.PortfolioItem.Id
            });
        }

        await _dbContext.SaveChangesAsync();
    }
}
