# Azure Identity & .NET 10 Integration 
A unified Console Application that serves as an identity-testing tool. The application proves it can successfully acquire an Access Token using three different identity scenarios.
This project is a .NET 10 console application designed to demonstrate and validate Microsoft Entra ID authentication using three different identity flows.

The application acts as an identity testing tool, allowing a developer to acquire and validate access tokens using both user-based and app-only authentication scenarios.

# Project Goals

1. Demonstrate correct usage of MSAL.NET in a .NET 10 application

2. Support multiple authentication flows in a single console app

3. Validate tokens by calling Microsoft Graph

4. Follow secure configuration and repository best practices

5. Ensure the project can be cloned and run in under 5 minutes

# Supported Authentication Flows

# Application Flow

1. Load configuration from appsettings.json

2. Display a menu to select authentication flow

3. Acquire an access token using the selected flow

4. Validate the token:

  * User flows → Call Microsoft Graph and display user profile

* Daemon flow → Confirm token acquisition success

# Phase 1: Azure Portal Configuration
  # 1. App Registration
      a. Navigate to Microsoft Entra ID → App registrations
      b. Create a New registration
      c. Set:
         Name: AzureIdentityDemo
         Supported account types:
         Accounts in any organizational directory and personal Microsoft accounts
      d. Record:
         Application (Client) ID
         Directory (Tenant) ID

# Redirect URI (Interactive Flow)
1. Platform: Public client / Mobile & desktop
2. URI: http://localhost

# API Permissions
  #  Microsoft Graph
    1. Delegated: User.Read
    2. Application: User.Read.All (for daemon flow)
Admin consent is required for application permissions.

# Phase 2: Environment Setup
# Configuration File

Create appsettings.json in the project root:

        {
          "AzureAd": {
            "TenantId": "<TENANT_ID>",
            "ClientId": "<CLIENT_ID>",
            "ClientSecret": "<CLIENT_SECRET>"
          }
        }

# Security Notice

1. appsettings.json must NOT be committed
2. It is included in .gitignore
3. Use appsettings.example.json as a template

# Phase 3: Code Implementation
# Public Client Flows 
1. Implemented using PublicClientApplicationBuilder
2. Supports:
   * Browser-based interactive login
   * Device code login for non-browser environments
   * Uses MSAL.NET
   * Retrieves user profile via Microsoft Graph

# Confidential Client Flow 
1. Implemented using ConfidentialClientApplicationBuilder
2. Uses client credentials (client secret)
3. No user interaction
4. Designed for background services and APIs
5. Securely loads secrets from configuration

# Phase 4: Validation
# User-Based Flows
After successful authentication:

      Success! Logged in as: <User Display Name>
(User data retrieved from Microsoft Graph)

# Daemon Flow
      Daemon flow successful. Access token acquired.

# How to Run the Project
      dotnet restore
      dotnet run
Select an authentication method from the menu.

# Collaboration Model
1. Each authentication flow is implemented independently
2. All flows expose a common contract:
      Acquire token → return access token
3. Program.cs acts as the orchestration layer
4. Code reviews performed via Pull Requests



