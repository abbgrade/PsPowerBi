#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Dataset' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'Connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'works' {
            $datasets = Get-PowerBiDataset
            # $datasets | Should -Not -BeNullOrEmpty
        }

        Context 'Workspace' {

            BeforeAll {
                $script:workspace = Get-PowerBiCapacity 'myCapacity' | Get-PowerBIWorkspace -Name 'myWorkspace'
            }

            It 'works with workspace filter' {
                $datasets = $script:workspace | Get-PowerBiDataset

                $datasets | Should -Not -BeNullOrEmpty

                $datasets.Count | Should -BeGreaterThan 0

                $workspaceId = ( $datasets | Select-Object -First 1 ).WorkspaceId
                $workspaceId | Should -Not -BeNullOrEmpty

                $datasets | Where-Object { $_.WorkspaceId -ne $script:workspace.Id } | Should -BeNullOrEmpty
            }

            It 'works with name filter' {
                $datasets = $script:workspace | Get-PowerBiDataset -Name 'myDataset'

                $datasets | Should -Not -BeNullOrEmpty

                $datasets.Count | Should -Be 1

                $workspaceId = ( $datasets | Select-Object -First 1 ).WorkspaceId
                $workspaceId | Should -Not -BeNullOrEmpty

                $datasets | Where-Object { $_.WorkspaceId -ne $script:workspace.Id } | Should -BeNullOrEmpty
            }
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}