using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class BroadcastViewModel : BatchMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(BroadcastCommand))]
    [ObservableProperty] 
    private string? _tx;

    public BroadcastViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanBroadcast()
    {
        return Tx is not null
               && Tx.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBroadcast))]
    private async Task Broadcast()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcBroadcastResult);
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

    protected override void OnRpcError(RpcErrorResult rpcErrorResult)
    {
        NavigationService.Navigate(rpcErrorResult.Error);
    }

    protected override void OnError(Error error)
    {
        NavigationService.Navigate(error);
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
