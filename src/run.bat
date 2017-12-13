cd .\LitleServer
start "server" dotnet run --config LitleServer.csproj 

cd ..\LiveServer.Client
start "client 1" dotnet run --config LiveServer.Client.csproj
start "client 2" dotnet run --config LiveServer.Client.csproj
start "client 3" dotnet run --config LiveServer.Client.csproj 
start "client 4" dotnet run --config LiveServer.Client.csproj 
start "client 5" dotnet run --config LiveServer.Client.csproj 
start "client 6" dotnet run --config LiveServer.Client.csproj 
start "client 7" dotnet run --config LiveServer.Client.csproj 
start "client 8" dotnet run --config LiveServer.Client.csproj 
start "client 9" dotnet run --config LiveServer.Client.csproj 
start "client 10" dotnet run --config LiveServer.Client.csproj 