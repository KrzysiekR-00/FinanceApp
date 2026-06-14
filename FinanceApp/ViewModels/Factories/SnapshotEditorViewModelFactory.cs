using Domain;
using Services;

namespace FinanceApp.ViewModels.Factories;

internal class SnapshotEditorViewModelFactory
{
    private readonly PortfolioItemService _portfolioItemService;
    private readonly PortfolioItemUnitService _portfolioItemUnitService;
    private readonly SnapshotService _snapshotService;

    public SnapshotEditorViewModelFactory(PortfolioItemService portfolioItemService, PortfolioItemUnitService portfolioItemUnitService, SnapshotService snapshotService)
    {
        _portfolioItemService = portfolioItemService;
        _portfolioItemUnitService = portfolioItemUnitService;
        _snapshotService = snapshotService;
    }

    internal async Task<SnapshotEditorViewModel> CreateNew()
    {
        var date = DateOnly.FromDateTime(DateTime.Today);
        var portfolioItemUnits = await _portfolioItemUnitService.GetPortfolioItemUnits();
        var portfolioItems = await _portfolioItemService.GetPortfolioItems();

        var snapshot = new PortfolioSnapshot()
        {
            Date = date,
            ExchangeRates = portfolioItemUnits.Select(u => new ExchangeRateSnapshot()
            {
                Id = 0,
                Date = date,
                PortfolioItemUnit = u,
                Value = 0
            }).ToArray(),
            PortfolioItems = portfolioItems.Select(p => new PortfolioItemSnapshot()
            {
                Id = 0,
                Date = date,
                PortfolioItem = p,
                Quantity = 0
            }).ToArray()
        };

        return new SnapshotEditorViewModel(snapshot, _portfolioItemService, _portfolioItemUnitService, _snapshotService);
    }

    //internal SnapshotEditorViewModel CreateEdit()
    //{
    //    return new SnapshotEditorViewModel();
    //}
}
