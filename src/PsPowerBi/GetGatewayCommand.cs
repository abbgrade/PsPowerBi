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

            WriteVerbose($"Request gateways.");
            IList<Models.Gateway> gateways = Connection.Gateways.GetGateways().Value;
            WriteVerbose($"{ gateways.Count } gateways received.");

            if (Name != null) {
                WriteVerbose($"Filter gateways by name { Name }.");
                Models.Gateway gateway = gateways.Where(c => c.Name == Name).Single();
                WriteObject(gateway);
            } else {
                foreach (Models.Gateway gateway in gateways)
                {
                    WriteObject(gateway);
                }
            }
        }
    }
}
