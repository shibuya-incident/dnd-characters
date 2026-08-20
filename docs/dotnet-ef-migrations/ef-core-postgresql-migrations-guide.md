# EF Core + PostgreSQL Migrations in ASP.NET Core

A practical guide to setting up Entity Framework Core migrations from scratch in an ASP.NET Core API using PostgreSQL.

This guide assumes a Clean Architecture-style solution with projects similar to:

```text
DndCharacters/
├── DndCharacters.API/
├── DndCharacters.Application/
├── DndCharacters.Domain/
└── DndCharacters.Infrastructure/
```

The important idea is:

- `Domain` contains the entities.
- `Application` contains use cases and abstractions.
- `Infrastructure` contains EF Core, the `DbContext`, repositories, and migrations.
- `API` is the startup project.

---

# 1. Prerequisites

## Install the .NET SDK

Install the .NET SDK version used by the project.

For a .NET 10 project, verify that the SDK is available with:

```powershell
dotnet --version
```

You should get a version similar to:

```text
10.x.x
```

You can also list every installed SDK:

```powershell
dotnet --list-sdks
```

If `dotnet` is not recognized, install the .NET SDK from the official Microsoft .NET website and reopen the terminal afterward.

---

# 2. Install Docker

For local development, PostgreSQL can run inside Docker while the ASP.NET Core API runs normally from Visual Studio, Rider, or `dotnet run`.

Verify Docker:

```powershell
docker --version
```

Verify Docker Compose:

```powershell
docker compose version
```

Make sure Docker Desktop is running before starting PostgreSQL.

---

# 3. Create the PostgreSQL Docker Compose file

At the repository root, create:

```text
docker-compose.yml
```

Example:

```yaml
services:
  db:
    image: postgres:18
    container_name: dnd-characters-dev

    environment:
      POSTGRES_DB: dnd_characters
      POSTGRES_USER: admin
      POSTGRES_PASSWORD: admin

    ports:
      - "5432:5432"

    volumes:
      - postgres_data:/var/lib/postgresql

volumes:
  postgres_data:
```

Start PostgreSQL:

```powershell
docker compose up -d
```

Check that it is running:

```powershell
docker compose ps
```

View the logs:

```powershell
docker compose logs db
```

Stop the container:

```powershell
docker compose down
```

Delete the container **and the database volume**:

```powershell
docker compose down -v
```

Be careful with `-v`: it deletes the persisted database data.

---

# 4. PostgreSQL connection details

With the previous Docker Compose configuration:

```text
Host: localhost
Port: 5432
Database: dnd_characters
Username: admin
Password: admin
```

A typical connection string is:

```text
Host=localhost;Port=5432;Database=dnd_characters;Username=admin;Password=admin
```

For example, in:

```text
DndCharacters.API/appsettings.Development.json
```

```json
{
  "ConnectionStrings": {
    "Database": "Host=localhost;Port=5432;Database=dnd_characters;Username=admin;Password=admin"
  }
}
```

## Common PostgreSQL connection error

If PostgreSQL says:

```text
FATAL: database "admin" does not exist
```

authentication is probably already working.

The client is simply trying to connect to a database named `admin`.

Set the database explicitly to:

```text
dnd_characters
```

---

# 5. Install the EF Core PostgreSQL provider

The Infrastructure project needs the Npgsql EF Core provider.

From the solution directory:

```powershell
dotnet add .\DndCharacters.Infrastructure\ package Npgsql.EntityFrameworkCore.PostgreSQL
```

For a .NET 10 project, use a provider version compatible with EF Core 10.

It is a good idea to keep your EF Core-related versions aligned as closely as possible.

---

# 6. Install EF Core Design

The EF CLI tooling requires:

```text
Microsoft.EntityFrameworkCore.Design
```

Because the API is the startup project, the API project must reference the Design package.

Example `DndCharacters.API.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.10" />
    <PackageReference Include="Microsoft.OpenApi" Version="2.11.0" />
    <PackageReference Include="Scalar.AspNetCore" Version="2.16.18" />

    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\DndCharacters.Application\DndCharacters.Application.csproj" />
    <ProjectReference Include="..\DndCharacters.Infrastructure\DndCharacters.Infrastructure.csproj" />
  </ItemGroup>

</Project>
```

