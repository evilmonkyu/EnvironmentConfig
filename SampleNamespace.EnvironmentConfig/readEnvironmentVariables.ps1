param(
   [parameter(Position=0)][string]$filename,
   [parameter(Position=1)][string]$environment
)

(Get-Content $filename | ConvertFrom-Json) | ForEach-Object {
   $envProp = $null
   $variable = $_
   $environment,"default" | ForEach-Object {
      $e = $_
      $r = ($variable.psobject.properties | Where-Object { $_.Name -match "(,|^)$e(,|$)" })
      if (-not $envProp -and $r.Length -eq 1) {
         $envProp = $r[0].Name
      }
   }   
   if ($env) {
      Write-Host "$($_.name)=$($_.$envProp)"
   }
}
