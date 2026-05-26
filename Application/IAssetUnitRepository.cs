using Domain;

namespace Services;

public interface IAssetUnitRepository
{
    AssetUnit[] GetAssetUnits();
    void SaveAssetUnits(AssetUnit[] assetUnits);
    int GetMainUnitId();
    void SaveMainUnitId(int id);
}
