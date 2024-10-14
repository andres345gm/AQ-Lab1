-- Crear la base de datos arq_per_db
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'arq_per_db')
BEGIN
    CREATE DATABASE arq_per_db;
END
GO

USE arq_per_db;
GO

-- Crear la tabla persona
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[persona]') AND type in (N'U'))
BEGIN
    CREATE TABLE persona (
        cc INT NOT NULL PRIMARY KEY,
        nombre NVARCHAR(45) NOT NULL,
        apellido NVARCHAR(45) NOT NULL,
        genero CHAR(1) NOT NULL CHECK (genero IN ('M', 'F')), -- Usamos CHAR(1) para el genero
        edad INT NULL
    );
END
GO

-- Crear la tabla profesion
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[profesion]') AND type in (N'U'))
BEGIN
    CREATE TABLE profesion (
        id INT NOT NULL PRIMARY KEY,
        nom NVARCHAR(90) NOT NULL,
        des NVARCHAR(MAX) NULL -- NVARCHAR(MAX) para el campo TEXT en SQL Server
    );
END
GO

-- Crear la tabla estudios
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[estudios]') AND type in (N'U'))
BEGIN
    CREATE TABLE estudios (
        id_prof INT NOT NULL,
        cc_per INT NOT NULL,
        fecha DATE NULL,
        univer NVARCHAR(50) NULL,
        PRIMARY KEY (id_prof, cc_per),
        FOREIGN KEY (cc_per) REFERENCES persona(cc),
        FOREIGN KEY (id_prof) REFERENCES profesion(id)
    );
END
GO

-- Crear la tabla telefono
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[telefono]') AND type in (N'U'))
BEGIN
    CREATE TABLE telefono (
        num NVARCHAR(15) NOT NULL PRIMARY KEY,
        oper NVARCHAR(45) NOT NULL,
        duenio INT NOT NULL,
        FOREIGN KEY (duenio) REFERENCES persona(cc)
    );
END
GO