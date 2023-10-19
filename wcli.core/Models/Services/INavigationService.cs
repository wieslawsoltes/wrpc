using CommunityToolkit.Mvvm.Input;

namespace WasabiCli.Models.Services;

public interface INavigationService
{
    IRoutable? CurrentRoutable { get; set; }
    IRelayCommand<IRoutable?> NavigateToCommand { get; }
    IRelayCommand<IRoutable?> ClearAndNavigateToCommand { get; }
    IRelayCommand NavigateBackCommand { get; }
    IRelayCommand ClearCommand { get; }
    void Clear();
    void NavigateBack();
    void NavigateTo(IRoutable? routable);
    void ClearAndNavigateTo(IRoutable? routable);
}
