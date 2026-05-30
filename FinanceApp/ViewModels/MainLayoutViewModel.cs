using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Navigation;

namespace FinanceApp.ViewModels;

internal partial class MainLayoutViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial Navigator Navigator { get; set; } = null!;

    private readonly PortfolioItemUnitsListViewModel _PortfolioItemUnitsListViewModel;
    private readonly PortfolioItemsListViewModel _portfolioItemsListViewModel;

    public MainLayoutViewModel(PortfolioItemUnitsListViewModel PortfolioItemUnitsListViewModel, PortfolioItemsListViewModel portfolioItemsListViewModel)
    {
        Navigator = new Navigator(null);

        _PortfolioItemUnitsListViewModel = PortfolioItemUnitsListViewModel;
        _portfolioItemsListViewModel = portfolioItemsListViewModel;
    }

    [RelayCommand]
    private async Task OpenPortfolioItemUnitsList()
    {
        await Navigator.NavigateTo(_PortfolioItemUnitsListViewModel);
    }

    [RelayCommand]
    private async Task OpenPortfolioItemsList()
    {
        await Navigator.NavigateTo(_portfolioItemsListViewModel);
    }
}
