namespace Management.Clean.Identity.Constants;

public static class Configs
{
    public const string JwtSettings = "JwtSettings";
    public const string JwtIssuer = $"{JwtSettings}:Issuer";
    public const string JwtAudience = $"{JwtSettings}:Audience";
    public const string JwtKey = $"{JwtSettings}:Key";
    public const string ConnectionString = "HrDatabaseConnectionString";
    public const string UidClaim = "uid";
}
