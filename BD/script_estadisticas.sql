-------------------------------Estadistica destinos con mas pasajes vendidos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].destinosConMasPasajesVendidos(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Descripcion varchar(80))

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
			select top 5 t.Descripcion
			from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
					from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
					where p.viaje_cod = v.viaje_cod and
					v.ruta_id = r.ruta_id and
					r.ciu_cod_d = c.ciu_cod and
					p.pasaje_cancelado = 0
					group by c.ciu_desc, p.pasaje_fecha_compra) t
			where year(t.Fecha) = @anio and month(t.Fecha) between 1 and 6
			group by t.Descripcion
			order by sum(t.cantidad) desc
else
	insert @variable_tabla 
			select top 5 t.Descripcion
			from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
					from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
					where p.viaje_cod = v.viaje_cod and
					v.ruta_id = r.ruta_id and
					r.ciu_cod_d = c.ciu_cod and
					p.pasaje_cancelado = 0
					group by c.ciu_desc, p.pasaje_fecha_compra) t
			where year(t.Fecha) = @anio and month(t.Fecha) between 7 and 12
			group by t.Descripcion
			order by sum(t.cantidad) desc
		
return;
end
GO

-------------------------------Estadistica destino con aeronaves mas vacias-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].destinosConAeronaveMasVacia(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Descripcion varchar(80))

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 t.Descripcion
		from (select t2.Descripcion, (select count(*) from [ABSTRACCIONX4].butacasDisponibles(t2.viaje_cod, t2.aero_matri)) Cantidad
				from (select c.ciu_desc Descripcion, v.viaje_fecha_salida Fecha, v.viaje_cod, v.aero_matri
						from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
						where v.ruta_id = r.ruta_id and
						r.ciu_cod_d = c.ciu_cod and 
						year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6) t2
				group by t2.Descripcion, t2.viaje_cod, t2.aero_matri) t
		group by t.Descripcion
		order by sum(t.cantidad) desc
else
	insert @variable_tabla 
		select top 5 t.Descripcion
		from (select t2.Descripcion, (select count(*) from [ABSTRACCIONX4].butacasDisponibles(t2.viaje_cod, t2.aero_matri)) Cantidad
				from (select c.ciu_desc Descripcion, v.viaje_fecha_salida Fecha, v.viaje_cod, v.aero_matri
						from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
						where v.ruta_id = r.ruta_id and
						r.ciu_cod_d = c.ciu_cod and 
						year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 7 and 12) t2
				group by t2.Descripcion, t2.viaje_cod, t2.aero_matri) t
		group by t.Descripcion
		order by sum(t.cantidad) desc

return;
end
GO


-------------------------------Estadistica destinos con mas pasajes cancelados-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].destinosConMasPasajesCancelados(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Descripcion varchar(80))

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 t.Descripcion
		from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
				where p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				p.pasaje_cancelado = 1
				group by c.ciu_desc, p.pasaje_fecha_compra) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 1 and 6
		group by t.Descripcion
		order by sum(t.cantidad) desc
else
	insert @variable_tabla 
		select top 5 t.Descripcion
		from (select c.ciu_desc Descripcion, p.pasaje_fecha_compra Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c
				where p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				p.pasaje_cancelado = 1
				group by c.ciu_desc, p.pasaje_fecha_compra) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 7 and 12
		group by t.Descripcion
		order by sum(t.cantidad) desc

return;
end
GO


-------------------------------Estadistica aeronaves con mayor cantidad de dias fuera de servicio-----------------
CREATE FUNCTION [ABSTRACCIONX4].cantidadDiasFueraDeServicio(@matricula varchar(8))

RETURNS smallint

AS

begin
declare @fechaFS datetime
declare @fechaRS datetime

set @fechaFS = (select a.aero_fecha_fs from abstraccionx4.aeronaves a where a.aero_matri = @matricula)
set	@fechaRS = (select a.aero_fecha_rs from abstraccionx4.aeronaves a where a.aero_matri = @matricula)

if(@fechaFS <> null)
	return (select datediff(day, @fechaRS, @fechaFS)  
			from [ABSTRACCIONX4].aeronaves a
			where a.aero_matri = @matricula)

	return 0
end
GO

CREATE FUNCTION [ABSTRACCIONX4].aeronavesConMayorFueraDeServicio(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Descripcion varchar(8), CantidadDias smallint)

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 a.aero_matri, [ABSTRACCIONX4].cantidadDiasFueraDeServicio(a.aero_matri) CantidadDias
		from [ABSTRACCIONX4].aeronaves a
		where year(a.aero_fecha_fs) = @anio and month(a.aero_fecha_fs) between 1 and 6
		order by a.aero_matri desc
else
	insert @variable_tabla	
		select top 5 a.aero_matri, [ABSTRACCIONX4].cantidadDiasFueraDeServicio(a.aero_matri) CantidadDias
		from [ABSTRACCIONX4].aeronaves a
		where year(a.aero_fecha_fs) = @anio and month(a.aero_fecha_fs) between 7 and 12
		order by a.aero_matri desc

return;
end	
GO
