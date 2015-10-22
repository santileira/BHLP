
-------------------------------Tipo Lista-------------------------------
CREATE TYPE [ABSTRACCIONX4].Lista AS TABLE 
( elemento VARCHAR(30) )

GO

-------------------------------Alta Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AltaRol
	@Nombre VARCHAR(30),
	@Funcionalidades Lista Readonly
AS
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.ROLES (ROL_NOMBRE) VALUES (@Nombre)
		INSERT INTO ABSTRACCIONX4.FUNCIONES_ROLES (ROL_COD,FUNC_COD)
			SELECT ABSTRACCIONX4.DarCodigoDeRol(@Nombre),
			ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento) FROM @Funcionalidades
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH

GO

-------------------------------Dar Codigo De Rol-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DarCodigoDeRol (@Rol VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Rol_Cod TINYINT
	SELECT @Rol_Cod = R.ROL_COD 
	FROM ABSTRACCIONX4.ROLES R
	WHERE R.ROL_NOMBRE = @Rol
	IF(@Rol_Cod is NULL)
	SET @Rol_Cod = 0
	RETURN @Rol_Cod
END
GO

-------------------------------Dar Codigo De Funcionalidad-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].DarCodigoDeFuncionalidad (@Funcion VARCHAR(60))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Func_Cod TINYINT
	SELECT @Func_Cod = FUNC_COD 
	FROM ABSTRACCIONX4.FUNCIONALIDADES
	WHERE FUNC_DESC = @Funcion
	IF(@Func_Cod is NULL)
	SET @Func_Cod = 0
	RETURN @Func_Cod
END
GO
-------------------------------Baja Rol-------------------------------

--DROP PROCEDURE ABSTRACCIONX4.BajaRol
CREATE PROCEDURE [ABSTRACCIONX4].BajaRol
	@Nombre VARCHAR(30)	
AS
	-- VER SI VA TRANSACCION
		DECLARE @Codigo TINYINT
		SET @Codigo = [ABSTRACCIONX4].DarCodigoDeRol(@Nombre)
		
		UPDATE ABSTRACCIONX4.ROLES 
		SET ROL_ESTADO = 0 WHERE ROL_NOMBRE = @Nombre
		------Podr�a ir en trigger! 
		UPDATE ABSTRACCIONX4.USUARIOS 
		SET ROL_COD = NULL WHERE ROL_COD = @Codigo
GO

-------------------------------Modificar Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarRol
	@NombreOriginal VARCHAR(30),
	@NombreNuevo VARCHAR(30),
	@Funcionalidades Lista READONLY,
	@Estado BIT
AS
	BEGIN TRY
		UPDATE ABSTRACCIONX4.ROLES 
			SET ROL_NOMBRE = @NombreNuevo, ROL_ESTADO = @Estado 
			WHERE ROL_NOMBRE = @NombreOriginal
		DECLARE @CodigoRol TINYINT
		SET @CodigoRol = [ABSTRACCIONX4].DarCodigoDeRol(@NombreNuevo)
		EXEC [ABSTRACCIONX4].ActualizarFuncionalidades @CodigoRol,@Funcionalidades
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @NombreNuevo + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO


-------------------------------Actualizar Funcionalidades-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ActualizarFuncionalidades
	@CodigoRol TINYINT,
	@FuncionalidadesNuevas Lista READONLY
AS
BEGIN
	INSERT INTO [ABSTRACCIONX4].FUNCIONES_ROLES (ROL_COD,FUNC_COD)
		SELECT @CodigoRol,
			   ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento)
		FROM @FuncionalidadesNuevas
		WHERE elemento NOT IN (SELECT * FROM [ABSTRACCIONX4].FuncionalidadesRol(@CodigoRol))
	
	DELETE FROM ABSTRACCIONX4.FUNCIONES_ROLES
		WHERE ROL_COD = @CodigoRol AND
			  FUNC_COD NOT IN
				(SELECT ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento)
				 FROM @FuncionalidadesNuevas)
END

GO

-------------------------------Funcionalidades del rol-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].FuncionalidadesRol
	(@CodigoRol TINYINT)
