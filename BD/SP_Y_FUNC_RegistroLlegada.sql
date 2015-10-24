/****************************************************************
 *						llegaADestinoCorrecto (FALTA PROBAR)
 ****************************************************************/
CREATE FUNCTION [ABSTRACCIONX4].llegaADestinoCorrecto(@MatriculaTxt varchar(8), @ciuDestinoTxt varchar(80))
RETURNS INT
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
 *						agregarFechaLlegada (FALTA PROBAR)
 ****************************************************************/

CREATE PROCEDURE [ABSTRACCIONX4].agregarFechaLlegada
	@fecha datetime, @viajecod int
AS
	UPDATE [ABSTRACCIONX4].VIAJES
	SET VIAJE_FECHA_LLEGADA = @fecha
	WHERE VIAJE_COD = @viajecod
GO