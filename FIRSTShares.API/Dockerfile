FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY published/FIRSTShares.API.dll ./
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "FIRSTShares.API.dll"]