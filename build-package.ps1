#Requires -Version 5.1

[CmdletBinding()]
param (
  [string] $Configuration = 'Release',
  [switch] $ContinuousIntegration = $false,
  [switch] $WithBinLog = $false
)

$opts = @( '--nologo' )
if ($WithBinLog) {
  $opts += '-bl'
}

$ErrorActionPreference = 'Stop'

function Complete-BuildStep {
  param (
    [string] $BinLogKeyword,
    [string] $FailureTerm
  )
  if (Test-Path msbuild.binlog) {
    if (Test-Path msbuild.$BinLogKeyword.binlog) {
      Remove-Item -Force msbuild.$BinLogKeyword.binlog
    }
    Rename-Item msbuild.binlog msbuild.$BinLogKeyword.binlog
  }
  Write-Host ''
}

if (Test-Path -Path output) {
  Write-Host 'Cleaning up existing build output...'
  Remove-Item -Recurse -Force output
  Write-Host ''
}

Write-Host "Running package restore..."
dotnet restore $opts
Complete-BuildStep 'restore'
if ($LASTEXITCODE -ne 0) {
  Write-Error "PACKAGE RESTORE FAILED"
}

Write-Host "Building the solution (Configuration: $Configuration)..."
$props = @()
if ($ContinuousIntegration) {
  $props += '-p:ContinuousIntegrationBuild=true'
  $props += '-p:Deterministic=true'
}
if ($Configuration -eq 'Debug') {
  $props += '-p:DebugMessageImportance=high'
}
dotnet build $opts --no-restore "-c:$Configuration" $props
Complete-BuildStep 'build'
if ($LASTEXITCODE -ne 0) {
  Write-Error "SOLUTION BUILD FAILED"
}

Write-Host 'Running tests...'
dotnet test $opts --no-build "-c:$Configuration"
Complete-BuildStep 'test'
if ($LASTEXITCODE -ne 0) {
  Write-Error "UNIT TESTS FAILED"
}

Write-Host 'Creating packages...'
dotnet pack $opts --no-build "-c:$Configuration"
Complete-BuildStep 'pack'
if ($LASTEXITCODE -ne 0) {
  Write-Error "PACKAGE CREATION FAILED"
}
