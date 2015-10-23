
drop function [ABSTRACCIONX4].buscarViajesDisponibles

select *
from [ABSTRACCIONX4].buscarViajesDisponibles('2016-18-05', ' Ámsterdam', ' Nueva York')

select distinct v.VIAJE_FECHA_SALIDA Fecha_Salida, v.VIAJE_FECHA_LLEGADAE Fecha_Llegada, c1.CIU_DESC Origen, c2.CIU_DESC Destino, s.SERV_DESC
			from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.RUTAS_AEREAS r1, ABSTRACCIONX4.RUTAS_AEREAS r2,ABSTRACCIONX4.CIUDADES c1, ABSTRACCIONX4.CIUDADES c2,ABSTRACCIONX4.SERVICIOS s
			where v.RUTA_ID = r1.RUTA_ID and
				v.RUTA_ID = r2.RUTA_ID and
				r1.CIU_COD_O = c1.CIU_COD and
				r2.CIU_COD_D = c2.CIU_COD and
				r1.SERV_COD = s.SERV_COD
	order by Fecha_Salida


	



select *
from ABSTRACCIONX4.VIAJES

select VIAJE_FECHA_SALIDA, RUTA_ID ,COUNT(*)
from ABSTRACCIONX4.VIAJES
GROUP BY VIAJE_FECHA_SALIDA, RUTA_ID
order by VIAJE_FECHA_SALIDA

2016-04-14 08:00:00.000
origen 5  Zúrich
destino 17  Nueva York

select *
from ABSTRACCIONX4.RUTAS_AEREAS
order by RUTA_COD

select *
from ABSTRACCIONX4.CIUDADES
order by CIU_COD

select *
from ABSTRACCIONX4.SERVICIOS


