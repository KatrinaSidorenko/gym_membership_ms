using Gymly.Core.Helpers;

namespace Gymly.Shared.DTOs;

public class MemberPayment
{
    [CustomColumn("class_name")]
    public string ClassName { get; set; }

    [CustomColumn("date")]
    public DateTime Date { get; set; }

    [CustomColumn("amount_paid")]
    public decimal Amount { get; set; }
}
