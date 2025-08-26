#!/bin/bash
set -e

echo "Applying all pending migrations..."
dotnet ef database update --project api.csproj --startup-project api.csproj

echo "Database updated successfully!"
