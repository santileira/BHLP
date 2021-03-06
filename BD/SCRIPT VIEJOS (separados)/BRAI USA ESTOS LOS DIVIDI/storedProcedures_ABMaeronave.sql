



-------------------------------Actualizar Butacas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AgregarButacas 
@Matricula VARCHAR(8), 
@CantidadPasillo TINYINT, 
@CantidadVentanilla TINYINT
AS
BEGIN
	DECLARE @i SMALLINT
	SET @i = 0
	DECLARE @CantidadButacas TINYINT
	SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
	WHILE (@i < @CantidadPasillo)
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_PISO , AERO_MATRI , BUT_TIPO)
		VALUES (@i , 1 , @Matricula , 'Pasillo')
		SET @i = @i + 1
	END

	WHILE (@i < @CantidadButacas)
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_PISO , AERO_MATRI , BUT_TIPO)
		VALUES (@i , 1 , @Matricula , 'Ventanilla')
		SET @i = @i + 1
	END
	
END

GO





-------------------------------Baja Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @TieneViajeEnEsasFechas BIT
	SET @TieneViajeEnEsasFechas = [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,@FechaReinicio)

	IF @TieneViajeEnEsasFechas = 1
	BEGIN
		DECLARE @Error varchar(80)
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		RETURN
	END
	
	UPDATE ABSTRACCIONX4.AERONAVES 
			SET AERO_BAJA_FS = 1, AERO_FECHA_RS = @FechaReinicio , AERO_FECHA_FS = @FechaBaja
			WHERE AERO_MATRI = @Matricula
END
GO

-------------------------------Dar de Baja Logica-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
	DECLARE @TieneViajeEnEsasFechas BIT
	SET @TieneViajeEnEsasFechas = [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,NULL)

	IF @TieneViajeEnEsasFechas = 1
	BEGIN
		DECLARE @Error varchar(80)
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		RETURN
	END

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

-------------------------------Tiene viaje entre fechas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].TieneViajeEntreFechas
	(@Matricula VARCHAR(8),@Fecha1 DATETIME,@Fecha2 DATETIME)
RETURNS BIT
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@Fecha2)

	DECLARE @Cantidad INT
	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.VIAJES
		WHERE AERO_MATRI = @Matricula
			AND ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@FechaMaxima) = 1

	IF @Cantidad > 0
		RETURN 1
	RETURN 0
END

GO

-------------------------------Cantidad Butacas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].CantidadButacas
	(@Matricula VARCHAR(8) , @Tipo VARCHAR(30))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Cantidad SMALLINT
	
	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.BUTACAS
		   WHERE AERO_MATRI = @Matricula
		   AND BUT_TIPO = @Tipo
	RETURN @Cantidad
END
GO

-------------------------------Cancelar Pasajes Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].CancelarPasajesEncomiendasAeronave
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)

	IF @FechaReinicio IS NULL
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
				WHERE AERO_MATRI = @Matricula
		END
	ELSE
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_BAJA_FS = 1 , AERO_FECHA_FS = @FechaBaja , AERO_FECHA_RS = @FechaReinicio
				WHERE AERO_MATRI = @Matricula
		END
		
		
		UPDATE ABSTRACCIONX4.PASAJES
			SET PASAJE_CANCELADO = 1 
			WHERE AERO_MATRI = @Matricula AND 
				  PASAJE_COD IN (SELECT * FROM [ABSTRACCIONX4].PasajesEntreFechas(@FechaBaja,@FechaMaxima))

		UPDATE ABSTRACCIONX4.ENCOMIENDAS
			SET ENCOMIENDA_CANCELADO = 1 
			WHERE AERO_MATRI = @Matricula AND 
				  ENCOMIENDA_COD IN (SELECT * FROM [ABSTRACCIONX4].EncomiendasEntreFechas(@FechaBaja,@FechaMaxima))
END

GO

-------------------------------Pasajes entre fechas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].PasajesEntreFechas
	(@Fecha1 DATETIME, @Fecha2 DATETIME)
RETURNS @Pasajes TABLE (PASAJE_COD INT)
AS
BEGIN
	INSERT INTO @Pasajes
	SELECT PASAJE_COD 
			FROM ABSTRACCIONX4.PASAJES P JOIN ABSTRACCIONX4.VIAJES V ON (P.VIAJE_COD = V.VIAJE_COD)
			WHERE ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@Fecha2) = 1

	RETURN
END

GO

-------------------------------Encomiendas entre fechas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].EncomiendasEntreFechas
	(@Fecha1 DATETIME, @Fecha2 DATETIME)
