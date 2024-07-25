USE master;
GO

DROP DATABASE if EXISTS DBColegioProfesional;
GO

CREATE DATABASE DBColegioProfesional;
GO

USE DBColegioProfesional;
GO

-- Tabla Miembros
CREATE TABLE Miembros (
    id_miembro INT IDENTITY(1,1) PRIMARY KEY,
    dni VARCHAR(8) NOT NULL,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    direccion VARCHAR(255),
    email VARCHAR(100),
    telefono VARCHAR(15),
    universidad VARCHAR(100),
    titulo VARCHAR(100),
    fecha_graduacion DATE,
    foto_url VARCHAR(255),
    estado VARCHAR(20) NOT NULL,
    fecha_registro DATE
);
GO


-- Procedimiento para obtener todos los miembros
CREATE PROCEDURE ObtenerMiembros
AS
BEGIN
    SELECT 
        id_miembro, 
        dni, 
        nombres, 
        apellidos, 
        fecha_nacimiento, 
        direccion, 
        email, 
        telefono, 
        universidad, 
        titulo, 
        fecha_graduacion, 
        foto_url, 
        estado, 
        fecha_registro
    FROM Miembros;
END;
GO
-- Ejemplo para ObtenerMiembros
EXEC ObtenerMiembros;
GO 

-- Procedimiento para insertar un nuevo miembro
CREATE PROCEDURE InsertarMiembro
    @dni VARCHAR(20),
    @nombres VARCHAR(50),
    @apellidos VARCHAR(50),
    @fecha_nacimiento DATETIME,
    @direccion VARCHAR(255),
    @email VARCHAR(100),
    @telefono VARCHAR(20),
    @universidad VARCHAR(100),
    @titulo VARCHAR(50),
    @fecha_graduacion DATETIME,
    @foto_url VARCHAR(255),
    @estado VARCHAR(20),
    @fecha_registro DATETIME
AS
BEGIN
    INSERT INTO Miembros (
        dni, 
        nombres, 
        apellidos, 
        fecha_nacimiento, 
        direccion, 
        email, 
        telefono, 
        universidad, 
        titulo, 
        fecha_graduacion, 
        foto_url, 
        estado, 
        fecha_registro
    ) VALUES (
        @dni, 
        @nombres, 
        @apellidos, 
        @fecha_nacimiento, 
        @direccion, 
        @email, 
        @telefono, 
        @universidad, 
        @titulo, 
        @fecha_graduacion, 
        @foto_url, 
        @estado, 
        @fecha_registro
    );
END;
GO


-- Ejemplos para InsertarMiembro
EXEC InsertarMiembro '12345678', 'Juan', 'Perez', '1980-01-01', 'Av. Siempre Viva 123', 'juan.perez@example.com', '123456789', 'Universidad Nacional', 'Ingeniero', '2002-12-15', 'http://example.com/juan.jpg', 'Activo', '2023-01-01';
EXEC InsertarMiembro '23456789', 'Maria', 'Gomez', '1990-05-10', 'Calle Falsa 456', 'maria.gomez@example.com', '987654321', 'Universidad Estatal', 'Licenciada', '2010-06-20', 'http://example.com/maria.jpg', 'Inactivo', '2023-02-01';
EXEC InsertarMiembro '34567890', 'Luis', 'Rodriguez', '1975-09-25', 'Av. Principal 789', 'luis.rodriguez@example.com', '5647382910', 'Universidad Privada', 'Doctor', '1998-11-30', 'http://example.com/luis.jpg', 'Activo', '2023-03-01';
GO 

-- Procedimiento para actualizar un miembro existente
CREATE PROCEDURE ActualizarMiembro
    @id_miembro INT,
    @dni VARCHAR(20),
    @nombres VARCHAR(50),
    @apellidos VARCHAR(50),
    @fecha_nacimiento DATETIME,
    @direccion VARCHAR(255),
    @email VARCHAR(100),
    @telefono VARCHAR(20),
    @universidad VARCHAR(100),
    @titulo VARCHAR(50),
    @fecha_graduacion DATETIME,
    @foto_url VARCHAR(255),
    @estado VARCHAR(20),
    @fecha_registro DATETIME
AS
BEGIN
    UPDATE Miembros
    SET 
        dni = @dni,
        nombres = @nombres,
        apellidos = @apellidos,
        fecha_nacimiento = @fecha_nacimiento,
        direccion = @direccion,
        email = @email,
        telefono = @telefono,
        universidad = @universidad,
        titulo = @titulo,
        fecha_graduacion = @fecha_graduacion,
        foto_url = @foto_url,
        estado = @estado,
        fecha_registro = @fecha_registro
    WHERE id_miembro = @id_miembro;