Then restore:

```powershell
dotnet restore
```

Verify the package:

```powershell
dotnet list .\DndCharacters.API\ package
```

You should see:

```text
Microsoft.EntityFrameworkCore.Design
```

---

# 7. Install the `dotnet-ef` CLI tool

Install globally:

```powershell
dotnet tool install --global dotnet-ef
```

If it is already installed:

```powershell
dotnet tool update --global dotnet-ef
```

Verify:

```powershell
dotnet ef --version
```

If PowerShell says:

```text
dotnet-ef does not exist
```

and you just installed it, close and reopen the terminal so the global tools path is refreshed.

---

# 8. Create the DbContext

In Infrastructure:

```text
DndCharacters.Infrastructure/
└── Persistence/
    └── AppDbContext.cs
```

Example:

```csharp
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DndCharacters.Infrastructure.Persistence;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
```

`ApplyConfigurationsFromAssembly` automatically discovers classes implementing:

```csharp
IEntityTypeConfiguration<T>
```

inside the Infrastructure assembly.

---

# 9. Register the DbContext in the API

The API startup must configure the database provider.

Example:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Database"));
});
```

You need:

```csharp
using Microsoft.EntityFrameworkCore;
```

and the API must reference Infrastructure.

---

# 10. Example domain entities

Example `Shop`:

```csharp
public class Shop : Entity
{
    public required string Name { get; set; }
    public string? ProfileImage { get; set; }
    public ShopType ShopType { get; set; }
    public required string OwnerName { get; set; }

    public ICollection<Item> Items { get; set; } = [];

    public static Shop Create(
        string name,
        string? profileImage,
        ShopType shopType,
        string ownerName)
    {
        return new Shop
        {
            Name = name,
            ProfileImage = profileImage,
            ShopType = shopType,
            OwnerName = ownerName
        };
    }
}
```

Example `Item`:

```csharp
public class Item : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    public ItemType ItemType { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; private set; }

    public bool IsOutOfStock => Stock == 0;

    public int ShopId { get; set; }
}
```

For monetary values, prefer:

```csharp
decimal
```

instead of:

```csharp
double
```

because floating-point values are not ideal for money.

---

# 11. Create entity type configurations

A clean Infrastructure structure could be:

```text
Persistence/
├── AppDbContext.cs
└── Configurations/
    ├── ShopConfiguration.cs
    └── ItemConfiguration.cs
```

## ShopConfiguration

```csharp
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.Configurations;

