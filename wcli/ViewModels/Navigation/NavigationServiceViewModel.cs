using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.Navigation;

namespace WasabiCli.ViewModels.Navigation;

public partial class NavigationServiceViewModel : ViewModelBase, INavigationService
{
    [ObservableProperty] private Stack<object>? _dialogs;
    [ObservableProperty] private object? _currentDialog;

    public NavigationServiceViewModel()
    {
        Dialogs = new Stack<object>();
        CurrentDialog = null;
    }

    [RelayCommand]
    public void Navigate(object? dialog)
    {
        if (dialog is null)
        {
            Dialogs?.Clear();
        }
        else
        {
            Dialogs?.Push(dialog);
        }

        CurrentDialog = dialog;
    }

    [RelayCommand]
    public void Back()
    {
        if (Dialogs is not null)
        {
            if (Dialogs.Count > 0)
            {
                Dialogs.Pop();
            }

            if (Dialogs.TryPeek(out var dialog))
            {
                CurrentDialog = dialog;
            }
            else
            {
                CurrentDialog = null;
            }
        }
    }

    [RelayCommand]
    public void Clear()
    {
        Dialogs?.Clear();
    }
}
