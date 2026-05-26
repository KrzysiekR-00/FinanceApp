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

    private bool _isInitialized = true;

    public class AssetUnitViewModel
    {
        public required AssetUnit AssetUnit { get; set; }
        public required bool CanEdit { get; set; }
    }

    public AssetUnitsListViewModel(AssetUnitService assetUnitService)
    {
        _assetUnitService = assetUnitService;
    }

    public override async Task OnNavigateTo()
    {
        _isInitialized = false;

        await ReloadList();

        var mainUnitId = await _assetUnitService.GetMainUnitId();
        MainUnit = AssetUnits.FirstOrDefault(u => u.AssetUnit.Id == mainUnitId);

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };

        _isInitialized = true;
    }

    private async Task ReloadList()
    {
        var units = await _assetUnitService.GetAssetUnits();

        var viewModels = units.Select(u => new AssetUnitViewModel()
        {
            AssetUnit = u,
            CanEdit = false
        });

        foreach (var viewModel in viewModels)
        {
            viewModel.CanEdit = await _assetUnitService.CanEditAssetUnit(viewModel.AssetUnit);
        }

        AssetUnits = new ObservableCollection<AssetUnitViewModel>(viewModels);
    }

    partial void OnMainUnitChanged(AssetUnitViewModel? value)
    {
        if (MainUnit == null) return;
        if (_isInitialized == false) return;

        _ = _assetUnitService.SaveMainUnitId(MainUnit.AssetUnit.Id);
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedUnit != null) EditedUnit = SelectedUnit.AssetUnit;
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedUnit != null)
        {
            await _assetUnitService.DeleteAssetUnit(SelectedUnit.AssetUnit);

            await ReloadList();
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrEmpty(EditedUnit.Symbol)) return;

        if (EditedUnit.Id == 0)
        {
            await _assetUnitService.CreateAssetUnit(EditedUnit);
        }
        else
        {
            await _assetUnitService.UpdateAssetUnit(EditedUnit);
        }

        await ReloadList();

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };
    }
}
