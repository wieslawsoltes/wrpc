using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class SpeedUpTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
