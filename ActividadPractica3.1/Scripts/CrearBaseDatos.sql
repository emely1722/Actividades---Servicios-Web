IF DB_ID('Biblioteca') IS NULL
BEGIN
    CREATE DATABASE Biblioteca;
END
GO

USE Biblioteca;
GO

USE Biblioteca;
GO

CREATE TABLE Libros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(200) NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    AnioPublicacion INT NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    NumeroPaginas INT NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Disponible BIT NOT NULL
);
GO

INSERT INTO Libros (Titulo, Autor, AnioPublicacion, Genero, NumeroPaginas, Precio, Disponible)
VALUES 
('La Mañosa', 'Juan Bosch', 1936, 'Novela', 240, 450.00, 1),
('Cuentos Escritos en el Exilio', 'Juan Bosch', 1962, 'Cuento', 180, 350.00, 1),
('Cien Años de Soledad', 'Gabriel García Márquez', 1967, 'Realismo Mágico', 471, 850.00, 1),
('Poesías Completas', 'Salomé Ureña', 1920, 'Poesía', 150, 250.00, 1);
GO

SELECT * FROM Libros;
GO