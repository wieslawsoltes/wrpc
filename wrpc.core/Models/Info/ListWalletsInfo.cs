using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class ListWalletsInfo
{
    [JsonPropertyName("wallets")]
    public List<WalletInfo>? Wallets { get; set; }
}
