using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class BuildInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
