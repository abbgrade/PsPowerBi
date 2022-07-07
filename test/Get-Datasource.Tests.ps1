#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Get-Datasource' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'Connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        Context 'Dataset' {

            BeforeAll {
                $script:dataset = Get-PowerBiCapacity 'myCapacity' | Get-PowerBIWorkspace | Select-Object -First 5 | Get-PowerBiDataset | Select-Object -First 1
            }

            It 'works with dataset filter' {

                $datasources = $script:dataset | Get-PowerBiDatasource
                $datasources | Should -Not -BeNullOrEmpty

                $datasources | Where-Object { $_.DatasetId -ne $script:dataset.Id } | Should -BeNullOrEmpty
            }
                
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}