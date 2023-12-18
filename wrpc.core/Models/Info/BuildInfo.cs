using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class BuildInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