RETURNS @Encomiendas TABLE (ENCOMIENDA_COD INT)
AS
BEGIN
	INSERT INTO @Encomiendas
	SELECT ENCOMIENDA_COD 
			FROM ABSTRACCIONX4.ENCOMIENDAS E JOIN ABSTRACCIONX4.VIAJES V ON (E.VIAJE_COD = V.VIAJE_COD)
			WHERE ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@Fecha2) = 1

	RETURN
END

GO


-------------------------------Suplantar Aeronave Baja-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].SuplantarAeronave
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)
	
	DECLARE @MatriculaNueva VARCHAR(8)
	SET @MatriculaNueva = ABSTRACCIONX4.AeronaveDeMismasCaracteristicas(@Matricula,@FechaBaja,@FechaMaxima)
	
	IF @MatriculaNueva IS NULL
	BEGIN
		DECLARE @Error varchar(80)
		SET @Error = 'Ninguna aeronave de la flota tiene las mismas características'
		RAISERROR(@Error, 16, 1)
		RETURN
	END
	
	EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima
	EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima
	EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima

	IF @FechaReinicio IS NULL
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_BAJA_VU = 1 , AERO_FECHA_BAJA = @FechaBaja
				WHERE AERO_MATRI = @Matricula
		END
	ELSE
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_BAJA_FS = 1 , AERO_FECHA_FS = @FechaBaja , AERO_FECHA_RS = @FechaReinicio
				WHERE AERO_MATRI = @Matricula
		END
END

GO

-------------------------------Fecha de reinicio o maxima-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].FechaReinicioOMaxima
	(@FechaReinicio DATETIME)
RETURNS DATETIME
AS
BEGIN
	IF @FechaReinicio IS NULL
	BEGIN
		DECLARE @FechaMaxima DATETIME
		SELECT @FechaMaxima = MAX(VIAJE_FECHA_LLEGADA) FROM ABSTRACCIONX4.VIAJES
		RETURN @FechaMaxima
	END 
	RETURN @FechaReinicio
END

GO


-------------------------------Aeronave de mismas caracteristicas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].AeronaveDeMismasCaracteristicas
	(@Matricula VARCHAR(8),@FechaBaja DATETIME,@FechaReinicio DATETIME)
RETURNS VARCHAR(8)
AS
BEGIN
	DECLARE @TipoServicio TINYINT
	DECLARE @Fabricante VARCHAR(30)
	DECLARE @Modelo VARCHAR(30)
	DECLARE @CantidadKG NUMERIC(6,2)

	SELECT @TipoServicio = SERV_COD, @Fabricante = AERO_FAB,
		   @Modelo = AERO_MOD, 
		   @CantidadKG = AERO_CANT_KGS
		   FROM ABSTRACCIONX4.AERONAVES
		   WHERE AERO_MATRI = @Matricula


	DECLARE @MatriculaNueva VARCHAR(8)
	SET @MatriculaNueva = NULL
	SELECT TOP 1 @MatriculaNueva = AERO_MATRI
		FROM ABSTRACCIONX4.AERONAVES
		WHERE AERO_MATRI <> @Matricula AND
			  SERV_COD = @TipoServicio AND 
			  AERO_FAB = @Fabricante AND
			  AERO_MOD = @Modelo AND
			  [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_ALTA,@FechaBaja,@FechaReinicio) = 0 AND
			  AERO_CANT_KGS >= @CantidadKG AND
			  AERO_BAJA_FS = 0 AND AERO_BAJA_VU = 0 AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Pasillo') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Pasillo') AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Ventanilla') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Ventanilla') AND
			  ABSTRACCIONX4.DisponibleParaTodosLosVuelosDe(AERO_MATRI,@Matricula,@FechaBaja,@FechaReinicio) = 1
			  

	RETURN @MatriculaNueva
END

GO

-------------------------------Cantidad de butacas p/v de una aeronave-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].CantidadButacas
	(@Matricula VARCHAR(8),@Tipo VARCHAR(15))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Butacas SMALLINT

	SELECT @Butacas = COUNT(*) 
		FROM ABSTRACCIONX4.BUTACAS
		WHERE AERO_MATRI = @Matricula AND
			  BUT_TIPO = @Tipo

	RETURN @Butacas
END

GO

-------------------------------Disponible para todos los vuelos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DisponibleParaTodosLosVuelosDe
	(@MatriculaNueva VARCHAR(8),@MatriculaVieja VARCHAR(8),@FechaBaja DATETIME,@FechaReinicio DATETIME)
