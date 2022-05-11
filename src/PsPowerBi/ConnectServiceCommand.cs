using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using System;
using System.Management.Automation;
using System.Security;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommunications.Connect, "Service", DefaultParameterSetName = PARAMETERSET_PROPERTIES_INTEGRATED)]
    [OutputType(typeof(PowerBIClient))]
    public class ConnectServiceCommand : PSCmdlet
    {
        #region ParameterSets
        private const string PARAMETERSET_PROPERTIES_INTEGRATED = "Properties_IntegratedSecurity";
        private const string PARAMETERSET_PROPERTIES_INTERACTIVE = "Properties_InteractiveAuthentication";
        private const string PARAMETERSET_PROPERTIES_CREDENTIAL = "Properties_Credential";
        #endregion

        internal static PowerBIClient SessionConnection { get; set; }

        //The client id that Azure AD created when you registered your client app.
        private const string clientID = "37f3b47a-a274-4c6b-b84f-69498c78c96e";

        private const string tenantId = "582259a1-dcaa-4cca-b1cf-e60d3f045ecd";

        [Parameter(
            ParameterSetName = PARAMETERSET_PROPERTIES_CREDENTIAL,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public string Username { get; set; }

        [Parameter(
            ParameterSetName = PARAMETERSET_PROPERTIES_CREDENTIAL,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public SecureString Password { get; set; }

        [Parameter( 
            ParameterSetName = PARAMETERSET_PROPERTIES_INTERACTIVE,
            Mandatory = true
        )]
        public SwitchParameter InteractiveAuthentication { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var publicClientApp = PublicClientApplicationBuilder
                .Create(clientID)
                .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                .WithDefaultRedirectUri()
                .Build();

            AuthenticationResult authenticationResult = null;

            switch(ParameterSetName)
            {
                case PARAMETERSET_PROPERTIES_CREDENTIAL:
                    authenticationResult = publicClientApp.AcquireTokenByUsernamePassword(
                        scopes: new[] { "https://analysis.windows.net/powerbi/api/.default" },
                        username: Username,
                        password: Password
                    ).ExecuteAsync().Result;
                    break;

                case PARAMETERSET_PROPERTIES_INTEGRATED:
                    authenticationResult = publicClientApp.AcquireTokenByIntegratedWindowsAuth(
                        scopes: new[] { "https://analysis.windows.net/powerbi/api/.default" }
                    ).ExecuteAsync().Result;
                    break;

                case PARAMETERSET_PROPERTIES_INTERACTIVE:
                    authenticationResult = publicClientApp.AcquireTokenInteractive(
                        scopes: new[] { "https://analysis.windows.net/powerbi/api/.default" }
                    ).ExecuteAsync().Result;
                    break;

                default:
                    throw new NotSupportedException($"Parameterset {ParameterSetName} is not supported.");
            }

            var tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");
            SessionConnection = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials);

            WriteVerbose("Connected to Power BI");

            WriteObject(SessionConnection);
        }
    }
}
