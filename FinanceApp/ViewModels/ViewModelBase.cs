using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceApp.ViewModels;

internal abstract class ViewModelBase : ObservableObject
{
    //public virtual string Title => string.Empty;

    public virtual void OnNavigateTo()
    {

    }
}
