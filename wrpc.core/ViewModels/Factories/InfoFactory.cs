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
    public static AccountInfoViewModel ToViewModel(this AccountInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new AccountInfoViewModel(rpcService, navigationService)
        {
            Name = info.Name,
            PublicKey = info.PublicKey,
            KeyPath = info.KeyPath,
        };
    }

    public static AddressInfoViewModel ToViewModel(this AddressInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new AddressInfoViewModel(rpcService, navigationService)
        {
            Address = info.Address,
            KeyPath = info.KeyPath,
            Label = info.Label,
            PublicKey = info.PublicKey,
            ScriptPubKey = info.ScriptPubKey,
        };
    }

    public static PayInCoinjoinInfoViewModel ToViewModel(this PayInCoinjoinInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new PayInCoinjoinInfoViewModel(rpcService, navigationService)
        {
            PaymentId = info.PaymentId,
        };
    }

    public static BroadcastInfoViewModel ToViewModel(this BroadcastInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new BroadcastInfoViewModel(rpcService, navigationService)
        {
            TxId = info.TxId,
        };
    }

    public static BuildInfoViewModel ToViewModel(this BuildInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new BuildInfoViewModel(rpcService, navigationService)
        {
            Tx = info.Tx,
        };
    }

    public static CancelTransactionInfoViewModel ToViewModel(this CancelTransactionInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new CancelTransactionInfoViewModel(rpcService, navigationService)
        {
            Tx = info.Tx,
        };
    }

    public static CoinInfoViewModel ToViewModel(this CoinInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new CoinInfoViewModel(rpcService, navigationService)
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

    public static CreateWalletInfoViewModel ToViewModel(this CreateWalletInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new CreateWalletInfoViewModel(rpcService, navigationService)
        {
            Mnemonic = info.Mnemonic,
        };
    }

    public static ErrorInfoViewModel ToViewModel(this ErrorInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new ErrorInfoViewModel(rpcService, navigationService)
        {
            Code = info.Code,
            Message = info.Message,
        };
    }

    public static GetFeeRatesInfoViewModel ToViewModel(this GetFeeRatesInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new GetFeeRatesInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            FeeRates = info.FeeRates?
                .ToDictionary(
                    x => x.Key, 
                    x => x.Value),
        };
    }

    public static GetHistoryInfoViewModel ToViewModelAdapter(this GetHistoryInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName, ICommand refreshCommand)
    {
        return new GetHistoryInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            //Transactions = info.Transactions?
            //    .Select(x => x.ToViewModel(rpcService, navigationService))
            //    .ToList(),
            Transactions = info.Transactions?
                .Select(x => new TransactionAdapterViewModel(rpcService, navigationService, batchManager, walletName, x.ToViewModel(rpcService, navigationService)))
                .OrderByDescending(x => x.TransactionInfo.DateTime)
                .ToList(),
        };
    }

    public static KeyInfoViewModel ToViewModel(this KeyInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new KeyInfoViewModel(rpcService, navigationService)
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

    public static ListCoinsInfoViewModel ToViewModel(this ListCoinsInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new ListCoinsInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            Coins = info.Coins?
                .Select(x => x.ToViewModel(rpcService, navigationService))
                .OrderByDescending(x => x.AnonymityScore)
                .ToList(),
        };
    }

    public static ListKeysInfoViewModel ToViewModel(this ListKeysInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new ListKeysInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            Keys = info.Keys?
                .Select(x => x.ToViewModel(rpcService, navigationService))
                .ToList(),
        };
    }

    public static ListUnspentCoinsInfoViewModel ToViewModelAdapter(this ListUnspentCoinsInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName, ICommand refreshCommand)
    {
        return new ListUnspentCoinsInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            // Coins = info.Coins?
            //     .Select(x => x.ToViewModel(rpcService, navigationService))
            //     .ToList(),
            Coins = info.Coins?
                .Select(x => new CoinAdapterViewModel(rpcService, navigationService, batchManager, walletName, x.ToViewModel(rpcService, navigationService)))
                .OrderByDescending(x => x.CoinInfo.AnonymityScore)
                .ToList(),
        };
    }

    public static ListWalletsInfoViewModel ToViewModel(this ListWalletsInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new ListWalletsInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            Wallets = info.Wallets?
                .Select(x => x.ToViewModel(rpcService, navigationService, refreshCommand))
                .OrderBy(x => x.WalletName)
                .ToList(),
        };
    }

    public static PeerInfoViewModel ToViewModel(this PeerInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new PeerInfoViewModel(rpcService, navigationService)
        {
            IsConnected = info.IsConnected,
            LastSeen = info.LastSeen,
            Endpoint = info.Endpoint,
            UserAgent = info.UserAgent,
        };
    }

    public static SendInfoViewModel ToViewModel(this SendInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new SendInfoViewModel(rpcService, navigationService)
        {
            TxId = info.TxId,
            Tx = info.Tx,
        };
    }

    public static SpeedUpTransactionInfoViewModel ToViewModel(this SpeedUpTransactionInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new SpeedUpTransactionInfoViewModel(rpcService, navigationService)
        {
            Tx = info.Tx,
        };
    }

    public static StatusInfoViewModel ToViewModel(this StatusInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new StatusInfoViewModel(rpcService, navigationService, refreshCommand)
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
                .Select(x => x.ToViewModel(rpcService, navigationService))
                .ToList(),
        };
    }

    public static TransactionInfoViewModel ToViewModel(this TransactionInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new TransactionInfoViewModel(rpcService, navigationService)
        {
            DateTime = info.DateTime,
            Height = info.Height,
            Amount = info.Amount,
            Label = info.Label,
            Tx = info.Tx,
            IsLikelyCoinJoin = info.IsLikelyCoinJoin,
        };
    }

    public static WalletInfoViewModel ToViewModel(this WalletInfo info, IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
    {
        return new WalletInfoViewModel(rpcService, navigationService, refreshCommand)
        {
            WalletName = info.WalletName,
            WalletFile = info.WalletFile,
            State = info.State,
            MasterKeyFingerprint = info.MasterKeyFingerprint,
            Accounts = info.Accounts?
                .Select(x => x.ToViewModel(rpcService, navigationService))
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
}
