using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;

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
