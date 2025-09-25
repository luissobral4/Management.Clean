namespace Management.Clean.Application.Features.Common;

public static class ValidationMessages
{
    public static string PropertyRequired(string propertyName) => $"{propertyName} is required.";
    public static string PropertyMaxSize(string propertyName, int size) => $"{propertyName} must be less than {size}.";
    public static string PropertyMinSize(string propertyName, int size) => $"{propertyName} must be greater than {size}.";
    public static string ObjectAlreadyExists(string objectType) => $"{objectType} already exists.";
    public static string ObjectDontExists(string objectType) => $"{objectType} already exists.";
    public static string ObjectInvalid(string objectType) => $"Invalid {objectType}.";
    public static string DateMustBeAfter(string objectType, string comparisonValue) => $"{objectType} must be after {comparisonValue}.";
    public static string DateMustBeBefore(string objectType, string comparisonValue) => $"{objectType} must be before {comparisonValue}.";
    public const string UpdateInvalid = "Validation errors in update request for {0} - {1}.";
    public static string CredentialsInvalid(string id) => $"Invalid credentials for {id}.";
}