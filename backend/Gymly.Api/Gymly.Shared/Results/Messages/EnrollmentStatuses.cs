namespace Gymly.Shared.Results.Messages;

public class EnrollmentStatuses
{
    public static ResultData FailToGetEnrollments = new("FAIL_TO_GET_ENROLLMENTS", "Fail to get all enrollments");
    public static ResultData FailToCreateEnrollment = new("FAIL_TO_CREATE_ENROLLMENT", "Fail to create enrollment");
    public static ResultData FailToEnrollMemberToClass = new("FAIL_TO_ENROLL_MEMBER_TO_CLASS", "Fail to enroll member to class");
}
