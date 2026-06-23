using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;

namespace FinanceApp.ViewModels;

internal partial class PortfolioItemUnitsSnapshotsViewModel : ViewModelBase
{
    private readonly SnapshotService _snapshotService;
    private readonly PortfolioItemUnitService _portfolioItemUnitService;

    [ObservableProperty]
    public partial PortfolioItemUnit[] PortfolioItemUnits { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemUnit SelectedPortfolioItemUnit { get; set; } = null!;

    [ObservableProperty]
    public partial ExchangeRateSnapshot[] Snapshots { get; set; } = null!;

    [ObservableProperty]
    public partial ExchangeRateSnapshot SelectedSnapshot { get; set; } = null!;

    [ObservableProperty]
    public partial ExchangeRateSnapshot EditedSnapshot { get; set; } = null!;

    public PortfolioItemUnitsSnapshotsViewModel(SnapshotService snapshotService, PortfolioItemUnitService portfolioItemUnitService)
    {
        _snapshotService = snapshotService;
        _portfolioItemUnitService = portfolioItemUnitService;
    }

    public override async Task OnNavigateTo()
    {
        //_isInitialized = false;

        var mainUnitId = await _portfolioItemUnitService.GetMainUnitId();

        var units = await _portfolioItemUnitService.GetPortfolioItemUnits();

        PortfolioItemUnits = units.Where(u => u.Id != mainUnitId).ToArray();

        if (PortfolioItemUnits.Length > 0)
        {
            SelectedPortfolioItemUnit = PortfolioItemUnits[0];
        }

        //_isInitialized = true;
    }

    [RelayCommand]
    private async Task Reload()
    {
        Snapshots = await _snapshotService.GetExchangeRateSnapshots(SelectedPortfolioItemUnit.Id);

        EditedSnapshot = new ExchangeRateSnapshot()
        {
            Id = 0,
            Date = DateOnly.FromDateTime(DateTime.Today),
            Value = 0,
            PortfolioItemUnit = SelectedPortfolioItemUnit
        };
    }

    [RelayCommand]
    private void Edit()
    {
        EditedSnapshot = SelectedSnapshot;
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedSnapshot != null)
        {
            await _snapshotService.DeleteExchangeRateSnapshot(SelectedSnapshot);

            await Reload();
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        //if (string.IsNullOrEmpty(EditedUnit.Symbol)) return;

        if (EditedSnapshot.Id == 0)
        {
            await _snapshotService.CreateExchangeRateSnapshot(EditedSnapshot);
        }
        else
        {
            await _snapshotService.UpdateExchangeRateSnapshot(EditedSnapshot);
        }

        await Reload();
    }
}
