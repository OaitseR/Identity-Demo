using AzureIdentityDemo.Auth;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string tenantId = config["AzureAd:TenantId"]
    ?? throw new Exception("TenantId not found in configuration");

string clientId = config["AzureAd:ClientId"]
    ?? throw new Exception("ClientId not found in configuration");

Console.WriteLine("=================================");
Console.WriteLine(" Azure Identity Test Console ");
Console.WriteLine("=================================");
Console.WriteLine("1 - Interactive Login");
Console.WriteLine("2 - Device Code Login");
Console.WriteLine("3 - Daemon (Client Credentials)");
Console.WriteLine("0 - Exit");
Console.Write("Select an option: ");

var choice = Console.ReadLine();

string? token = null;

switch (choice)
{
    case "1":
        Console.WriteLine("\nStarting Interactive Authentication...");
        token = await InteractiveAuth.RunAsync(tenantId, clientId);

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.WriteLine("Authentication failed. No token returned.");
            return;
        }

        Console.WriteLine("Authentication successful!");
        Console.WriteLine("Access Token acquired!");

        await GraphHelper.GetUserProfileAsync(token);
        break;

    case "2":
        Console.WriteLine("\nStarting Device Code Authentication...");
        token = await DeviceCodeAuth.RunAsync(tenantId, clientId);

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.WriteLine("Authentication failed. No token returned.");
            return;
        }

        Console.WriteLine("Authentication successful!");
        Console.WriteLine("Access Token acquired!");

        await GraphHelper.GetUserProfileAsync(token);
        break;

    case "3":
        Console.WriteLine("\nStarting Daemon Authentication...");
        var daemonService = new DaemonAuth(config);
        token = await daemonService.AuthenticateAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.WriteLine("Daemon authentication failed. No token returned.");
            return;
        }

        Console.WriteLine("Daemon flow completed successfully!");
        Console.WriteLine("Access Token acquired (Application token).");
        break;

    case "0":
        Console.WriteLine("Exiting application...");
        return;

    default:
        Console.WriteLine("Invalid choice.");
        break;
}
