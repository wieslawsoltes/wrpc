using System.Linq;
using System.Windows.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Info;
using WasabiRpc.ViewModels.Methods.Adapters;

namespace WasabiRpc.ViewModels.Factories;

public static class InfoFactory
{
    public static AccountInfoViewModel ToViewModel(
        this AccountInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new AccountInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Name = info.Name,
            PublicKey = info.PublicKey,
            KeyPath = info.KeyPath,
        };
    }

    public static AddressInfoViewModel ToViewModel(
        this AddressInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new AddressInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Address = info.Address,
            KeyPath = info.KeyPath,
            Label = info.Label,
            PublicKey = info.PublicKey,
            ScriptPubKey = info.ScriptPubKey,
        };
    }

    public static PayInCoinjoinInfoViewModel ToViewModel(
        this PayInCoinjoinInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new PayInCoinjoinInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            PaymentId = info.PaymentId,
        };
    }

    public static BroadcastInfoViewModel ToViewModel(
        this BroadcastInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new BroadcastInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            TxId = info.TxId,
        };
    }

    public static BuildInfoViewModel ToViewModel(
        this BuildInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new BuildInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Tx = info.Tx,
        };
    }

    public static CancelTransactionInfoViewModel ToViewModel(
        this CancelTransactionInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new CancelTransactionInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Tx = info.Tx,
        };
    }

    public static CoinInfoViewModel ToViewModel(
        this CoinInfo info, 
        IRpcServiceViewModel rpcService,
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new CoinInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            TxId = info.TxId,
            Index = info.Index,
            Amount = info.Amount,
            AnonymityScore = info.AnonymityScore,
            Confirmed = info.Confirmed,
            Confirmations = info.Confirmations,
            KeyPath = info.KeyPath,
            Address = info.Address,
            SpentBy = info.SpentBy,
        };
    }

    public static CreateWalletInfoViewModel ToViewModel(
        this CreateWalletInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new CreateWalletInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Mnemonic = info.Mnemonic,
        };
    }

    public static ErrorInfoViewModel ToViewModel(
        this ErrorInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new ErrorInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Code = info.Code,
            Message = info.Message,
        };
    }

    public static GetFeeRatesInfoViewModel ToViewModel(
        this GetFeeRatesInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new GetFeeRatesInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            FeeRates = info.FeeRates?
                .ToDictionary(
                    x => x.Key, 
                    x => x.Value),
        };
    }

    public static GetHistoryInfoViewModel ToViewModelAdapter(
        this GetHistoryInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService,
        IBatchManager batchManager, 
        string? walletName, 
        ICommand refreshCommand)
    {
        return new GetHistoryInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            //Transactions = info.Transactions?
            //    .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
            //    .ToList(),
            Transactions = info.Transactions?
                .Select(x => new TransactionAdapterViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName, x.ToViewModel(rpcService, navigationService, detailsNavigationService)))
                .OrderByDescending(x => x.TransactionInfo.DateTime)
                .ToList(),
        };
    }

    public static KeyInfoViewModel ToViewModel(
        this KeyInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new KeyInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            FullKeyPath = info.FullKeyPath,
            Internal = info.Internal,
            KeyState = info.KeyState,
            Label = info.Label,
            ScriptPubKey = info.ScriptPubKey,
            PubKey = info.PubKey,
            PubKeyHash = info.PubKeyHash,
            Address = info.Address,
        };
    }

    public static ListCoinsInfoViewModel ToViewModel(
        this ListCoinsInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new ListCoinsInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            Coins = info.Coins?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
                .OrderByDescending(x => x.AnonymityScore)
                .ToList(),
        };
    }

    public static ListKeysInfoViewModel ToViewModel(
        this ListKeysInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new ListKeysInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            Keys = info.Keys?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
                .ToList(),
        };
    }

    public static ListUnspentCoinsInfoViewModel ToViewModelAdapter(
        this ListUnspentCoinsInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, 
        string? walletName, 
        ICommand refreshCommand)
    {
        return new ListUnspentCoinsInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            // Coins = info.Coins?
            //     .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
            //     .ToList(),
            Coins = info.Coins?
                .Select(x => new CoinAdapterViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName, x.ToViewModel(rpcService, navigationService, detailsNavigationService)))
                .OrderByDescending(x => x.CoinInfo.AnonymityScore)
                .ToList(),
        };
    }

    public static ListWalletsInfoViewModel ToViewModel(
        this ListWalletsInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new ListWalletsInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            Wallets = info.Wallets?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand))
                .OrderBy(x => x.WalletName)
                .ToList(),
        };
    }

    public static PeerInfoViewModel ToViewModel(
        this PeerInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new PeerInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            IsConnected = info.IsConnected,
            LastSeen = info.LastSeen,
            Endpoint = info.Endpoint,
            UserAgent = info.UserAgent,
        };
    }

    public static SendInfoViewModel ToViewModel(
        this SendInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new SendInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            TxId = info.TxId,
            Tx = info.Tx,
        };
    }

    public static SpeedUpTransactionInfoViewModel ToViewModel(
        this SpeedUpTransactionInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new SpeedUpTransactionInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Tx = info.Tx,
        };
    }

    public static StatusInfoViewModel ToViewModel(
        this StatusInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new StatusInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            TorStatus = info.TorStatus,
            OnionService = info.OnionService,
            BackendStatus = info.BackendStatus,
            BestBlockchainHeight = info.BestBlockchainHeight,
            BestBlockchainHash = info.BestBlockchainHash,
            FiltersCount = info.FiltersCount,
            FiltersLeft = info.FiltersLeft,
            Network = info.Network,
            ExchangeRate = info.ExchangeRate,
            Peers = info.Peers?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
                .ToList(),
        };
    }

    public static TransactionInfoViewModel ToViewModel(
        this TransactionInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new TransactionInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            DateTime = info.DateTime,
            Height = info.Height,
            Amount = info.Amount,
            Label = info.Label,
            Tx = info.Tx,
            IsLikelyCoinJoin = info.IsLikelyCoinJoin,
        };
    }

    public static WalletInfoViewModel ToViewModel(
        this WalletInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new WalletInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            WalletName = info.WalletName,
            WalletFile = info.WalletFile,
            State = info.State,
            MasterKeyFingerprint = info.MasterKeyFingerprint,
            Accounts = info.Accounts?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
                .ToList(),
            Balance = info.Balance,
            AnonScoreTarget = info.AnonScoreTarget,
            IsWatchOnly = info.IsWatchOnly,
            IsHardwareWallet = info.IsHardwareWallet,
            IsAutoCoinjoin = info.IsAutoCoinjoin,
            IsRedCoinIsolation = info.IsRedCoinIsolation,
            CoinjoinStatus = info.CoinjoinStatus,
        };
    }

    public static PaymentInCoinjoinStateInfoViewModel ToViewModel(
        this PaymentInCoinjoinStateInfo info, 
        IRpcServiceViewModel rpcService,
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new PaymentInCoinjoinStateInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Status = info.Status,
            Round = info.Round,
            TxId = info.TxId,
        };
    }

    public static PaymentInCoinjoinInfoViewModel ToViewModel(
        this PaymentInCoinjoinInfo info, 
        IRpcServiceViewModel rpcService,
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new PaymentInCoinjoinInfoViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Id = info.Id,
            Amount = info.Amount,
            Destination = info.Destination,
            State = info.State?.ToViewModel(rpcService, navigationService, detailsNavigationService),
            Address = info.Address,
        };
    }

    public static ListPaymentsInCoinjoinInfoViewModel ToViewModel(
        this ListPaymentsInCoinjoinInfo info, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
    {
        return new ListPaymentsInCoinjoinInfoViewModel(rpcService, navigationService, detailsNavigationService, refreshCommand)
        {
            Payments = info.Payments?
                .Select(x => x.ToViewModel(rpcService, navigationService, detailsNavigationService))
                .ToList(),
        };
    }
}
