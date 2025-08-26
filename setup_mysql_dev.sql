-- Create database if not exists
CREATE DATABASE IF NOT EXISTS WiventoryDB;

-- Create user if not exists
CREATE USER IF NOT EXISTS "Wiventory"@"localhost" IDENTIFIED BY "12345aA@";

-- Grant priviledges
GRANT ALL ON WiventoryDB.* TO "Wiventory"@"localhost";
