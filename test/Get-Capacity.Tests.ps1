#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Capacity' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'works without name filter' {
            $capacities = Get-PowerBiCapacity
            $capacities | Should -Not -BeNullOrEmpty
            $capacities.Count | Should -BeGreaterThan 1
        }

        It 'works with name filter' {
            $capacity = Get-PowerBiCapacity -Name 'myCapacity'
            $capacity | Should -Not -BeNullOrEmpty
            $capacity.DisplayName | Should -Be 'myCapacity'
        }

        It 'works with name filter by position' {
            $capacity = Get-PowerBiCapacity 'myCapacity'
            $capacity | Should -Not -BeNullOrEmpty
            $capacity.DisplayName | Should -Be 'myCapacity'
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}