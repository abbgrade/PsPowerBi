requires Configuration

[System.IO.FileInfo] $global:Manifest = "$PSScriptRoot/../src/PsPowerBi/bin/$Configuration/netcoreapp3.1/publish/PsPowerBi.psd1"


# Synopsis: Build project.
task Build {
	exec { dotnet publish ./src/PsPowerBi -c $Configuration }
}

# Synopsis: Remove files.
task Clean {
	remove src/PsPowerBi/bin, src/PsPowerBi/obj
}

# Synopsis: Install the module.
task Install -Jobs Build, {
    $info = Import-PowerShellDataFile $global:Manifest.FullName
    $version = ([System.Version] $info.ModuleVersion)
    $name = $global:Manifest.BaseName
    $defaultModulePath = $env:PsModulePath -split ';' | Select-Object -First 1
    $installPath = Join-Path $defaultModulePath $name $version.ToString()
    New-Item -Type Directory $installPath -Force | Out-Null
    Get-ChildItem $global:Manifest.Directory | Copy-Item -Destination $installPath -Recurse -Force
}

# Synopsis: Publish the module to PSGallery.
task Publish -Jobs Install, {

	assert ( $Configuration -eq 'Release' )

	Publish-Module -Name PsPowerBi -NuGetApiKey $NuGetApiKey
}