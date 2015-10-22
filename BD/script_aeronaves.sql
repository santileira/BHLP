-------------------------------Convertir datetime a formato yyyy/dd/mm hh:mm:ss.0000----------------------------------------------
CREATE FUNCTION [ABSTRACCIONX4].convert_datetime

 (@fecha DATETIME)

RETURNS datetime

AS

BEGIN
	return DATETIMEFROMPARTS(year(@fecha), day(@fecha), month(@fecha), datepart(hour, @fecha), datepart(minute, @fecha), datepart(second, @fecha), 0)
END
GO
-------------------------------Fecha entre dos fechas----------------------------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_between

 (@fecha DATETIME, @fecha1 DATETIME, @fecha2 DATETIME)

RETURNS smallint

AS

BEGIN
	if(datediff(minute, '1900-01-01 00:00:00.0000000', @fecha) between datediff(minute, '1900-01-01 00:00:00.0000000', @fecha1) and datediff(minute, '1900-01-01 00:00:00.0000000', @fecha2))
	begin
		return 1
	end
	
	return 0
END
GO
-------------------------------Saber si un datetime esta entre otros dos datetime------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_is_between
 (@fecha DATETIME, @fecha1 DATETIME, @fecha2 DATETIME)

RETURNS smallint

AS

BEGIN
	if(@fecha1 <= @fecha2)
		begin
			if([ABSTRACCIONX4].datetime_between(@fecha, @fecha1, @fecha2) = 1)
				begin
					return 1
				end
		end
	else
		begin
			if([ABSTRACCIONX4].datetime_between(@fecha, @fecha2, @fecha1) = 1)
				begin
					return 1
				end
		end

	return 0
END

GO

-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_disponible

 (@matricula VARCHAR(8), @fecha_salida DATETIME, @fecha_llegada_estimada DATETIME)

RETURNS smallint

AS
	
BEGIN

	return (select case 
						when @matricula not in (select distinct AERO_MATRI
												from ABSTRACCIONX4.VIAJES v
												where
												([ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_SALIDA, [ABSTRACCIONX4].convert_datetime(@fecha_salida), [ABSTRACCIONX4].convert_datetime(@fecha_llegada_estimada)) = 1) or
												([ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_LLEGADAE,[ABSTRACCIONX4].convert_datetime(@fecha_salida), [ABSTRACCIONX4].convert_datetime(@fecha_llegada_estimada)) = 1) or
												([ABSTRACCIONX4].datetime_is_between([ABSTRACCIONX4].convert_datetime(@fecha_salida), v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE) = 1) or
												([ABSTRACCIONX4].datetime_is_between([ABSTRACCIONX4].convert_datetime(@fecha_llegada_estimada), v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE) = 1)
												)
						then 1
						else 0
					end)
END
GO



