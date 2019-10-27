FROM mcr.microsoft.com/dotnet/core/runtime:2.2
COPY tele/bin/Release/netcoreapp2.2/publish/ app/

ENTRYPOINT ["dotnet", "app/tele.dll"]