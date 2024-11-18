using Gymly.Core.Helpers;
using Newtonsoft.Json;

namespace Gymly.Shared.Requests.SportClass;

public class CreateSportClassRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("instructor_name")]
    public string InstructorName { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}
