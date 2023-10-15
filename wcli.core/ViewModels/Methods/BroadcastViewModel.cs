using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class BroadcastViewModel : RpcMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(BroadcastCommand))]
    [ObservableProperty] 
    private string? _tx;

    public BroadcastViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    private bool CanBroadcast()
    {
        return Tx is not null
               && Tx.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBroadcast))]
    private async Task Broadcast()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcBroadcastResult>(job);
        if (result is RpcBroadcastResult { Result: not null } rpcBroadcastResult)
        {
            OnRpcSuccess(rpcBroadcastResult);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            OnRpcError(rpcErrorResult);
        }
        else if (result is Error error)
        {
            OnError(error);
        }
    }

    protected override void OnRpcSuccess(Rpc rpcResult)
    {
        if (rpcResult is RpcBroadcastResult rpcBroadcastResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(rpcBroadcastResult.Result);
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "broadcast",
            Params = new []
            {
                Tx
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
