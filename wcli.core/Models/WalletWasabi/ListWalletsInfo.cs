using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class ListWalletsInfo
{
    [JsonPropertyName("wallets")]
    public List<WalletInfo>? Wallets { get; set; }
}
