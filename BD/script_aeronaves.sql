-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_disponible

 (@matricula VARCHAR(8), @fecha_salida DATETIME)

RETURNS bit

AS

BEGIN
	return (select count(AERO_MATRI)
			from ABSTRACCIONX4.VIAJES v
			where v.AERO_MATRI = @matricula and
			((@fecha_salida between v.VIAJE_FECHA_SALIDA and v.VIAJE_FECHA_LLEGADAE) or
			(@fecha_salida < v.VIAJE_FECHA_SALIDA and @fecha_salida < v.VIAJE_FECHA_SALIDA) or
			(v.VIAJE_FECHA_SALIDA = @fecha_salida)))
END