RETURNS BIT
AS
BEGIN
	DECLARE @Cantidad INT
	SET @Cantidad = 0

	/*DECLARE @MaximaFechaSalida DATETIME
	SELECT @MaximaFechaSalida = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)*/

	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.VIAJES v
		WHERE v.AERO_MATRI = @MatriculaVieja AND
			  [ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1
			  AND
			  [ABSTRACCIONX4].aeronave_disponible(@MatriculaNueva,v.VIAJE_FECHA_SALIDA,v.VIAJE_FECHA_LLEGADAE) = 0

	IF @Cantidad > 0
		RETURN 0
	RETURN 1
END

GO



-------------------------------Borrar Butacas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarButacas 
@Matricula VARCHAR(8)
AS
BEGIN
	DELETE FROM [ABSTRACCIONX4].BUTACAS 
	WHERE AERO_MATRI = @Matricula
END
GO

-------------------------------Modificar Aeronave Pasajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronavePasajes
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	IF(@FechaBaja is NULL)
	BEGIN
		UPDATE [ABSTRACCIONX4].PASAJES
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
	END
	ELSE BEGIN
	UPDATE [ABSTRACCIONX4].PASAJES
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja AND 
			  [ABSTRACCIONX4].ExisteViajeEntreFechas(
			  [ABSTRACCIONX4].FechaSalidaDeViaje(VIAJE_COD),
			   @FechaBaja,@FechaReinicio) = 1
	END
END
GO
-------------------------------Modificar Aeronave Encomiendas-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveEncomiendas
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	IF(@FechaBaja IS NULL)
	BEGIN
		UPDATE [ABSTRACCIONX4].ENCOMIENDAS
			SET AERO_MATRI = @MatriculaNueva
			WHERE AERO_MATRI = @MatriculaVieja
	END
	ELSE
	BEGIN
	UPDATE [ABSTRACCIONX4].ENCOMIENDAS
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja AND 
				  [ABSTRACCIONX4].ExisteViajeEntreFechas(
				  [ABSTRACCIONX4].FechaSalidaDeViaje(VIAJE_COD),
				  @FechaBaja,@FechaReinicio) = 1
	END
END
GO

-------------------------------Fecha de salida de viaje-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].FechaSalidaDeViaje
	(@ViajeCod INT)
RETURNS DATETIME
AS
BEGIN
	DECLARE @FechaSalida DATETIME
	SELECT @FechaSalida = VIAJE_FECHA_SALIDA FROM ABSTRACCIONX4.VIAJES WHERE VIAJE_COD = @ViajeCod
	RETURN @FechaSalida
END

GO

-------------------------------Modificar Aeronave Butacas-------------------------------

CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveButacas
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].BUTACAS
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja
END
GO


------------------------------Modificar Aeronave Viajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].VIAJES
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja AND
			  [ABSTRACCIONX4].ExisteViajeEntreFechas(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1
END
GO

-------------------------------Viajes asignados a aeronave-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ExisteViajeEntreFechas
	(@FechaViaje DATETIME,@Fecha1 DATETIME,@Fecha2 DATETIME)
RETURNS BIT
AS
BEGIN
	IF @Fecha1 IS NULL
		RETURN 1

	DECLARE @Resultado BIT
	SET @Resultado = [ABSTRACCIONX4].datetime_is_between(@FechaViaje,@Fecha1,@Fecha2)

	RETURN @Resultado
END

GO


-------------------------------Modificacion Butaca-------------------------------
CREATE TRIGGER [ABSTRACCIONX4].ModificacionButaca
ON [ABSTRACCIONX4].BUTACAS
INSTEAD OF UPDATE
AS
DECLARE @MatriculaVieja VARCHAR(8)
DECLARE @MatriculaNueva VARCHAR(8)
declaRE @Error varchar(100)
BEGIN
	SELECT TOP 1 @MatriculaVieja = AERO_MATRI FROM DELETED
	SELECT TOP 1 @MatriculaNueva = AERO_MATRI FROM INSERTED
	
	IF(UPDATE(AERO_MATRI))
	BEGIN TRY
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_TIPO , AERO_MATRI , BUT_PISO)
		SELECT BUT_NRO , BUT_TIPO , AERO_MATRI , BUT_PISO FROM INSERTED
		
		EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @MatriculaVieja , @MatriculaNueva , NULL , NULL
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @MatriculaVieja , @MatriculaNueva , NULL , NULL
		
		DELETE FROM [ABSTRACCIONX4].BUTACAS WHERE AERO_MATRI = @MatriculaVieja
	END TRY
	BEGIN CATCH
	SET @Error = 'ENTRE A ERROR TRIGGER BUTACA' + @MatriculaNueva 
				RAISERROR(@Error, 16, 1)
	END CATCH
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

