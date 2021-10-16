using Microsoft.PowerBI.Api;
using System;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Datasource")]
    public class RemoveDatasourceCommand : PSCmdlet
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

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

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

            WriteVerbose($"Remove datasource { datasourceId } from gateway { gatewayId }.");
            if (WhatIf.ToBool() == false)
                Connection.Gateways.DeleteDatasource(gatewayId, datasourceId);
        }
    }
}
