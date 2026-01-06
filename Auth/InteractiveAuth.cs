using Microsoft.Identity.Client;

public static class InteractiveAuth
{
    public static async Task<string> RunAsync(string tenantId, string clientId)
    {
        var app = PublicClientApplicationBuilder
            .Create(clientId)
            .WithTenantId(tenantId)
            .WithRedirectUri("http://localhost")
            .Build();

        string[] scopes = { "User.Read" };

        var result = await app.AcquireTokenInteractive(scopes)
            .ExecuteAsync();

        return result.AccessToken;
    }
}
