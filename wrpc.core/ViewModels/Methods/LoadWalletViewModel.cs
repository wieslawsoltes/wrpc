using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class LoadWalletViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public LoadWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task LoadWallet()
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
        var result = await RpcService.Send<RpcLoadWalletResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcLoadWalletResult rpcLoadWalletResult)
        {
            OnRpcSuccess(rpcLoadWalletResult);
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
        var successViewModel = new Success { Message = $"Loaded wallet {WalletName}" }.ToViewModel(RpcService, NavigationService);
        NavigationService.NavigateTo(successViewModel);
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "loadwallet",
            Params = new []
            {
                WalletName,
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("loadwallet", requestBody, rpcServerUri, typeof(RpcLoadWalletResult));
    }
}
