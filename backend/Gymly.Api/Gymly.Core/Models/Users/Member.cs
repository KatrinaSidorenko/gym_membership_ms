using Gymly.Core.Helpers;

namespace Gymly.Core.Models.Users;

public class Member : Identity 
{
    [CustomColumn("member_id")]
    public long MemberId { get; set; }

    public override long GetId() => MemberId;
}
