using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceApp.ViewModels;

internal partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableObject StartupViewModel { get; set; } = null!;

    [ObservableProperty]
    public partial string WindowTitle { get; set; } = string.Empty;

    public MainWindowViewModel(MainLayoutViewModel mainLayoutViewModel)
    {
        WindowTitle = "Aplikacja do śledzenia stanu finansów";

        StartupViewModel = mainLayoutViewModel;
    }
}