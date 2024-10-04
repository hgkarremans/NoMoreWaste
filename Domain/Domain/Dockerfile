# Base image for running the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
EXPOSE 8080  

# Build stage with the SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Domain/Domain/Domain.csproj", "Domain/Domain/"]
RUN dotnet restore "Domain/Domain/Domain.csproj"

# Copy the rest of the application files
COPY . .

# Build the application
WORKDIR "/src/Domain/Domain"
RUN dotnet build "Domain.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Domain.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "Domain.dll"]
