CREATE DATABASE SistemaVeterinaria;
GO

USE SistemaVeterinaria;
GO

CREATE TABLE Usuarios (
    Login_Usuario NVARCHAR(50) PRIMARY KEY,
    Clave_Usuario NVARCHAR(50),
    Nombre_Usuario NVARCHAR(100)
);
GO

CREATE TABLE Razas (
    ID_Raza INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Raza NVARCHAR(100),
    Tipo_Mascota NVARCHAR(50)
);
GO

CREATE TABLE TiposMascotas (
    ID_TipoMascota INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_TipoMascota NVARCHAR(100)
);
GO

CREATE TABLE Clientes (
    ID_Cliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Cliente NVARCHAR(100),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(255)
);
GO

CREATE TABLE Medicos (
    ID_Medico INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Medico NVARCHAR(100),
    Especialidad NVARCHAR(100)
);
GO

CREATE TABLE Mascotas (
    ID_Mascota INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Mascota NVARCHAR(100),
    ID_Raza INT FOREIGN KEY REFERENCES Razas(ID_Raza),
    ID_TipoMascota INT FOREIGN KEY REFERENCES TiposMascotas(ID_TipoMascota),
    ID_Cliente INT FOREIGN KEY REFERENCES Clientes(ID_Cliente),
    Comida_Favorita NVARCHAR(100)
);
GO

CREATE TABLE Citas (
    ID_Cita INT IDENTITY(1,1) PRIMARY KEY,
    ID_Mascota INT FOREIGN KEY REFERENCES Mascotas(ID_Mascota),
    ID_Medico INT FOREIGN KEY REFERENCES Medicos(ID_Medico),
    Proxima_fecha DATETIME
);
GO