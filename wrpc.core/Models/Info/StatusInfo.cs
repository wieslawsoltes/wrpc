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

public class StatusInfo
{
    [JsonPropertyName("torStatus")]
    public string? TorStatus { get; set; }

    [JsonPropertyName("onionService")]
    public string? OnionService { get; set; }

    [JsonPropertyName("backendStatus")]
    public string? BackendStatus { get; set; }

    [JsonPropertyName("bestBlockchainHeight")]
    public string? BestBlockchainHeight { get; set; }

    [JsonPropertyName("bestBlockchainHash")]
    public string? BestBlockchainHash { get; set; }

    [JsonPropertyName("filtersCount")]
    public int FiltersCount { get; set; }

    [JsonPropertyName("filtersLeft")]
    public int FiltersLeft { get; set; }

    [JsonPropertyName("network")]
    public string? Network { get; set; }

    [JsonPropertyName("exchangeRate")]
    public decimal ExchangeRate { get; set; }

    [JsonPropertyName("peers")]
    public List<PeerInfo>? Peers { get; set; }
}
