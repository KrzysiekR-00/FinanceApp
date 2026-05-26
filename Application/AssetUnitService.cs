using Domain;

namespace Services;

public class AssetUnitService
{
    private readonly IAssetUnitRepository _repository;

    public AssetUnitService(IAssetUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<AssetUnit[]> GetAssetUnits()
    {
        return await _repository.GetAssetUnits();
    }

    public async Task CreateAssetUnit(AssetUnit assetUnit)
    {
        await _repository.CreateAssetUnit(assetUnit);
    }

    public async Task<bool> CanEditAssetUnit(AssetUnit assetUnit)
    {
        return true;
    }

    public async Task UpdateAssetUnit(AssetUnit assetUnit)
    {
        if (!await CanEditAssetUnit(assetUnit)) return;

        await _repository.UpdateAssetUnit(assetUnit);
    }

    public async Task DeleteAssetUnit(AssetUnit assetUnit)
    {
        if (!await CanEditAssetUnit(assetUnit)) return;

        await _repository.DeleteAssetUnit(assetUnit);
    }

    public async Task<int> GetMainUnitId()
    {
        return await _repository.GetMainUnitId();
    }

    public async Task SaveMainUnitId(int id)
    {
        await _repository.SaveMainUnitId(id);
    }
}
