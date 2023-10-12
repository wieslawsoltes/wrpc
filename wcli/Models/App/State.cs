using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.App;

public class State
{
    [JsonPropertyName("wallets")]
    public List<string>? Wallets { get; set; }

    [JsonPropertyName("selectedWallet")]
    public string? SelectedWallet { get; set; }

    [JsonPropertyName("batches")]
    public List<Batch>? Batches { get; set; }
}
