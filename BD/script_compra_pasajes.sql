-------------------------------Importe de una encomienda-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].importeEncomienda(@kilos numeric(7,2), @origen varchar(80), @destino varchar(80))
RETURNS table

AS		
	return(
			select (r.RUTA_PRECIO_BASE_KG * @kilos) IMPORTE
				from ABSTRACCIONX4.RUTAS_AEREAS r, ABSTRACCIONX4.SERVICIOS s
				where r.SERV_COD = s.SERV_COD and
				r.CIU_COD_O = (select c1.CIU_COD	
								from ABSTRACCIONX4.CIUDADES c1
								where @origen = c1.CIU_DESC) and
				r.CIU_COD_D = (select c2.CIU_COD	
								from ABSTRACCIONX4.CIUDADES c2
								where @destino = c2.CIU_DESC)
			)
GO
-------------------------------Importe de un pasaje-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].importePasaje(@origen varchar(80), @destino varchar(80))
RETURNS table

AS		
	return(
			select (r.RUTA_PRECIO_BASE_PASAJE * (1 + s.SERV_PORC / 100)) IMPORTE
				from ABSTRACCIONX4.RUTAS_AEREAS r, ABSTRACCIONX4.SERVICIOS s
				where r.SERV_COD = s.SERV_COD and
				r.CIU_COD_O = (select c1.CIU_COD	
								from ABSTRACCIONX4.CIUDADES c1
								where @origen = c1.CIU_DESC) and
				r.CIU_COD_D = (select c2.CIU_COD	
								from ABSTRACCIONX4.CIUDADES c2
								where @destino = c2.CIU_DESC)
			)
GO

-------------------------------Buscar cliente para un cierto dni y apellido-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].buscarCliente(@dni numeric(10,0), @ape varchar(60))
RETURNS table

AS		
	return(
			select *
				from ABSTRACCIONX4.CLIENTES c
				where CLI_APELLIDO = @ape and
				CLI_DNI = @dni
				)
		
GO

-------------------------------Kg disponibles en la aeronave de un viaje determinado-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].kilosDisponibles(@viaje_cod int, @matricula varchar(8))
RETURNS table

AS		
	return(
			select((select a.AERO_CANT_KGS
			from ABSTRACCIONX4.AERONAVES a
			where AERO_MATRI = @matricula) - (select sum(e.ENCOMIENDA_PESO_KG)
												from ABSTRACCIONX4.ENCOMIENDAS e
												where e.AERO_MATRI = @matricula and
												e.VIAJE_COD = @viaje_cod)) Kilos
				)
		
GO

-------------------------------Butacas pasillo disponibles para una aeronave en un viaje determinado-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].butacasDisponibles(@viaje_cod int, @matricula varchar(8))
RETURNS table

AS
	return(
			select b.BUT_NRO, b.BUT_TIPO
			from ABSTRACCIONX4.BUTACAS b
			where AERO_MATRI = @matricula and
			b.BUT_NRO not in(select p.BUT_NRO
								from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
								where p.BUT_NRO = b.BUT_NRO and
								p.AERO_MATRI = b.AERO_MATRI and
								p.AERO_MATRI = @matricula and p.VIAJE_COD = @viaje_cod)
			)
GO


-------------------------------Filtrar los viajes disponibles para una fecha y ruta------------------------------
CREATE FUNCTION [ABSTRACCIONX4].buscarViajesDisponibles(@fecha datetime, @origen varchar(80), @destino varchar(80))
RETURNS table

AS
	return (select distinct v.VIAJE_COD, v.AERO_MATRI,v.VIAJE_FECHA_SALIDA Fecha_Salida, v.VIAJE_FECHA_LLEGADAE Fecha_Llegada, 
				c1.CIU_DESC Origen, c2.CIU_DESC Destino, s.SERV_DESC Tipo_Servicio
			from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.RUTAS_AEREAS r1, ABSTRACCIONX4.RUTAS_AEREAS r2,
				ABSTRACCIONX4.CIUDADES c1, ABSTRACCIONX4.CIUDADES c2,ABSTRACCIONX4.SERVICIOS s
			where v.RUTA_ID = r1.RUTA_ID and
				v.RUTA_ID = r2.RUTA_ID and
				r1.CIU_COD_O = c1.CIU_COD and
				r2.CIU_COD_D = c2.CIU_COD and
				r1.SERV_COD = s.SERV_COD and
				@origen = c1.CIU_DESC and
				@destino = c2.CIU_DESC and
				year(v.VIAJE_FECHA_SALIDA) = year(@fecha) and
				month(v.VIAJE_FECHA_SALIDA) = month(@fecha) and
				day(v.VIAJE_FECHA_SALIDA) = day(@fecha))
GO



