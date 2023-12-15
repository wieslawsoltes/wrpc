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
using System;
using System.Collections.Generic;
using WasabiRpc.Models.Rpc.Results;

namespace WasabiRpc.ViewModels.BatchMode;

public static class RpcMethodResultTypeRegistry
{
    public static Dictionary<string, Type> Results { get; }

    static RpcMethodResultTypeRegistry()
    {
        Results = new ()
        {
            ["broadcast"] = typeof(RpcBroadcastResult),
            ["build"] = typeof(RpcBuildResult),
            ["canceltransaction"] = typeof(RpcCancelTransactionResult),
            ["createwallet"] = typeof(RpcCreateWalletResult),
            ["excludefromcoinjoin"] = typeof(RpcExcludeFromCoinJoinResult),
            ["getfeerates"] = typeof(RpcGetFeeRatesResult),
            ["gethistory"] = typeof(RpcGetHistoryResult),
            ["getnewaddress"] = typeof(RpcGetNewAddressResult),
            ["getstatus"] = typeof(RpcGetStatusResult),
            ["getwalletinfo"] = typeof(RpcGetWalletInfoResult),
            ["listcoins"] = typeof(RpcListCoinsResult),
            ["listkeys"] = typeof(RpcListKeysResult),
            ["listunspentcoins"] = typeof(RpcListUnspentCoinsResult),
            ["listwallets"] = typeof(RpcListWalletsResult),
            ["loadwallet"] = typeof(RpcLoadWalletResult),
            ["payincoinjoin"] = typeof(RpcPayInCoinjoinResult),
            ["recoverwallet"] = typeof(RpcRecoverWalletResult),
            ["send"] = typeof(RpcSendResult),
            ["speeduptransaction"] = typeof(RpcSpeedUpTransactionResult),
            ["startcoinjoinsweep"] = typeof(RpcStartCoinJoinSweepResult),
            ["startcoinjoin"] = typeof(RpcStartCoinJoinResult),
            ["stopcoinjoin"] = typeof(RpcStopCoinJoinResult),
            ["stop"] = typeof(string),
        };
    }
}
