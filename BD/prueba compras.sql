
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

	select( (select 2) - (select 1))

	select *
	from ABSTRACCIONX4.CIUDADES

	select *
	from ABSTRACCIONX4.AERONAVES
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


select *
from abstraccionx4.pasajes

select *
from abstraccionx4.CLIENTES

select a.AERO_CANT_BUTACAS
					from ABSTRACCIONX4.AERONAVES a
					where a.AERO_MATRI = 'BZD-177'

select count(*)
					from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
					where p.BUT_NRO = b.BUT_NRO and
					p.VIAJE_COD = 8507 and
					b.AERO_MATRI = 'BZD-177'


select *
from ABSTRACCIONX4.AERONAVES
where AERO_MATRI = 'HKR-319'

select b.BUT_NRO
from ABSTRACCIONX4.BUTACAS b
where b.BUT_NRO 

select b.BUT_NRO, b.BUT_TIPO
from ABSTRACCIONX4.BUTACAS b
where AERO_MATRI = 'HKR-319' and
b.BUT_NRO not in(select p.BUT_NRO
from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
where p.BUT_NRO = b.BUT_NRO and
p.AERO_MATRI = b.AERO_MATRI and
p.AERO_MATRI = 'HKR-319' and p.VIAJE_COD = 5463
)

select((select a.AERO_CANT_KGS
		from ABSTRACCIONX4.AERONAVES a
		where AERO_MATRI = 'HKR-319') - (select sum(e.ENCOMIENDA_PESO_KG)
											from ABSTRACCIONX4.ENCOMIENDAS e
											where e.AERO_MATRI = 'HKR-319' and
											e.VIAJE_COD = 5463)) Kilos




select *
from ABSTRACCIONX4.AERONAVES

select *
from ABSTRACCIONX4.VIAJES
select *
from (select b.BUT_NRO, b.BUT_TIPO
from ABSTRACCIONX4.BUTACAS b
where AERO_MATRI = 'HKR-319' and
b.BUT_NRO not in(select p.BUT_NRO
from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
where p.BUT_NRO = b.BUT_NRO and
p.AERO_MATRI = b.AERO_MATRI and
p.AERO_MATRI = 'HKR-319' and p.VIAJE_COD = 5463
)) t
where t.BUT_TIPO = 'Pasillo'



select p.BUT_NRO
from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
where p.BUT_NRO = b.BUT_NRO and
p.AERO_MATRI = b.AERO_MATRI and
p.AERO_MATRI = 'HKR-319' and p.VIAJE_COD = 5463
and b.BUT_TIPO = 'Ventanilla'




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
				c1.CIU_DESC Origen, c2.CIU_DESC Destino, s.SERV_DESC
			from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.RUTAS_AEREAS r1, ABSTRACCIONX4.RUTAS_AEREAS r2,
				ABSTRACCIONX4.CIUDADES c1, ABSTRACCIONX4.CIUDADES c2,ABSTRACCIONX4.SERVICIOS s
			where v.RUTA_ID = r1.RUTA_ID and
				v.RUTA_ID = r2.RUTA_ID and
				r1.CIU_COD_O = c1.CIU_COD and
				r2.CIU_COD_D = c2.CIU_COD and
				r1.SERV_COD = s.SERV_COD


select distinct c.CLI_COD, c.CLI_DNI, c.CLI_NOMBRE, c.CLI_APELLIDO, c.CLI_DIRECCION, c.CLI_TELEFONO, c.CLI_MAIL, c.CLI_FECHA_NAC
				from ABSTRACCIONX4.CLIENTES c
				where c.CLI_COD = 1298 and
				(select case 
						when 1298 not in (select distinct t.CLI_COD
												from (select distinct p.CLI_COD, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE
														from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.PASAJES p
														where v.VIAJE_COD = p.VIAJE_COD) t
												where 
												([ABSTRACCIONX4].datetime_is_between(t.VIAJE_FECHA_SALIDA, '2016-02-02 01:01:00.000', '2016-02-02 15:01:00.000') = 1) or
												([ABSTRACCIONX4].datetime_is_between(t.VIAJE_FECHA_LLEGADAE, '2016-02-02 01:01:00.000', '2016-02-02 15:01:00.000') = 1) or
												([ABSTRACCIONX4].datetime_is_between('2016-02-02 01:01:00.000', t.VIAJE_FECHA_SALIDA, t.VIAJE_FECHA_LLEGADAE) = 1) or
												([ABSTRACCIONX4].datetime_is_between('2016-02-02 15:01:00.000', t.VIAJE_FECHA_SALIDA, t.VIAJE_FECHA_LLEGADAE) = 1)
												)
						then 1
						else 0
					end) = 1

select distinct p.CLI_COD, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE
from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.PASAJES p
where v.VIAJE_COD = p.VIAJE_COD and p.CLI_COD = 1298 and
[ABSTRACCIONX4].datetime_is_between('2016-22-09', v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE) = 0

select *
from abstraccionx4.pasajes p
where p.CLI_COD = 1298

select *
from ABSTRACCIONX4.VIAJES v
where v.VIAJE_COD = 8469

select *
from ABSTRACCIONX4.CLIENTES c
where c.CLI_COD = 1298