END;
GO

-- Ejemplos para ActualizarMiembro
EXEC ActualizarMiembro 1, '12345678', 'Juan', 'Perez', '1980-01-01', 'Av. Siempre Viva 1234', 'juan.perez@newmail.com', '123456789', 'Universidad Nacional', 'Ingeniero', '2002-12-15', 'http://example.com/juan_new.jpg', 'Activo', '2023-01-01';
EXEC ActualizarMiembro 2, '23456789', 'Maria', 'Gomez', '1990-05-10', 'Calle Falsa 456', 'maria.gomez@newmail.com', '987654321', 'Universidad Estatal', 'Licenciada', '2010-06-20', 'http://example.com/maria_new.jpg', 'Inactivo', '2023-02-01';
EXEC ActualizarMiembro 3, '34567890', 'Luis', 'Rodriguez', '1975-09-25', 'Av. Principal 789', 'luis.rodriguez@newmail.com', '5647382910', 'Universidad Privada', 'Doctor', '1998-11-30', 'http://example.com/luis_new.jpg', 'Activo', '2023-03-01';
GO



-- Procedimiento para eliminar un miembro
CREATE PROCEDURE EliminarMiembro
    @id_miembro INT
AS
BEGIN
    DELETE FROM Miembros
    WHERE id_miembro = @id_miembro;
END;
GO



-- Ejemplos para EliminarMiembro
EXEC EliminarMiembro @id_miembro=12345678;

GO

-- Tabla Documentos
CREATE TABLE Documentos (
    id_documento INT IDENTITY(1,1) PRIMARY KEY,
    id_miembro INT NOT NULL,
    tipo_documento VARCHAR(50) NOT NULL,
    documento_url VARCHAR(255) NOT NULL,
    fecha_carga TIMESTAMP,
    estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (id_miembro) REFERENCES Miembros(id_miembro)
);
GO



-- Procedimiento para obtener todos los documentos
CREATE PROCEDURE ObtenerDocumentos
AS
BEGIN
    SELECT 
        id_documento, 
        id_miembro, 
        tipo_documento, 
        documento_url, 
        fecha_carga, 
        estado
    FROM Documentos;
END;
GO

-- Ejemplo para ObtenerDocumentos
EXEC ObtenerDocumentos;
GO
 
-- Procedimiento para insertar un nuevo documento
CREATE PROCEDURE InsertarDocumento
    @id_miembro INT,
    @tipo_documento VARCHAR(50),
    @documento_url VARCHAR(255),
    @fecha_carga DATETIME,
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Documentos (
        id_miembro, 
        tipo_documento, 
        documento_url, 
        fecha_carga, 
        estado
    ) VALUES (
        @id_miembro, 
        @tipo_documento, 
        @documento_url, 
        @fecha_carga, 
        @estado
    );
END;
GO

-- Ejemplos para InsertarDocumento
EXEC InsertarDocumento 1, 'Diploma', 'http://example.com/diploma_juan.pdf', 'Activo';
EXEC InsertarDocumento 2, 'Certificado', 'http://example.com/certificado_maria.pdf', 'Inactivo';
EXEC InsertarDocumento 3, 'Titulo', 'http://example.com/titulo_luis.pdf', 'Activo';
GO


-- Procedimiento para actualizar un documento existente
CREATE PROCEDURE ActualizarDocumento
    @id_documento INT,
    @id_miembro INT,
    @tipo_documento VARCHAR(50),
    @documento_url VARCHAR(255),
    @fecha_carga DATETIME,
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE Documentos
    SET 
        id_miembro = @id_miembro,
        tipo_documento = @tipo_documento,
        documento_url = @documento_url,
        fecha_carga = @fecha_carga,
        estado = @estado
    WHERE id_documento = @id_documento;
END;
GO

EXEC ActualizarDocumento 1, 1, 'Diploma', 'http://example.com/diploma_juan_new.pdf', 'Activo';
GO 

-- Procedimiento para eliminar un documento
CREATE PROCEDURE EliminarDocumento
    @id_documento INT
AS
BEGIN
    DELETE FROM Documentos
    WHERE id_documento = @id_documento;
END;
GO


-- Ejemplos para EliminarDocumento
EXEC EliminarDocumento 1;

GO 



-- Tabla Certificaciones
CREATE TABLE Certificaciones (
    id_certificacion INT IDENTITY(1,1) PRIMARY KEY,
    id_documento INT NOT NULL,
    tipo_certificacion VARCHAR(50) NOT NULL,
    fecha_emision DATE NOT NULL,
    fecha_expiracion DATE,
    certificado_url VARCHAR(255),
    estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (id_documento) REFERENCES Documentos(id_documento)
);
GO


