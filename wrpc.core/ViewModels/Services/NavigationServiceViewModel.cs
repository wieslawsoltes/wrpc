/*
 * wrpc A Graphical User Interface for using the Wasabi Wallet RPC.
 * Copyright (C) 2023  Wiesław Šoltés
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Services;

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
        CurrentRoutable = null;
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
