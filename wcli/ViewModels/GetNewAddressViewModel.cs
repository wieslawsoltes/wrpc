using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels;

public partial class GetNewAddressViewModel : ViewModelBase
{
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _label;

    public GetNewAddressViewModel(RpcServiceViewModel rpcService, NavigationServiceViewModel navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName; 
    }

    public RpcServiceViewModel RpcService { get; }

    private NavigationServiceViewModel NavigationService { get; }

    [RelayCommand]
    private async Task GetNewAddress()
    {
        // {"jsonrpc":"2.0","id":"1","method":"getnewaddress","params":["Daniel, Alice"]}
        var requestBody = new RpcMethod()
        {
            Method = "getnewaddress",
            // TODO:
            Params = new []
            {
                Label
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetNewAddressResult);
        if (rpcResult is RpcGetNewAddressResult { Result: not null } rpcGetNewAddressResult)
        {
            // TODO:
            NavigationService.Clear();
            NavigationService.Navigate(rpcGetNewAddressResult.Result);
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }
}
