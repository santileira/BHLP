-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].pasajero_disponible

 (@cli_cod int, @viaje_cod int, @fecha_salida datetime, @fecha_llegada_estimada datetime)

RETURNS table

AS
	return (select distinct c.CLI_COD, c.CLI_DNI, c.CLI_NOMBRE, c.CLI_APELLIDO, c.CLI_DIRECCION, c.CLI_TELEFONO, c.CLI_MAIL, c.CLI_FECHA_NAC
				from ABSTRACCIONX4.CLIENTES c
				where c.CLI_COD = @cli_cod and
				(select case 
						when @cli_cod not in (select distinct t.CLI_COD
												from (select distinct p.CLI_COD, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE
														from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.PASAJES p
														where v.VIAJE_COD = p.VIAJE_COD and p.CLI_COD = @viaje_cod) t
												where 
												([ABSTRACCIONX4].datetime_is_between(t.VIAJE_FECHA_SALIDA, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(t.VIAJE_FECHA_LLEGADAE, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_salida, t.VIAJE_FECHA_SALIDA, t.VIAJE_FECHA_LLEGADAE) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_llegada_estimada, t.VIAJE_FECHA_SALIDA, t.VIAJE_FECHA_LLEGADAE) = 1)
												)
						then 1
						else 0
					end) = 1
			)
GO

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
												e.VIAJE_COD = @viaje_cod and
												e.ENCOMIENDA_CANCELADO = 0)) Kilos
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
								p.PASAJE_CANCELADO = 0 and
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



--------------------------------actualizarDatosDelCliente-----------------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].actualizarDatosDelCliente
	@dni numeric(10,0), @ape varchar(60),@nombre varchar(60),@direccion varchar(80),@mail varchar(60), @fechanac datetime,@telefono int
AS
	UPDATE [ABSTRACCIONX4].CLIENTES
	SET CLI_NOMBRE = @nombre ,CLI_DIRECCION = @direccion ,CLI_MAIL = @mail, CLI_FECHA_NAC = @fechanac, CLI_TELEFONO = @telefono
	WHERE CLI_DNI = @dni AND CLI_APELLIDO = @ape
GO

--------------------------------ingresarDatosDelCliente--------------------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].ingresarDatosDelCliente
	@dni numeric(10,0), @ape varchar(60),@nombre varchar(60),@direccion varchar(80),@mail varchar(60), @fechanac datetime,@telefono int
AS
	INSERT INTO [ABSTRACCIONX4].CLIENTES
	(CLI_DNI,CLI_APELLIDO,CLI_NOMBRE,CLI_DIRECCION,CLI_MAIL,CLI_FECHA_NAC,CLI_TELEFONO) VALUES(@dni,@ape,@nombre,@direccion,@mail,@fechanac,@telefono)


GO

--------------------------------ingresarDatosDeCompra (PREGUNTAR)------------------------------------------------

create PROCEDURE [ABSTRACCIONX4].ingresarDatosDeCompra
	@codigoPNR int,
	@tarjetaNro numeric(16,0),
	@cliCod int
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.COMPRAS
			(COMP_PNR,TARJ_NRO,CLI_COD)
			VALUES (@codigoPNR,@tarjetaNro,@cliCod)
	END TRY
	BEGIN CATCH
	END CATCH


GO

--------------------------------ingresarDatosDePasajes------------------------------------------------

create PROCEDURE [ABSTRACCIONX4].ingresarDatosDePasajes
	@cliCod int,
	@viajeCod int,
	@pasajePrecio numeric(7,2),
	@pasajeFechaCompra datetime,
	@butNro smallint,
	@aeroMatri varchar(8)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.PASAJES
			(CLI_COD,VIAJE_COD,PASAJE_PRECIO,PASAJE_FECHA_COMPRA,BUT_NRO,AERO_MATRI)
			VALUES (@cliCod,@viajeCod,@pasajePrecio,@pasajeFechaCompra,@butNro,@aeroMatri)
	END TRY
	BEGIN CATCH
	END CATCH


GO


--------------------------------ingresarDatosDeEncomiendas------------------------------------------------

create PROCEDURE [ABSTRACCIONX4].ingresarDatosDeEncomiendas
	@cliCod int,
	@viajeCod int,
	@encomiendaPrecio numeric(7,2),
	@encomiendaFechaCompra datetime,
	@encomiendaPesoKG numeric(6,2),
	@aeroMatri varchar(8)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.ENCOMIENDAS
			(CLI_COD,VIAJE_COD,ENCOMIENDA_PRECIO,ENCOMIENDA_FECHA_COMPRA,ENCOMIENDA_PESO_KG,AERO_MATRI)
			VALUES (@cliCod,@viajeCod,@encomiendaPrecio,@encomiendaFechaCompra,@encomiendaPesoKG,@aeroMatri)
	END TRY
	BEGIN CATCH
	END CATCH


GO
