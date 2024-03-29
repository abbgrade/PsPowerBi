﻿using Microsoft.PowerBI.Api;
using Newtonsoft.Json;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Datasource")]
    [OutputType(typeof(Models.Datasource))]
    public class GetDatasourceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = "ByDataset")]
        [ValidateNotNullOrEmpty()]
        public Models.Dataset Dataset { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = "ByGateway")]
        [ValidateNotNullOrEmpty()]
        public Models.Gateway Gateway { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiService");

            switch (this.ParameterSetName)
            {
                case "ByDataset":
                    {
                        WriteVerbose($"Request datasources with filter on dataset { Dataset.Id }.");
                        var datasources = Connection.Datasets.GetDatasources(Dataset.Id).Value;
                        WriteVerbose($"{ datasources.Count } datasources received with filter on dataset { Dataset.Id }.");

                        foreach (var datasource in datasources)
                        {
                            var result = new PSObject(datasource);
                            result.Properties.Add(new PSNoteProperty("DatasetId", Dataset.Id));
                            WriteObject(result);
                        }
                        break;
                    }
                case "ByGateway":
                    {
                        WriteVerbose($"Request datasources with filter on gateway { Gateway.Id }.");
                        var datasources = Connection.Gateways.GetDatasources(Gateway.Id).Value;
                        WriteVerbose($"{ datasources.Count } datasources received with filter on gateway { Gateway.Id }.");

                        foreach (var datasource in datasources)
                        {
                            var result = new PSObject(datasource);
                            result.Properties.Add(new PSNoteProperty("GatewayId", Gateway.Id));

                            dynamic dynamicObject = JsonConvert.DeserializeObject(datasource.ConnectionDetails);
                            result.Properties.Add(new PSNoteProperty("Server", dynamicObject?.server.Value));
                            result.Properties.Add(new PSNoteProperty("Database", dynamicObject?.database.Value));

                            WriteObject(result);
                        }
                        break;
                    }
                default:
                    throw new System.NotImplementedException(this.ParameterSetName);
            }
        }
    }
}
