# Database setup

This project uses EF Core + SQL Server with `AggrazeDbContext`.

## Prerequisites

1. SQL Server LocalDB or SQL Server Express running locally.
2. .NET SDK that matches the project target framework.
3. `dotnet-ef` tool installed:

```powershell
dotnet tool install --global dotnet-ef
```

## Connection string

The default connection string is in:

- `Aggraze.WebApi/appsettings.json`
- `Aggraze.WebApi/appsettings.Development.json`

Override it with user-secrets or environment variables for local/private values.

## Create first migration

From the repository root:

```powershell
dotnet ef migrations add InitialIdentity `
  --project backend/Aggraze.Infrastructure/Aggraze.Infrastructure.csproj `
  --startup-project backend/Aggraze.WebApi/Aggraze.WebApi.csproj `
  --context AggrazeDbContext `
  --output-dir Migrations
```

## Apply migration

```powershell
dotnet ef database update `
  --project backend/Aggraze.Infrastructure/Aggraze.Infrastructure.csproj `
  --startup-project backend/Aggraze.WebApi/Aggraze.WebApi.csproj `
  --context AggrazeDbContext
```

The API also runs `Database.Migrate()` on startup, so pending migrations are applied automatically when the app starts.
