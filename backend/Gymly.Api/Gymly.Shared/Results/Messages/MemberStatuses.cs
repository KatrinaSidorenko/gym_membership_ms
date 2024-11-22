namespace Gymly.Shared.Results.Messages;

public class MemberStatuses
{
    public static ResultData FailToGetAllMembers = new("FAIL_TO_GET_ALL_MEMBERS", "Fail to get all members");
    public static ResultData FailToCreateMember = new("FAIL_TO_CREATE_MEMBER", "Fail to create member");
    public static ResultData FailToCheckMemberExistence = new("FAIL_TO_CHECK_MEMBER_EXISTENCE", "Fail to check member existence");
    public static ResultData NoSuchMemberExists = new("NO_SUCH_MEMBER_EXISTS", "No such member exists");
    public static ResultData MemberNotFound = new("MEMBER_NOT_FOUND", "Member not found");
}
