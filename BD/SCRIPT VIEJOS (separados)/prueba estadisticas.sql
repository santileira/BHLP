

select *
from [ABSTRACCIONX4].destinosConMasPasajesVendidos(1, 2016)

select *
from [ABSTRACCIONX4].destinosConAeronaveMasVacia(1, 2016)

select *
from [ABSTRACCIONX4].clientesConMasMillas(1, 2016)

select *
from [ABSTRACCIONX4].destinosConMasPasajesCancelados(2,2016)

select *
from [ABSTRACCIONX4].aeronavesConMayorFueraDeServicio(2, 2016)

select top 1 *
from abstraccionx4.clientes

select top 1 *
from abstraccionx4.pasajes

select (a.AERO_CANT_BUTACAS - v.CANT_BUT_OCUPADAS) Cantidad
from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.AERONAVES a
where v.AERO_MATRI = a.AERO_MATRI


select *
from abstraccionx4.ciudades

select top 1 *
from abstraccionx4.pasajes p



select top 5 t.Descripcion
		from (select t2.Descripcion, (select count(*) from [ABSTRACCIONX4].butacasDisponibles(t2.viaje_cod, t2.aero_matri)) Cantidad
				from (select c.ciu_desc Descripcion, v.viaje_fecha_salida Fecha, v.viaje_cod, v.aero_matri
						from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
						where v.ruta_id = r.ruta_id and
						r.ciu_cod_d = c.ciu_cod and 
						year(v.viaje_fecha_salida) = 2016 and month(v.viaje_fecha_salida) between 1 and 6) t2
				group by t2.Descripcion, t2.viaje_cod, t2.aero_matri) t
		group by t.Descripcion
		order by sum(t.cantidad) desc



select top t.apellido + ', ' + t.nombre, (t.MillasPasajes + t.MillasEncomiendas) Millas
from (select c.cli_nombre nombre, c.cli_apellido apellido, 
			
		from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.VIAJES v, abstraccionx4.clientes c
		where 
		and v.VIAJE_COD = p.viaje_cod and
		p.cli_cod = c.cli_cod) t
group by t.apellido, t.nombre, t.MillasPasajes, t.MillasEncomiendas
order by (t.MillasPasajes + t.MillasEncomiendas) desc

select t.nombre, t.apellido, (t.MillasEncomiendas + t.MillasPasajes) Millas
from
(select distinct c.cli_nombre nombre, c.cli_apellido apellido, 
	(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasPasajes(c.cli_dni, c.cli_apellido)) MillasPasajes,
	(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(c.cli_dni, c.cli_apellido)) MillasEncomiendas
from ABSTRACCIONX4.CLIENTES c, ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.VIAJES v
where year(v.viaje_fecha_salida) = 2016 and month(v.viaje_fecha_salida) between 1 and 6 and
v.VIAJE_COD = p.viaje_cod and p.cli_cod = c.cli_cod) t
order by (t.MillasEncomiendas + t.MillasPasajes) desc




select *
from abstraccionx4.PASAJES





