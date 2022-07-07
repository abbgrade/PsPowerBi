#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Gateway' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'works without name filter' {
            $gateways = Get-PowerBiGateway
            $gateways | Should -Not -BeNullOrEmpty
            $gateways.Count | Should -BeGreaterThan 1
        }

        It 'works with name filter' {
            $gateway = Get-PowerBiGateway -Name 'myGateway'
            $gateway | Should -Not -BeNullOrEmpty
            $gateway.Name | Should -Be 'myGateway'
        }

        It 'works with name filter by position' {
            $gateway = Get-PowerBiGateway 'myGateway'
            $gateway | Should -Not -BeNullOrEmpty
            $gateway.Name | Should -Be 'myGateway'
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}