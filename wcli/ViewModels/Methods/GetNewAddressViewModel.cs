using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.ViewModels.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class GetNewAddressViewModel : ViewModelBase
{
    [ObservableProperty] private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [ObservableProperty] 
    private string? _label;

    public GetNewAddressViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        Label = "Label";
    }

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanGetNewAddress()
    {
        return Label is not null 
               && Label.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private async Task GetNewAddress()
    {
        var requestBody = new RpcMethod
        {
            Method = "getnewaddress",
            Params = new []
            {
                Label
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetNewAddressResult);
        if (result is RpcGetNewAddressResult { Result: not null } rpcGetNewAddressResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(rpcGetNewAddressResult.Result);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }
}
