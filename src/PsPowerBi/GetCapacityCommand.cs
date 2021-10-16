using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PsPowerBi
{
    [Cmdlet(VerbsCommon.Get, "Capacity")]
    [OutputType(typeof(Capacity))]
    public class GetCapacityCommand : PSCmdlet
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
                throw new PSArgumentNullException(nameof(Connection), $"run Connect-PowerBiConnection");

            WriteVerbose($"Request capacities.");
            IList<Capacity> capacities = Connection.Capacities.GetCapacities().Value;
            WriteVerbose($"{ capacities.Count } capacities received.");

            if (Name != null) {
                WriteVerbose($"Filter capacities by name { Name }.");
                Capacity capacity = capacities.Where(c => c.DisplayName == Name).Single();
                WriteObject(capacity);
            } else {
                foreach (Capacity capacity in capacities)
                {
                    WriteObject(capacity);
                }
            }
        }
    }
}
