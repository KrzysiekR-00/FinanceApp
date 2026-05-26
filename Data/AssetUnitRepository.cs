using Data.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Data;

public class AssetUnitRepository : IAssetUnitRepository
{
    private const string MainUnitIdSettingKey = "MainUnitId";

    private readonly AppDbContext _dbContext;

    public AssetUnitRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AssetUnit[]> GetAssetUnits()
    {
        return await _dbContext.AssetUnits.Select(u => new AssetUnit()
        {
            Id = u.Id,
            Symbol = u.Symbol,
            UnitModifier = u.UnitModifier
        }).ToArrayAsync();
    }

    public async Task CreateAssetUnit(AssetUnit assetUnit)
    {
        var entity = new AssetUnitEntity
        {
            Symbol = assetUnit.Symbol,
            UnitModifier = assetUnit.UnitModifier
        };

        _dbContext.AssetUnits.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAssetUnit(AssetUnit assetUnit)
    {
        var entity = await _dbContext.AssetUnits.FirstAsync(x => x.Id == assetUnit.Id);

        entity.Symbol = assetUnit.Symbol;
        entity.UnitModifier = assetUnit.UnitModifier;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAssetUnit(AssetUnit assetUnit)
    {
        var entity = await _dbContext.AssetUnits.FirstAsync(x => x.Id == assetUnit.Id);

        _dbContext.AssetUnits.Remove(entity);

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
