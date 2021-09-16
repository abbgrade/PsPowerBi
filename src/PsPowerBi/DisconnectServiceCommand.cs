﻿using Microsoft.Identity.Client;
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
        public PowerBIClient Connection { get;set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Connection == null)
                Connection = ConnectServiceCommand.SessionConnection;

            Connection.Dispose();
            WriteVerbose("Disconnected from Power BI");
        }
    }
}
