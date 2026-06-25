using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Navigation;
using FinanceApp.ViewModels.Factories;

namespace FinanceApp.ViewModels;

internal partial class MainLayoutViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial Navigator Navigator { get; set; } = null!;

    private readonly PortfolioItemUnitsListViewModel _portfolioItemUnitsListViewModel;
    private readonly PortfolioItemsListViewModel _portfolioItemsListViewModel;
    private readonly SnapshotEditorViewModelFactory _snapshotEditorViewModelFactory;
    private readonly PortfolioItemsSnapshotsViewModel _portfolioItemsSnapshotsViewModel;
    private readonly PortfolioItemUnitsSnapshotsViewModel _portfolioItemUnitsSnapshotsViewModel;
    private readonly DashboardViewModel _dashboardViewModel;

    public MainLayoutViewModel(
        PortfolioItemUnitsListViewModel portfolioItemUnitsListViewModel,
        PortfolioItemsListViewModel portfolioItemsListViewModel,
        SnapshotEditorViewModelFactory snapshotEditorViewModelFactory,
        PortfolioItemsSnapshotsViewModel portfolioItemsSnapshotsViewModel,
        PortfolioItemUnitsSnapshotsViewModel portfolioItemUnitsSnapshotsViewModel,
        DashboardViewModel dashboardViewModel
        )
    {
        Navigator = new Navigator(null);

        _portfolioItemUnitsListViewModel = portfolioItemUnitsListViewModel;
        _portfolioItemsListViewModel = portfolioItemsListViewModel;
        _snapshotEditorViewModelFactory = snapshotEditorViewModelFactory;
        _portfolioItemsSnapshotsViewModel = portfolioItemsSnapshotsViewModel;
        _portfolioItemUnitsSnapshotsViewModel = portfolioItemUnitsSnapshotsViewModel;
        _dashboardViewModel = dashboardViewModel;
    }

    [RelayCommand]
    private async Task OpenDashboard()
    {
        await Navigator.NavigateTo(_dashboardViewModel);
    }

    [RelayCommand]
    private async Task OpenPortfolioItemUnitsList()
    {
        await Navigator.NavigateTo(_portfolioItemUnitsListViewModel);
    }

    [RelayCommand]
    private async Task OpenPortfolioItemsList()
    {
        await Navigator.NavigateTo(_portfolioItemsListViewModel);
    }

    [RelayCommand]
    private async Task AddNewSnapshot()
    {
        await Navigator.NavigateTo(await _snapshotEditorViewModelFactory.CreateNew());
    }

    [RelayCommand]
    private async Task OpenPortfolioItemsSnapshots()
    {
        await Navigator.NavigateTo(_portfolioItemsSnapshotsViewModel);
    }

    [RelayCommand]
    private async Task OpenPortfolioItemUnitsSnapshots()
    {
        await Navigator.NavigateTo(_portfolioItemUnitsSnapshotsViewModel);
    }
}
