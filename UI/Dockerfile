# Use the official .NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files for all projects
COPY ["UI/UI.csproj", "UI/"]
COPY ["Domain/DomainServices/DomainServices.csproj", "Domain/DomainServices/"]
COPY ["Domain/Domain/Domain.csproj", "Domain/Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["VoedselApi/VoedselApi.csproj", "VoedselApi/"]

# Copy the rest of the application files (before restoring)
COPY . .

# Restore dependencies for all projects
RUN dotnet restore "UI/UI.csproj"

# Build the UI application
WORKDIR "/src/UI"
RUN dotnet build "UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Build the VoedselApi application
WORKDIR "/src/VoedselApi"
RUN dotnet build "VoedselApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the applications
FROM build AS publish
RUN dotnet publish "UI/UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish/UI /p:UseAppHost=false
RUN dotnet publish "VoedselApi/VoedselApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish/VoedselApi /p:UseAppHost=false

# Use the official ASP.NET runtime as a base image for running the applications
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish/UI ./UI
COPY --from=publish /app/publish/VoedselApi ./VoedselApi  
EXPOSE 8080
EXPOSE 8081

# Choose which application to run as the entry point
ENTRYPOINT ["dotnet", "UI/UI.dll"]