-- Procedimiento para obtener todas las certificaciones
CREATE PROCEDURE ObtenerCertificaciones
AS
BEGIN
    SELECT 
        id_certificacion, 
        id_documento, 
        tipo_certificacion, 
        fecha_emision, 
        fecha_expiracion, 
        certificado_url, 
        estado
    FROM Certificaciones;
END;
GO

-- Ejemplo para ObtenerCertificaciones
EXEC ObtenerCertificaciones;
GO 

-- Procedimiento para insertar una nueva certificación
CREATE PROCEDURE InsertarCertificacion
    @id_documento INT,
    @tipo_certificacion VARCHAR(50),
    @fecha_emision DATE,
    @fecha_expiracion DATE,
    @certificado_url VARCHAR(255),
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Certificaciones (
        id_documento, 
        tipo_certificacion, 
        fecha_emision, 
        fecha_expiracion, 
        certificado_url, 
        estado
    ) VALUES (
        @id_documento, 
        @tipo_certificacion, 
        @fecha_emision, 
        @fecha_expiracion, 
        @certificado_url, 
        @estado
    );
END;
GO

-- Ejemplos para InsertarCertificacion
EXEC InsertarCertificacion 1, 'Certificado de Idiomas', '2023-01-01', '2025-01-01', 'http://example.com/certificado_idiomas_juan.pdf', 'Activo';
EXEC InsertarCertificacion 2, 'Certificado de Estudios', '2023-02-01', NULL, 'http://example.com/certificado_estudios_maria.pdf', 'Inactivo';
EXEC InsertarCertificacion 3, 'Certificado de Competencias', '2023-03-01', '2024-03-01', 'http://example.com/certificado_competencias_luis.pdf', 'Activo';
GO 


-- Procedimiento para actualizar una certificación existente
CREATE PROCEDURE ActualizarCertificacion
    @id_certificacion INT,
    @id_documento INT,
    @tipo_certificacion VARCHAR(50),
    @fecha_emision DATE,
    @fecha_expiracion DATE,
    @certificado_url VARCHAR(255),
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE Certificaciones
    SET 
        id_documento = @id_documento,
        tipo_certificacion = @tipo_certificacion,
        fecha_emision = @fecha_emision,
        fecha_expiracion = @fecha_expiracion,
        certificado_url = @certificado_url,
        estado = @estado
    WHERE id_certificacion = @id_certificacion;
END;
GO


-- Ejemplos para ActualizarCertificacion
EXEC ActualizarCertificacion 1, 1, 'Certificado de Idiomas', '2023-01-01', '2025-01-01', 'http://example.com/certificado_idiomas_juan_new.pdf', 'Activo';
EXEC ActualizarCertificacion 2, 2, 'Certificado de Estudios', '2023-02-01', NULL, 'http://example.com/certificado_estudios_maria_new.pdf', 'Inactivo';
EXEC ActualizarCertificacion 3, 3, 'Certificado de Competencias', '2023-03-01', '2024-03-01', 'http://example.com/certificado_competencias_luis_new.pdf', 'Activo';
GO  

-- Procedimiento para eliminar una certificación
CREATE PROCEDURE EliminarCertificacion
    @id_certificacion INT
AS
BEGIN
    DELETE FROM Certificaciones
    WHERE id_certificacion = @id_certificacion;
END;
GO

-- Ejemplos para EliminarCertificacion
EXEC EliminarCertificacion 2;
GO



-- Tabla Pagos
CREATE TABLE Pagos (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    id_miembro INT NOT NULL,
    monto DECIMAL(10, 2) NOT NULL,
    fecha_pago DATE NOT NULL,
    tipo_pago VARCHAR(50) NOT NULL,
    comprobante_url VARCHAR(255),
    estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (id_miembro) REFERENCES Miembros(id_miembro)
);
GO


-- Procedimiento para obtener todos los pagos
CREATE PROCEDURE ObtenerPagos
AS
BEGIN
    SELECT 
        id_pago, 
        id_miembro, 
        monto, 
        fecha_pago, 
        tipo_pago, 
        comprobante_url, 
        estado
    FROM Pagos;
END;
GO

-- Ejemplo para ObtenerPagos
EXEC ObtenerPagos;
GO

