using Microsoft.PowerBI.Api;
using System;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Set, "Dataset")]
    public class SetDatasetCommand : PSCmdlet
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
        public PSObject Dataset { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        [Parameter()]
        public string ConnectionString { get; set; }

        [Parameter()]
        public Models.Datasource Datasource { get; set; }

        [Parameter()]
        public Models.Gateway Gateway { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            Guid workspaceId = (Guid) Dataset.Properties["WorkspaceId"].Value;
            Models.Dataset _dataset = (Models.Dataset) Dataset.BaseObject;

            if (Gateway != null)
            {
                Connection.Datasets.BindToGateway(
                    groupId: workspaceId,
                    datasetId: _dataset.Id,
                    bindToGatewayRequest: new Models.BindToGatewayRequest(gatewayObjectId: Gateway.Id)
                );
            }

            if (Datasource != null)
            {
                throw new PSNotSupportedException(nameof(Datasource));
            }

            if (!string.IsNullOrEmpty(ConnectionString))
            {
                WriteVerbose($"set connection string of dataset { _dataset.Id } ({ _dataset.Name }) in workspace { workspaceId }.");
                if (WhatIf.ToBool() == false)
                    Connection.Datasets.SetAllDatasetConnections(workspaceId, _dataset.Id, new Models.ConnectionDetails(ConnectionString));
            }
        }
    }
}
