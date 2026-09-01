$RootDirectory = $PSScriptRoot
$ArchiveName = Split-Path -Path $RootDirectory -Leaf
$ArchivePath = Join-Path -Path $RootDirectory -ChildPath "$ArchiveName.zip"

$MentionedPackages = @(
	"SQL",
	"Textbase.Application",
	"Textbase.Contracts",
	"Textbase.Domain",
	"Textbase.Host",
	"Textbase.Infrastructure",
	"Textbase.Integration"
)

if (Test-Path -LiteralPath $ArchivePath)
{
	Remove-Item -LiteralPath $ArchivePath -Force
}

Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

Write-Host "Creating $ArchiveName.zip ..."

$Archive = [System.IO.Compression.ZipFile]::Open($ArchivePath, [System.IO.Compression.ZipArchiveMode]::Create)

try
{
	foreach ($PackageName in $MentionedPackages)
	{
		$ProjectDirectory = Join-Path -Path $RootDirectory -ChildPath $PackageName

		if (-not (Test-Path -LiteralPath $ProjectDirectory -PathType Container))
		{
			throw "Project directory not found: $ProjectDirectory"
		}

		Write-Host "Packing $PackageName ..."

		foreach ($File in Get-ChildItem -LiteralPath $ProjectDirectory -File -Recurse)
		{
			$RelativePath = $File.FullName.Substring($ProjectDirectory.Length).TrimStart('\', '/')
			$PathParts = $RelativePath -split '[\\/]'

			if ($PathParts -contains "bin" -or $PathParts -contains "obj")
			{
				continue
			}

			$EntryName = "$PackageName/$($RelativePath.Replace('\', '/'))"

			[System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile(
				$Archive,
				$File.FullName,
				$EntryName,
				[System.IO.Compression.CompressionLevel]::Optimal) | Out-Null
		}
	}
}
finally
{
	$Archive.Dispose()
}

Write-Host "Created: $ArchivePath"
