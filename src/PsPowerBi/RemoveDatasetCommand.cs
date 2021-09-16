using Microsoft.PowerBI.Api;
using Models = Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Dataset")]
    public class RemoveDatasetCommand : PSCmdlet
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
        public PSObject Dataset { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            Guid workspaceId = (Guid) Dataset.Properties["WorkspaceId"].Value;
            Models.Dataset _dataset = (Models.Dataset) Dataset.BaseObject;
            string datasetId = _dataset.Id;

            // if (workspaceId == null)
            // {
            //     WriteVerbose($"Remove dataset { datasetId }.");
            //     if (WhatIf.ToBool() == false)
            //         Connection.Datasets.DeleteDataset(datasetId);
            // }
            // else
            {
                WriteVerbose($"Remove dataset { datasetId } ({ _dataset.Name }) in workspace { workspaceId }.");
                if (WhatIf.ToBool() == false)
                    Connection.Datasets.DeleteDatasetInGroup(workspaceId, datasetId);
            }
        }
    }
}
