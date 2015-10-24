
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
	from ABSTRACCIONX4.CIUDADES

	
	select *
	from ABSTRACCIONX4.BUTACAS
	where BUT_TIPO = 'Ventanilla'


select *
from ABSTRACCIONX4.VIAJES

select *
from ABSTRACCIONX4.RUTAS_AEREAS
where CIU_COD_D = 17 and CIU_COD_O = 35

select *
from ABSTRACCIONX4.VIAJES v
where v.RUTA_ID = 42

select * 
from ABSTRACCIONX4.AERONAVES a
where a.AERO_MATRI = 'BZD-177'





select a.AERO_CANT_BUTACAS
					from ABSTRACCIONX4.AERONAVES a
					where a.AERO_MATRI = 'BZD-177'

select count(*)
					from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
					where p.BUT_NRO = b.BUT_NRO and
					p.VIAJE_COD = 8507 and
					b.AERO_MATRI = 'BZD-177'








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


select *
from ABSTRACCIONX4.VIAJES
where AERO_MATRI = 'ASQ-169'

select *
from ABSTRACCIONX4.AERONAVES
where AERO_MATRI = 'ASQ-169'


select [ABSTRACCIONX4].kilosDisponibles(81, 'ASQ-169')

select(
(select a.AERO_CANT_KGS
				from ABSTRACCIONX4.AERONAVES a
				where a.AERO_MATRI = 'ASQ-169') -
			
				(select sum(e.ENCOMIENDA_PESO_KG)
				from ABSTRACCIONX4.ENCOMIENDAS e
				where e.VIAJE_COD = 81 and
				e.AERO_MATRI = 'ASQ-169'))


select [ABSTRACCIONX4].butacasDisponibles(330, 'ASQ-169', 1)

select ((select a.AERO_CANT_BUTACAS
					from ABSTRACCIONX4.AERONAVES a
					where a.AERO_MATRI = 'ASQ-169') -
			
					(select distinct count(p.PASAJE_COD)
					from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
					where p.BUT_NRO = b.BUT_NRO and
					p.VIAJE_COD = 330 and
					b.AERO_MATRI = 'ASQ-169' and
					b.BUT_TIPO = 'Pasillo')
					)
				


select distinct top 20 v.VIAJE_FECHA_SALIDA Fecha_Salida, v.VIAJE_FECHA_LLEGADAE Fecha_Llegada, 
				c1.CIU_DESC Origen, c2.CIU_DESC Destino, s.SERV_DESC, [ABSTRACCIONX4].butacasDisponibles(v.VIAJE_COD, v.AERO_MATRI, 1) Butacas_Pasillo_Disponibles, 
					[ABSTRACCIONX4].butacasDisponibles(v.VIAJE_COD, v.AERO_MATRI, 0) Butacas_Ventanilla_Disponibles, [ABSTRACCIONX4].kilosDisponibles(v.VIAJE_COD, v.AERO_MATRI) Kilos_Disponibles
			from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.RUTAS_AEREAS r1, ABSTRACCIONX4.RUTAS_AEREAS r2,
				ABSTRACCIONX4.CIUDADES c1, ABSTRACCIONX4.CIUDADES c2,ABSTRACCIONX4.SERVICIOS s
			where v.RUTA_ID = r1.RUTA_ID and
				v.RUTA_ID = r2.RUTA_ID and
				r1.CIU_COD_O = c1.CIU_COD and
				r2.CIU_COD_D = c2.CIU_COD and
				r1.SERV_COD = s.SERV_COD