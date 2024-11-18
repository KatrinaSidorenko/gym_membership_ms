using Gymly.Core.Helpers;
using Gymly.Core.Models.Users;

namespace Gymly.Core.Models;

public class Enrollment : BaseEntity
{
    [CustomColumn("class_id")]
    public long ClassId { get; set; }

    [CustomColumn("member_id")]
    public long MemberId { get; set; }

    [CustomColumn("enrollment_date")]
    public DateTime EnrollmentDate { get; set; }

    public Member Member { get; set; }
    public SportClass SportClass { get; set; }
    public Payment Payment { get; set; }
}
