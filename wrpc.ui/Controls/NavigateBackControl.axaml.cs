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
}
