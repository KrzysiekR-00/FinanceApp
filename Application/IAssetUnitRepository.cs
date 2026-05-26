using Domain;

namespace Services;

public interface IAssetUnitRepository
{
    Task<AssetUnit[]> GetAssetUnits();
    Task CreateAssetUnit(AssetUnit assetUnit);
    Task UpdateAssetUnit(AssetUnit assetUnit);
    Task DeleteAssetUnit(AssetUnit assetUnit);
    Task<int> GetMainUnitId();
    Task SaveMainUnitId(int id);
}
