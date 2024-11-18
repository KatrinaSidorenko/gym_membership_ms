using Newtonsoft.Json;

namespace Gymly.Shared.Requests.Payment;

public class CreatePaymentRequest
{
    [JsonProperty("enrollment_id")]
    public long EnrollmentId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
