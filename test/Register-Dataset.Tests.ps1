#Requires -Modules @{ ModuleName='Pester'; ModuleVersion='5.0.0' }

Describe 'Register-Dataset' {
    BeforeAll {
        Import-Module -Name $PSScriptRoot/../src/PsPowerBi/bin/Debug/net5.0/publish/PsPowerBi.psd1 -Force -ErrorAction 'Stop'
    }

    Context 'Connection' {

        BeforeAll {
            $script:connection = Connect-PowerBiService
        }

        Context 'Dataset' {

            BeforeAll {
                $script:workspace = Get-PowerBiCapacity 'myCapacity' | Get-PowerBIWorkspace | Select-Object -First 1
                $script:dataset = $script:workspace | Get-PowerBiDataset | Select-Object -First 1
                $script:gateway = Get-PowerBiGateway -Name 'myGateway'
            }

            It 'works' {
                $script:dataset | Register-PowerBiDataset -Gateway $script:gateway
            }
        }

        AfterAll {
            $script:connection | Disconnect-PowerBiService
        }

    }
}