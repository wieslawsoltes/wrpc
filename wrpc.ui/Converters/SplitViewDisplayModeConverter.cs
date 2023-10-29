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
