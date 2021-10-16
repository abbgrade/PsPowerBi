using Microsoft.PowerBI.Api;
using System.Linq;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Workspace")]
    [OutputType(typeof(Models.Group))]
    public class GetWorkspaceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty()]
        public Models.Capacity Capacity { get; set; }

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
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            WriteVerbose($"Request workspaces.");
            var workspaces = Connection.Groups.GetGroups().Value;
            WriteVerbose($"{ workspaces.Count } workspaces received.");

            if (Capacity != null) {
                WriteVerbose($"Filter workspaces on capacity { Capacity.DisplayName }");
                workspaces = workspaces.Where(w => w.CapacityId == Capacity.Id).ToList();
            }

            if (Name != null) {
                WriteVerbose($"Filter workspaces on name { Name }");
                workspaces = workspaces.Where(w => w.Name == Name).ToList();
            }

            foreach (var workspace in workspaces)
            {
                WriteObject(workspace);
            }
        }
    }
}
