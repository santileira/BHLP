
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

	EXECUTE [ABSTRACCIONX4].BorrarPasajes @IdRuta
	EXECUTE [ABSTRACCIONX4].BorrarEncomiendas @IdRuta
GO

-------------------------------Tiene Viaje Programado-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].TieneViajeProgramado (@IdRuta INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Tiene INT
	SELECT @Tiene = COUNT(*) FROM [ABSTRACCIONX4].VIAJES V WHERE V.RUTA_ID = @IdRuta
	IF(@Tiene > 0)
	RETURN 1
	ELSE
	RETURN 0
END
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
			' con el código ' + CONVERT(VARCHAR,@Codigo) + ' y servicio ' + @Servicio
		RAISERROR(@Error, 16, 1)
	END CATCH
END

GO


