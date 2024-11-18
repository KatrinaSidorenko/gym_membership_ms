using Gymly.Core.Constants;

namespace Gymly.Core.Models.Users;

public class Identity : BaseEntity
{
    public string Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
