namespace Gymly.Shared.Results.Messages;

public class MemberStatuses
{
    public static ResultData FailToGetAllMembers = new("FAIL_TO_GET_ALL_MEMBERS", "Fail to get all members");
    public static ResultData FailToCreateMember = new("FAIL_TO_CREATE_MEMBER", "Fail to create member");
}
