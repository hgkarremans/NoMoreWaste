# Variables
DOCKER_IMAGE=mcr.microsoft.com/mssql/server:2022-latest
CONTAINER_NAME=sqlserver-db
SA_PASSWORD=YourStrongPassword!
SQL_PORT=1433
APPLICATION_DB=ApplicationDbContext
IDENTITY_DB=NoMoreWasteIdentityDbContext

# Project paths for EF Core migrations
PROJECT_FILE=./Infrastructure.csproj

# Docker container commands
start-db:
	@echo "Starting SQL Server in Docker..."
	@docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$(SA_PASSWORD)" \
		-p $(SQL_PORT):1433 --name $(CONTAINER_NAME) -d $(DOCKER_IMAGE) || \
		{ echo "Container $(CONTAINER_NAME) is already running"; exit 0; }

	# Wait for SQL Server to start
	@sleep 10

	# Check if sqlcmd tools are installed, install them if not
	@echo "Checking if sqlcmd tools are installed..."
	@docker exec -it $(CONTAINER_NAME) bash -c "command -v /opt/mssql-tools/bin/sqlcmd || { echo 'Installing sqlcmd tools...'; apt-get update && apt-get install -y mssql-tools unixodbc-dev; }"

	# Create the databases
	@echo "Creating databases..."
	@docker exec -it $(CONTAINER_NAME) /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $(SA_PASSWORD) -Q \
		"IF DB_ID('$(APPLICATION_DB)') IS NULL CREATE DATABASE [$(APPLICATION_DB)];"
	@docker exec -it $(CONTAINER_NAME) /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $(SA_PASSWORD) -Q \
		"IF DB_ID('$(IDENTITY_DB)') IS NULL CREATE DATABASE [$(IDENTITY_DB)];"
	@echo "Databases $(APPLICATION_DB) and $(IDENTITY_DB) created."

stop-db:
	@echo "Stopping SQL Server container..."
	@docker stop $(CONTAINER_NAME)
	@echo "SQL Server container stopped."

remove-db:
	@echo "Removing SQL Server container..."
	@docker rm -f $(CONTAINER_NAME)  # Force remove the container
	@echo "SQL Server container removed."

migrate:
	@echo "Running EF Core migrations for ApplicationDbContext..."
	@dotnet ef migrations add --project Infrastructure.csproj --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.ApplicationDbContext --configuration Debug devApplication1 --output-dir Migrations
	@echo "Migrations applied for ApplicationDbContext."

	@echo "Running EF Core migrations for IdentityDbContext..."
	@dotnet ef migrations add --project Infrastructure.csproj --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.NoMoreWasteIdentityDbContext --configuration Debug IdentityInit2 --output-dir Migrations
	@echo "Migrations applied for IdentityDbContext."

migrate-application:
	@echo "Running EF Core migrations for ApplicationDbContext..."
	@dotnet ef database update --project $(PROJECT_FILE) --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.ApplicationDbContext
	@echo "Migrations applied for ApplicationDbContext."

migrate-identity:
	@echo "Running EF Core migrations for IdentityDbContext..."
	@dotnet ef database update --project $(PROJECT_FILE) --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.NoMoreWasteIdentityDbContext
	@echo "Migrations applied for IdentityDbContext."

# New target to remove all migrations
remove-migrations:
	@echo "Removing all migrations for ApplicationDbContext..."
	@while dotnet ef migrations remove --project $(PROJECT_FILE) --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.ApplicationDbContext; do :; done
	@echo "All migrations removed for ApplicationDbContext."

	@echo "Removing all migrations for IdentityDbContext..."
	@while dotnet ef migrations remove --project $(PROJECT_FILE) --startup-project ../UI/UI.csproj --context Infrastructure.ContextClasses.NoMoreWasteIdentityDbContext; do :; done
	@echo "All migrations removed for IdentityDbContext."

# Convenience targets
reset-db: stop-db remove-db start-db migrate
	@echo "Database reset completed."

# Default target
all: start-db migrate migrate-application migrate-identity
	@echo "Database started and migrations applied."