-- Procedimiento para insertar un nuevo pago
CREATE PROCEDURE InsertarPago
    @id_miembro INT,
    @monto DECIMAL(18, 2),
    @fecha_pago DATETIME,
    @tipo_pago VARCHAR(50),
    @comprobante_url VARCHAR(255),
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Pagos (
        id_miembro, 
        monto, 
        fecha_pago, 
        tipo_pago, 
        comprobante_url, 
        estado
    ) VALUES (
        @id_miembro, 
        @monto, 
        @fecha_pago, 
        @tipo_pago, 
        @comprobante_url, 
        @estado
    );
END;
GO

-- Ejemplos para InsertarPago
EXEC InsertarPago 1, 500.00, '2023-01-15', 'Tarjeta de Crédito', 'http://example.com/comprobante_pago_juan.pdf', 'Pagado';
EXEC InsertarPago 2, 700.00, '2023-02-20', 'Transferencia Bancaria', 'http://example.com/comprobante_pago_maria.pdf', 'Pendiente';
EXEC InsertarPago 3, 1000.00, '2023-03-25', 'Efectivo', NULL, 'Pagado';
GO

-- Procedimiento para actualizar un pago existente
CREATE PROCEDURE ActualizarPago
    @id_pago INT,
    @id_miembro INT,
    @monto DECIMAL(18, 2),
    @fecha_pago DATETIME,
    @tipo_pago VARCHAR(50),
    @comprobante_url VARCHAR(255),
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE Pagos
    SET 
        id_miembro = @id_miembro,
        monto = @monto,
        fecha_pago = @fecha_pago,
        tipo_pago = @tipo_pago,
        comprobante_url = @comprobante_url,
        estado = @estado
    WHERE id_pago = @id_pago;
END;
GO

-- Ejemplos para ActualizarPago
EXEC ActualizarPago 1, 1, 600.00, '2023-01-15', 'Tarjeta de Crédito', 'http://example.com/comprobante_pago_juan_new.pdf', 'Pagado';
EXEC ActualizarPago 2, 2, 800.00, '2023-02-20', 'Transferencia Bancaria', 'http://example.com/comprobante_pago_maria_new.pdf', 'Pendiente';
EXEC ActualizarPago 3, 3, 1200.00, '2023-03-25', 'Efectivo', NULL, 'Pagado';
GO

-- Procedimiento para eliminar un pago
CREATE PROCEDURE EliminarPago
    @id_pago INT
AS
BEGIN
    DELETE FROM Pagos
    WHERE id_pago = @id_pago;
END;
GO

-- Ejemplos para EliminarPago
EXEC EliminarPago 3;
GO


-- Tabla Renovaciones
CREATE TABLE Renovaciones (
    id_renovacion INT IDENTITY(1,1) PRIMARY KEY,
    id_miembro INT NOT NULL,
    id_pago INT NOT NULL,
    id_documento INT NOT NULL,
    fecha_solicitud DATE NOT NULL,
    fecha_aprobacion DATE,
    estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (id_miembro) REFERENCES Miembros(id_miembro),
    FOREIGN KEY (id_pago) REFERENCES Pagos(id_pago),
    FOREIGN KEY (id_documento) REFERENCES Documentos(id_documento)
);
GO



-- Procedimiento para obtener todas las renovaciones
CREATE PROCEDURE ObtenerRenovaciones
AS
BEGIN
    SELECT 
        id_renovacion, 
        id_miembro, 
        id_pago, 
        id_documento, 
        fecha_solicitud, 
        fecha_aprobacion, 
        estado
    FROM Renovaciones;
END;
GO

-- Ejemplo para ObtenerRenovaciones
EXEC ObtenerRenovaciones;
GO 

-- Procedimiento para insertar una nueva renovación
CREATE PROCEDURE InsertarRenovacion
    @id_miembro INT,
    @id_pago INT,
    @id_documento INT,
    @fecha_solicitud DATETIME,
    @fecha_aprobacion DATETIME,
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Renovaciones (
        id_miembro, 
        id_pago, 
        id_documento, 
        fecha_solicitud, 
        fecha_aprobacion, 
        estado
    ) VALUES (
        @id_miembro, 
        @id_pago, 
        @id_documento, 
        @fecha_solicitud, 
        @fecha_aprobacion, 
        @estado
    );
END;
GO

-- Ejemplos para InsertarRenovacion
EXEC InsertarRenovacion 1, 1, 1, '2023-01-01', '2023-01-05', 'Aprobado';
EXEC InsertarRenovacion 2, 2, 2, '2023-02-01', NULL, 'Pendiente';
EXEC InsertarRenovacion 3, 3, 3, '2023-03-01', '2023-03-03', 'Aprobado';
GO


