using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class CreateWalletInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _mnemonic;

    public CreateWalletInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