internal sealed class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("shops");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ProfileImage)
            .HasMaxLength(500);

        builder.Property(x => x.ShopType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.OwnerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.ShopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## ItemConfiguration

```csharp
using DndCharacters.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DndCharacters.Infrastructure.Persistence.Configurations;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ItemType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.Stock)
            .IsRequired();

        builder.Property(x => x.ShopId)
            .IsRequired();

        builder.Ignore(x => x.IsOutOfStock);
    }
}
```

---

# 12. Saving enums as strings

EF normally stores enums using their numeric value.

For example:

```text
0
1
2
```

To store their names instead:

```csharp
builder.Property(x => x.ShopType)
    .HasConversion<string>()
    .HasMaxLength(50)
    .IsRequired();
```

and:

```csharp
builder.Property(x => x.ItemType)
    .HasConversion<string>()
    .HasMaxLength(50)
    .IsRequired();
```

The database will then contain values such as:

```text
Magic
Blacksmith
Weapon
Potion
Armor
```

## Important trade-off

If an enum value is renamed in C#:

```csharp
MagicItem
```

to:

```csharp
MagicalItem
```

existing rows still contain:

```text
MagicItem
```

Changing enum names can therefore become a database migration concern.

---

# 13. Create the first migration

From the directory containing the projects:

```powershell
dotnet ef migrations add ShopInitialMigration `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

The important distinction is:

```text
--project
```

is the project where migrations should be generated.

For this architecture:

```text
DndCharacters.Infrastructure
```

And:

```text
--startup-project
```

is the executable project EF uses to start the application and resolve the `DbContext`.

For this architecture:

```text
DndCharacters.API
```

---

# 14. Why EF needs the startup project

At design time, EF needs to create the `AppDbContext`.

To do that, it starts the API and uses its:

- Dependency Injection container
- Configuration
- connection strings
- Infrastructure registration
- Npgsql provider registration

So this is normal:

```text
Infrastructure
    contains migrations

API
    is used to bootstrap EF tooling
```

This is why `Microsoft.EntityFrameworkCore.Design` must also be available from the startup project.

---

# 15. Inspect the generated migration

Do not blindly apply migrations.

Open the generated migration first.

You should see operations such as:

```csharp
migrationBuilder.CreateTable(...)
```

For enum-to-string mappings, check that the generated PostgreSQL columns are string-based rather than integers.

Check:

- table names
- primary keys
- foreign keys
- nullability
- string lengths
- decimal precision
- delete behavior
- enum column types

---

# 16. Apply the migration to PostgreSQL

Once the migration looks correct:

```powershell
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

EF will connect to PostgreSQL and execute every migration that has not yet been applied.

Afterward, PostgreSQL should contain tables similar to:

```text
shops
items
__EFMigrationsHistory
```

The table:

```text
__EFMigrationsHistory
```

is maintained by EF Core and records which migrations have already been applied.

---

# 17. Normal migration workflow

From now on, the workflow is:

```text
Change entity/configuration
        ↓
Create migration
        ↓
Review migration
        ↓
Apply migration
```

Example:

```powershell
dotnet ef migrations add AddShopProfileImage `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

Then:

```powershell
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

Use descriptive migration names, for example:

```text
InitialCreate
AddShopProfileImage
AddItemStock
ChangePriceToDecimal
AddShopType
CreateInventoryTables
```

---

# 18. Remove the latest migration

If you created a migration and realize it is wrong **before applying it**:

```powershell
dotnet ef migrations remove `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

Then fix the configuration and generate it again.

Avoid manually deleting migration files unless you understand exactly what EF has generated.

---

# 19. Default seed data with HasData

For deterministic default/reference data, EF Core can include seed data inside migrations.

Example:

```csharp
builder.HasData(
    new
    {
        Id = 1,
        Name = "The Arcane Forge",
        ProfileImage = (string?)null,
        ShopType = ShopType.Magic,
        OwnerName = "Garrick"
    });
```

And for an item:

```csharp
builder.HasData(
    new
    {
        Id = 1,
        Name = "Potion of Healing",
        Description = "A red potion that restores health.",
        ItemType = ItemType.Potion,
        Price = 50m,
        Stock = 10,
        ShopId = 1
    });
```

Anonymous objects are convenient because seed data may need to set properties that have private setters in the domain model.

After adding seed data, generate another migration:

```powershell
dotnet ef migrations add AddDefaultShopData `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

Then:

```powershell
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

The generated migration will normally contain:

```csharp
migrationBuilder.InsertData(...)
```

---

# 20. HasData vs development data seeder

`HasData` is good for:

- reference data
- deterministic default values
- small sets of demo data
- data that should be part of database migration history

It is less suitable for:

- hundreds of generated records
- Faker-generated data
- environment-specific test fixtures
- complex object creation logic

For richer development-only data, prefer something like:

```text
Infrastructure/
└── Persistence/
    └── Seeding/
        └── DevelopmentDataSeeder.cs
```

and execute it only in the Development environment.

---

# 21. Generated IDs and EF Change Tracker

When inserting an entity:

```csharp
Shop shop = Shop.Create(...);

dbContext.Shops.Add(shop);
```

the entity normally still has:

```csharp
shop.Id == 0
```

because no SQL has been executed yet.

After:

```csharp
await dbContext.SaveChangesAsync();
```

PostgreSQL generates the ID and EF Core updates the tracked object automatically.

Conceptually:

```text
Shop.Create()
    ↓
Id = 0

DbSet.Add(shop)
    ↓
Entity state = Added

SaveChangesAsync()
    ↓
INSERT INTO shops ...

PostgreSQL generates ID
    ↓
EF receives generated value

shop.Id = generated ID
```

You do not need to manually reload the entity.

---

# 22. Repository + Unit of Work approach

A repository does not need to return the generated ID.

Example:

```csharp
internal sealed class ShopRepository(AppDbContext dbContext)
    : IShopRepository
{
    public void Add(Shop shop)
    {
        dbContext.Shops.Add(shop);
    }
}
```

Then the application use case controls when changes are committed:

```csharp
Shop shop = Shop.Create(
    request.Name,
    request.ProfileImage,
    request.ShopType,
    request.OwnerName);

shopRepository.Add(shop);

await unitOfWork.SaveChangesAsync();

return new CreateShopResponse(
    shop.Id,
    shop.Name,
    shop.ProfileImage,
    shop.ShopType,
    shop.OwnerName);
```

After `SaveChangesAsync`, `shop.Id` already contains the generated PostgreSQL identity value.

The responsibility split is:

```text
Repository
    manages persistence operations for entities

Unit of Work
    decides when tracked changes are committed
```

EF Core's `DbContext` already behaves as a Unit of Work and Change Tracker. A custom `IUnitOfWork` abstraction is mainly useful to keep the Application layer independent of EF Core.

Example:

```csharp
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
```

Then:

```csharp
public sealed class AppDbContext : DbContext, IUnitOfWork
{
}
```

---

# 23. Add vs AddAsync

For normal EF Core inserts:

```csharp
dbContext.Shops.Add(shop);
```

is usually sufficient.

`AddAsync` normally does not perform database I/O.

Its asynchronous behavior is mainly useful for uncommon value generators that themselves require asynchronous access.

For normal PostgreSQL identity/sequence-generated keys, this is generally clean:

```csharp
public void Add(Shop shop)
{
    dbContext.Shops.Add(shop);
}
```

and later:

```csharp
await unitOfWork.SaveChangesAsync();
```

---

# 24. Troubleshooting

## Error: dotnet-ef does not exist

Example:

```text
Could not execute because the specified command or file was not found.

dotnet-ef does not exist.
```

Install the tool:

```powershell
dotnet tool install --global dotnet-ef
```

or update it:

```powershell
dotnet tool update --global dotnet-ef
```

Verify:

```powershell
dotnet ef --version
```

---

## Error: Build failed, but Visual Studio builds successfully

Run the command with verbose output:

```powershell
dotnet ef migrations add InitialCreate `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\ `
  --verbose
```

EF performs more than just a normal compile.

It also needs to:

- locate the `DbContext`
- load EF design-time services
- start the startup project
- resolve the service provider
- load the database provider
- generate migration code

The verbose output usually reveals the real design-time error.

---

## Error: startup project does not reference Microsoft.EntityFrameworkCore.Design

Example:

```text
Your startup project 'DndCharacters.API' doesn't reference
Microsoft.EntityFrameworkCore.Design.
```

Add:

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.4">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
</PackageReference>
```

to the startup project.

Then:

```powershell
dotnet restore
```

Verify:

```powershell
dotnet list .\DndCharacters.API\ package
```

Then rerun the migration.

---

## PostgreSQL authentication failed

Example:

```text
FATAL: password authentication failed for user "admin"
```

Check your Docker variables carefully.

Correct:

```yaml
POSTGRES_DB: dnd_characters
POSTGRES_USER: admin
POSTGRES_PASSWORD: admin
```

Typos such as:

```yaml
POSRTGRES_DB
POSTGRE_USER
```

will not be recognized by the official PostgreSQL Docker image.

If PostgreSQL was already initialized with the wrong values, fixing the YAML alone may not be enough because the existing volume contains the old database initialization.

For a disposable local database:

```powershell
docker compose down -v
docker compose up -d
```

---

## Database does not exist

Example:

```text
FATAL: database "admin" does not exist
```

Make sure the client uses:

```text
Database=dnd_characters
```

rather than defaulting the database name to the username.

---

# 25. Useful diagnostic commands

Build API:

```powershell
dotnet build .\DndCharacters.API\
```

Build Infrastructure:

```powershell
dotnet build .\DndCharacters.Infrastructure\
```

Restore:

```powershell
dotnet restore
```

List direct packages:

```powershell
dotnet list .\DndCharacters.API\ package
```

List direct and transitive packages:

```powershell
dotnet list .\DndCharacters.API\ package --include-transitive
```

Check EF CLI:

```powershell
dotnet ef --version
```

Create migration with diagnostics:

```powershell
dotnet ef migrations add TestMigration `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\ `
  --verbose
```

---

# 26. Recommended final structure

```text
DndCharacters/
│
├── DndCharacters.API/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── DndCharacters.API.csproj
│
├── DndCharacters.Application/
│   ├── Interfaces/
│   ├── Services/
│   └── Dtos/
│
├── DndCharacters.Domain/
│   ├── Entities/
│   │   ├── Shop.cs
│   │   └── Item.cs
│   └── Enum/
│
├── DndCharacters.Infrastructure/
│   └── Persistence/
│       ├── AppDbContext.cs
│       │
│       ├── Configurations/
│       │   ├── ShopConfiguration.cs
│       │   └── ItemConfiguration.cs
│       │
│       ├── Migrations/
│       │
│       ├── Seeding/
│       │
│       └── Shops/
│           └── ShopRepository.cs
│
└── docker-compose.yml
```

---

# 27. Complete first-time checklist

- [ ] Install the correct .NET SDK
- [ ] Verify `dotnet --version`
- [ ] Install Docker Desktop
- [ ] Create `docker-compose.yml`
- [ ] Start PostgreSQL with `docker compose up -d`
- [ ] Configure the connection string
- [ ] Install `Npgsql.EntityFrameworkCore.PostgreSQL`
- [ ] Add `Microsoft.EntityFrameworkCore.Design` to the startup project
- [ ] Install the `dotnet-ef` global tool
- [ ] Create `AppDbContext`
- [ ] Register `AppDbContext` with `UseNpgsql`
- [ ] Create `IEntityTypeConfiguration<T>` mappings
- [ ] Configure enum-to-string conversions if desired
- [ ] Run `dotnet restore`
- [ ] Create the first migration
- [ ] Review the generated migration
- [ ] Run `dotnet ef database update`
- [ ] Verify the tables in PostgreSQL
- [ ] Add deterministic seed data if needed
- [ ] Create a migration for the seed
- [ ] Apply the new migration

---

# 28. Command cheat sheet

## Start PostgreSQL

```powershell
docker compose up -d
```

## Install EF CLI

```powershell
dotnet tool install --global dotnet-ef
```

## Create migration

```powershell
dotnet ef migrations add MigrationName `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

## Apply migrations

```powershell
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

## Remove latest unapplied migration

```powershell
dotnet ef migrations remove `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

## Diagnose migration problems

```powershell
dotnet ef migrations add MigrationName `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\ `
  --verbose
```

## Reset local PostgreSQL database

```powershell
docker compose down -v
docker compose up -d
dotnet ef database update `
  --project .\DndCharacters.Infrastructure\ `
  --startup-project .\DndCharacters.API\
```

---

# Summary

For a Clean Architecture ASP.NET Core API using PostgreSQL:

```text
Domain
    owns the entities

Infrastructure
    owns EF Core mappings, DbContext, repositories and migrations

API
    acts as the startup project

PostgreSQL
    runs independently, for example through Docker
```

The essential EF migration workflow is:

```text
Entity/configuration change
        ↓
dotnet ef migrations add
        ↓
Review migration
        ↓
dotnet ef database update
        ↓
PostgreSQL schema updated
```

Keep database concerns in Infrastructure, use the API only as the design-time startup host, review every generated migration before applying it, and let `SaveChangesAsync` act as the point where EF commits tracked changes and retrieves generated database IDs.
