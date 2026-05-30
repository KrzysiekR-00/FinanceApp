using Data.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Data;

public class PortfolioItemRepository : IPortfolioItemRepository
{
    private readonly AppDbContext _dbContext;

    public PortfolioItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PortfolioItem[]> GetPortfolioItems()
    {
        return await _dbContext.PortfolioItems.Include(p => p.Unit).Select(p => new PortfolioItem()
        {
            Id = p.Id,
            Name = p.Name,
            Type = p.Type,
            Unit = new PortfolioItemUnit()
            {
                Id = p.Unit.Id,
                Symbol = p.Unit.Symbol,
                UnitModifier = p.Unit.UnitModifier
            }
        }).ToArrayAsync();
    }

    public async Task CreatePortfolioItem(PortfolioItem portfolioItem)
    {
        var entity = new PortfolioItemEntity
        {
            Name = portfolioItem.Name,
            Type = portfolioItem.Type,
            UnitId = portfolioItem.Unit.Id
        };

        _dbContext.PortfolioItems.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePortfolioItem(PortfolioItem portfolioItem)
    {
        var entity = await _dbContext.PortfolioItems.FirstAsync(x => x.Id == portfolioItem.Id);

        entity.Name = portfolioItem.Name;
        entity.Type = portfolioItem.Type;
        entity.UnitId = portfolioItem.Unit.Id;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePortfolioItem(PortfolioItem portfolioItem)
    {
        var entity = await _dbContext.PortfolioItems.FirstAsync(x => x.Id == portfolioItem.Id);

        _dbContext.PortfolioItems.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}
