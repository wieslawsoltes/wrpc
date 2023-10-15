using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

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

    [JsonPropertyName("coinjoinStatus")]
    public string? CoinJoinStatus { get; set; }
    
}
