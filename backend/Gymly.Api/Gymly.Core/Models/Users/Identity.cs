using Gymly.Core.Constants;
using Gymly.Core.Helpers;

namespace Gymly.Core.Models.Users;

public class Identity
{
    public long Id { get; set; }
    public virtual long GetId() => Id;
    public string Role { get; set; }

    [CustomColumn("name")]
    public string Name { get; set; }

    [CustomColumn("email")]
    public string Email { get; set; }

    [CustomColumn("phone_number")]
    public string Phone { get; set; }

    [CustomColumn("password")]
    public string Password { get; set; }
}
