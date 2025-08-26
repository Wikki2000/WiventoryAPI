#!/bin/bash
set -e  # Exit immediately if a command fails

if [ -z "$1" ]; then
  echo "Usage: $0 <MigrationName>"
  exit 1
fi

MIGRATION_NAME=$1

echo "Adding migration: $MIGRATION_NAME"
dotnet ef migrations add "$MIGRATION_NAME" --project api.csproj --startup-project api.csproj

echo "Updating database..."
dotnet ef database update --project api.csproj --startup-project api.csproj

echo "Migration '$MIGRATION_NAME' added and applied successfully!"

