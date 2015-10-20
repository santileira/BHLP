-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_disponible

 (@matricula VARCHAR(8), @fecha_salida DATETIME, @fecha_llegada_estimada DATETIME)

RETURNS bit

AS

BEGIN
	return (select case 
						when @matricula not in (select distinct AERO_MATRI
												from ABSTRACCIONX4.VIAJES v
												where
												(v.VIAJE_FECHA_SALIDA <= @fecha_salida and v.VIAJE_FECHA_LLEGADAE >= @fecha_salida) or
												(v.VIAJE_FECHA_SALIDA >= @fecha_salida and v.VIAJE_FECHA_LLEGADAE <= @fecha_llegada_estimada) or
												(v.VIAJE_FECHA_SALIDA >= @fecha_salida and v.VIAJE_FECHA_SALIDA <= @fecha_llegada_estimada and v.VIAJE_FECHA_LLEGADAE >= @fecha_llegada_estimada))
						then 1
						else 0
					end)
END