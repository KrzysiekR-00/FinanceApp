using CommunityToolkit.Mvvm.ComponentModel;
using Services;
using System.Data;

namespace FinanceApp.ViewModels;

internal partial class DashboardViewModel : ViewModelBase
{
    private readonly SnapshotService _snapshotService;
    private readonly PortfolioItemService _portfolioItemService;
    private readonly PortfolioItemUnitService _portfolioItemUnitService;

    [ObservableProperty]
    public partial DataView Data { get; set; } = null!;

    public DashboardViewModel(SnapshotService snapshotService, PortfolioItemService portfolioItemService, PortfolioItemUnitService portfolioItemUnitService)
    {
        _snapshotService = snapshotService;
        _portfolioItemService = portfolioItemService;
        _portfolioItemUnitService = portfolioItemUnitService;
    }

    public override async Task OnNavigateTo()
    {
        //var table = new DataTable();

        //table.Columns.Add("Imię");
        //table.Columns.Add("Wiek");

        //table.Rows.Add("Jan", 30);
        //table.Rows.Add("Anna", 25);

        //Data = table.DefaultView;

        var table = new DataTable();

        var portfolioItems = await _portfolioItemService.GetPortfolioItems();
        var mainUnitId = await _portfolioItemUnitService.GetMainUnitId();
        var units = await _portfolioItemUnitService.GetPortfolioItemUnits();
        units = units.Where(u => u.Id != mainUnitId).ToArray();

        table.Columns.Add("Data");
        foreach (var unit in units)
        {
            table.Columns.Add(unit.Symbol);
        }
        foreach (var item in portfolioItems)
        {
            table.Columns.Add(item.Name);
        }

        var snapshots = await _snapshotService.GetPortfolioSnapshots();

        foreach (var snapshot in snapshots)
        {
            var values = new List<object>();
            values.Add(snapshot.Date);
            foreach (var unit in units)
            {
                values.Add(snapshot.ExchangeRates.FirstOrDefault(er => er.PortfolioItemUnit.Id == unit.Id)?.Value ?? -1m);
            }
            foreach (var item in portfolioItems)
            {
                values.Add(snapshot.PortfolioItems.FirstOrDefault(pi => pi.PortfolioItem.Id == item.Id)?.Quantity ?? -1m);
            }
            table.Rows.Add(values.ToArray());
        }

        Data = table.DefaultView;
    }
}
