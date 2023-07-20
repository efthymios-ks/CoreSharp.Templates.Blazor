set rootFolderAbsolutePath="%~dp0..\.." 

:: Goto to artifacts folder
cd "%rootFolderAbsolutePath%"
cd "artifacts"

:: Install 
dotnet new install "CoreSharp.Templates.Blazor.Server.7.1.0.nupkg"

pause 