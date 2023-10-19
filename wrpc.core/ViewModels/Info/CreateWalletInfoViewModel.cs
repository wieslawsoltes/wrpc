using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class CreateWalletInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _mnemonic;

    public CreateWalletInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
