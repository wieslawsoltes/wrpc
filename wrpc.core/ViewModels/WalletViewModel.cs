using CommunityToolkit.Mvvm.ComponentModel;

namespace WasabiRpc.ViewModels;

public partial class WalletViewModel : ViewModelBase
{
    [ObservableProperty] 
    private string? _walletName;
}