RETURNS @FuncionalidadesRol TABLE (Funcionalidad VARCHAR(30))
AS
BEGIN
	INSERT INTO @FuncionalidadesRol
	SELECT FUNC_DESC 
		FROM FUNCIONALIDADES F JOIN FUNCIONES_ROLES FR ON (F.FUNC_COD = FR.FUNC_COD)
		WHERE ROL_COD = @CodigoRol
	RETURN
END

GO

-------------------------------Baja Funcionalidades-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BajaFuncionalidades
	@NombreRol VARCHAR(30)	
AS
	BEGIN TRY
		DECLARE @Codigo TINYINT
		SET @Codigo = [ABSTRACCIONX4].DarCodigoDeRol(@NombreRol)
		
		DELETE FROM [ABSTRACCIONX4].FUNCIONES_ROLES
		WHERE ROL_COD = @Codigo
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		--SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Alta Aeronave-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].AltaAeronave
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadButacas SMALLINT,
	@CantidadKG NUMERIC(6,2),
	@FechaAlta DATETIME
AS
	BEGIN TRY
		DECLARE @CodigoServicio TINYINT
		SELECT @CodigoServicio=SERV_COD FROM ABSTRACCIONX4.SERVICIOS WHERE SERV_DESC = @TipoDeServicio
		INSERT INTO ABSTRACCIONX4.AERONAVES 
			(AERO_MOD,AERO_MATRI,AERO_FAB,SERV_COD,AERO_CANT_BUTACAS,AERO_CANT_KGS,AERO_FECHA_ALTA)
			VALUES (@Modelo,@Matricula,@Fabricante,@CodigoServicio,@CantidadButacas,@CantidadKG,@FechaAlta)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe una aeronave con matr�cula ' + @Matricula
		RAISERROR(@Error, 16, 1)
	END CATCH
GO


-------------------------------Baja Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @TieneViajeComprado BIT
	SET @TieneViajeComprado = [ABSTRACCIONX4].TieneViajeComprado(@Matricula)

	IF @TieneViajeComprado = 1
	BEGIN
		DECLARE @Error varchar(80)
		SET @Error = 'La aeronave de matr�cula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
	END
	
	UPDATE ABSTRACCIONX4.AERONAVES 
			SET AERO_BAJA_FS = 1, AERO_FECHA_RS = @FechaReinicio , AERO_FECHA_FS = @FechaBaja
			WHERE AERO_MATRI = @Matricula
END
GO

CREATE PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
	UPDATE ABSTRACCIONX4.AERONAVES 
		SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
		WHERE AERO_MATRI = @Matricula
GO


-------------------------------Tiene viaje comprado-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].TieneViajeComprado
	(@Matricula VARCHAR(8))
RETURNS BIT
AS
BEGIN
	DECLARE @ResultadoPasajes INT
	DECLARE @ResultadoEncomiendas INT 
	
	SELECT @ResultadoPasajes = COUNT(*) 
		FROM ABSTRACCIONX4.PASAJES
		   WHERE AERO_MATRI = @Matricula

	SELECT @ResultadoEncomiendas = COUNT(*) 
		FROM ABSTRACCIONX4.ENCOMIENDAS
		   WHERE AERO_MATRI = @Matricula

	IF @ResultadoPasajes > 0 OR @ResultadoEncomiendas > 0
		RETURN 1
	RETURN 0
END

GO


-------------------------------Cancelar Aeronave Baja-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].CancelarAeronaveBaja
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
BEGIN
		UPDATE ABSTRACCIONX4.AERONAVES 
			SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
			WHERE AERO_MATRI = @Matricula
		
		UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_CANCELADO = 1 WHERE AERO_MATRI = @Matricula
		UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_CANCELADO = 1 WHERE AERO_MATRI = @Matricula
END

GO

-------------------------------Cancelar Aeronave Baja-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].CancelarAeronaveBaja
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
BEGIN
		UPDATE ABSTRACCIONX4.AERONAVES 
			SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
			WHERE AERO_MATRI = @Matricula
		
		UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_CANCELADO = 1 WHERE AERO_MATRI = @Matricula
		UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_CANCELADO = 1 WHERE AERO_MATRI = @Matricula
END

GO


-------------------------------Modificar Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarAeronave
	@MatriculaActual VARCHAR(8),
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadButacas SMALLINT,
	@CantidadKG NUMERIC(6,2)
