using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Dataset")]
    [OutputType(typeof(Dataset))]
    public class GetDatasetCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public Group Workspace { get; set; }

        [Parameter(
            Mandatory = false
        )]
        [ValidateNotNullOrEmpty()]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            if (Workspace == null)
            {
                WriteVerbose($"Request datasets.");
                var datasets = Connection.Datasets.GetDatasets().Value;
                WriteVerbose($"{ datasets.Count } datasets received.");

                foreach (var dataset in datasets)
                {
                    if ( Name == null || dataset.Name == Name)
                        WriteObject(dataset);
                }
            }
            else
            {
                WriteVerbose($"Request datasets with filter on workspace { Workspace.Id }.");
                var datasets = Connection.Datasets.GetDatasetsInGroup(groupId: Workspace.Id).Value;
                WriteVerbose($"{ datasets.Count } datasets received with filter on workspace { Workspace.Id }.");

                foreach (var dataset in datasets)
                {
                    if ( Name == null || dataset.Name == Name) {
                        var result = new PSObject(dataset);
                        result.Properties.Add(new PSNoteProperty("WorkspaceId", Workspace.Id));
                        result.Properties.Add(new PSNoteProperty("DatasourceId", dataset.Id));
                        WriteObject(result);
                    }
                }
            }
        }
    }
}