-- Procedimiento para actualizar una renovación existente
CREATE PROCEDURE ActualizarRenovacion
    @id_renovacion INT,
    @id_miembro INT,
    @id_pago INT,
    @id_documento INT,
    @fecha_solicitud DATETIME,
    @fecha_aprobacion DATETIME,
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE Renovaciones
    SET 
        id_miembro = @id_miembro,
        id_pago = @id_pago,
        id_documento = @id_documento,
        fecha_solicitud = @fecha_solicitud,
        fecha_aprobacion = @fecha_aprobacion,
        estado = @estado
    WHERE id_renovacion = @id_renovacion;
END;
GO

-- Ejemplos para ActualizarRenovacion
EXEC ActualizarRenovacion 1, 1, 1, 1, '2023-01-01', '2023-01-06', 'Aprobado';
EXEC ActualizarRenovacion 2, 2, 2, 2, '2023-02-01', NULL, 'Pendiente';
EXEC ActualizarRenovacion 3, 3, 3, 3, '2023-03-01', '2023-03-04', 'Aprobado';
GO 


-- Procedimiento para eliminar una renovación
CREATE PROCEDURE EliminarRenovacion
    @id_renovacion INT
AS
BEGIN
    DELETE FROM Renovaciones
    WHERE id_renovacion = @id_renovacion;
END;
GO

-- Ejemplos para EliminarRenovacion
EXEC EliminarRenovacion 1;
GO




-- Tabla Usuarios
CREATE TABLE Usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    id_miembro INT,
    username VARCHAR(50) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    rol VARCHAR(20) NOT NULL,
    fecha_creacion DATE,
    ultimo_acceso TIMESTAMP,
    FOREIGN KEY (id_miembro) REFERENCES Miembros(id_miembro)
);
GO



-- Procedimiento para obtener todos los usuarios
CREATE PROCEDURE ObtenerUsuarios
AS
BEGIN
    SELECT 
        id_usuario, 
        id_miembro, 
        username, 
        password_hash, 
        rol, 
        fecha_creacion, 
        ultimo_acceso
    FROM Usuarios;
END;
GO

-- Procedimiento para insertar un nuevo usuario
CREATE PROCEDURE InsertarUsuario
    @id_miembro INT,
    @username VARCHAR(50),
    @password_hash VARCHAR(255),
    @rol VARCHAR(20),
    @fecha_creacion DATETIME,
    @ultimo_acceso DATETIME
AS
BEGIN
    INSERT INTO Usuarios (
        id_miembro, 
        username, 
        password_hash, 
        rol, 
        fecha_creacion, 
        ultimo_acceso
    ) VALUES (
        @id_miembro, 
        @username, 
        @password_hash, 
        @rol, 
        @fecha_creacion, 
        @ultimo_acceso
    );
END;
GO

-- Ejemplos para InsertarUsuario
EXEC InsertarUsuario 1, 'usuario1', 'hash1', 'Admin', '2023-01-01', '2023-01-05';
EXEC InsertarUsuario 2, 'usuario2', 'hash2', 'Usuario', '2023-02-01', NULL;
EXEC InsertarUsuario 3, 'usuario3', 'hash3', 'Admin', '2023-03-01', '2023-03-03';
GO


-- Procedimiento para actualizar un usuario existente
CREATE PROCEDURE ActualizarUsuario
    @id_usuario INT,
    @id_miembro INT,
    @username VARCHAR(50),
    @password_hash VARCHAR(255),
    @rol VARCHAR(20),
    @fecha_creacion DATETIME,
    @ultimo_acceso DATETIME
AS
BEGIN
    UPDATE Usuarios
    SET 
        id_miembro = @id_miembro,
        username = @username,
        password_hash = @password_hash,
        rol = @rol,
        fecha_creacion = @fecha_creacion,
        ultimo_acceso = @ultimo_acceso
    WHERE id_usuario = @id_usuario;
END;
GO

-- Ejemplos para ActualizarUsuario
EXEC ActualizarUsuario 1, 1, 'usuario1_actualizado', 'hash1', 'Admin', '2023-01-01', '2023-01-06';
EXEC ActualizarUsuario 2, 2, 'usuario2_actualizado', 'hash2', 'Usuario', '2023-02-01', NULL;
EXEC ActualizarUsuario 3, 3, 'usuario3_actualizado', 'hash3', 'Admin', '2023-03-01', '2023-03-04';
GO



-- Procedimiento para eliminar un usuario
CREATE PROCEDURE EliminarUsuario
    @id_usuario INT
AS
BEGIN
    DELETE FROM Usuarios
    WHERE id_usuario = @id_usuario;
END;
GO

-- Ejemplos para EliminarUsuario
EXEC EliminarUsuario 1;
GO