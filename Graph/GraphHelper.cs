using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Net.Http.Headers;

public static class GraphHelper
{
    public static async Task GetUserProfileAsync(string accessToken)
    {
        var authProvider = new BaseBearerTokenAuthenticationProvider(
            new SimpleAccessTokenProvider(accessToken));

        var graphClient = new GraphServiceClient(authProvider);

        var user = await graphClient.Me.GetAsync();

        Console.WriteLine($"Success! Logged in as: {user?.DisplayName}");
    }
}

// Helper class for Graph SDK v5
public class SimpleAccessTokenProvider : IAccessTokenProvider
{
    private readonly string _accessToken;

    public SimpleAccessTokenProvider(string accessToken)
    {
        _accessToken = accessToken;
    }

    public Task<string> GetAuthorizationTokenAsync(
        Uri uri,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_accessToken);
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }
        = new AllowedHostsValidator();
}
