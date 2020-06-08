param(
   [parameter(Position=0)][string]$filename,
   [parameter(Position=1)][string]$environment
)

$result = @()
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

   $calculated = [pscustomobject]@{key = $_.name; value = $null; error = $false; toggle = $false}
   if ($envProp -and -not $error) {
      $calculated.value = [string]$_.$envProp
   } else {
      $calculated.error = $true
   }
   if ($_.toggle) {
      $calculated.toggle = [string]$_.toggle
    }
   $result += $calculated
}

$result | ForEach-Object {
   $c = $_
   if ($_.value) {
      Write-Host "$($_.key)=$($_.value)"
   } elseif (-not ($_.toggle -and ($result | Where-Object {$_.key -eq $c.toggle}).value -eq "false")) {
      Write-Host error $_.key
   }
}

