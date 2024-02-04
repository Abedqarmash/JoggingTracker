#
# EfAddMigrationStep.ps1
#
param(
    [string]$migrationName
)

dotnet ef migrations add -c "AppDbContext" $migrationName -v
dotnet ef database update
