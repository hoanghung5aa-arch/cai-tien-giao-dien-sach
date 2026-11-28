param(
    [Parameter(Mandatory=$true)][string]$DocPath,
    [Parameter(Mandatory=$true)][string]$PdfPath,
    [Parameter(Mandatory=$true)][string]$LogPath
)
$ErrorActionPreference = "Stop"
$DocPath = (Resolve-Path -LiteralPath $DocPath).Path
$PdfPath = [System.IO.Path]::GetFullPath($PdfPath)
function Log([string]$Message) {
    Add-Content -LiteralPath $LogPath -Value ("{0:o} {1}" -f (Get-Date), $Message)
}
$word = $null
$document = $null
try {
    Log "Creating Word COM instance"
    $word = New-Object -ComObject Word.Application
    Log "Word COM instance created"
    $word.Visible = $false
    $word.DisplayAlerts = 0
    Log "Opening document"
    $document = $word.Documents.Open($DocPath, $false, $false)
    Log "Document opened"
    foreach ($section in $document.Sections) {
        foreach ($headerFooter in $section.Headers) { [void]$headerFooter.Range.Fields.Update() }
        foreach ($headerFooter in $section.Footers) { [void]$headerFooter.Range.Fields.Update() }
    }
    Log "Fields updated"
    [void]$document.Fields.Update()
    foreach ($toc in $document.TablesOfContents) { [void]$toc.Update() }
    $document.Repaginate()
    foreach ($toc in $document.TablesOfContents) { [void]$toc.UpdatePageNumbers() }
    $document.Save()
    $document.ExportAsFixedFormat($PdfPath, 17)
    Log "PDF exported"
}
catch {
    Log ("ERROR " + $_.Exception.ToString())
    throw
}
finally {
    if ($document -ne $null) { $document.Close(0); [void][Runtime.InteropServices.Marshal]::ReleaseComObject($document) }
    if ($word -ne $null) { $word.Quit(); [void][Runtime.InteropServices.Marshal]::ReleaseComObject($word) }
    Log "Word closed"
}
