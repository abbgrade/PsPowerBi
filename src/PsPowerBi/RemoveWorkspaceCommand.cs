using Microsoft.PowerBI.Api;
using Models = Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand : PSCmdlet
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
        public PSObject Workspace { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            Models.Group _workspace = (Models.Group) Workspace.BaseObject;
            Guid workspaceId = _workspace.Id;

            WriteVerbose($"Remove workspace { workspaceId } ({ _workspace.Name }).");
            if (WhatIf.ToBool() == false)
                Connection.Groups.DeleteGroup(workspaceId);
        }
    }
}
