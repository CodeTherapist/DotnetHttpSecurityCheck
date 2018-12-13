

[CmdletBinding(PositionalBinding = $false)]
param(
    [ValidateSet('Debug', 'Release')]
    $Configuration = 'Release',
	[switch]
	$NoBuild
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

. .\Functions.ps1

$arguments = New-Object System.Collections.ArrayList

[void]$arguments.Add("-p:Configuration=$Configuration")

if ($NoBuild) {
	[void]$arguments.Add('--no-build')
}

$artifacts = "$PSScriptRoot\artifacts\"

Remove-Item -Recurse $artifacts -ErrorAction Ignore

exec dotnet pack -o $artifacts @arguments

write-host -f green 'Pack done!'

