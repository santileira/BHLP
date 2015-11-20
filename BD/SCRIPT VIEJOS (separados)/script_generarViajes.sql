CREATE PROCEDURE [ABSTRACCIONX4].generarNuevoViaje
	@salida datetime, 
	@llegadaEstimada datetime, 
	@ruta int, 
	@matricula varchar(8)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.VIAJES 
			(VIAJE_FECHA_SALIDA, VIAJE_FECHA_LLEGADA, VIAJE_FECHA_LLEGADAE, AERO_MATRI, RUTA_ID)
			VALUES (@salida, null, @llegadaEstimada, @matricula, @ruta)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe un viaje programado con los mismos detalles'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

