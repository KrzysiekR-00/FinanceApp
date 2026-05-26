using Domain;

namespace Services;

public interface IAssetUnitRepository
{
    AssetUnit[] GetAssetUnits();
    void CreateAssetUnit(AssetUnit assetUnit);
    void UpdateAssetUnit(AssetUnit assetUnit);
    void DeleteAssetUnit(AssetUnit assetUnit);
    int GetMainUnitId();
    void SaveMainUnitId(int id);
}
