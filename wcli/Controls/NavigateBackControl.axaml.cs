using Avalonia;
using Avalonia.Controls.Primitives;

namespace WasabiCli.Controls;

public class NavigateBackControl : HeaderedContentControl
{
    public static readonly StyledProperty<bool> EnableBackProperty = 
        AvaloniaProperty.Register<NavigateBackControl, bool>(nameof(EnableBack), true);

    public bool EnableBack
    {
        get => GetValue(EnableBackProperty);
        set => SetValue(EnableBackProperty, value);
    }
}
