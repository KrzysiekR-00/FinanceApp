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

    public int GetMainUnitId()
    {
        return _repository.GetMainUnitId();
    }

    public void SaveAssetUnits(AssetUnit[] assetUnits)
    {
        _repository.SaveAssetUnits(assetUnits);
    }

    public void SaveMainUnitId(int id)
    {
        _repository.SaveMainUnitId(id);
    }
}