AS
BEGIN
	BEGIN TRY
		UPDATE ABSTRACCIONX4.AERONAVES
			SET AERO_MOD = @Modelo, AERO_MATRI = @Matricula, AERO_FAB = @Fabricante,
				SERV_COD = ABSTRACCIONX4.ObtenerCodigoServicio(@TipoDeServicio),
				AERO_CANT_BUTACAS = @CantidadButacas, AERO_CANT_KGS = @CantidadKG
			WHERE AERO_MATRI = @MatriculaActual
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe una aeronave con matr�cula ' + @Matricula
		RAISERROR(@Error, 16, 1)
	END CATCH
END

GO



SELECT * FROM [ABSTRACCIONX4].AERONAVES
SELECT * FROM [ABSTRACCIONX4].BUTACAS WHERE AERO_MATRI = 'JORGE'
SELECT * FROM [ABSTRACCIONX4].ENCOMIENDAS WHERE AERO_MATRI = 'JORGE'
SELECT * FROM [ABSTRACCIONX4].VIAJES WHERE AERO_MATRI = 'JORGE'
SELECT * FROM [ABSTRACCIONX4].PASAJES WHERE AERO_MATRI = 'JORGE'
--DBC-748
UPDATE [ABSTRACCIONX4].AERONAVES
SET AERO_MATRI = 'PESCE'
WHERE AERO_MATRI = 'DBC-748'
GO
-------------------------------Modificar Aeronave Pasajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronavePasajes 
@MatriculaVieja VARCHAR(8) , 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].PASAJES 
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
END
GO
-------------------------------Modificar Aeronave Encomiendas-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveEncomiendas
@MatriculaVieja VARCHAR(8) , 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].ENCOMIENDAS
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
END
GO

-------------------------------Modificar Aeronave BUTACAS-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveButacas
@MatriculaVieja VARCHAR(8) , 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].BUTACAS
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
END
GO

-------------------------------Modificar Aeronave Viajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
@MatriculaVieja VARCHAR(8) , 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].VIAJES
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
END
GO

-------------------------------Modificacion Matricula-------------------------------

CREATE TRIGGER [ABSTRACCIONX4].ModificarMatricula
ON [ABSTRACCIONX4].AERONAVES
INSTEAD OF UPDATE
AS
DECLARE @MatriculaVieja VARCHAR(8)
DECLARE @MatriculaNueva VARCHAR(8)
BEGIN
	SELECT @MatriculaVieja = AERO_MATRI FROM DELETED
	SELECT @MatriculaNueva = AERO_MATRI FROM INSERTED
	
	IF(UPDATE(AERO_MATRI))
	BEGIN
		INSERT INTO [ABSTRACCIONX4].AERONAVES 
		(AERO_MOD , AERO_MATRI , AERO_FAB , SERV_COD , AERO_CANT_BUTACAS , AERO_CANT_KGS)
		SELECT AERO_MOD, AERO_MATRI, AERO_FAB,
		SERV_COD , AERO_CANT_BUTACAS , AERO_CANT_KGS
		FROM INSERTED
		
		/*EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @MatriculaVieja , @MatriculaNueva
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @MatriculaVieja , @MatriculaNueva*/
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveButacas @MatriculaVieja , @MatriculaNueva
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @MatriculaVieja , @MatriculaNueva
		
		DELETE FROM [ABSTRACCIONX4].AERONAVES WHERE AERO_MATRI = @MatriculaVieja
		
		
	END
END
GO
-------------------------------Modificacion Butaca-------------------------------
CREATE TRIGGER [ABSTRACCIONX4].ModificacionButaca
ON [ABSTRACCIONX4].BUTACAS
INSTEAD OF UPDATE
AS
DECLARE @MatriculaVieja VARCHAR(8)
DECLARE @MatriculaNueva VARCHAR(8)
BEGIN
	SELECT @MatriculaVieja = AERO_MATRI FROM DELETED
	SELECT @MatriculaNueva = AERO_MATRI FROM INSERTED
	
	IF(UPDATE(AERO_MATRI))
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS 
		
		SELECT * FROM INSERTED
		
		EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @MatriculaVieja , @MatriculaNueva
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @MatriculaVieja , @MatriculaNueva
		
		DELETE FROM [ABSTRACCIONX4].BUTACAS WHERE AERO_MATRI = @MatriculaVieja
	END
END
GO

