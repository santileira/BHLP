/****************************************************************
 *						llegaADestinoCorrecto 
 ****************************************************************/
CREATE FUNCTION [ABSTRACCIONX4].llegaADestinoCorrecto(@MatriculaTxt varchar(8), @ciuDestinoTxt varchar(80))
RETURNS INT --NO PONER SMALLINT 
AS 
	BEGIN
	DECLARE @rutaId int,@ciuDestinoId int, @ciuDestino varchar(80), @viajecod int 
	SELECT TOP 1 @rutaId=RUTA_ID, @viajecod = VIAJE_COD FROM [ABSTRACCIONX4].VIAJES WHERE AERO_MATRI=@MatriculaTxt AND VIAJE_FECHA_LLEGADA IS NULL ORDER BY VIAJE_FECHA_SALIDA
	SELECT @ciuDestinoId = CIU_COD_D FROM [ABSTRACCIONX4].RUTAS_AEREAS WHERE RUTA_ID = @rutaId 	
	SELECT @ciuDestino = CIU_DESC FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_COD = @ciuDestinoId

	BEGIN
		IF(@ciuDestino = @ciuDestinoTxt)
		BEGIN
		RETURN @viajecod
		END
	END	
	
	RETURN -1
	
	END
GO


/****************************************************************
 *						esOrigenCorrecto 
 ****************************************************************/

CREATE FUNCTION [ABSTRACCIONX4].esOrigenCorrecto(@MatriculaTxt varchar(8), @ciuOrigenTxt varchar(80))
RETURNS BIT
AS 
	BEGIN
	DECLARE @rutaId int,@ciuOrigenId int, @ciuOrigen varchar(80) 
	SELECT TOP 1 @rutaId=RUTA_ID FROM [ABSTRACCIONX4].VIAJES WHERE AERO_MATRI=@MatriculaTxt AND VIAJE_FECHA_LLEGADA IS NULL ORDER BY VIAJE_FECHA_SALIDA
	SELECT @ciuOrigenId = CIU_COD_O FROM [ABSTRACCIONX4].RUTAS_AEREAS WHERE RUTA_ID = @rutaId 	
	SELECT @ciuOrigen = CIU_DESC FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_COD = @ciuOrigenId

	BEGIN
		IF(@ciuOrigen = @ciuOrigenTxt)
		BEGIN
		RETURN 1
		END
	END	
	
	RETURN 0
	
	END
GO


/****************************************************************
 *						obtenerFechaSalidaDeUnViaje 
 ****************************************************************/

CREATE FUNCTION [ABSTRACCIONX4].obtenerFechaSalidaDeUnViaje(@ViajeCod int)
RETURNS datetime
AS 
	BEGIN
	DECLARE @fechaSalida datetime
	SELECT @fechaSalida = VIAJE_FECHA_SALIDA FROM [ABSTRACCIONX4].VIAJES WHERE VIAJE_COD = @ViajeCod

	RETURN @fechaSalida
	
	END
GO


/****************************************************************
 *						agregarFechaLlegada 
 ****************************************************************/

CREATE PROCEDURE [ABSTRACCIONX4].agregarFechaLlegada
	@fecha datetime, @viajecod int
AS
	UPDATE [ABSTRACCIONX4].VIAJES
	SET VIAJE_FECHA_LLEGADA = @fecha
	WHERE VIAJE_COD = @viajecod
GO

