using System;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class TransactionInfo
{
    [JsonPropertyName("datetime")]
    public DateTimeOffset DateTime { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("tx")]
    public string? Tx { get; set; }

    [JsonPropertyName("islikelycoinjoin")]
    public bool IsLikelyCoinJoin { get; set; }
}
