
-------------------------------Alta Rol-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].AltaRol
	@Nombre VARCHAR(30),
	@Funcionalidades 
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

-------------------------------Baja Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BajaRol
	@Nombre VARCHAR(30)	
AS
	BEGIN TRY
		DECLARE @Codigo TINYINT
		SELECT @Codigo=R.ROL_COD FROM ABSTRACCIONX4.ROLES R WHERE R.ROL_NOMBRE = @Nombre
		
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
DROP PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
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


