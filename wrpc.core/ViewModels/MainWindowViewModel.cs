﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods;

namespace WasabiRpc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private IRpcServiceViewModel _rpcService;

    [NotifyCanExecuteChangedFor(nameof(AddWalletCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveWalletCommand))]
    [ObservableProperty]
    private ObservableCollection<WalletViewModel>? _wallets;

    [NotifyCanExecuteChangedFor(nameof(AddWalletCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveWalletCommand))]
    [NotifyCanExecuteChangedFor(nameof(LoadWalletCommand))]
    [NotifyCanExecuteChangedFor(nameof(ListCoinsCommand))]
    [NotifyCanExecuteChangedFor(nameof(ListUnspentCoinsCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetWalletInfoCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [NotifyCanExecuteChangedFor(nameof(SpeedUpTransactionCommand))]
    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetHistoryCommand))]
    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinjoinCommand))]
    [NotifyCanExecuteChangedFor(nameof(ListKeysCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartCoinJoinCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartCoinJoinSweepCommand))]
    [NotifyCanExecuteChangedFor(nameof(StopCoinJoinCommand))]
    [ObservableProperty] 
    private WalletViewModel? _selectedWallet;

    [ObservableProperty] private ObservableCollection<RpcMethodViewModel>? _rpcMethods;

    public MainWindowViewModel(INavigationService navigationService, IRpcServiceViewModel rpcService, State state)
    {
        RpcService = rpcService;
        NavigationService = navigationService;

        var wallets = 
            state.Wallets?.Select(x => new WalletViewModel { WalletName = x }) ?? new List<WalletViewModel>();

        Wallets = new ObservableCollection<WalletViewModel>(wallets);

        SelectedWallet = !string.IsNullOrEmpty(state.SelectedWallet)
            ? Wallets.FirstOrDefault(x => x.WalletName == state.SelectedWallet) 
            : Wallets.FirstOrDefault();

        RpcMethods = new ObservableCollection<RpcMethodViewModel>
        {
            new ("GetStatus", GetStatusCommand),
            new ("CreateWallet", CreateWalletCommand),
            new ("RecoverWallet", RecoverWalletCommand),
            new ("LoadWallet", LoadWalletCommand),
            new ("ListCoins", ListCoinsCommand),
            new ("ListUnspentCoins", ListUnspentCoinsCommand),
            new ("GetWalletInfo", GetWalletInfoCommand),
            new ("GetNewAddress", GetNewAddressCommand),
            new ("Send", SendCommand),
            new ("SpeedUpTransaction", SpeedUpTransactionCommand),
            new ("CancelTransaction", CancelTransactionCommand),
            new ("Build", BuildCommand),
            new ("PayInCoinjoin", PayInCoinjoinCommand),
            new ("Broadcast", BroadcastCommand),
            new ("GetHistory", GetHistoryCommand),
            new ("ExcludeFromCoinjoin", ExcludeFromCoinjoinCommand),
            new ("ListKeys", ListKeysCommand),
            new ("StartCoinJoin", StartCoinJoinCommand),
            new ("StartCoinJoinSweep", StartCoinJoinSweepCommand),
            new ("StopCoinJoin", StopCoinJoinCommand),
            new ("GetFeeRates", GetFeeRatesCommand),
            new ("ListWallets", ListWalletsCommand),
            new ("Stop", StopCommand)
        };
    }

    public INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task LoadWallets()
    {
        var listWalletsViewModel = new ListWalletsViewModel(RpcService, NavigationService);
        var job = listWalletsViewModel.CreateJob();
        var result = await RpcService.Send<RpcListWalletsResult>(job, NavigationService);
        if (result is RpcListWalletsResult { Result: not null } rpcListWalletsResult)
        {
            var wallets = rpcListWalletsResult
                .Result
                .Select(x => new WalletViewModel { WalletName = x.WalletName })
                .OrderBy(x => x.WalletName);

            if (Wallets is not null)
            {
                Wallets = new ObservableCollection<WalletViewModel>(wallets);
                SelectedWallet = Wallets.FirstOrDefault();
            }
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.NavigateTo(rpcErrorResult.Error.ToViewModel(RpcService, NavigationService));
        }
        else if (result is Error error)
        {
            NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
        }
    }

    private bool CanAddWallet()
    {
        return Wallets is not null;
    }

    [RelayCommand(CanExecute = nameof(CanAddWallet))]
    private void AddWallet()
    {
        if (Wallets is not null)
        {
            Wallets.Add(new WalletViewModel { WalletName = "Wallet" });
            SelectedWallet = Wallets.LastOrDefault();
        }
    }

    private bool CanRemoveWallet()
    {
        return Wallets is not null 
               && SelectedWallet is not null;
    }

    [RelayCommand(CanExecute = nameof(CanRemoveWallet))]
    private void RemoveWallet()
    {
        if (Wallets is not null && SelectedWallet is not null)
        {
            Wallets.Remove(SelectedWallet);
            SelectedWallet = Wallets.FirstOrDefault();
        }
    }

    [RelayCommand]
    private async Task GetStatus()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var getStatusViewModel = new GetStatusViewModel(RpcService, NavigationService);
            await getStatusViewModel.GetStatusCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand]
    private void CreateWallet()
    {
        var createWalletViewModel = new CreateWalletViewModel(RpcService, NavigationService);
        NavigationService.NavigateTo(createWalletViewModel);
    }

    [RelayCommand]
    private void RecoverWallet()
    {
        var recoverWalletViewModel = new RecoverWalletViewModel(RpcService, NavigationService);
        NavigationService.NavigateTo(recoverWalletViewModel);
    }

    private bool CanLoadWallet()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanLoadWallet))]
    private async Task LoadWallet()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var loadWalletViewModel = new LoadWalletViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await loadWalletViewModel.LoadWalletCommand.ExecuteAsync(null);
        }
    }

    private bool CanListCoins()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListCoins))]
    private async Task ListCoins()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var listCoinsViewModel = new ListCoinsViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await listCoinsViewModel.ListCoinsCommand.ExecuteAsync(null);
        }
    }

    private bool CanListUnspentCoins()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListUnspentCoins))]
    private async Task ListUnspentCoins()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var listUnspentCoinsViewModel = new ListUnspentCoinsViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await listUnspentCoinsViewModel.ListUnspentCoinsCommand.ExecuteAsync(null);
        }
    }

    private bool CanGetWalletInfo()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetWalletInfo))]
    private async Task GetWalletInfo()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var getWalletInfoViewModel = new GetWalletInfoViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await getWalletInfoViewModel.GetWalletInfoCommand.ExecuteAsync(null);
        }
    }

    private bool CanGetNewAddress()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private void GetNewAddress()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var getNewAddressViewModel = new GetNewAddressViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(getNewAddressViewModel);
        }
    }

    private bool CanSend()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSend))]
    private void Send()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var sendViewModel = new SendViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(sendViewModel);
        }
    }

    private bool CanSpeedUpTransaction()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSpeedUpTransaction))]
    private void SpeedUpTransaction()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var speedUpTransactionViewModel = new SpeedUpTransactionViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(speedUpTransactionViewModel);
        }
    }

    private bool CanCancelTransaction()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCancelTransaction))]
    private void CancelTransaction()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var cancelTransactionViewModel = new CancelTransactionViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(cancelTransactionViewModel);
        }
    }

    private bool CanBuild()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBuild))]
    private void Build()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var buildViewModel = new BuildViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(buildViewModel);
        }
    }

    private bool CanPayInCoinjoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanPayInCoinjoin))]
    private void PayInCoinjoin()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var payInCoinjoinViewModel = new PayInCoinjoinViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(payInCoinjoinViewModel);
        }
    }

    [RelayCommand]
    private void Broadcast()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var broadcastViewModel = new BroadcastViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(broadcastViewModel);
        }
    }

    private bool CanGetHistory()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetHistory))]
    private async Task GetHistory()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var getHistoryViewModel = new GetHistoryViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await getHistoryViewModel.GetHistoryCommand.ExecuteAsync(null);
        }
    }

    private bool CanCanExcludeFromCoinjoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCanExcludeFromCoinjoin))]
    private void ExcludeFromCoinjoin()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var excludeFromCoinJoinViewModel = new ExcludeFromCoinJoinViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(excludeFromCoinJoinViewModel);
        }
    }

    private bool CanListKeys()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListKeys))]
    private async Task ListKeys()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var listKeysViewModel = new ListKeysViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await listKeysViewModel.ListKeysCommand.ExecuteAsync(null);
        }
    }

    private bool CanStartCoinJoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanStartCoinJoin))]
    private void StartCoinJoin()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var startCoinJoinViewModel = new StartCoinJoinViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(startCoinJoinViewModel);
        }
    }
    private bool CanStartCoinJoinSweep()
    {
        return SelectedWallet?.WalletName is not null
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanStartCoinJoinSweep))]
    private void StartCoinJoinSweep()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var startCoinJoinSweepViewModel = new StartCoinJoinSweepViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            NavigationService.NavigateTo(startCoinJoinSweepViewModel);
        }
    }

    private bool CanStopCoinJoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanStopCoinJoin))]
    private async Task StopCoinJoin()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var stopCoinJoinViewModel = new StopCoinJoinViewModel(RpcService, NavigationService, SelectedWallet.WalletName);
            await stopCoinJoinViewModel.StopCoinJoinCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand]
    private async Task GetFeeRates()
    {
        var getFeeRatesViewModel = new GetFeeRatesViewModel(RpcService, NavigationService);
        await getFeeRatesViewModel.GetFeeRatesCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task ListWallets()
    {
        var listWalletsViewModel = new ListWalletsViewModel(RpcService, NavigationService);
        await listWalletsViewModel.ListWalletsCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task Stop()
    {
        var stopViewModel = new StopViewModel(RpcService, NavigationService);
        await stopViewModel.StopCommand.ExecuteAsync(null);
    }
}
