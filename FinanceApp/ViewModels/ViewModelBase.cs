using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceApp.ViewModels;

internal abstract class ViewModelBase : ObservableObject
{
    public virtual async Task OnNavigateTo()
    {

    }
}
