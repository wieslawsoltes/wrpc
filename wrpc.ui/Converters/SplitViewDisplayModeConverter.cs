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
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace WasabiRpc.Converters;

public class SplitViewDisplayModeConverter : IMultiValueConverter
{
    public static readonly SplitViewDisplayModeConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2)
        {
            if (values[0] is bool isContentVisible && values[1] is bool isWideContent)
            {
                if (isWideContent)
                {
                    return !isContentVisible ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Inline;
                }

                return !isContentVisible ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            }
        }

        return AvaloniaProperty.UnsetValue;
    }
}
