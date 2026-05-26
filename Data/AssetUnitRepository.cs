using Data.Entities;
using Domain;
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

    public AssetUnit[] GetAssetUnits()
    {
        return [.. _dbContext.AssetUnits.Select(u => new AssetUnit()
        {
            Id = u.Id,
            Symbol = u.Symbol,
            UnitModifier = u.UnitModifier
        })];
    }

    public void CreateAssetUnit(AssetUnit assetUnit)
    {
        var entity = new AssetUnitEntity
        {
            Symbol = assetUnit.Symbol,
            UnitModifier = assetUnit.UnitModifier
        };

        _dbContext.AssetUnits.Add(entity);

        _dbContext.SaveChanges();
    }

    public void UpdateAssetUnit(AssetUnit assetUnit)
    {
        var entity = _dbContext.AssetUnits.First(x => x.Id == assetUnit.Id);

        entity.Symbol = assetUnit.Symbol;
        entity.UnitModifier = assetUnit.UnitModifier;

        _dbContext.SaveChanges();
    }

    public void DeleteAssetUnit(AssetUnit assetUnit)
    {
        var entity = _dbContext.AssetUnits.First(x => x.Id == assetUnit.Id);

        _dbContext.AssetUnits.Remove(entity);

        _dbContext.SaveChanges();
    }

    public int GetMainUnitId()
    {
        var setting = _dbContext.Settings.FirstOrDefault(s => s.Key == MainUnitIdSettingKey);

        if (setting != null)
        {
            if (int.TryParse(setting.Value, out var id))
            {
                return id;
            }
        }

        return 0;
    }

    public void SaveMainUnitId(int id)
    {
        var setting = _dbContext.Settings.FirstOrDefault(s => s.Key == MainUnitIdSettingKey);

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

        _dbContext.SaveChanges();
    }
}
