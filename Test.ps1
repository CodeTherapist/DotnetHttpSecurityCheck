

[CmdletBinding(PositionalBinding = $false)]
param(
    [ValidateSet('Debug', 'Release')]
    $Configuration = 'Debug',
	[switch]
	$CollectCoverage,
	[switch]
	$NoBuild,
	[switch]
	$NoIntegrationTests
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

. .\Functions.ps1

$arguments = New-Object System.Collections.ArrayList

if ($NoBuild) {
	[void]$arguments.Add('--no-build')
}

if($NoIntegrationTests) {
	[void]$arguments.Add('--filter="Category!=Integration"')
}

[void]$arguments.Add("-p:Configuration=$Configuration")

if ($CollectCoverage) { 
	[void]$arguments.Add("-p:CollectCoverage=true")
} 

exec dotnet test "$PSScriptRoot\test\CodeTherapy.HttpSecurityCheck.Tests\CodeTherapy.HttpSecurityCheck.Tests.csproj" `
    @arguments
   
write-host -f green 'Test Done!'
