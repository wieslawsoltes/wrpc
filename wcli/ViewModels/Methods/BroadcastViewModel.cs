using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

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

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanBroadcast()
    {
        return Tx is not null
               && Tx.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBroadcast))]
    private async Task Broadcast()
    {
        var requestBody = new RpcMethod
        {
            Method = "broadcast",
            Params = new []
            {
                Tx
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcBroadcastResult);
        if (result is RpcBroadcastResult { Result: not null } rpcBroadcastResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(rpcBroadcastResult.Result);
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
