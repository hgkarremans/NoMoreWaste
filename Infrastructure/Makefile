# Variables
DOCKER_IMAGE=mcr.microsoft.com/mssql/server:2022-latest
CONTAINER_NAME=sqlserver-db
SA_PASSWORD=YourStrongPassword!
SQL_PORT=1433

# .NET project paths (adjust to your project structure)
APPLICATION_PROJECT=./src/Application
IDENTITY_PROJECT=./src/Identity

# Docker container commands
start-db:
	@echo "Starting SQL Server in Docker..."
	@docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$(SA_PASSWORD)" \
		-p $(SQL_PORT):1433 --name $(CONTAINER_NAME) -d $(DOCKER_IMAGE)
	@echo "SQL Server container started."

stop-db:
	@echo "Stopping SQL Server container..."
	@docker stop $(CONTAINER_NAME)
	@echo "SQL Server container stopped."

remove-db:
	@echo "Removing SQL Server container..."
	@docker rm $(CONTAINER_NAME)
	@echo "SQL Server container removed."

# EF Core migrations
migrate-application:
	@echo "Running EF Core migrations for ApplicationDbContext..."
	@dotnet ef database update --project $(APPLICATION_PROJECT) --context ApplicationDbContext
	@echo "Migrations applied for ApplicationDbContext."

migrate-identity:
	@echo "Running EF Core migrations for IdentityDbContext..."
	@dotnet ef database update --project $(IDENTITY_PROJECT) --context NoMoreWasteIdentityDbContext
	@echo "Migrations applied for IdentityDbContext."

# Convenience targets
migrate: migrate-application migrate-identity

reset-db: stop-db remove-db start-db migrate
	@echo "Database reset completed."

# Default target
all: start-db migrate
	@echo "Database started and migrations applied."