-------------------------------Obter Codigo de Servicio-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Codigo TINYINT
	SELECT @Codigo = SERV_COD FROM [ABSTRACCIONX4].SERVICIOS WHERE SERV_DESC = @TipoDeServicio
	RETURN @Codigo
END
GO

-------------------------------Modificar Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarAeronave
	@MatriculaActual VARCHAR(8),
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadPasillo SMALLINT,
	@CantidadVentanilla SMALLINT,
	@CantidadKG NUMERIC(6,2)
AS
		DECLARE @Error varchar(80)
		DECLARE @ExisteMatricula BIT
BEGIN 
		DECLARE @CodigoServicio SMALLINT
		DECLARE @viajeComprado BIT
		DECLARE @CantidadButacas SMALLINT
		SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
		SELECT @ExisteMatricula = COUNT(*) FROM [ABSTRACCIONX4].AERONAVES WHERE AERO_MATRI = @Matricula AND AERO_MATRI <> @MatriculaActual
		SET @viajeComprado = [ABSTRACCIONX4].tieneViajeComprado(@MatriculaActual)
		--EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual	
		
		IF(@ExisteMatricula = 0)
		BEGIN
			SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
			--si tiene viaje comprado solo modifico su nombre, no se puede otra cosa EN AERONAVES
			IF( @viajeComprado = 1)
			BEGIN
				IF(@Matricula != @MatriculaActual)
				BEGIN	
					INSERT INTO [ABSTRACCIONX4].AERONAVES 
					(AERO_MOD , AERO_MATRI , AERO_FAB , SERV_COD , AERO_CANT_BUTACAS , AERO_CANT_KGS) VALUES
					(@Modelo , @Matricula , @Fabricante , @CodigoServicio , @CantidadButacas , @CantidadKG)

					EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @MatriculaActual , @Matricula , NULL , NULL
					EXECUTE [ABSTRACCIONX4].ModificarAeronaveButacas @MatriculaActual , @Matricula

					DELETE FROM [ABSTRACCIONX4].AERONAVES
					WHERE AERO_MATRI = @MatriculaActual
				END
			END
			ELSE
			BEGIN
					EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual
				
					UPDATE ABSTRACCIONX4.AERONAVES
					SET AERO_MOD = @Modelo , AERO_FAB = @Fabricante, AERO_MATRI = @Matricula ,
					SERV_COD = @CodigoServicio,
					AERO_CANT_BUTACAS = @CantidadButacas, AERO_CANT_KGS = @CantidadKG
					WHERE AERO_MATRI = @MatriculaActual

					EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
			END
		END	
		ELSE
		BEGIN
			SET @Error = '(modificar aeronave)Ya existe una aeronave con matrícula ' + @Matricula
			RAISERROR(@Error, 16, 1)
		END	
		
END
GO


-------------------------------Obter Codigo de Ciudad-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad(@Ciudad VARCHAR(80))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Codigo SMALLINT
	SELECT @Codigo = CIU_COD FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_DESC = @Ciudad
	RETURN @Codigo
END
GO


-------------------------------Alta Aeronave-------------------------------

create PROCEDURE [ABSTRACCIONX4].AltaAeronave
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadPasillo SMALLINT,
	@CantidadVentanilla SMALLINT,
	@CantidadKG NUMERIC(6,2),
	@FechaAlta DATETIME,
	@CiudadPrincipal VARCHAR(80)
AS
	BEGIN TRY
		DECLARE @CodigoServicio TINYINT
		DECLARE @CantidadButacas TINYINT
		DECLARE @CodigoCiudad SMALLINT
		SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
		SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
		SET @CodigoCiudad = [ABSTRACCIONX4].ObtenerCodigoCiudad(@CiudadPrincipal)

		INSERT INTO ABSTRACCIONX4.AERONAVES 
			(AERO_MOD,AERO_MATRI,AERO_FAB,SERV_COD,AERO_CANT_BUTACAS,AERO_CANT_KGS,AERO_FECHA_ALTA , CIU_COD_P)
			VALUES (@Modelo,@Matricula,@Fabricante,@CodigoServicio,@CantidadButacas,@CantidadKG,@FechaAlta,@CodigoCiudad)
		EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe una aeronave con matrícula ' + @Matricula
		RAISERROR(@Error, 16, 1)
	END CATCH


