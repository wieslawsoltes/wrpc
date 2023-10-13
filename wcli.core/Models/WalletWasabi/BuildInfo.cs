using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class BuildInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
