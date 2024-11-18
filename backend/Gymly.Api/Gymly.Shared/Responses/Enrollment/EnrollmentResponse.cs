using Newtonsoft.Json;

namespace Gymly.Shared.Responses.Enrollment;

public class EnrollmentResponse
{
    [JsonProperty("class_id")]
    public long ClassId { get; set; }

    [JsonProperty("member")]
    public MemberResponse Member { get; set; }

    [JsonProperty("payment")]
    public PaymentResponse? Payment { get; set; }
}
