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

RETURNS @variable_tabla TABLE (Descripcion varchar(80), Cantidad smallint)

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 t.Descripcion, sum(t.Cantidad) Cantidad
		from (select c.ciu_desc Descripcion, (a.AERO_CANT_BUTACAS - v.CANT_BUT_OCUPADAS) Cantidad
				from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c, ABSTRACCIONX4.AERONAVES a
				where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6
				and v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				a.AERO_MATRI = v.AERO_MATRI
						) t
		group by t.Descripcion
		order by sum(t.Cantidad) desc
else
	insert @variable_tabla 
		select top 5 t.Descripcion, sum(t.Cantidad) Cantidad
		from (select c.ciu_desc Descripcion, (a.AERO_CANT_BUTACAS - v.CANT_BUT_OCUPADAS) Cantidad
				from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c, ABSTRACCIONX4.AERONAVES a
				where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 7 and 12
				and v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				a.AERO_MATRI = v.AERO_MATRI) t
		group by t.Descripcion
		order by sum(t.Cantidad) desc
		
return;
end
GO


-------------------------------Estadistica clientes con mas puntos acumulados-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].clientesConMasMillas(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Nombre varchar(80), Apellido varchar(80), Cantidad smallint)

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 t.nombre, t.apellido, (t.MillasEncomiendas + t.MillasPasajes) Millas
		from
		(select distinct c.cli_nombre nombre, c.cli_apellido apellido, 
			(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasPasajes(c.cli_dni, c.cli_apellido)) MillasPasajes,
			(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(c.cli_dni, c.cli_apellido)) MillasEncomiendas
		from ABSTRACCIONX4.CLIENTES c, ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.VIAJES v
		where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6 and
		v.VIAJE_COD = p.viaje_cod and p.cli_cod = c.cli_cod) t
		order by (t.MillasEncomiendas + t.MillasPasajes) desc
else
	insert @variable_tabla 
		select top 5 t.nombre, t.apellido, (t.MillasEncomiendas + t.MillasPasajes) Millas
		from
		(select distinct c.cli_nombre nombre, c.cli_apellido apellido, 
			(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasPasajes(c.cli_dni, c.cli_apellido)) MillasPasajes,
			(select sum("Cant. de Millas") from [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(c.cli_dni, c.cli_apellido)) MillasEncomiendas
		from ABSTRACCIONX4.CLIENTES c, ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.VIAJES v
		where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 7 and 12 and
		v.VIAJE_COD = p.viaje_cod and p.cli_cod = c.cli_cod) t
		order by (t.MillasEncomiendas + t.MillasPasajes) desc
		
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
