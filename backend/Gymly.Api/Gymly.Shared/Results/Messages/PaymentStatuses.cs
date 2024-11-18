namespace Gymly.Shared.Results.Messages;

public class PaymentStatuses
{
    public static ResultData FailToGetMemberPayments = new("FAIL_TO_GET_MEMBER_PAYMENTS", "Fail to get member payments");
    public static ResultData PaymentAmountNotMatchClassPrice = new("PAYMENT_SUM_NOT_MATCH_CLASS_PRICE", "Payment amount not match class price");
    public static ResultData FailToCreatePayment = new("FAIL_TO_CREATE_PAYMENT", "Fail to create payment");
}
