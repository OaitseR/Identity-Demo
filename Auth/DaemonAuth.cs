using System;
using System.Collections.Generic;
using System.Text;

namespace AzureIdentityDemo.Auth
{
    using Microsoft.Identity.Client;
    using Microsoft.Extensions.Configuration;

    public class DaemonAuth
    {
        private readonly IConfiguration _configuration;

        public DaemonAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync()
        {
            var tenantId = _configuration["AzureAd:TenantId"]
                ?? throw new Exception("TenantId not found");

            var clientId = _configuration["AzureAd:ClientId"]
                ?? throw new Exception("ClientId not found");

            var clientSecret = _configuration["AzureAd:ClientSecret"]
                ?? throw new Exception("ClientSecret not found");

            var app = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
                .Build();

            string[] scopes = { "https://graph.microsoft.com/.default" };

            var result = await app
                .AcquireTokenForClient(scopes)
                .ExecuteAsync();

            Console.WriteLine("Daemon authentication successful.");
            Console.WriteLine($"Access Token (first 20 chars): {result.AccessToken.Substring(0, 20)}...");

            return result.AccessToken;
        }


        //public async Task AuthenticateAsync(IConfigurationRoot config)
        //{
        //    var tenantId = _configuration["AzureAd:TenantId"];
        //    var clientId = _configuration["AzureAd:ClientId"];
        //    var clientSecret = _configuration["AzureAd:ClientSecret"];

        //    var app = ConfidentialClientApplicationBuilder
        //        .Create(clientId)
        //        .WithClientSecret(clientSecret)
        //        .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
        //        .Build();

        //    string[] scopes = { "https://graph.microsoft.com/.default" };

        //    var result = await app
        //        .AcquireTokenForClient(scopes)
        //        .ExecuteAsync();

        //    Console.WriteLine("Daemon authentication successful.");
        //    Console.WriteLine($"Access Token (first 20 chars): {result.AccessToken.Substring(0, 20)}...");
        //}
    }

}
