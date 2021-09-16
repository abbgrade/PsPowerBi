using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommunications.Connect, "Service")]
    [OutputType(typeof(PowerBIClient))]
    public class ConnectServiceCommand : PSCmdlet
    {

        internal static PowerBIClient SessionConnection { get; set; }

        //The client id that Azure AD created when you registered your client app.
        private const string clientID = "37f3b47a-a274-4c6b-b84f-69498c78c96e";

        private const string tenantId = "582259a1-dcaa-4cca-b1cf-e60d3f045ecd";

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public string Username { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public SecureString Password { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var publicClientApp = PublicClientApplicationBuilder
                .Create(clientID)
                .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                .Build();

            AuthenticationResult authenticationResult = null;

            if (Username != null) {
                authenticationResult = publicClientApp.AcquireTokenByUsernamePassword(
                    scopes: new[] { "https://analysis.windows.net/powerbi/api/.default" },
                    username: Username,
                    password: Password
                ).ExecuteAsync().Result;
            } else {
                authenticationResult = publicClientApp.AcquireTokenByIntegratedWindowsAuth(
                    scopes: new[] { "https://analysis.windows.net/powerbi/api/.default" }
                ).ExecuteAsync().Result;
            }

            var tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");
            SessionConnection = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials);

            WriteVerbose("Connected to Power BI");

            WriteObject(SessionConnection);
        }
    }
}
