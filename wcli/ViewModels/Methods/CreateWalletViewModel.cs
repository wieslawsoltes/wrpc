using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels.Methods;

public partial class CreateWalletViewModel : BatchMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CreateWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] private string? _walletPassword;

    public CreateWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = "Wallet";
        WalletPassword = "";
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanCreateWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCreateWallet))]
    private async Task CreateWallet()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job, ModelsJsonContext.Default.RpcCreateWalletResult);
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
            NavigationService.Clear();
            NavigationService.Navigate(new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result });
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
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
