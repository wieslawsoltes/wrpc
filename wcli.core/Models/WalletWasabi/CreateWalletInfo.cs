using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class CreateWalletInfo
{
    [JsonPropertyName("mnemonic")]
    public string? Mnemonic { get; set; }
}
