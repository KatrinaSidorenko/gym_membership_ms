using Gymly.Core.Helpers;

namespace Gymly.Core.Models;

public class Payment
{
    [CustomColumn("payment_id")]
    public long PaymentId { get; set; }

    [CustomColumn("enrollment_id")] 
    public long EnrollmentId { get; set; }

    [CustomColumn("payment_date")]
    public DateTime PaymentDate { get; set; }

    [CustomColumn("amount_paid")]
    public decimal Amount { get; set; }
}
