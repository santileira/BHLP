-------------------------------Obtener Historial de Millas Pasajes-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasPasajes(@dni numeric (10,0), @ape varchar(30))
RETURNS TABLE
AS 
	RETURN(
		SELECT	P.PASAJE_COD Codigo,
				'Pasaje' as "Tipo",
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_O) as Origen,  
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_D) as Destino, 
				P.PASAJE_FECHA_COMPRA as "Fecha de Compra",P.PASAJE_PRECIO as Precio,P.PASAJE_MILLAS as "Cant. de Millas"
			FROM ABSTRACCIONX4.CLIENTES C
			JOIN ABSTRACCIONX4.PASAJES P ON C.CLI_COD=P.CLI_COD
			JOIN ABSTRACCIONX4.VIAJES V ON P.VIAJE_COD = V.VIAJE_COD 
			JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON R.RUTA_ID = V.RUTA_ID
		WHERE C.CLI_DNI = @dni AND C.CLI_APELLIDO = @ape AND VIAJE_FECHA_LLEGADA is NOT NULL AND VIAJE_FECHA_LLEGADA BETWEEN 
			GETDATE() + 365  - 365 AND GETDATE() + 730 AND PASAJE_CANCELADO = 0 AND P.PASAJE_MILLAS != 0

		)
GO

-------------------------------Obtener Historial de Millas Encomiendas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(@dni numeric (10,0), @ape varchar(30))
RETURNS TABLE
AS 
	RETURN(
		SELECT	E.ENCOMIENDA_COD as Codigo,
				'Encomienda' as "Tipo",
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_O) as Origen,  
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_D) as Destino, 
				E.ENCOMIENDA_FECHA_COMPRA as "Fecha de Compra",E.ENCOMIENDA_PRECIO as Precio,E.ENCOMIENDA_MILLAS as "Cant. de Millas"
			FROM ABSTRACCIONX4.CLIENTES C
			JOIN ABSTRACCIONX4.ENCOMIENDAS E ON C.CLI_COD=E.CLI_COD
			JOIN ABSTRACCIONX4.VIAJES V ON E.VIAJE_COD = V.VIAJE_COD 
			JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON R.RUTA_ID = V.RUTA_ID
		WHERE C.CLI_DNI = @dni AND C.CLI_APELLIDO = @ape AND VIAJE_FECHA_LLEGADA is NOT NULL AND VIAJE_FECHA_LLEGADA BETWEEN 
			GETDATE() + 365  - 365 AND GETDATE() + 730 AND ENCOMIENDA_CANCELADO = 0 AND E.ENCOMIENDA_MILLAS !=0

		)
GO

-------------------------------Descontar millas-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].DescontarMillas
	@cantMillas int,
	@dni int,
	@ape varchar(30)
AS
BEGIN
	DECLARE @MillasPasaje int
	DECLARE @seguir bit
	DECLARE @Cod int
	DECLARE @tipo varchar(15)
	DECLARE @Fecha datetime
	SET @seguir = 1
	DECLARE cursorPasajes CURSOR FOR (SELECT Codigo,Tipo,[Cant. de Millas],[Fecha de Compra] FROM ABSTRACCIONX4.obtenerHistorialMillasPasajes(@dni,@ape) UNION SELECT Codigo,Tipo,[Cant. de Millas],[Fecha de Compra] FROM ABSTRACCIONX4.obtenerHistorialMillasEncomiendas(@dni,@ape)) ORDER BY [Fecha de Compra] 
	OPEN cursorPasajes
	FETCH NEXT FROM cursorPasajes INTO @Cod,@tipo,@MillasPasaje,@Fecha
	WHILE(@seguir=1)
	BEGIN
	
		
		IF(@cantMillas <= @MillasPasaje)
		BEGIN

			IF(@tipo = 'Pasaje')
			UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_MILLAS = @MillasPasaje - @cantMillas WHERE PASAJE_COD = @Cod
			ELSE
			UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_MILLAS = @MillasPasaje - @cantMillas WHERE ENCOMIENDA_COD = @Cod

		SET @seguir = 0
		END

		IF(@cantMillas > @MillasPasaje)
		BEGIN

			IF(@tipo = 'Pasaje')
			UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_MILLAS = 0 WHERE PASAJE_COD = @Cod
			ELSE
			UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_MILLAS = 0 WHERE ENCOMIENDA_COD = @Cod

		SET @cantMillas = @cantMillas - @MillasPasaje 
		END


	FETCH NEXT FROM cursorPasajes INTO @Cod,@tipo,@MillasPasaje,@Fecha
	END
END

GO

-------------------------------Obtener Ciudad dado el Codigo-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCiudadDesc(@Cod_Ciu int)
RETURNS varchar(15)
AS
BEGIN
	DECLARE @ciudad varchar(15)
	SELECT @ciudad = CIU_DESC FROM CIUDADES WHERE CIU_COD =@Cod_Ciu
	RETURN @ciudad
END

GO


--------------------------------Reducir Cantidad de Stock en un Premio ----------------------

ALTER PROCEDURE [ABSTRACCIONX4].reducirStockDePremio
	@descripcion varchar(100),
	@cantidadSolicitada int
AS
BEGIN
	UPDATE [ABSTRACCIONX4].PREMIOS
	SET PREMIO_STOCK = PREMIO_STOCK - @cantidadSolicitada
	WHERE PREMIO_DETALLE = @descripcion
END

--------------------------------Obtener Puntos de un Premio ----------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerPuntosDePremio(@descripcion varchar(100))
RETURNS int
AS 
	BEGIN
	DECLARE @puntos int
	SELECT @puntos = PREMIO_PUNTOS FROM [ABSTRACCIONX4].PREMIOS WHERE PREMIO_DETALLE = @descripcion

	RETURN @puntos
	
	END
GO


