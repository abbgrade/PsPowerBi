using Microsoft.PowerBI.Api;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommunications.Disconnect, "Service")]
    public class DisconnectServiceCommand : PSCmdlet
    {

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public PowerBIClient Connection { get; set; } = ConnectServiceCommand.SessionConnection;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            Connection.Dispose();
            WriteVerbose("Disconnected from Power BI");
        }
    }
}
