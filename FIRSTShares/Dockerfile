FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app

COPY publish .

ENTRYPOINT ["dotnet", "FIRSTShares.dll"]