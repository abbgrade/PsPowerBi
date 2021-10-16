using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System.IO;
using System;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.New, "Report")]
    [OutputType(typeof(Report))]
    public class NewReportCommand : PSCmdlet
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
        public Group Workspace { get; set; }

        [Parameter()]
        public Dataset Dataset { get; set; }

        [Parameter(
            Mandatory = true
        )]
        public FileInfo PbixFile { get; set; }

        [Parameter()]
        public string Name { get; set; }

        [Parameter()]
        public ImportConflictHandlerMode? ImportConflictHandlerMode { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            if (!PbixFile.Exists)
                throw new PSInvalidOperationException($"{nameof(PbixFile)} does not exist.");

            if (string.IsNullOrEmpty(Name))
                Name = PbixFile.Name;

            Report report;
            if (Workspace == null)
            {
                WriteVerbose("$Add report {Name} to personal workspace.");
                var import = Connection.Imports.PostImportWithFile(
                    fileStream: new FileStream(path: PbixFile.FullName, mode: FileMode.Open),
                    datasetDisplayName: Name,
                    nameConflict: ImportConflictHandlerMode
                );

                // Check if Import is finished
                while (
                    import.ImportState == null ||
                    (!import.ImportState.Equals("Succeeded", StringComparison.InvariantCultureIgnoreCase) &&
                    !import.ImportState.Equals("Failed", StringComparison.InvariantCultureIgnoreCase)))
                {
                    import = Connection.Imports.GetImport(importId: import.Id);
                }

                if (import.ImportState.Equals("Failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ApplicationException("The ImportState is Failed");
                }

                report = import.Reports.Single();
                WriteObject(report);
            }
            else
            {
                WriteVerbose($"Add report {Name} to workspace {Workspace.Name}.");
                var import = Connection.Imports.PostImportWithFileInGroup(
                    groupId: Workspace.Id, 
                    fileStream: new FileStream(path: PbixFile.FullName, mode: FileMode.Open), 
                    datasetDisplayName: Name, 
                    nameConflict: ImportConflictHandlerMode
                );

                // Check if Import is finished
                while (
                    import.ImportState == null ||
                    (!import.ImportState.Equals("Succeeded", StringComparison.InvariantCultureIgnoreCase) &&
                    !import.ImportState.Equals("Failed", StringComparison.InvariantCultureIgnoreCase)))
                {
                    import = Connection.Imports.GetImportInGroup(groupId: Workspace.Id, importId: import.Id);
                }

                if (import.ImportState.Equals("Failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ApplicationException("The ImportState is Failed");
                }

                report = import.Reports.Single();
                var result = new PSObject(report);
                result.Properties.Add(new PSNoteProperty("WorkspaceId", Workspace.Id));
                WriteObject(result);
            }

            if ( Dataset != null)
            {
                WriteVerbose($"Assign report to dataset {Dataset.Name}");

                if (Workspace == null)
                    Connection.Reports.RebindReport(reportId: report.Id, requestParameters: new RebindReportRequest(Dataset.Id));
                else
                    Connection.Reports.RebindReportInGroup(groupId: Workspace.Id, reportId: report.Id, requestParameters: new RebindReportRequest(Dataset.Id));
            }
        }
    }
}
