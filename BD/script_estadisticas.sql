-------------------------------Estadistica destinos con mas pasajes vendidos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].destinosConMasPasajesVendidosPrimerSemestre(@anio smallint)

RETURNS table

AS
return (select top 5 t.Descripcion
		from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
				where p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				p.pasaje_cancelado = 0
				group by c.ciu_desc, p.pasaje_fecha_compra) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 1 and 6
		group by t.Descripcion
		order by sum(t.cantidad) desc)
GO

CREATE FUNCTION [ABSTRACCIONX4].destinosConMasPasajesVendidosSegundoSemestre(@anio smallint)

RETURNS table

AS
return (select top 5 t.Descripcion
		from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
				where p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				p.pasaje_cancelado = 0
				group by c.ciu_desc, p.pasaje_fecha_compra) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 7 and 12
		group by t.Descripcion
		order by sum(t.cantidad) desc)
GO

-------------------------------Estadistica destino con aeronaves mas vacias-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].destinosConAeronaveMasVaciaPrimerSemestre(@anio smallint)

RETURNS table

AS
return (select top 5 t.Descripcion
		from (select t2.Descripcion, (select count(*) from [ABSTRACCIONX4].butacasDisponibles(t2.viaje_cod, t2.aero_matri)) Cantidad
				from (select c.ciu_desc Descripcion, v.viaje_fecha_salida Fecha, v.viaje_cod, v.aero_matri
						from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
						where v.ruta_id = r.ruta_id and
						r.ciu_cod_d = c.ciu_cod and 
						year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6) t2
				group by t2.Descripcion, t2.viaje_cod, t2.aero_matri) t
		group by t.Descripcion
		order by sum(t.cantidad) desc)
GO

CREATE FUNCTION [ABSTRACCIONX4].destinosConAeronaveMasVaciaSegundoSemestre(@anio smallint)

RETURNS table

AS
return (select top 5 t.Descripcion
		from (select t2.Descripcion, (select count(*) from [ABSTRACCIONX4].butacasDisponibles(t2.viaje_cod, t2.aero_matri)) Cantidad
				from (select c.ciu_desc Descripcion, v.viaje_fecha_salida Fecha, v.viaje_cod, v.aero_matri
						from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
						where v.ruta_id = r.ruta_id and
						r.ciu_cod_d = c.ciu_cod and 
						year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 7 and 12) t2
				group by t2.Descripcion, t2.viaje_cod, t2.aero_matri) t
		group by t.Descripcion
		order by sum(t.cantidad) desc)
GO