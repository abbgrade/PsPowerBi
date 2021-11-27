<#
.Synopsis
	Build script <https://github.com/nightroman/Invoke-Build>
#>

param(
	[ValidateSet('Debug', 'Release')]
	[string] $Configuration = 'Debug',

	[switch] $Force,

	[string] $NuGetApiKey = $env:nuget_apikey
)

. $PSScriptRoot\tasks\Build.Tasks.ps1

task Build -Jobs PsPowerBi.Build
task Clean -Jobs PsPowerBi.Clean
task Doc -Jobs PsPowerBi.Doc
task Install -Jobs PsPowerBi.Install
task Publish -Jobs PsPowerBi.Publish
task Uninstall -Jobs PsPowerBi.Uninstall