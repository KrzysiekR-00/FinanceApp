using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels;

internal partial class PortfolioItemUnitsListViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<PortfolioItemUnitViewModel> PortfolioItemUnits { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemUnitViewModel? SelectedUnit { get; set; } = null;

    [ObservableProperty]
    public partial PortfolioItemUnit EditedUnit { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemUnitViewModel? MainUnit { get; set; } = null;

    private readonly PortfolioItemUnitService _portfolioItemUnitService;

    private bool _isInitialized = true;

    public class PortfolioItemUnitViewModel
    {
        public required PortfolioItemUnit PortfolioItemUnit { get; set; }
        public required bool CanEdit { get; set; }
    }

    public PortfolioItemUnitsListViewModel(PortfolioItemUnitService portfolioItemUnitService)
    {
        _portfolioItemUnitService = portfolioItemUnitService;
    }

    public override async Task OnNavigateTo()
    {
        _isInitialized = false;

        await ReloadList();

        var mainUnitId = await _portfolioItemUnitService.GetMainUnitId();
        MainUnit = PortfolioItemUnits.FirstOrDefault(u => u.PortfolioItemUnit.Id == mainUnitId);

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };

        _isInitialized = true;
    }

    private async Task ReloadList()
    {
        var units = await _portfolioItemUnitService.GetPortfolioItemUnits();

        var viewModels = units.Select(u => new PortfolioItemUnitViewModel()
        {
            PortfolioItemUnit = u,
            CanEdit = false
        }).ToArray();

        foreach (var viewModel in viewModels)
        {
            viewModel.CanEdit = await _portfolioItemUnitService.CanEditPortfolioItemUnit(viewModel.PortfolioItemUnit);
        }

        PortfolioItemUnits = new ObservableCollection<PortfolioItemUnitViewModel>(viewModels);
    }

    partial void OnMainUnitChanged(PortfolioItemUnitViewModel? value)
    {
        if (MainUnit == null) return;
        if (_isInitialized == false) return;

        _ = _portfolioItemUnitService.SaveMainUnitId(MainUnit.PortfolioItemUnit.Id);
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedUnit != null) EditedUnit = SelectedUnit.PortfolioItemUnit;
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedUnit != null)
        {
            await _portfolioItemUnitService.DeletePortfolioItemUnit(SelectedUnit.PortfolioItemUnit);

            await ReloadList();
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrEmpty(EditedUnit.Symbol)) return;

        if (EditedUnit.Id == 0)
        {
            await _portfolioItemUnitService.CreatePortfolioItemUnit(EditedUnit);
        }
        else
        {
            await _portfolioItemUnitService.UpdatePortfolioItemUnit(EditedUnit);
        }

        await ReloadList();

        EditedUnit = new() { Id = 0, Symbol = string.Empty, UnitModifier = 1 };
    }
}
