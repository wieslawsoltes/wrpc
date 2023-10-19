using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class CreateWalletInfo
{
    [JsonPropertyName("mnemonic")]
    public string? Mnemonic { get; set; }
}
