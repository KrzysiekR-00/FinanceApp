using Domain;

namespace Services;

public class AssetUnitService
{
    private readonly IAssetUnitRepository _repository;

    public AssetUnitService(IAssetUnitRepository repository)
    {
        _repository = repository;
    }

    public AssetUnit[] GetAssetUnits()
    {
        return _repository.GetAssetUnits();
    }

    public void CreateAssetUnit(AssetUnit assetUnit)
    {
        _repository.CreateAssetUnit(assetUnit);
    }

    public bool CanEditAssetUnit(AssetUnit assetUnit)
    {
        return true;
    }

    public void UpdateAssetUnit(AssetUnit assetUnit)
    {
        if (!CanEditAssetUnit(assetUnit)) return;

        _repository.UpdateAssetUnit(assetUnit);
    }

    public void DeleteAssetUnit(AssetUnit assetUnit)
    {
        if (!CanEditAssetUnit(assetUnit)) return;

        _repository.DeleteAssetUnit(assetUnit);
    }

    public int GetMainUnitId()
    {
        return _repository.GetMainUnitId();
    }

    public void SaveMainUnitId(int id)
    {
        _repository.SaveMainUnitId(id);
    }
}
