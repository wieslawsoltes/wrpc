using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class ListCoinsInfo
{
    [JsonPropertyName("coins")]
    public List<CoinInfo>? Coins { get; set; }
}