GO

-------------------------------Obtener Codigo de Ciudad-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DatosDeAeronaveASuplantar(@Matricula VARCHAR(8),@FechaBaja DATETIME)
RETURNS @Datos TABLE (AERO_MOD VARCHAR(30),AERO_FAB VARCHAR(30),SERV_DESC VARCHAR(30),CIU_DESC VARCHAR(80),
				BUT_PASILLO SMALLINT, BUT_VENTANILLA SMALLINT, CANT_KGS NUMERIC(6,2))
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = ABSTRACCIONX4.FechaReinicioOMaxima(NULL)

	INSERT INTO @Datos
		SELECT AERO_MOD, AERO_FAB, s.SERV_DESC, 
			   [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula,@FechaBaja),
			   (SELECT COUNT(*) 
					FROM ABSTRACCIONX4.BUTACAS 
					WHERE AERO_MATRI = @Matricula AND BUT_TIPO = 'Pasillo'),
				(SELECT COUNT(*) 
					FROM ABSTRACCIONX4.BUTACAS 
					WHERE AERO_MATRI = @Matricula AND BUT_TIPO = 'Ventanilla'),
				AERO_CANT_KGS
			FROM ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (s.SERV_COD = a.SERV_COD)
			WHERE AERO_MATRI = @Matricula

	RETURN
END
GO


-------------------------------Ciudad en la que se encuentra-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula VARCHAR(8),@FechaBaja DATETIME)
RETURNS VARCHAR(80)
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = ABSTRACCIONX4.FechaReinicioOMaxima(NULL)

	DECLARE @Ciudad VARCHAR(80)

	SELECT TOP 1 @Ciudad = c.CIU_DESC 
		FROM ABSTRACCIONX4.VIAJES v JOIN ABSTRACCIONX4.RUTAS_AEREAS r ON (v.RUTA_ID=r.RUTA_ID)
									JOIN ABSTRACCIONX4.CIUDADES c ON (r.CIU_COD_O=C.CIU_COD)
		WHERE v.AERO_MATRI = @Matricula AND 
			  ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaMaxima) = 1
		ORDER BY v.VIAJE_FECHA_SALIDA

	RETURN @Ciudad
END
GO




/*
DROP  PROCEDURE [ABSTRACCIONX4].BorrarEncomiendas
DROP PROCEDURE [ABSTRACCIONX4].BorrarPasajes
DROP FUNCTION [ABSTRACCIONX4].TieneViajeAsignado
DROP TRIGGER [ABSTRACCIONX4].ModificacionButaca
DROP FUNCTION [ABSTRACCIONX4].ExisteViajeEntreFechas
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveButacas
DROP FUNCTION [ABSTRACCIONX4].FechaSalidaDeViaje
DROP PROCEDURE [ABSTRACCIONX4].AltaAeronave
DROP PROCEDURE [ABSTRACCIONX4].ModificarAeronave
DROP PROCEDURE [ABSTRACCIONX4].AgregarButacas
DROP PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
DROP PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
DROP PROCEDURE [ABSTRACCIONX4].BorrarButacas
DROP FUNCTION [ABSTRACCIONX4].TieneViajeComprado
DROP FUNCTION [ABSTRACCIONX4].CantidadButacas
DROP PROCEDURE [ABSTRACCIONX4].CancelarPasajesEncomiendasAeronave
DROP FUNCTION [ABSTRACCIONX4].PasajesEntreFechas
DROP FUNCTION [ABSTRACCIONX4].EncomiendasEntreFechas
DROP PROCEDURE [ABSTRACCIONX4].SuplantarAeronave
DROP FUNCTION [ABSTRACCIONX4].FechaReinicioOMaxima
DROP FUNCTION [ABSTRACCIONX4].AeronaveDeMismasCaracteristicas
DROP FUNCTION [ABSTRACCIONX4].CantidadButacas
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronavePasajes
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveEncomiendas
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
DROP FUNCTION [ABSTRACCIONX4].CantidadButacas
DROP FUNCTION [ABSTRACCIONX4].DisponibleParaTodosLosVuelosDe
DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigoServicio
DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad
DROP FUNCTION [ABSTRACCIONX4].TieneViajeEntreFechas
DROP FUNCTION [ABSTRACCIONX4].DatosDeAeronaveASuplantar
DROP FUNCTION [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra
*/