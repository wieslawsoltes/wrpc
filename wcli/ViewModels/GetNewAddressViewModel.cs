using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels;

public partial class GetNewAddressViewModel : ViewModelBase
{
    [ObservableProperty] private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [ObservableProperty] 
    private string? _label;

    public GetNewAddressViewModel(RpcServiceViewModel rpcService, NavigationServiceViewModel navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        Label = "Label";
    }

    public RpcServiceViewModel RpcService { get; }

    private NavigationServiceViewModel NavigationService { get; }

    private bool CanGetNewAddress()
    {
        return Label is not null 
               && Label.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
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
