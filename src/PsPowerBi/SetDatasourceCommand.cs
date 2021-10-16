using Microsoft.PowerBI.Api;
using Models = Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.PowerBI.Api.Models.Credentials;
using Microsoft.PowerBI.Api.Extensions;
using System.Security;
using System.Net;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Set, "Datasource")]
    public class SetDatasourceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PSObject Datasource { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

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
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            SetCredentialByUsernamePassword();

            Guid datasourceId = Guid.Empty;
            Guid gatewayId = Guid.Empty;
            {
                var _datasource = Datasource.BaseObject as Models.Datasource;
                var _gatewayDatasource = Datasource.BaseObject as Models.GatewayDatasource;

                if (_datasource != null) {
                    datasourceId = _datasource.DatasourceId.Value;
                    gatewayId = _datasource.GatewayId.Value;
                }

                if (_gatewayDatasource != null) {
                    datasourceId = _gatewayDatasource.Id;
                    gatewayId = _gatewayDatasource.GatewayId;
                }
            }


            if (Credential != null)
            {
                WriteVerbose($"Set credential of datasource { datasourceId } on gateway { gatewayId }.");
                if (WhatIf.ToBool() == false)
                {
                    var _gateway = Connection.Gateways.GetGateway(gatewayId);
                    var _credentialsEncryptor = new AsymmetricKeyEncryptor(_gateway.PublicKey);

                    var credential = new Models.CredentialDetails(
                        credentialsBase: new BasicCredentials(
                            username: Credential.UserName,
                            password: Credential.GetNetworkCredential().Password
                        ),
                        privacyLevel: PrivacyLevel,
                        encryptedConnection: EncryptedConnection,
                        credentialsEncryptor: _credentialsEncryptor
                    );
                    Connection.Gateways.UpdateDatasource(gatewayId, datasourceId, new Models.UpdateDatasourceRequest(credential));
                }
            }
        }
    }
}
