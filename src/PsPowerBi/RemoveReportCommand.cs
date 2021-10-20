using Microsoft.PowerBI.Api;
using System;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Remove, "Report")]
    public class RemoveReportCommand : PSCmdlet
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
        public PSObject Report { get; set; }

        [Parameter()]
        public SwitchParameter WhatIf { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiService");

            Guid workspaceId = (Guid) Report.Properties["WorkspaceId"].Value;
            Models.Report _report = (Models.Report) Report.BaseObject;
            Guid reportId = _report.Id;

            // if (workspaceId == null)
            // {
            //     WriteVerbose($"Remove report { reportId }.");
            //     if (WhatIf.ToBool() == false)
            //         Connection.Reports.DeleteReport(reportId);
            // }
            // else
            {
                WriteVerbose($"Remove report { reportId } ({ _report.Name }) in workspace { workspaceId }.");
                if (WhatIf.ToBool() == false)
                    Connection.Reports.DeleteReportInGroup(workspaceId, reportId);
            }
        }
    }
}
