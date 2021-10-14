using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Workspace")]
    [OutputType(typeof(Group))]
    public class GetWorkspaceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get;set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public Capacity Capacity { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 0
        )]
        [ValidateNotNullOrEmpty()]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            WriteVerbose($"Request workspaces.");
            var workspaces = Connection.Groups.GetGroups().Value;
            WriteVerbose($"{ workspaces.Count } workspaces received.");

            if (Capacity != null) {
                WriteVerbose($"Filter workspaces on capacity { Capacity.DisplayName }");
                workspaces = workspaces.Where(w => w.CapacityId == Capacity.Id).ToList<Group>();
            }

            if (Name != null) {
                WriteVerbose($"Filter workspaces on name { Name }");
                workspaces = workspaces.Where(w => w.Name == Name).ToList<Group>();
            }

            foreach (var workspace in workspaces)
            {
                WriteObject(workspace);
            }
        }
    }
}
