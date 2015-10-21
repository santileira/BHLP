
-------------------------------Alta Rol-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].AltaRol
	@Nombre VARCHAR(30)
	
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.ROLES (ROL_NOMBRE) VALUES (@Nombre)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Alta Funcionalidad-------------------------------
--DROP PROCEDURE [ABSTRACCIONX4].AltaFuncionalidad
CREATE PROCEDURE [ABSTRACCIONX4].AltaFuncionalidad
	@Funcion VARCHAR(60),
	@Rol VARCHAR(30)
AS
	BEGIN TRY
		DECLARE @CodRol TINYINT
		DECLARE @CodFunc TINYINT
		SET @CodRol = [ABSTRACCIONX4].DarCodigoDeRol(@Rol)
		SET @CodFunc = [ABSTRACCIONX4].DarCodigoDeFuncionalidad(@Funcion)
		
		INSERT INTO ABSTRACCIONX4.FUNCIONES_ROLES (ROL_COD , FUNC_COD) VALUES (@CodRol , @CodFunc)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		--SET @Error = 'El nombre ' + @Rol + ' ya esta en uso para otro rol'
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
	BEGIN TRY
		DECLARE @Codigo TINYINT
		SET @Codigo = [ABSTRACCIONX4].DarCodigoDeRol(@Nombre)
		
		UPDATE ABSTRACCIONX4.ROLES 
		SET ROL_ESTADO = 0 WHERE ROL_NOMBRE = @Nombre
		------Podría ir en trigger! 
		UPDATE ABSTRACCIONX4.USUARIOS 
		SET ROL_COD = NULL WHERE ROL_COD = @Codigo
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		--SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Modificar Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarRol
	@NombreNuevo VARCHAR(30),
	@NombreOriginal VARCHAR(30)
AS
	BEGIN TRY
		UPDATE ABSTRACCIONX4.ROLES 
		SET ROL_NOMBRE = @NombreNuevo WHERE ROL_NOMBRE = @NombreOriginal
		
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @NombreNuevo + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Baja Funcionalidades-------------------------------
--DROP PROCEDURE [ABSTRACCIONX4].BajaFuncionalidades
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
		SET @Error = 'Ya existe una aeronave con matrícula ' + @Matricula
		RAISERROR(@Error, 16, 1)
	END CATCH
GO


-------------------------------Baja Aeronave-------------------------------
--DROP PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
CREATE PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
	UPDATE ABSTRACCIONX4.AERONAVES 
		SET AERO_BAJA_FS = 1, AERO_FECHA_RS = @FechaReinicio , AERO_FECHA_FS = @FechaBaja
		WHERE AERO_MATRI = @Matricula
GO

CREATE PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
	UPDATE ABSTRACCIONX4.AERONAVES 
		SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
		WHERE AERO_MATRI = @Matricula
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
	UPDATE ABSTRACCIONX4.AERONAVES
		SET AERO_MOD = @Modelo, AERO_MATRI = @Matricula, AERO_FAB = @Fabricante,
			SERV_COD = ABSTRACCIONX4.ObtenerCodigoServicio(@TipoDeServicio),
			AERO_CANT_BUTACAS = @CantidadButacas, AERO_CANT_KGS = @CantidadKG
		WHERE AERO_MATRI = @MatriculaActual
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
			' con el código ' + CONVERT(VARCHAR,@Codigo) + ' y servicio ' + @Servicio
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad (@Ciudad VARCHAR(80))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Codigo SMALLINT
	SELECT @Codigo = CIU_COD FROM CIUDADES WHERE CIU_DESC = @Ciudad
	RETURN @Codigo
END

GO

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
	UPDATE ABSTRACCIONX4.RUTAS_AEREAS
		SET RUTA_COD = @Codigo, SERV_COD = ABSTRACCIONX4.ObtenerCodigoServicio(@Servicio),
			CIU_COD_O = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadOrigen),
			CIU_COD_D = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadDestino),
			RUTA_PRECIO_BASE_PASAJE = @PrecioPasaje,
			RUTA_PRECIO_BASE_KG = @PrecioeEncomienda
		WHERE RUTA_ID = @IdRuta
GO

