set rootFolderAbsolutePath="%~dp0..\.." 
set templateFolderRelativePath="Solution\.template.config"

:: Copy server template 
cd %rootFolderAbsolutePath%
cd %templateFolderRelativePath%
copy /y "template.Server.json" "template.json"

:: Build solution 
cd %rootFolderAbsolutePath%
dotnet pack -c Release "Template.Server.csproj" -o "artifacts"

:: Delete template for safety 
cd %rootFolderAbsolutePath%
cd %templateFolderRelativePath%
del "template.json"

pause 