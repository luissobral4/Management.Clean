namespace Management.Clean.Application.Features.Common;

public static class EmailParams
{
    public const string SubjectRequestSubmitted = "Leave Request Submitted";

    public const string SubjectRequestCancelled = "Leave Request Cancelled";

    public const string SubjectRequestStatusUpdated = "Leave Request Status Updated";

    private static string Body(DateTime startDate, DateTime endDate, string type, string objectType = "leave request") =>
        $"Your {objectType} for {startDate:D} to {endDate:D} has beem {type} successfuly";

    public static string BodyUpdated(DateTime startDate, DateTime endDate) =>
        Body(startDate, endDate, "updated", "leave request approval status");

    public static string BodyStatusUpdated(DateTime startDate, DateTime endDate) =>
        Body(startDate, endDate, "updated");

    public static string BodySubmited(DateTime startDate, DateTime endDate) =>
        Body(startDate, endDate, "submited");

    public static string BodyCancelled(DateTime startDate, DateTime endDate) =>
        Body(startDate, endDate, "cancelled"); 

}