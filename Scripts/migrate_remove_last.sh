#!/bin/bash

# Remove the last EF Core migration (rollback)
dotnet ef migrations remove

# Optional: Update the database to the previous migration state
dotnet ef database update
