#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Workspace' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'Connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'works' {
            $workspaces = Get-PowerBiWorkspace
            $workspaces | Should -Not -BeNullOrEmpty
        }

        Context 'Capacity' {

            BeforeAll {
                $script:capacity = Get-PowerBiCapacity 'myCapacity'
            }

            It 'works with capacity filter' {
                $workspaces = Get-PowerBiWorkspace -Capacity $script:capacity
                $workspaces | Should -Not -BeNullOrEmpty
                $workspaces | Where-Object { $_.CapacityId -ne $capacity.Id } | Should -BeNullOrEmpty
            }

            It 'works with capacity filter in pipeline' {
                $workspaces = $script:capacity | Get-PowerBiWorkspace
                $workspaces | Should -Not -BeNullOrEmpty
                $workspaces | Where-Object { $_.CapacityId -ne $capacity.Id } | Should -BeNullOrEmpty
            }

            It 'works with workspace name filter' {
                $workspace = Get-PowerBiWorkspace -Name 'myWorkspace'
                $workspace | Should -Not -BeNullOrEmpty
                $workspace.Name | Should -Be 'myWorkspace'
            }

        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}