using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain;
using Services;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels;

internal partial class PortfolioItemsListViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<PortfolioItemViewModel> PortfolioItems { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemViewModel? SelectedItem { get; set; } = null;

    [ObservableProperty]
    public partial PortfolioItem EditedItem { get; set; } = null!;

    [ObservableProperty]
    public partial AssetUnit[] AssetUnits { get; set; }

    [ObservableProperty]
    public partial AssetUnit MainUnit { get; set; }

    private readonly PortfolioItemService _portfolioItemService;
    private readonly AssetUnitService _assetUnitService;

    public class PortfolioItemViewModel
    {
        public required PortfolioItem PortfolioItem { get; set; }
        public required bool CanEdit { get; set; }
    }

    public PortfolioItemsListViewModel(PortfolioItemService portfolioItemService, AssetUnitService assetUnitService)
    {
        _portfolioItemService = portfolioItemService;
        _assetUnitService = assetUnitService;
    }

    public override async Task OnNavigateTo()
    {
        await ReloadList();

        AssetUnits = await _assetUnitService.GetAssetUnits();
        var mainUnitId = await _assetUnitService.GetMainUnitId();
        MainUnit = AssetUnits.First(u => u.Id == mainUnitId);

        EditedItem = new() { Id = 0, Name = string.Empty, Unit = MainUnit };
    }

    private async Task ReloadList()
    {
        var items = await _portfolioItemService.GetPortfolioItems();

        var viewModels = items.Select(i => new PortfolioItemViewModel()
        {
            PortfolioItem = i,
            CanEdit = false
        });

        foreach (var viewModel in viewModels)
        {
            viewModel.CanEdit = await _portfolioItemService.CanEditPortfolioItem(viewModel.PortfolioItem);
        }

        PortfolioItems = new ObservableCollection<PortfolioItemViewModel>(viewModels);
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedItem != null) EditedItem = SelectedItem.PortfolioItem;
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedItem != null)
        {
            await _portfolioItemService.DeletePortfolioItem(SelectedItem.PortfolioItem);

            await ReloadList();
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrEmpty(EditedItem.Name)) return;

        if (EditedItem.Id == 0)
        {
            await _portfolioItemService.CreatePortfolioItem(EditedItem);
        }
        else
        {
            await _portfolioItemService.UpdatePortfolioItem(EditedItem);
        }

        await ReloadList();

        EditedItem = new() { Id = 0, Name = string.Empty, Unit = MainUnit };
    }
}
