using Microsoft.PowerBI.Api;
using Models = Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Datasource")]
    public class RemoveDatasourceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; }

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
                Connection = ConnectServiceCommand.SessionConnection;

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
