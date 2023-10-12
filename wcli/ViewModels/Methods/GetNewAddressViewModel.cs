using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class GetNewAddressViewModel : BatchMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [ObservableProperty] 
    private string? _label;

    public GetNewAddressViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        Label = "Label";
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanGetNewAddress()
    {
        return Label is not null 
               && Label.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private async Task GetNewAddress()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcGetNewAddressResult);
        if (result is RpcGetNewAddressResult { Result: not null } rpcGetNewAddressResult)
        {
            OnRpcSuccess(rpcGetNewAddressResult);
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
        if (rpcResult is RpcGetNewAddressResult rpcGetNewAddressResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(rpcGetNewAddressResult.Result);
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
            Method = "getnewaddress",
            Params = new []
            {
                Label
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
