using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymly.Shared.Requests.Enrollment;

public class CreateEnrollmentRequest
{
    public long MemberId { get; set; }
    public long ClassId { get; set; }
}
