using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels.Methods;

public partial class GetHistoryViewModel : BatchMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public GetHistoryViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task GetHistory()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcGetHistoryResult);
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
        if (rpcResult is RpcGetHistoryResult rpcGetHistoryResult)
        {
            NavigationService.Navigate(new GetHistoryInfo { Transactions = rpcGetHistoryResult.Result });
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
            Method = "gethistory"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
