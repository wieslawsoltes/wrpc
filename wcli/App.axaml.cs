using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using WasabiCli.ViewModels;
using WasabiCli.Views;

namespace WasabiCli;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var navigationService = new NavigationServiceViewModel();

            Navigation.NavigationService = navigationService;

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(navigationService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
