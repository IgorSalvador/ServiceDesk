namespace ServiceDesk.Api;

public static class ApiConfiguration
{
    public const string CorsPolicyName = "BlazorWasm";
    public static List<string> FrontendUrls { get; set; } = [];
    public static List<string> BackendUrls { get; set; } = [];
}