using Newtonsoft.Json;

namespace Gymly.Shared.Responses.Auth;

public class UserResponse
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }
}
