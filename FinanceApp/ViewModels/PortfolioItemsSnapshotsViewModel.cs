using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;

namespace FinanceApp.ViewModels;

internal partial class PortfolioItemsSnapshotsViewModel : ViewModelBase
{
    private readonly SnapshotService _snapshotService;
    private readonly PortfolioItemService _portfolioItemService;

    [ObservableProperty]
    public partial PortfolioItem[] PortfolioItems { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItem SelectedPortfolioItem { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemSnapshot[] Snapshots { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemSnapshot SelectedSnapshot { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemSnapshot EditedSnapshot { get; set; } = null!;

    public PortfolioItemsSnapshotsViewModel(SnapshotService snapshotService, PortfolioItemService portfolioItemService)
    {
        _snapshotService = snapshotService;
        _portfolioItemService = portfolioItemService;
    }

    public override async Task OnNavigateTo()
    {
        //_isInitialized = false;

        PortfolioItems = await _portfolioItemService.GetPortfolioItems();
        if (PortfolioItems.Length > 0)
        {
            SelectedPortfolioItem = PortfolioItems[0];
        }

        //_isInitialized = true;
    }

    [RelayCommand]
    private async Task Reload()
    {
        Snapshots = await _snapshotService.GetPortfolioItemSnapshots(SelectedPortfolioItem.Id);

        EditedSnapshot = new PortfolioItemSnapshot()
        {
            Id = 0,
            Date = DateOnly.FromDateTime(DateTime.Today),
            Quantity = 0,
            PortfolioItem = SelectedPortfolioItem
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
            await _snapshotService.DeletePortfolioItemSnapshot(SelectedSnapshot);

            await Reload();
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        //if (string.IsNullOrEmpty(EditedUnit.Symbol)) return;

        if (EditedSnapshot.Id == 0)
        {
            await _snapshotService.CreatePortfolioItemSnapshot(EditedSnapshot);
        }
        else
        {
            await _snapshotService.UpdatePortfolioItemSnapshot(EditedSnapshot);
        }

        await Reload();
    }
}
