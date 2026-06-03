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

    public MainLayoutViewModel(PortfolioItemUnitsListViewModel portfolioItemUnitsListViewModel, PortfolioItemsListViewModel portfolioItemsListViewModel, SnapshotEditorViewModelFactory snapshotEditorViewModelFactory)
    {
        Navigator = new Navigator(null);

        _portfolioItemUnitsListViewModel = portfolioItemUnitsListViewModel;
        _portfolioItemsListViewModel = portfolioItemsListViewModel;
        _snapshotEditorViewModelFactory = snapshotEditorViewModelFactory;
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
        await Navigator.NavigateTo(_snapshotEditorViewModelFactory.CreateNew());
    }
}
