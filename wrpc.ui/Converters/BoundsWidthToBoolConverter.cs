using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace WasabiRpc.Converters;

public class BoundsWidthToBoolConverter : IValueConverter
{
    public static readonly BoundsWidthToBoolConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            if (parameter is string thresholdString)
            {
                if (double.TryParse(thresholdString, out var threshold))
                {
                    return width >= threshold;
                }
            }
            else
            {
                if (parameter is double threshold)
                {
                    return width >= threshold;
                }
            }
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
