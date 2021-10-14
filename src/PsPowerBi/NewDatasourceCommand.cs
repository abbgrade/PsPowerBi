using Microsoft.PowerBI.Api;
using Models = Microsoft.PowerBI.Api.Models;
using System.Management.Automation;
using Microsoft.PowerBI.Api.Models.Credentials;
using Microsoft.PowerBI.Api.Extensions;
using System.Security;
using Microsoft.PowerBI.Api.Extensions.Models.Credentials;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.New, "Datasource")]
    public class NewDatasourceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public Models.Gateway Gateway { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        [Parameter(
            Mandatory = true
        )]
        public string Name { get; set; }

        [Parameter()]
        public PSCredential Credential { get; set; }

        [Parameter()]
        public string Username { get; private set; }

        [Parameter()]
        public SecureString Password { get; private set; }

        [Parameter()]
        public Models.PrivacyLevel PrivacyLevel { get; set; } = Models.PrivacyLevel.Private;

        [Parameter()]
        public Models.EncryptedConnection EncryptedConnection { get; set; } = Models.EncryptedConnection.Encrypted;

        [Parameter()]
        public string Type { get; set; } = "Sql";

        [Parameter()]
        public string SqlServerName { get; set; }

        [Parameter()]
        public string SqlDatabaseName { get; set; }

        private void SetCredentialByUsernamePassword()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                Credential = new PSCredential(userName: Username, password: Password);
            }
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            SetCredentialByUsernamePassword();

            switch (Type)
            {
                case "Sql":
                    {
                        if (string.IsNullOrEmpty(SqlServerName))
                            throw new PSInvalidOperationException($"Parameter {nameof(SqlServerName)} is required for SQL datasources.");

                        if (string.IsNullOrEmpty(SqlDatabaseName))
                            throw new PSInvalidOperationException($"Parameter {nameof(SqlDatabaseName)} is required for SQL datasources.");
                    }
                    break;

                default: throw new PSNotSupportedException($"DataSourceType {Type} is not supported.");
            }

            {
                //WriteVerbose($"Set credential of datasource { datasourceId } on gateway { gatewayId }.");
                if (WhatIf.ToBool() == false)
                {
                    ICredentialsEncryptor _credentialsEncryptor = null;

                    if (Gateway != null)
                    {
                        _credentialsEncryptor = new AsymmetricKeyEncryptor(Gateway.PublicKey);
                    }

                    if (Gateway != null)
                    {
                        var request = new Models.PublishDatasourceToGatewayRequest
                        {
                            DataSourceType = Type,
                            DataSourceName = Name,
                            ConnectionDetails = $"{{ \"server\":\"{SqlServerName}\", \"database\":\"{SqlDatabaseName}\" }}"
                        };

                        if (Credential != null)
                            request.CredentialDetails = new Models.CredentialDetails(
                                credentialsBase: new BasicCredentials(
                                    username: Credential.UserName,
                                    password: Credential.GetNetworkCredential().Password
                                ),
                                privacyLevel: PrivacyLevel,
                                encryptedConnection: EncryptedConnection,
                                credentialsEncryptor: _credentialsEncryptor
                            );

                        Connection.Gateways.CreateDatasource(gatewayId: Gateway.Id, request);
                    }
                    else
                        throw new PSNotSupportedException("Datasource creation without gateway.");
                }
            }
        }
    }
}
