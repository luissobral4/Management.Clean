namespace Management.Clean.Application.Features.Common;

public static class HelperFunctions
{
    public static int CalculateRequestedDays(DateTime startDate, DateTime endDate)
    {
        return (int)(endDate - startDate).TotalDays;
    }
}