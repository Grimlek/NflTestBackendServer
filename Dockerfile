FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AngularTestBackendServer/AngularTestBackendServer.csproj", "AngularTestBackendServer/"]
COPY ["AngularTestBackendServer.Core/AngularTestBackendServer.Core.csproj", "AngularTestBackendServer.Core/"]
COPY ["AngularTestBackendServer.Data/AngularTestBackendServer.Data.csproj", "AngularTestBackendServer.Data/"]
RUN dotnet restore "AngularTestBackendServer/AngularTestBackendServer.csproj"
COPY . .
WORKDIR "/src/AngularTestBackendServer"
RUN dotnet build "AngularTestBackendServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AngularTestBackendServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AngularTestBackendServer.dll"]
