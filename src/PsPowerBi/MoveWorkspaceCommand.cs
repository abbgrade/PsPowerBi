using Microsoft.PowerBI.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Move, "Workspace")]
    public class MoveWorkspaceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false
        )]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public PSObject Workspace { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public Capacity Capacity { get; private set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Group _workspace = (Group) Workspace.BaseObject;

            WriteVerbose($"Move workspace '{ _workspace.Name }' ({ _workspace.Id }) to capacity '{ Capacity.DisplayName }' ({ Capacity.Id })");

            if (WhatIf.ToBool() == false)
                Connection.Groups.AssignToCapacity(groupId: _workspace.Id, requestParameters: new AssignToCapacityRequest(capacityId: Capacity.Id));

        }
    }
}
