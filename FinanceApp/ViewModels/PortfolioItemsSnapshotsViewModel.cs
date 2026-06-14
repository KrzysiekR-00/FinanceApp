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
    }
}
