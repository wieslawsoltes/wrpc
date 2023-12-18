using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class CreateWalletInfo
{
    [JsonPropertyName("mnemonic")]
    public string? Mnemonic { get; set; }
}
