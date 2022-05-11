using Microsoft.PowerBI.Api;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Models = Microsoft.PowerBI.Api.Models;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Gateway")]
    [OutputType(typeof(Models.Gateway))]
    public class GetGatewayCommand : PSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty()]
        public string Name { get; set; }

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
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiService");

            WriteVerbose($"Request capacities.");
            IList<Models.Gateway> capacities = Connection.Gateways.GetGateways().Value;
            WriteVerbose($"{ capacities.Count } capacities received.");

            if (Name != null) {
                WriteVerbose($"Filter capacities by name { Name }.");
                Models.Gateway gateway = capacities.Where(c => c.Name == Name).Single();
                WriteObject(gateway);
            } else {
                foreach (Models.Gateway gateway in capacities)
                {
                    WriteObject(gateway);
                }
            }
        }
    }
}
