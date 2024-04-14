FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./", "/src/RUDPServer/"]
RUN dotnet restore "./RUDPServer/RUDPServer.csproj"
COPY . .
WORKDIR "/src/RUDPServer"
RUN dotnet build "./RUDPServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/RUDPServer/RUDPServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RUDPServer.dll"]