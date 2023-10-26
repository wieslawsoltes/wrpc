using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetHistoryViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public GetHistoryViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task GetHistory()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        await Execute(job);
    }

    public override async Task Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetHistoryResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcGetHistoryResult { Result: not null } rpcGetHistoryResult)
        {
            OnRpcSuccess(rpcGetHistoryResult);
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
        if (rpcResult is RpcGetHistoryResult rpcGetHistoryResult && WalletName is not null)
        {
            NavigationService.NavigateTo(new GetHistoryInfo { Transactions = rpcGetHistoryResult.Result }.ToViewModelAdapter(RpcService, NavigationService, BatchManager, WalletName));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "gethistory"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("gethistory", requestBody, rpcServerUri, typeof(RpcGetHistoryResult));
    }
}
