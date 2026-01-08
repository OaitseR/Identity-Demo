using AzureIdentityDemo.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models.ExternalConnectors;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string tenantId = config["AzureAd:TenantId"]
    ?? throw new Exception("TenantId not found in configuration");

string clientId = config["AzureAd:ClientId"]
    ?? throw new Exception("ClientId not found in configuration");


Console.WriteLine("Choose authentication method:");
Console.WriteLine("1 - Interactive Login");
Console.WriteLine("2 - Device Code Login");
Console.WriteLine("3 - Daemon (Client Credentials) Login");

var choice = Console.ReadLine();

string token;

switch (choice)
{
    case "1":
        token = await InteractiveAuth.RunAsync(tenantId, clientId);
        await GraphHelper.GetUserProfileAsync(token);
        break;

    case "2":
        token = await DeviceCodeAuth.RunAsync(tenantId, clientId);
        await GraphHelper.GetUserProfileAsync(token);
        break;

case "3":
        var daemonService = new DaemonAuth(config);
        token = await daemonService.AuthenticateAsync();
        Console.WriteLine("Daemon flow completed successfully.");
        break;
default:
    throw new Exception("Invalid choice");
};

await GraphHelper.GetUserProfileAsync(token);


