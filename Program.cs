using Microsoft.Extensions.Configuration;

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

var choice = Console.ReadLine();
string token = choice switch
{
    "1" => await InteractiveAuth.RunAsync(tenantId, clientId),
    "2" => await DeviceCodeAuth.RunAsync(tenantId, clientId),
    _ => throw new Exception("Invalid choice")
};

await GraphHelper.GetUserProfileAsync(token);


