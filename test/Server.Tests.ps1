#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Connect-Server' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'no connection' {

        It 'connects' {
            $script:connection = Connect-PowerBiService
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }
    }

    Context 'connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'disconnects' {
            $script:connection | Disconnect-PowerBiService
        }

    }
}