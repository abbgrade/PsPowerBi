@{
    RootModule = 'PsPowerBi.dll'
    ModuleVersion = '0.1.0'
    GUID = '638cdb08-f357-4d85-80fa-ff95ab6a1341'
    DefaultCommandPrefix = 'PowerBi'
    Author = 'Steffen Kampmann'
    Copyright = '(c) 2021 Steffen Kampmann. Alle Rechte vorbehalten.'
    Description = 'PsPowerBi connects Power Bi and PowerShell. It gives you PowerShell Cmdlets with the power of Microsoft.PowerBI.Api.'
    PowerShellVersion = '7.0'

    CmdletsToExport = @(
        'Connect-Service',
        'Disconnect-Service',
        'Get-Capacity',
        'Get-Dataset',
        'Get-Datasource',
        'Get-Gateway',
        'Get-Report',
        'Get-Workspace',
        'Move-Workspace',
        'New-Datasource',
        'New-Report',
        'Register-Dataset',
        'Remove-Dataset',
        'Remove-Datasource',
        'Remove-Report',
        'Remove-Workspace',
        'Set-Dataset',
        'Set-Datasource',
        'Sync-Dataset'
    )

    PrivateData = @{

        PSData = @{
            Category = 'Databases'
            Tags = @('powerbi')
            LicenseUri = 'https://github.com/abbgrade/PsPowerBi/blob/main/LICENSE'
            ProjectUri = 'https://github.com/abbgrade/PsPowerBi'
            IsPrerelease = 'True'
        }
    }
}