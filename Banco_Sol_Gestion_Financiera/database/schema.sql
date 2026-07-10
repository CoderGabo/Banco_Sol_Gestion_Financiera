-- ==========================================
-- Banco Sol - Sistema de Gestion Financiera
-- Schema
-- ==========================================

CREATE TABLE IF NOT EXISTS "Users"
(
    "Id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(150) NOT NULL UNIQUE,
    "Password" VARCHAR(255) NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS "Incomes"
(
    "Id" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "UserId" INTEGER NOT NULL,
    "Amount" NUMERIC(18,2) NOT NULL,
    "Currency" VARCHAR(3) NOT NULL,
    "Description" VARCHAR(255) NOT NULL,
    "Source" VARCHAR(50) NOT NULL,
    "ReceivedAt" TIMESTAMP NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
);