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
		INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
			VALUES ('INVITADO',ABSTRACCIONX4.DarCodigoDeRol(@Nombre))
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
		------Podría ir en trigger! 
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