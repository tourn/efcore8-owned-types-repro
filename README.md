This repo seeks to reproduce a perceived bug with the EF Core 8.0.2 migration generator regarding owned types.

The model is a cut-down version of the model we are using in production. It was working fine with EF Core 7,
But starts acting strange with EF Core 8.

The following commands runs `dotnet ef migrations add` twice. The first creates an the initial snapshot and some migrations.
For the second, we'd expect the snapshot to be untouched, and the created migration to contain no operations.

```
cd projects\Repo
rm -r .\Repro\Migrations\; 
dotnet ef migrations add --project .\Repro\Repro.csproj --context ReproDbContext Test; 
dotnet ef migrations add --project .\Repro\Repro.csproj --context ReproDbContext TestAgain
```

However, while the second migration command does not touch the snapshot, the migration contains a "drop key" and
a "drop column" commands for a column that does not exist:

```csharp
            migrationBuilder.DropForeignKey(
                name: "FK_Staffings_Staffings_StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings");

            migrationBuilder.DropColumn(
                name: "StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings");
```

This has the following effects:
* The *TestAgain* migration fails to apply
* Every subsequent migration will contain these commands, since the snapshot does not reflect these changes

## Scenarios
I tested 3 scenarios and stored the output in the *results/* folder.

* owned-1prop-broken: The current repo state, where the problem is visible
* owned-2prop-okay: When another prop is added to the *Staffing* model, the problem disappears
* complex-1prop-okay: When switching to complex types, the problem disappears
