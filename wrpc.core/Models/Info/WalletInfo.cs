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
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class WalletInfo
{
    [JsonPropertyName("walletName")]
    public string? WalletName { get; set; }

    [JsonPropertyName("walletFile")]
    public string? WalletFile { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }
   
    [JsonPropertyName("masterKeyFingerprint")]
    public string? MasterKeyFingerprint { get; set; }

    [JsonPropertyName("accounts")]
    public List<AccountInfo>? Accounts { get; set; }

    [JsonPropertyName("balance")]
    public long Balance { get; set; }

    [JsonPropertyName("anonScoreTarget")]
    public int AnonScoreTarget { get; set; }

    [JsonPropertyName("isWatchOnly")]
    public bool IsWatchOnly { get; set; }

    [JsonPropertyName("isHardwareWallet")]
    public bool IsHardwareWallet { get; set; }

    [JsonPropertyName("isAutoCoinjoin")]
    public bool IsAutoCoinjoin { get; set; }

    [JsonPropertyName("isRedCoinIsolation")]
    public bool IsRedCoinIsolation { get; set; }

    [JsonPropertyName("coinjoinStatus")]
    public string? CoinjoinStatus { get; set; }
}
