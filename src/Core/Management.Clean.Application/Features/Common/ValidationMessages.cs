namespace Management.Clean.Application.Features.Common;

public static class ValidationMessages
{
    public static string PropertyRequired(string propertyName) => $"{propertyName} is required.";
    public static string PropertyMaxSize(string propertyName, int size) => $"{propertyName} must be less than {size}.";
    public static string PropertyMinSize(string propertyName, int size) => $"{propertyName} must be greater than {size}.";
    public static string ObjectAlreadyExists(string objectType) => $"{objectType} already exists.";
    public static string ObjectInvalid(string objectType) => $"Invalid {objectType}.";
}