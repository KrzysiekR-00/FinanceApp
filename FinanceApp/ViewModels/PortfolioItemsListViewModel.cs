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
    public partial PortfolioItemUnit[] PortfolioItemUnits { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemType[] PortfolioItemTypes { get; set; } = null!;

    [ObservableProperty]
    public partial PortfolioItemUnit? MainUnit { get; set; }

    private readonly PortfolioItemService _portfolioItemService;
    private readonly PortfolioItemUnitService _PortfolioItemUnitService;

    public class PortfolioItemViewModel
    {
        public required PortfolioItem PortfolioItem { get; set; }
        public required bool CanEdit { get; set; }
    }

    public PortfolioItemsListViewModel(PortfolioItemService portfolioItemService, PortfolioItemUnitService PortfolioItemUnitService)
    {
        _portfolioItemService = portfolioItemService;
        _PortfolioItemUnitService = PortfolioItemUnitService;

        PortfolioItemTypes =
        [
            PortfolioItemType.Asset,
            PortfolioItemType.Liability
        ];
    }

    public override async Task OnNavigateTo()
    {
        await ReloadList();

        PortfolioItemUnits = await _PortfolioItemUnitService.GetPortfolioItemUnits();
        var mainUnitId = await _PortfolioItemUnitService.GetMainUnitId();
        MainUnit = PortfolioItemUnits.FirstOrDefault(u => u.Id == mainUnitId);

        if (MainUnit != null)
        {
            EditedItem = new() { Id = 0, Name = string.Empty, Type = PortfolioItemType.Asset, Unit = MainUnit };
        }
    }

    private async Task ReloadList()
    {
        var items = await _portfolioItemService.GetPortfolioItems();

        var viewModels = items.Select(i => new PortfolioItemViewModel()
        {
            PortfolioItem = i,
            CanEdit = false
        }).ToArray();

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

        if (MainUnit != null)
        {
            EditedItem = new() { Id = 0, Name = string.Empty, Type = PortfolioItemType.Asset, Unit = MainUnit };
        }
    }
}
