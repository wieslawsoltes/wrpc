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
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Params.Transactions;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class CancelTransactionViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty]
    private string? _walletPassword;

    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty]
    private string? _txId;

    public CancelTransactionViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private bool CanCancelTransaction()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletPassword is not null
               && TxId is not null 
               && TxId.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCancelTransaction))]
    private async Task CancelTransaction()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcCancelTransactionResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcCancelTransactionResult { Result: not null } rpcCancelTransactionResult)
        {
            return new BuildInfo { Tx = rpcCancelTransactionResult.Result }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new CancelTransactionRpcMethod
        {
            Method = "canceltransaction",
            Params = new CancelTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("canceltransaction", requestBody, rpcServerUri);
    }
}