-------------------------------Viajes asignados a aeronave-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].TieneViajeAsignado
	(@Matricula VARCHAR(8))
RETURNS BIT
AS
BEGIN
	DECLARE @Resultado INT 
	SELECT @Resultado = COUNT(*) FROM ABSTRACCIONX4.VIAJES WHERE AERO_MATRI=@Matricula
	IF @Resultado > 0
		RETURN 1
	RETURN 0
END

GO


-------------------------------Alta Ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AltaRuta
	@Codigo INT,
	@Servicio VARCHAR(30),
	@CiudadOrigen VARCHAR(80),
	@CiudadDestino VARCHAR(80),
	@PrecioPasaje NUMERIC(5,2),
	@PrecioeEncomienda NUMERIC(5,2)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.RUTAS_AEREAS
			(RUTA_COD,SERV_COD,CIU_COD_O,CIU_COD_D,RUTA_PRECIO_BASE_PASAJE,RUTA_PRECIO_BASE_KG)
			VALUES (@Codigo,ABSTRACCIONX4.ObtenerCodigoServicio(@Servicio),
			ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadOrigen),
			ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadDestino),
			@PrecioPasaje,@PrecioeEncomienda)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(255)
		SET @Error = 'Ya existe una ruta de ' + @CiudadOrigen + ' a ' + @CiudadDestino +
			' con el c�digo ' + CONVERT(VARCHAR,@Codigo) + ' y servicio ' + @Servicio
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Obtener Codigo Ciudad-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad (@Ciudad VARCHAR(80))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Codigo SMALLINT
	SELECT @Codigo = CIU_COD FROM CIUDADES WHERE CIU_DESC = @Ciudad
	RETURN @Codigo
END

GO


-------------------------------Obtener Codigo Servicio-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoServicio (@Servicio VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Codigo TINYINT
	SELECT @Codigo = SERV_COD FROM SERVICIOS WHERE SERV_DESC = @Servicio
	RETURN @Codigo
END

GO

-------------------------------Baja Ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BajaRuta
	@IdRuta INT
AS
	UPDATE ABSTRACCIONX4.RUTAS_AEREAS
		SET RUTA_ESTADO = 0
		WHERE RUTA_ID=@IdRuta
GO


-------------------------------Borrar Pasajes-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarPasajes
@IdRuta INT
AS
UPDATE ABSTRACCIONX4.PASAJES 
SET PASAJE_CANCELADO = 1
WHERE PASAJE_COD IN (SELECT P.PASAJE_COD FROM [ABSTRACCIONX4].PASAJES P , [ABSTRACCIONX4].VIAJES V
WHERE P.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta)

GO


-------------------------------Borrar Encomiendas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarEncomiendas
@IdRuta INT
AS
UPDATE ABSTRACCIONX4.ENCOMIENDAS
SET ENCOMIENDA_CANCELADO = 1
WHERE ENCOMIENDA_COD IN (SELECT E.ENCOMIENDA_COD FROM [ABSTRACCIONX4].ENCOMIENDAS E , [ABSTRACCIONX4].VIAJES V
WHERE E.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta)

GO

-------------------------------Alta Ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarRuta
	@IdRuta INT,
	@Codigo INT,
	@Servicio VARCHAR(30),
	@CiudadOrigen VARCHAR(80),
	@CiudadDestino VARCHAR(80),
	@PrecioPasaje NUMERIC(5,2),
	@PrecioeEncomienda NUMERIC(5,2)
AS
BEGIN
	BEGIN TRY
		UPDATE ABSTRACCIONX4.RUTAS_AEREAS
			SET RUTA_COD = @Codigo, SERV_COD = ABSTRACCIONX4.ObtenerCodigoServicio(@Servicio),
				CIU_COD_O = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadOrigen),
				CIU_COD_D = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadDestino),
				RUTA_PRECIO_BASE_PASAJE = @PrecioPasaje,
				RUTA_PRECIO_BASE_KG = @PrecioeEncomienda
			WHERE RUTA_ID = @IdRuta
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(255)
		SET @Error = 'Ya existe una ruta de ' + @CiudadOrigen + ' a ' + @CiudadDestino +
			' con el c�digo ' + CONVERT(VARCHAR,@Codigo) + ' y servicio ' + @Servicio
		RAISERROR(@Error, 16, 1)
	END CATCH
END

GO

