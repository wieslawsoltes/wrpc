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

public partial class CreateWalletViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CreateWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] private string? _walletPassword;

    public CreateWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = "Wallet";
        WalletPassword = "";
    }

    private bool CanCreateWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCreateWallet))]
    private async Task CreateWallet()
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
        var result = await RpcService.Send<RpcCreateWalletResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcCreateWalletResult { Result: not null } rpcCreateWalletResult)
        {
            OnRpcSuccess(rpcCreateWalletResult);
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
        if (rpcResult is RpcCreateWalletResult rpcCreateWalletResult)
        {
            var createWalletInfoViewModel = new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result }.ToViewModel(RpcService, NavigationService);
            NavigationService.ClearAndNavigateTo(createWalletInfoViewModel);
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("createwallet", requestBody, rpcServerUri, typeof(RpcCreateWalletResult));
    }
}
