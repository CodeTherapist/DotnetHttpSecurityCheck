

[CmdletBinding(PositionalBinding = $false)]
param(
    [ValidateSet('Debug', 'Release')]
    $Configuration = 'Release',
    [switch]
    $CollectCoverage,
    [switch]
    $NoIntegrationTests,
    [switch]
    $Pack
)

Set-StrictMode -Version 1
$ErrorActionPreference = 'Stop'

write-host -f Magenta '*** Build ***'
.\Build.ps1 -Configuration $Configuration

write-host -f Magenta '*** Test ***'
.\Test.ps1 -Configuration $Configuration -NoBuild -CollectCoverage:$CollectCoverage -NoIntegrationTests:$NoIntegrationTests

if($Pack) {
    write-host -f Magenta '*** Pack ***'
    .\Pack.ps1 -Configuration $Configuration -NoBuild
}