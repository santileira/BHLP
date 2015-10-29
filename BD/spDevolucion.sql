--------------------------------Obtener Codigo--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigo(@PNR INT)
RETURNS INT
AS
BEGIN
	DECLARE @Codigo INT
	SELECT @Codigo = COMP_COD FROM [ABSTRACCIONX4].COMPRAS WHERE @PNR = COMP_PNR
	RETURN @Codigo
END
GO

--------------------------------Esta en viaje pasaje--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].EstaEnViajePasaje(@Codigo INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Esta BIT
	DECLARE @Cantidad INT

	SELECT @Cantidad = COUNT(*) FROM PASAJES P , VIAJES V
	WHERE P.VIAJE_COD = V.VIAJE_COD AND P.PASAJE_COD = @Codigo
	AND [ABSTRACCIONX4].datetime_is_between(GETDATE(),V.VIAJE_FECHA_SALIDA,V.VIAJE_FECHA_LLEGADAE) = 0
	AND [ABSTRACCIONX4].datetime_esMayor(GETDATE() , V.VIAJE_FECHA_LLEGADAE) = 1

	IF(@Cantidad > 0)
	SET @Esta  = 1
	ELSE
	SET @Esta  = 0
	RETURN @Esta
END
GO

--------------------------------Esta en viaje encomienda--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].EstaEnViajeEncomienda(@Codigo INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Esta BIT
	DECLARE @Cantidad INT

	SELECT @Cantidad = COUNT(*) FROM ENCOMIENDAS E , VIAJES V
	WHERE E.VIAJE_COD = V.VIAJE_COD AND E.ENCOMIENDA_COD = @Codigo
	AND [ABSTRACCIONX4].datetime_is_between(GETDATE(),V.VIAJE_FECHA_SALIDA,V.VIAJE_FECHA_LLEGADAE) = 0
	AND [ABSTRACCIONX4].datetime_esMayor(GETDATE() , V.VIAJE_FECHA_LLEGADAE) = 1
	IF(@Cantidad > 0)
	RETURN 1
	RETURN 0
END
GO

--------------------------------Fecha es mayor que Fecha1--------------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_esMayor(@Fecha DATETIME , @Fecha1 DATETIME)
RETURNS BIT
AS
BEGIN

IF(datediff(minute, '1900-01-01 00:00:00.0000000', @Fecha) > datediff(minute, '1900-01-01 00:00:00.0000000',  @Fecha1))
	BEGIN
		RETURN 1
	END
		RETURN 0
END
GO


--------------------------------Llenar Encomiendas--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].LlenarEncomiendas(@Codigo INT)
RETURNS TABLE 
AS

	RETURN(SELECT	[ENCOMIENDA_COD],
					[COMP_COD],
					[CLI_COD],
					[VIAJE_COD],
					[ENCOMIENDA_PRECIO],
					[ENCOMIENDA_FECHA_COMPRA],
					[ENCOMIENDA_PESO_KG],
					[AERO_MATRI] ,
					[ENCOMIENDA_CANCELADO] 
			FROM ABSTRACCIONX4.ENCOMIENDAS
			WHERE COMP_COD = @Codigo AND
			[ABSTRACCIONX4].EstaEnViajeEncomienda(ENCOMIENDA_COD) = 0 AND
			ENCOMIENDA_CANCELADO = 0
		  )

GO




--------------------------------Llenar Pasajes--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].LlenarPasajes(@Codigo INT)
RETURNS TABLE /*([PASAJE_COD] INT,
			   [COMP_COD] INT,
			   [CLI_COD] INT,
			   [VIAJE_COD] INT ,
	           [PASAJE_PRECIO] NUMERIC(7,2),
	           [PASAJE_FECHA_COMPRA] DATETIME,
	           [BUT_NRO] SMALLINT,
	           [AERO_MATRI] VARCHAR(8),
	           [PASAJE_CANCELADO] BIT)*/
AS

	RETURN(SELECT	[PASAJE_COD],
					[COMP_COD],
					[CLI_COD],
					[VIAJE_COD],
					[PASAJE_PRECIO],
					[PASAJE_FECHA_COMPRA],
					[BUT_NRO],
					[AERO_MATRI] ,
					[PASAJE_CANCELADO] 
			FROM ABSTRACCIONX4.PASAJES 
			WHERE COMP_COD = @Codigo AND
			[ABSTRACCIONX4].EstaEnViajePasaje(PASAJE_COD) = 0 AND
			PASAJE_CANCELADO = 0
		  )

GO

-------------------------------Tipo Lista1-------------------------------
CREATE TYPE [ABSTRACCIONX4].Lista1 AS TABLE 
( elemento VARCHAR(30) )

GO


--------------------------------Cancelar Pasajes y Encomiendas--------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].CancelarPasajesYEncomiendas
	@Codigo INT,
	@Pasajes Lista1 readonly,
	@Encomiendas Lista1 readonly,
	@FechaDevolucion DATETIME,
	@Motivo VARCHAR(255)
AS
BEGIN
	DECLARE @CodigoDev INT
	INSERT INTO [ABSTRACCIONX4].DEVOLUCIONES (DEVOLUC_FECHA , DEVOLUC_MOTIVO)
	VALUES (@FechaDevolucion , @Motivo)
END
GO



/*
DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigo
DROP FUNCTION [ABSTRACCIONX4].datetime_esMayor
DROP FUNCTION [ABSTRACCIONX4].EstaEnViajeEncomienda
DROP FUNCTION [ABSTRACCIONX4].EstaEnViajePasaje
DROP FUNCTION [ABSTRACCIONX4].LlenarEncomiendas
DROP FUNCTION [ABSTRACCIONX4].LlenarPasajes
DROP PROCEDURE [ABSTRACCIONX4].CancelarPasajesYEncomiendas
DROP TYPE [ABSTRACCIONX4].Lista1 
*/