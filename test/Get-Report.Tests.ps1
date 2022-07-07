#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Report' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'Connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        It 'works' {
            $reports = Get-PowerBiReport
            # $reports | Should -Not -BeNullOrEmpty
        }

        Context 'Workspace' {

            BeforeAll {
                $script:workspace = Get-PowerBiCapacity 'myCapacity' | Get-PowerBIWorkspace | Select-Object -First 1
            }

            It 'works with workspace filter' {
                $reports = $script:workspace | Get-PowerBiReport

                $reports | Should -Not -BeNullOrEmpty

                $reports.Count | Should -BeGreaterThan 0

                $workspaceId = ( $reports | Select-Object -First 1 ).WorkspaceId
                $workspaceId | Should -Not -BeNullOrEmpty

                $reports | Where-Object { $_.WorkspaceId -ne $script:workspace.Id } | Should -BeNullOrEmpty
            }
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}