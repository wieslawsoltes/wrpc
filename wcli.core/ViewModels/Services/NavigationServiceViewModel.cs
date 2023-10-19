using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Services;

public partial class NavigationServiceViewModel : ViewModelBase, INavigationService
{
    [ObservableProperty] private Stack<IRoutable>? _routableStack;
    [ObservableProperty] private IRoutable? _currentRoutable;

    public NavigationServiceViewModel()
    {
        RoutableStack = new Stack<IRoutable>();
        CurrentRoutable = null;
    }

    [RelayCommand]
    public void Clear()
    {
        RoutableStack?.Clear();
    }

    [RelayCommand]
    public void NavigateBack()
    {
        if (RoutableStack is not null)
        {
            if (RoutableStack.Count > 0)
            {
                RoutableStack.Pop();
            }

            if (RoutableStack.TryPeek(out var dialog))
            {
                CurrentRoutable = dialog;
            }
            else
            {
                CurrentRoutable = null;
            }
        }
    }

    [RelayCommand]
    public void NavigateTo(IRoutable? routable)
    {
        if (routable is null)
        {
            RoutableStack?.Clear();
        }
        else
        {
            RoutableStack?.Push(routable);
        }

        CurrentRoutable = routable;
    }

    [RelayCommand]
    public void ClearAndNavigateTo(IRoutable? routable)
    {
        Clear();
        NavigateTo(routable);
    }
}
