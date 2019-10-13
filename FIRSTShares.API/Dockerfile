FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY published/FIRSTShares.API.dll ./
RUN dotnet publish .

ENTRYPOINT ["dotnet", "FIRSTShares.API.dll"]