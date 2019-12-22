FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY bin/Debug/netcoreapp2.1/publish .
ENTRYPOINT ["dotnet", "dotnetcore.study.dll"]