using Gymly.Core.Helpers;

namespace Gymly.Core.Models;

public class Payment : BaseEntity
{
    [CustomColumn("enrollment_id")] 
    public long EnrollmentId { get; set; }

    [CustomColumn("payment_date")]
    public DateTime PaymentDate { get; set; }

    [CustomColumn("amount_paid")]
    public decimal Amount { get; set; }
}
