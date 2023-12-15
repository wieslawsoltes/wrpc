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
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace WasabiRpc.Controls;

public class NavigateBackControl : HeaderedContentControl
{
    public static readonly StyledProperty<ICommand?> BackCommandProperty = 
        AvaloniaProperty.Register<NavigateBackControl, ICommand?>(nameof(BackCommand));

    public static readonly StyledProperty<bool> EnableBackProperty = 
        AvaloniaProperty.Register<NavigateBackControl, bool>(nameof(EnableBack), true);

    public static readonly StyledProperty<ICommand?> RefreshCommandProperty = 
        AvaloniaProperty.Register<NavigateBackControl, ICommand?>(nameof(RefreshCommand));

    public ICommand? BackCommand
    {
        get => GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }

    public bool EnableBack
    {
        get => GetValue(EnableBackProperty);
        set => SetValue(EnableBackProperty, value);
    }

    public ICommand? RefreshCommand
    {
        get => GetValue(RefreshCommandProperty);
        set => SetValue(RefreshCommandProperty, value);
    }

}
