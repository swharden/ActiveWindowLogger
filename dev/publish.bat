cd ..\src\ActiveWindowLogger
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained false -p:DebugType=embedded -o bin/publish
explorer bin\publish
pause