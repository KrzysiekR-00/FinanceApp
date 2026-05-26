using CommunityToolkit.Mvvm.ComponentModel;
using FinanceApp.ViewModels;

namespace FinanceApp.Navigation;

internal partial class Navigator : ObservableObject
{
    [ObservableProperty]
    public partial ViewModelBase CurrentViewModel { get; set; } = null!;

    internal Navigator(ViewModelBase startupViewModel)
    {
        _ = NavigateTo(startupViewModel);
    }

    internal async Task NavigateTo(ViewModelBase viewModel)
    {
        if (CurrentViewModel == viewModel) return;
        if (CurrentViewModel?.GetType() == viewModel.GetType()) return;

        CurrentViewModel = viewModel;

        await CurrentViewModel.OnNavigateTo();
    }
}