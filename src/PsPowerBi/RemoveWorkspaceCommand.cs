using Microsoft.PowerBI.Api;
using System;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand : PSCmdlet
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
        public PSObject Workspace { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiService");

            Models.Group _workspace = (Models.Group) Workspace.BaseObject;
            Guid workspaceId = _workspace.Id;

            WriteVerbose($"Remove workspace { workspaceId } ({ _workspace.Name }).");
            if (WhatIf.ToBool() == false)
                Connection.Groups.DeleteGroup(workspaceId);
        }
    }
}
