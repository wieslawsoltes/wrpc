using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels.Methods;

public partial class GetFeeRatesViewModel : BatchMethodViewModel
{
    public GetFeeRatesViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task GetFeeRates()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcGetFeeRatesResult);
        if (result is RpcGetFeeRatesResult { Result: not null } rpcGetFeeRatesResult)
        {
            OnRpcSuccess(rpcGetFeeRatesResult);
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
        if (rpcResult is RpcGetFeeRatesResult rpcGetFeeRatesResult)
        {
            NavigationService.Navigate(new GetFeeRatesInfo { FeeRates = rpcGetFeeRatesResult.Result });
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
            Method = "getfeerates"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
