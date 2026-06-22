IF DB_ID('Biblioteca') IS NULL
BEGIN
    CREATE DATABASE Biblioteca;
END
GO

USE Biblioteca;
GO

CREATE TABLE Autores
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(50) NOT NULL,
    AnioNacimiento INT NOT NULL,
    CONSTRAINT CK_Autores_AnioNacimiento CHECK (AnioNacimiento >= 1500 AND AnioNacimiento <= 2100)
);
GO

CREATE TABLE Libros
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    AnioPublicacion INT NOT NULL,
    Genero NVARCHAR(50) NOT NULL,
    NumeroPaginas INT NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Disponible BIT NOT NULL,
    AutorId INT NOT NULL,
    CONSTRAINT FK_Libros_Autores FOREIGN KEY (AutorId) REFERENCES Autores(Id) ON DELETE CASCADE,
    CONSTRAINT CK_Libros_AnioPublicacion CHECK (AnioPublicacion >= 1450 AND AnioPublicacion <= 2100),
    CONSTRAINT CK_Libros_NumeroPaginas CHECK (NumeroPaginas > 0),
    CONSTRAINT CK_Libros_Precio CHECK (Precio >= 0)
);
GO

INSERT INTO Autores (Nombre, Nacionalidad, AnioNacimiento)
VALUES
('Juan Bosch', 'Dominicana', 1909),
('Gabriel Garcia Marquez', 'Colombiana', 1927),
('Salome Urena', 'Dominicana', 1850);
GO

INSERT INTO Libros (Titulo, AnioPublicacion, Genero, NumeroPaginas, Precio, Disponible, AutorId)
VALUES
('La Manosa', 1936, 'Novela', 240, 450.00, 1, 1),
('Cuentos Escritos en el Exilio', 1962, 'Cuento', 180, 350.00, 1, 1),
('Cien Anos de Soledad', 1967, 'Realismo Magico', 471, 850.00, 1, 2),
('Poesias Completas', 1920, 'Poesia', 150, 250.00, 1, 3);
GO

SELECT Id, Nombre, Nacionalidad, AnioNacimiento FROM Autores;
SELECT Id, Titulo, AnioPublicacion, Genero, NumeroPaginas, Precio, Disponible, AutorId FROM Libros;
GO