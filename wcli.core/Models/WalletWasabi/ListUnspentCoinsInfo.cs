using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class ListUnspentCoinsInfo
{
    [JsonPropertyName("coins")]
    public List<CoinInfo>? Coins { get; set; }
}
