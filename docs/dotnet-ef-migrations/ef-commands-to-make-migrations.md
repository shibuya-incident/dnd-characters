# 1. Verify .NET
dotnet --version

# 2. Verify Docker
docker --version
docker compose version

# 3. Start PostgreSQL
docker compose up -d

# 4. Install EF CLI
dotnet tool install --global dotnet-ef

# If already installed:
dotnet tool update --global dotnet-ef

# 5. Verify EF CLI
dotnet ef --version

# 6. Install PostgreSQL EF provider
dotnet add .\DndCharacters.Infrastructure\ package Npgsql.EntityFrameworkCore.PostgreSQL

# 7. Install EF Design package in startup project
dotnet add .\DndCharacters.API\ package Microsoft.EntityFrameworkCore.Design

# 8. Restore
dotnet restore

# 9. Create migration
dotnet ef migrations add InitialCreate `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\

# 10. Apply migration
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\

# 11. Create another migration later
dotnet ef migrations add MigrationName `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\

# 12. Apply latest migrations
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\

# 13. Remove latest unapplied migration
dotnet ef migrations remove `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\

# 14. Run migration command with verbose logs
dotnet ef migrations add MigrationName `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\ `
  --verbose

# 15. Check packages
dotnet list .\DndCharacters.API\ package
dotnet list .\DndCharacters.API\ package --include-transitive

# 16. Reset local database completely
docker compose down -v
docker compose up -d

# 17. Reapply all migrations to empty DB
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\