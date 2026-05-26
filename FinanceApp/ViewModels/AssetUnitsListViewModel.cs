using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels;

internal partial class AssetUnitsListViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<AssetUnit> AssetUnits { get; set; } = null!;

    [ObservableProperty]
    public partial AssetUnit Selected { get; set; } = null!;

    [ObservableProperty]
    public partial AssetUnit? MainUnit { get; set; } = null;

    private readonly AssetUnitService _assetUnitService;

    public AssetUnitsListViewModel(AssetUnitService assetUnitService)
    {
        _assetUnitService = assetUnitService;
    }

    public override void OnNavigateTo()
    {
        AssetUnits = [.. _assetUnitService.GetAssetUnits()];

        var mainUnitId = _assetUnitService.GetMainUnitId();
        MainUnit = AssetUnits.FirstOrDefault(u => u.Id == mainUnitId);
    }

    [RelayCommand]
    private void Save()
    {
        _assetUnitService.SaveAssetUnits([.. AssetUnits]);
        if (MainUnit != null) _assetUnitService.SaveMainUnitId(MainUnit.Id);
    }
}
