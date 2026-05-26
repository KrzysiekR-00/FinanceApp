using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Navigation;

namespace FinanceApp.ViewModels;

internal partial class MainLayoutViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial Navigator Navigator { get; set; } = null!;

    private readonly AssetUnitsListViewModel _assetUnitsListViewModel;

    public MainLayoutViewModel(AssetUnitsListViewModel assetUnitsListViewModel)
    {
        _assetUnitsListViewModel = assetUnitsListViewModel;

        Navigator = new Navigator(null);
    }

    [RelayCommand]
    private void OpenAssetUnitsList()
    {
        Navigator.NavigateTo(_assetUnitsListViewModel);
    }
}
