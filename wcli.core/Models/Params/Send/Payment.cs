using System.Text.Json.Serialization;

namespace WasabiCli.Models.Params.Send;

public class Payment
{
    [JsonPropertyName("sendto")]
    public string? SendTo { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("subtractFee")]
    public bool SubtractFee { get; set; }
}
