using Avalonia;
using Avalonia.Controls.Primitives;
using WasabiCli.Models.Services;

namespace WasabiCli.Controls;

public class NavigateBackControl : HeaderedContentControl
{
    public static readonly StyledProperty<INavigationService?> NavigationServiceProperty = 
        AvaloniaProperty.Register<NavigateBackControl, INavigationService?>(nameof(NavigationService));

    public static readonly StyledProperty<bool> EnableBackProperty = 
        AvaloniaProperty.Register<NavigateBackControl, bool>(nameof(EnableBack), true);

    public INavigationService? NavigationService
    {
        get => GetValue(NavigationServiceProperty);
        set => SetValue(NavigationServiceProperty, value);
    }

    public bool EnableBack
    {
        get => GetValue(EnableBackProperty);
        set => SetValue(EnableBackProperty, value);
    }
}
