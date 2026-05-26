using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Navigation;

namespace FinanceApp.ViewModels;

internal partial class MainLayoutViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial Navigator Navigator { get; set; } = null!;

    private readonly AssetUnitsListViewModel _assetUnitsListViewModel;
    private readonly PortfolioItemsListViewModel _portfolioItemsListViewModel;

    public MainLayoutViewModel(AssetUnitsListViewModel assetUnitsListViewModel, PortfolioItemsListViewModel portfolioItemsListViewModel)
    {
        Navigator = new Navigator(null);

        _assetUnitsListViewModel = assetUnitsListViewModel;
        _portfolioItemsListViewModel = portfolioItemsListViewModel;
    }

    [RelayCommand]
    private async Task OpenAssetUnitsList()
    {
        await Navigator.NavigateTo(_assetUnitsListViewModel);
    }

    [RelayCommand]
    private async Task OpenPortfolioItemsList()
    {
        await Navigator.NavigateTo(_portfolioItemsListViewModel);
    }
}
