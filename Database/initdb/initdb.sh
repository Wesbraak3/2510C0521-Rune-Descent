#!/bin/bash
set -e

echo "Waiting for MongoDB to start..."
sleep 5

MONGO_URI="mongodb://localhost:27017"
DB_NAME="${MONGO_INITDB_DATABASE}"

echo "Applying schema..."
for file in /docker-entrypoint-initdb.d/schema/*.js; do
  echo "Running $file..."
  mongosh "$MONGO_URI/$DB_NAME" "$file"
done

echo "Seeding data..."
for file in /docker-entrypoint-initdb.d/data/*.js; do
  echo "Running $file..."
  mongosh "$MONGO_URI/$DB_NAME" "$file"
done

echo "Applying development data..."
for file in /docker-entrypoint-initdb.d/dev/*.js; do
  echo "Running $file..."
  mongosh "$MONGO_URI/$DB_NAME" "$file"
done

echo "MongoDB initialization complete."
