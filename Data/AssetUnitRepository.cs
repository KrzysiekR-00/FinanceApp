using Data.Entities;
using Domain;
using Services;

namespace Data;

public class AssetUnitRepository : IAssetUnitRepository
{
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

    public int GetMainUnitId()
    {
        return 0;
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

    public void SaveMainUnitId(int id)
    {
        //throw new NotImplementedException();
    }
}
