requires Configuration

[System.Version] $global:PsPowerBiVersion = New-Object System.Version (
	Import-PowerShellDataFile $PSScriptRoot\..\src\PsPowerBi\PsPowerBi.psd1
).ModuleVersion
[System.IO.DirectoryInfo] $global:PsPowerBiStage = "$PSScriptRoot\..\src\PsPowerBi\bin\$Configuration\netcoreapp3.1\publish"
[System.IO.FileInfo] $global:PsPowerBiManifest = "$global:PsPowerBiStage\PsPowerBi.psd1"
[System.IO.DirectoryInfo] $global:PsPowerBiDoc = "$PSScriptRoot\..\docs"
[System.IO.DirectoryInfo] $global:PsPowerBiInstallDirectory = Join-Path $env:PSModulePath.Split(';')[0] 'PsPowerBi' $global:PsPowerBiVersion

task PsPowerBi.Build.Dll -Jobs {
    exec { dotnet publish $PSScriptRoot\..\src\PsPowerBi -c $Configuration }
}

task PsPowerBi.Import -Jobs PsPowerBi.Build.Dll, {
    Import-Module $global:PsPowerBiManifest.FullName
}

task PsPowerBi.Doc.Init -If { -Not $global:PsPowerBiDoc.Exists -Or $Force } -Jobs PsPowerBi.Import, {
    New-MarkdownHelp -Module PsPowerBi -OutputFolder $global:PsPowerBiDoc -Force:$Force -ErrorAction Stop
}

task PsPowerBi.Doc -Jobs PsPowerBi.Import, {
    Update-MarkdownHelp -Path $global:PsPowerBiDoc
}

task PsPowerBi.Build.Help -Jobs PsPowerBi.Doc, {
    New-ExternalHelp -Path $global:PsPowerBiDoc -OutputPath $global:PsPowerBiStage\en-US\ -Force
}

task PsPowerBi.Build -If { -Not $global:PsPowerBiManifest.Exists -Or $Force } -Jobs PsPowerBi.Build.Dll, PsPowerBi.Build.Help

task PsPowerBi.Clean {
	remove $PSScriptRoot\..\src\PsPowerBi\bin, $PSScriptRoot\..\src\PsPowerBi\obj
}

task PsPowerBi.Uninstall -If { $global:PsPowerBiInstallDirectory.Exists } -Jobs {
	Remove-Item -Recurse -Force $global:PsPowerBiInstallDirectory.FullName
}

task PsPowerBi.Install -If { -Not $global:PsPowerBiInstallDirectory.Exists -Or $Force } -Jobs PsPowerBi.Build, {
	Get-ChildItem $global:PsPowerBiStage | Copy-Item -Destination $global:PsPowerBiInstallDirectory.FullName -Recurse -Force
}

# Synopsis: Publish the module to PSGallery.
task PsPowerBi.Publish -Jobs PsPowerBi.Install, {

	assert ( $Configuration -eq 'Release' )

	Publish-Module -Name PsPowerBi -NuGetApiKey $NuGetApiKey
}