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
												(@fecha_salida between v.VIAJE_FECHA_SALIDA and v.VIAJE_FECHA_LLEGADAE) or
												(@fecha_llegada_estimada between v.VIAJE_FECHA_SALIDA and v.VIAJE_FECHA_LLEGADAE))
						then 1
						else 0
					end)
END