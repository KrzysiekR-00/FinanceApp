using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels;

internal partial class AssetUnitsListViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<AssetUnitViewModel> AssetUnits { get; set; } = null!;

    [ObservableProperty]
    public partial AssetUnitViewModel? SelectedUnit { get; set; } = null;

    [ObservableProperty]
    public partial AssetUnit EditedUnit { get; set; } = null!;

    [ObservableProperty]
    public partial AssetUnitViewModel? MainUnit { get; set; } = null;

    private readonly AssetUnitService _assetUnitService;

    public class AssetUnitViewModel
    {
        public required AssetUnit AssetUnit { get; set; }
        public required bool CanEdit { get; set; }
    }

    public AssetUnitsListViewModel(AssetUnitService assetUnitService)
    {
        _assetUnitService = assetUnitService;
    }

    public override void OnNavigateTo()
    {
        ReloadList();

        var mainUnitId = _assetUnitService.GetMainUnitId();
        MainUnit = AssetUnits.FirstOrDefault(u => u.AssetUnit.Id == mainUnitId);

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };
    }

    private void ReloadList()
    {
        AssetUnits = [.. _assetUnitService
            .GetAssetUnits()
            .Select(u => new AssetUnitViewModel() {
                AssetUnit = u,
                CanEdit = _assetUnitService.CanEditAssetUnit(u)
            })];
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedUnit != null) EditedUnit = SelectedUnit.AssetUnit;
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedUnit != null)
        {
            _assetUnitService.DeleteAssetUnit(SelectedUnit.AssetUnit);

            ReloadList();
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrEmpty(EditedUnit.Symbol)) return;

        if (EditedUnit.Id == 0)
        {
            _assetUnitService.CreateAssetUnit(EditedUnit);
        }
        else
        {
            _assetUnitService.UpdateAssetUnit(EditedUnit);
        }

        ReloadList();

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };
    }
}
