-------------------------------Conversion de datetime a date------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_to_date

 (@fecha DATETIME)

RETURNS date

AS

BEGIN
	return DATEFROMPARTS(year(@fecha), month(@fecha), day(@fecha))
END
GO


-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_disponible

 (@matricula VARCHAR(8), @fecha_salida DATETIME, @fecha_llegada_estimada DATETIME)

RETURNS bit

AS

BEGIN
	DECLARE @salida_existente date, @llegada_existente date, @nueva_salida date, @nueva_llegada date;

	SET @nueva_salida = [ABSTRACCIONX4].datetime_to_date(@fecha_salida)
	SET @nueva_llegada = [ABSTRACCIONX4].datetime_to_date(@fecha_llegada_estimada)

	return (select case 
						when @matricula not in (select distinct AERO_MATRI
												from ABSTRACCIONX4.VIAJES v
												where(
												([ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_SALIDA) <= @nueva_salida and [ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_LLEGADAE) >= @nueva_salida) or
												([ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_SALIDA) >= @nueva_salida and [ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_LLEGADAE) <= @nueva_llegada) or
												([ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_SALIDA) >= @nueva_salida and [ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_SALIDA) <= @nueva_llegada and [ABSTRACCIONX4].datetime_to_date(v.VIAJE_FECHA_LLEGADAE) >= @nueva_llegada)
												))
						then 1
						else 0
					end)
END
GO



