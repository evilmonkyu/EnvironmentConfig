param(
   [parameter(Position=0)][string]$filename,
   [parameter(Position=1)][string]$environment
)

(Get-Content $filename | ConvertFrom-Json) | ForEach-Object {
   $envProp = $null
   $variable = $_
   $error = $false
   $environment,"default" | ForEach-Object {
      $e = $_
      $r = ($variable.psobject.properties | Where-Object { $_.Name -match "(,|^)$e(,|$)" })
      if ($r.Length -gt 1) {
         $error = $true
      }
      if (-not $envProp -and $r.Length -eq 1) {
         $envProp = $r[0].Name
      }      
   }   
   if ($envProp -and -not $error) {
      Write-Host "$($_.name)=$($_.$envProp)"
   } else {
      Write-Host error $_.name         
   }
}
