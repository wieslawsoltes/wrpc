using CommunityToolkit.Mvvm.ComponentModel;

namespace WasabiCli.ViewModels;

public partial class WalletViewModel : ViewModelBase
{
    [ObservableProperty] 
    private string? _walletName;

    public WalletViewModel()
    {
    }
}
