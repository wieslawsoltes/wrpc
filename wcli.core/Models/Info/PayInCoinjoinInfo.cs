using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class PayInCoinjoinInfo
{
    [JsonPropertyName("paymentId")]
    public string? PaymentId { get; set; }
}
