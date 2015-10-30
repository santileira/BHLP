

select *
from [ABSTRACCIONX4].destinosConMasPasajesVendidos(1, 2016)

select *
from [ABSTRACCIONX4].destinosConAeronaveMasVacia(2, 2016)

select *
from [ABSTRACCIONX4].destinosConMasPasajesCancelados(2,2016)

select *
from [ABSTRACCIONX4].aeronavesConMayorFueraDeServicio(2, 2016)


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


