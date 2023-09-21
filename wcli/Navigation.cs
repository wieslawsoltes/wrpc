using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.Navigation;

namespace WasabiCli;

public static class Navigation
{
    public static INavigationService? NavigationService { get; set; }

    public static IRelayCommand<object?>? NavigateCommand => NavigationService?.NavigateCommand;

    public static IRelayCommand? BackCommand => NavigationService?.BackCommand;

    public static IRelayCommand? ClearCommand => NavigationService?.ClearCommand;
}
