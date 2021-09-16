using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Gateway")]
    [OutputType(typeof(Gateway))]
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
        public PowerBIClient Connection { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            WriteVerbose($"Request capacities.");
            IList<Gateway> capacities = Connection.Gateways.GetGateways().Value;
            WriteVerbose($"{ capacities.Count } capacities received.");

            if (Name != null) {
                WriteVerbose($"Filter capacities by name { Name }.");
                Gateway gateway = capacities.Where(c => c.Name == Name).Single();
                WriteObject(gateway);
            } else {
                foreach (Gateway gateway in capacities)
                {
                    WriteObject(gateway);
                }
            }
        }
    }
}
