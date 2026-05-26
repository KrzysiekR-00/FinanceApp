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

    public void SaveAssetUnits(AssetUnit[] assetUnits)
    {
        var currentIds = _dbContext.AssetUnits.Select(u => u.Id).ToArray();

        var entities = assetUnits.Select(u => new AssetUnitEntity()
        {
            Id = u.Id,
            Symbol = u.Symbol,
            UnitModifier = u.UnitModifier
        });

        var toDeleteIds = currentIds.Where(c => !entities.Any(e => e.Id == c));

        _dbContext.AssetUnits.RemoveRange(_dbContext.AssetUnits.Where(u => toDeleteIds.Contains(u.Id)));

        _dbContext.AssetUnits.AddRange(entities.Where(e => e.Id == 0));

        foreach (var e in entities.Where(e => e.Id != 0))
        {
            var toUpdate = _dbContext.AssetUnits.First(u => u.Id == e.Id);

            toUpdate.Symbol = e.Symbol;
            toUpdate.UnitModifier = e.UnitModifier;
        }

        _dbContext.SaveChanges();
    }

    public void SaveMainUnitId(int id)
    {
        //throw new NotImplementedException();
    }
}
