using CommunityToolkit.Mvvm.Input;

namespace WasabiCli.Models.Navigation;

public interface INavigationService
{
    object? CurrentDialog { get; set; }
    IRelayCommand<object?> NavigateCommand { get; }
    IRelayCommand BackCommand { get; }
    IRelayCommand ClearCommand { get; }
    void Clear();
    void Back();
    void Navigate(object? dialog);
}
