# run.ps1
# This script runs the Blazor Server application.

$ProjectDir = "wowmenfashions"

Write-Host "Starting the WOWMENFASHIONS Blazor Application..." -ForegroundColor Cyan

# Only push location if we aren't already in the project directory
$pushed = $false
if (Test-Path $ProjectDir) {
    Push-Location $ProjectDir
    $pushed = $true
}

try {
    # Run the application
    dotnet run
}
finally {
    if ($pushed) {
        Pop-Location
    }
}
