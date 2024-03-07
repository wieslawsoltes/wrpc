using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class ListPaymentsInCoinjoinInfo
{
    [JsonPropertyName("payments")]
    public List<PaymentInCoinjoinInfo>? Payments { get; set; }
}
