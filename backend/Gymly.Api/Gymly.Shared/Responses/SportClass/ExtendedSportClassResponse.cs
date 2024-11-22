using Newtonsoft.Json;

namespace Gymly.Shared.Responses.SportClass;

public class ExtendedSportClassResponse
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("instructorName")]
    public string InstructorName { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("paidEnrollments")]
    public decimal PaidEnrollments { get; set; }
}
