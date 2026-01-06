using Microsoft.Identity.Client;

public static class DeviceCodeAuth
{
    public static async Task<string> RunAsync(string tenantId, string clientId)
    {
        var app = PublicClientApplicationBuilder
            .Create(clientId)
            .WithTenantId(tenantId)
            .Build();

        string[] scopes = { "User.Read" };

        var result = await app.AcquireTokenWithDeviceCode(
            scopes,
            deviceCode =>
            {
                Console.WriteLine(deviceCode.Message);
                return Task.CompletedTask;
            })
            .ExecuteAsync();

        return result.AccessToken;
    }
}
