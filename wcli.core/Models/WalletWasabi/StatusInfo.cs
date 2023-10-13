using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class StatusInfo
{
    [JsonPropertyName("torStatus")]
    public string? TorStatus { get; set; }

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
