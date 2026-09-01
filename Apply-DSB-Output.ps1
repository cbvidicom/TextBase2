[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

$rootPath   = $PSScriptRoot
$outputPath = Join-Path $rootPath 'Data Services Builder Output'

$directoryMappings = [ordered]@{
    'Application'    = 'Textbase.Application'
    'Contracts'      = 'Textbase.Contracts'
    'Domain'         = 'Textbase.Domain'
    'Host'           = 'Textbase.Host'
    'Infrastructure' = 'Textbase.Infrastructure'
    'Integration'    = 'Textbase.Integration'
}

function Move-DirectoryContent {
    param(
        [Parameter(Mandatory)]
        [string] $SourcePath,

        [Parameter(Mandatory)]
        [string] $DestinationPath
    )

    if (-not (Test-Path -LiteralPath $SourcePath -PathType Container)) {
        Write-Warning "Source directory does not exist: $SourcePath"
        return
    }

    if (-not (Test-Path -LiteralPath $DestinationPath -PathType Container)) {
        New-Item -ItemType Directory -Path $DestinationPath | Out-Null
    }

    foreach ($item in Get-ChildItem -LiteralPath $SourcePath -Force) {
        $destinationItemPath = Join-Path $DestinationPath $item.Name

        if ($item.PSIsContainer) {
            Move-DirectoryContent `
                -SourcePath $item.FullName `
                -DestinationPath $destinationItemPath
        }
        else {
            Move-Item `
                -LiteralPath $item.FullName `
                -Destination $destinationItemPath `
                -Force
        }
    }
}

if (-not (Test-Path -LiteralPath $outputPath -PathType Container)) {
    throw "Output directory does not exist: $outputPath"
}

foreach ($mapping in $directoryMappings.GetEnumerator()) {
    $sourcePath      = Join-Path $outputPath $mapping.Key
    $destinationPath = Join-Path $rootPath $mapping.Value

    Write-Host "Moving '$sourcePath' to '$destinationPath'..."
    Move-DirectoryContent `
        -SourcePath $sourcePath `
        -DestinationPath $destinationPath
}

# Delete all empty directories below "Data Services Builder Output",
# processing the deepest directories first. The output directory itself remains.
Get-ChildItem -LiteralPath $outputPath -Directory -Recurse -Force |
    Sort-Object { $_.FullName.Length } -Descending |
    ForEach-Object {
        $remainingItems = Get-ChildItem -LiteralPath $_.FullName -Force

        if ($remainingItems.Count -eq 0) {
            Remove-Item -LiteralPath $_.FullName -Force
        }
    }

Write-Host 'Data Services Builder output was moved successfully.'