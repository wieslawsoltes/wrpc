using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels;

public partial class BroadcastViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(BroadcastCommand))]
    [ObservableProperty] 
    private string? _tx;

    public BroadcastViewModel(RpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    public RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanBroadcast()
    {
        return Tx is not null
               && Tx.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBroadcast))]
    private async Task Broadcast()
    {
        // {"jsonrpc":"2.0","id":"1","method":"broadcast", "params":["020000000001021cb46e8537e18398c382d0d5622ad9e475245cd789e31fb2de285c802e7b56e50100000000fefffffffe0fa22998e2dd1e4d66b09bf7e253d201f7e0b88098973bfd5e6c5ac426398d0000000000feffffff02c315100000000000160014b60bfb6774881b531d70228fb36a5fd60bd36c6ca07a0300000000001600145d576a81f460e7a1ed254fe9bfff075ab3bc45650247304402206b6d7d282b796920bf1742ca996733866321e5b491cd4a50749b9f62192f635202200f7d5a2105da3361f41810868526545c1b01160e6381ab34c56956df2995bd2c012102549fa9f712caffdf63da7d077e6a26dc3d01ca275312e9f64d1d9accf949d2bc0247304402206afcb357fa31aa5d9d241b1365492b85f058f8029ba348ff7ffd2ee15bb972fe022064818c8ab49ce649a4bb37d645d933c5860b1d5a6916fe267f3fb29e6bafca10012102a80b143904f60ede946ba9a44f29df12b7fb16f6ae3dc93a728d6a00ef658e62ad2d2500"]}
        var requestBody = new RpcMethod
        {
            Method = "broadcast",
            Params = new []
            {
                Tx
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcBroadcastResult);
        if (rpcResult is RpcBroadcastResult { Result: not null } rpcBroadcastResult)
        {
            // TODO:
            NavigationService.Clear();
            NavigationService.Navigate(rpcBroadcastResult.Result);
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }
}
