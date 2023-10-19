using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

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
