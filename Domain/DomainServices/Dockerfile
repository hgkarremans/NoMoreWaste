FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Domain/DomainServices/DomainServices.csproj", "Domain/DomainServices/"]
COPY ["Domain/Domain/Domain.csproj", "Domain/Domain/"]
RUN dotnet restore "Domain/DomainServices/DomainServices.csproj"
COPY . .
WORKDIR "/src/Domain/DomainServices"
RUN dotnet build "DomainServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DomainServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainServices.dll"]
