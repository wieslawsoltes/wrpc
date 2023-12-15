/*
 * wrpc A Graphical User Interface for using the Wasabi Wallet RPC.
 * Copyright (C) 2023  Wiesław Šoltés
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;
using WasabiRpc.ViewModels.Methods;

namespace WasabiRpc.ViewModels;

public partial class MainWindowViewModel : RoutableViewModel
{
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

    public MainWindowViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService,
        IBatchManager batchManager, 
        State state)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        BatchManager = batchManager;

        var wallets = 
            state.Wallets?.Select(x => new WalletViewModel { WalletName = x }) ?? new List<WalletViewModel>();

        Wallets = new ObservableCollection<WalletViewModel>(wallets);

        SelectedWallet = !string.IsNullOrEmpty(state.SelectedWallet)
            ? Wallets.FirstOrDefault(x => x.WalletName == state.SelectedWallet) 
            : Wallets.FirstOrDefault();

        RpcMethods = new ObservableCollection<RpcMethodViewModel>
        {
            // Backend
            new ("GetStatus", GetStatusCommand),
            new ("Stop", StopCommand),
            // Wallet
            new ("CreateWallet", CreateWalletCommand),
            new ("RecoverWallet", RecoverWalletCommand),
            new ("LoadWallet", LoadWalletCommand),
            // Info
            new ("GetWalletInfo", GetWalletInfoCommand),
            new ("ListWallets", ListWalletsCommand),
            new ("ListCoins", ListCoinsCommand),
            new ("ListUnspentCoins", ListUnspentCoinsCommand),
            new ("GetHistory", GetHistoryCommand),
            new ("ListKeys", ListKeysCommand),
            new ("GetFeeRates", GetFeeRatesCommand),
            // Receive
            new ("GetNewAddress", GetNewAddressCommand),
            // Send
            new ("Send", SendCommand),
            new ("Build", BuildCommand),
            new ("Broadcast", BroadcastCommand),
            new ("PayInCoinjoin", PayInCoinjoinCommand),
            new ("SpeedUpTransaction", SpeedUpTransactionCommand),
            new ("CancelTransaction", CancelTransactionCommand),
            // Coinjoin
            new ("StartCoinJoin", StartCoinJoinCommand),
            new ("StartCoinJoinSweep", StartCoinJoinSweepCommand),
            new ("StopCoinJoin", StopCoinJoinCommand),
            new ("ExcludeFromCoinjoin", ExcludeFromCoinjoinCommand),
        };
    }

    public IBatchManager BatchManager { get; }

    [RelayCommand]
    private void ShowBatchManager()
    {
        DetailsNavigationService.Clear();
        NavigationService.NavigateTo(BatchManager);
    }

    [RelayCommand]
    private async Task LoadWallets()
    {
        var listWalletsViewModel = new ListWalletsViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        var job = listWalletsViewModel.CreateJob();
        var routable = await listWalletsViewModel.Execute(job);
        if (routable is ListWalletsInfoViewModel listWalletsInfoViewModel)
        {
            if (listWalletsInfoViewModel.Wallets is not null)
            {
                var wallets = listWalletsInfoViewModel.Wallets.Select(x => new WalletViewModel { WalletName = x.WalletName });
                Wallets = new ObservableCollection<WalletViewModel>(wallets);
                SelectedWallet = Wallets.FirstOrDefault();
            }
        }
        else if (routable is ErrorInfoViewModel errorInfoViewModel)
        {
            DetailsNavigationService.Clear();
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (routable is ErrorViewModel errorViewModel)
        {
            DetailsNavigationService.Clear();
            NavigationService.NavigateTo(errorViewModel);
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
            var getStatusViewModel = new GetStatusViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
            await getStatusViewModel.GetStatusCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand]
    private void CreateWallet()
    {
        var createWalletViewModel = new CreateWalletViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        DetailsNavigationService.Clear();
        NavigationService.NavigateTo(createWalletViewModel);
    }

    [RelayCommand]
    private void RecoverWallet()
    {
        var recoverWalletViewModel = new RecoverWalletViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        DetailsNavigationService.Clear();
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
            var loadWalletViewModel = new LoadWalletViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var listCoinsViewModel = new ListCoinsViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var listUnspentCoinsViewModel = new ListUnspentCoinsViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var getWalletInfoViewModel = new GetWalletInfoViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var getNewAddressViewModel = new GetNewAddressViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var sendViewModel = new SendViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var speedUpTransactionViewModel = new SpeedUpTransactionViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var cancelTransactionViewModel = new CancelTransactionViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var buildViewModel = new BuildViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var payInCoinjoinViewModel = new PayInCoinjoinViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
            NavigationService.NavigateTo(payInCoinjoinViewModel);
        }
    }

    [RelayCommand]
    private void Broadcast()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            var broadcastViewModel = new BroadcastViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
            DetailsNavigationService.Clear();
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
            var getHistoryViewModel = new GetHistoryViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var excludeFromCoinJoinViewModel = new ExcludeFromCoinJoinViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var listKeysViewModel = new ListKeysViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
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
            var startCoinJoinViewModel = new StartCoinJoinViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var startCoinJoinSweepViewModel = new StartCoinJoinSweepViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            DetailsNavigationService.Clear();
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
            var stopCoinJoinViewModel = new StopCoinJoinViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, SelectedWallet.WalletName);
            await stopCoinJoinViewModel.StopCoinJoinCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand]
    private async Task GetFeeRates()
    {
        var getFeeRatesViewModel = new GetFeeRatesViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        await getFeeRatesViewModel.GetFeeRatesCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task ListWallets()
    {
        var listWalletsViewModel = new ListWalletsViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        await listWalletsViewModel.ListWalletsCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task Stop()
    {
        var stopViewModel = new StopViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager);
        await stopViewModel.StopCommand.ExecuteAsync(null);
    }
}
