

[CmdletBinding(PositionalBinding = $false)]
param(
    [ValidateSet('Debug', 'Release')]
    $Configuration = 'Release'
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

. .\Functions.ps1

$arguments = New-Object System.Collections.ArrayList

[void]$arguments.Add("-p:Configuration=$Configuration")

exec dotnet build @arguments

write-host -f green 'Build Done!'
