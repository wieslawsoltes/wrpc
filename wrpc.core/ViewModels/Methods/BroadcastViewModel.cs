using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class BroadcastViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(BroadcastCommand))]
    [ObservableProperty] 
    private string? _tx;

    public BroadcastViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
    }

    private bool CanBroadcast()
    {
        return Tx is not null
               && Tx.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBroadcast))]
    private async Task Broadcast()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcBroadcastResult>(job.RpcMethod, job.RpcServerUri);
        if (result is RpcBroadcastResult { Result: not null } rpcBroadcastResult)
        {
            return rpcBroadcastResult.Result?.ToViewModel(RpcService, NavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService);
        }

        return null;
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

        return new Job("broadcast", requestBody, rpcServerUri);
    }
}
