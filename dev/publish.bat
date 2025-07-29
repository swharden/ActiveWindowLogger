cd ..\src\ActiveWindowLogger
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=false -o bin/publish
explorer bin
pause