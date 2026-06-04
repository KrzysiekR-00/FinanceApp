using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using FinanceApp.ViewModels.Snapshots;
using Services;

namespace FinanceApp.ViewModels;

internal partial class SnapshotEditorViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial PortfolioSnapshotViewModel Snapshot { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemUnit MainUnit { get; set; } = null!;

    private readonly PortfolioItemService _portfolioItemService;
    private readonly PortfolioItemUnitService _portfolioItemUnitService;
    private readonly SnapshotService _snapshotService;

    private readonly PortfolioSnapshot _snapshot;

    public SnapshotEditorViewModel(PortfolioSnapshot snapshot, PortfolioItemService portfolioItemService, PortfolioItemUnitService portfolioItemUnitService, SnapshotService snapshotService)
    {
        _portfolioItemService = portfolioItemService;
        _portfolioItemUnitService = portfolioItemUnitService;
        _snapshotService = snapshotService;

        _snapshot = snapshot;
    }

    public override async Task OnNavigateTo()
    {
        var mainUnitId = await _portfolioItemUnitService.GetMainUnitId();

        var units = await _portfolioItemUnitService.GetPortfolioItemUnits();
        MainUnit = units.First(u => u.Id == mainUnitId);

        Snapshot = new PortfolioSnapshotViewModel()
        {
            Date = _snapshot.Date,
            ExchangeRates = _snapshot.ExchangeRates
                .Where(u => u.PortfolioItemUnit.Id != mainUnitId)
                .Select(u => new ExchangeRateSnapshotViewModel()
                {
                    PortfolioItemUnit = u.PortfolioItemUnit,
                    Value = u.Value
                }).ToArray(),
            PortfolioItems = _snapshot.PortfolioItems.Select(p => new PortfolioItemSnapshotViewModel()
            {
                PortfolioItem = p.PortfolioItem,
                Quantity = p.Quantity
            }).ToArray()
        };
    }

    [RelayCommand]
    private async Task Save()
    {

    }
}
