using Data.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Data;

public class PortfolioItemUnitRepository : IPortfolioItemUnitRepository
{
    private const string MainUnitIdSettingKey = "MainUnitId";

    private readonly AppDbContext _dbContext;

    public PortfolioItemUnitRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PortfolioItemUnit[]> GetPortfolioItemUnits()
    {
        return await _dbContext.PortfolioItemUnits.Select(u => new PortfolioItemUnit()
        {
            Id = u.Id,
            Symbol = u.Symbol,
            UnitModifier = u.UnitModifier
        }).ToArrayAsync();
    }

    public async Task CreatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        var entity = new PortfolioItemUnitEntity
        {
            Symbol = portfolioItemUnit.Symbol,
            UnitModifier = portfolioItemUnit.UnitModifier
        };

        _dbContext.PortfolioItemUnits.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        var entity = await _dbContext.PortfolioItemUnits.FirstAsync(x => x.Id == portfolioItemUnit.Id);

        entity.Symbol = portfolioItemUnit.Symbol;
        entity.UnitModifier = portfolioItemUnit.UnitModifier;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePortfolioItemUnit(PortfolioItemUnit portfolioItemUnit)
    {
        var entity = await _dbContext.PortfolioItemUnits.FirstAsync(x => x.Id == portfolioItemUnit.Id);

        _dbContext.PortfolioItemUnits.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetMainUnitId()
    {
        var setting = await _dbContext.Settings.FirstOrDefaultAsync(s => s.Key == MainUnitIdSettingKey);

        if (setting != null)
        {
            if (int.TryParse(setting.Value, out var id))
            {
                return id;
            }
        }

        return 0;
    }

    public async Task SaveMainUnitId(int id)
    {
        var setting = await _dbContext.Settings.FirstOrDefaultAsync(s => s.Key == MainUnitIdSettingKey);

        if (setting == null)
        {
            _dbContext.Settings.Add(new SettingEntity()
            {
                Key = MainUnitIdSettingKey,
                Value = id.ToString()
            });
        }
        else
        {
            setting.Value = id.ToString();
        }

        await _dbContext.SaveChangesAsync();
    }
}
