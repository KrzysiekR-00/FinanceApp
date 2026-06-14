using Data.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
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

    public async Task<PortfolioItemSnapshot[]> GetPortfolioItemSnapshots(int portfolioItemId)
    {
        var entities = await _dbContext.PortfolioItemSnapshots
            .Where(e => e.PortfolioItemId == portfolioItemId)
            .Include(e => e.PortfolioItem)
            .Include(e => e.PortfolioItem.Unit)
            .OrderByDescending(e => e.Date)
            .ToArrayAsync();

        return entities.Select(e => new PortfolioItemSnapshot()
        {
            Id = e.Id,
            Date = e.Date,
            Quantity = e.Quantity,
            PortfolioItem = new PortfolioItem()
            {
                Id = e.PortfolioItem.Id,
                Name = e.PortfolioItem.Name,
                Type = e.PortfolioItem.Type,
                Unit = new PortfolioItemUnit()
                {
                    Id = e.PortfolioItem.Unit.Id,
                    Symbol = e.PortfolioItem.Unit.Symbol,
                    UnitModifier = e.PortfolioItem.Unit.UnitModifier
                }
            }
        }).ToArray();
    }

    public async Task CreatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        var entity = new PortfolioItemSnapshotEntity()
        {
            Date = snapshot.Date,
            Quantity = snapshot.Quantity,
            PortfolioItemId = snapshot.PortfolioItem.Id
        };

        _dbContext.PortfolioItemSnapshots.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        var entity = await _dbContext.PortfolioItemSnapshots.FirstAsync(x => x.Id == snapshot.Id);

        entity.Date = snapshot.Date;
        entity.Quantity = snapshot.Quantity;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePortfolioItemSnapshot(PortfolioItemSnapshot snapshot)
    {
        var entity = await _dbContext.PortfolioItemSnapshots.FirstAsync(x => x.Id == snapshot.Id);

        _dbContext.PortfolioItemSnapshots.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}
