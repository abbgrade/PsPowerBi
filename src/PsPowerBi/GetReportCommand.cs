using Microsoft.PowerBI.Api;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Report")]
    [OutputType(typeof(Models.Report))]
    public class GetReportCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public Models.Group Workspace { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiService");

            if (Workspace == null)
            {
                WriteVerbose($"Request reports.");
                var reports = Connection.Reports.GetReports().Value;
                WriteVerbose($"{ reports.Count } reports received.");

                foreach (var report in reports)
                {
                    WriteObject(report);
                }
            }
            else
            {
                WriteVerbose($"Request reports with filter on workspace { Workspace.Id }.");
                var reports = Connection.Reports.GetReportsInGroup(groupId: Workspace.Id).Value;
                WriteVerbose($"{ reports.Count } reports received with filter on workspace { Workspace.Id }.");

                foreach (var report in reports)
                {
                    var result = new PSObject(report);
                    result.Properties.Add(new PSNoteProperty("WorkspaceId", Workspace.Id));
                    WriteObject(result);
                }
            }
        }
    }
}
