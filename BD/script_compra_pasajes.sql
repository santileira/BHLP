-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].pasajero_disponible

 (@cli_cod int, @fecha_salida datetime, @fecha_llegada_estimada datetime)

RETURNS table

AS
	return (select distinct c.CLI_COD, c.CLI_DNI, c.CLI_NOMBRE, c.CLI_APELLIDO, c.CLI_DIRECCION, c.CLI_TELEFONO, c.CLI_MAIL, c.CLI_FECHA_NAC
				from ABSTRACCIONX4.CLIENTES c
				where c.CLI_COD = @cli_cod and
				(select case 
						when @cli_cod not in (select distinct t.CLI_COD
												from (select distinct p.CLI_COD, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE
														from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.PASAJES p
														where v.VIAJE_COD = p.VIAJE_COD) t
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
			b.BUT_NRO not in(select b.BUT_NRO
								from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b
								where p.BUT_NRO = b.BUT_NRO and
								p.AERO_MATRI = b.AERO_MATRI and
								p.PASAJE_CANCELADO = 0 and
								p.AERO_MATRI = @matricula and p.VIAJE_COD = @viaje_cod)
			)
GO

-------------------------------Butacas pasillo disponibles para una aeronave en un viaje determinado-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].butacasDisponiblesCantidad(@viaje_cod int, @matricula varchar(8))
RETURNS int

AS
begin
	return(select count(*)
			from [ABSTRACCIONX4].butacasDisponibles(@viaje_cod, @matricula))
end
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

--------------------------------ingresarCompra------------------------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].ingresarCompra
	@codigoPNR varchar(12),
	@nroTarjeta numeric(16,0),
	@formaDePago varchar(25),
	@codigoCli int
AS
	BEGIN
		IF(@formaDePago='Efectivo')
		BEGIN
			INSERT INTO ABSTRACCIONX4.COMPRAS
				(COMP_PNR,CLI_COD)
				VALUES (@codigoPNR,@codigoCli)
		END
		ELSE
		BEGIN
			INSERT INTO ABSTRACCIONX4.COMPRAS
				(COMP_PNR,TARJ_NRO,CLI_COD)
				VALUES (@codigoPNR,@nroTarjeta,@codigoCli)
		END
	END 
GO

--------------------------------ingresarDatosDePasajes(NO SE USA, POR AHORA)------------------------------------------------

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


--------------------------------ingresarDatosDeEncomiendas(NO SE USA, POR AHORA)------------------------------------------------

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


-------------------- Ingresar Datos de la Compra  ----------------------------

CREATE TYPE [ABSTRACCIONX4].TablePasajesType AS TABLE 

(CLI_COD int,
CLI_DNI decimal(10,0),
CLI_NOMBRE varchar(60),
CLI_APELLIDO varchar(60),
CLI_DIRECCION varchar(80),
CLI_TELEFONO int,
CLI_MAIL varchar(60),
CLI_FECHA_NAC datetime,
VIAJE_COD int,
IMPORTE decimal,
BUTACA int,
MATRICULA varchar(8),
ENCONTRADO BIT,
ACTUALIZAR BIT,
ES_COMPRADOR int
);
GO

CREATE TYPE [ABSTRACCIONX4].TableEncomiendasType AS TABLE 

(CLI_COD int,
CLI_DNI decimal(10,0),
CLI_NOMBRE varchar(60),
CLI_APELLIDO varchar(60),
CLI_DIRECCION varchar(80),
CLI_TELEFONO int,
CLI_MAIL varchar(60),
CLI_FECHA_NAC datetime,
VIAJE_COD int,
IMPORTE decimal,
PESO decimal,
MATRICULA varchar(8),
ENCONTRADO BIT,
ACTUALIZAR BIT,
ES_COMPRADOR int
);
GO


CREATE PROCEDURE [ABSTRACCIONX4].ingresarDatosDeCompra
	(@TablaPasajes [ABSTRACCIONX4].TablePasajesType READONLY,
	@TablaEncomiendas [ABSTRACCIONX4].TableEncomiendasType READONLY,
	@dni numeric(10,0), @ape varchar(60),@nombre varchar(60),@direccion varchar(80),@mail varchar(60), @fechanac datetime,@telefono int,
	@encontroComprador BIT, @actualizarComprador BIT,
	@codigoPNR varchar(12), @formaDePago varchar(25),@nroTarjeta numeric(16,0),@codSeg int,@vencMes int, @vencAnio int, @tipoTarjeta varchar(30),
	@agregarTarjeta BIT	
	)
AS
	BEGIN TRY
		--Abro transaccion
		------------------
		BEGIN TRANSACTION
		------------------

		-------------------------------------
		--ALTA/ACTUALIZACION DEL COMPRADOR---
		IF(@encontroComprador = 0) --hay que agregar al comprador
		BEGIN		
			EXEC [ABSTRACCIONX4].ingresarDatosDelCliente @dni,@ape,@nombre,@direccion,@mail,@fechanac,@telefono
		END
		ELSE
		BEGIN
			IF(@actualizarComprador = 1) -- si existe y se modifico, hay que actualizarlo
				EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @dni,@ape,@nombre,@direccion,@mail,@fechanac,@telefono
		END	
		-------------------------------------
		-------------------------------------
		
		-------------------------------------
		--ALTA DE COMPRA---------------------
		IF(@agregarTarjeta = 1)
		BEGIN
			DECLARE @tipo_cod int
			SET @tipo_cod = (SELECT TIPO_COD FROM ABSTRACCIONX4.TIPOS WHERE TIPO_DESC = @tipoTarjeta)
			EXEC [ABSTRACCIONX4].altaTarjeta @nroTarjeta,@codSeg,@vencMes,@vencAnio,@tipo_cod
		END

		DECLARE @cod_cli int
		SET @cod_cli = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @dni AND CLI_APELLIDO = @ape)
		EXEC [ABSTRACCIONX4].ingresarCompra @codigoPNR,@nroTarjeta,@formaDePago,@cod_cli
		-------------------------------------
		-------------------------------------


		--------------------------------------------------
		--ALTA DE PASAJES/ENCOMIENDAS---------------------
		DECLARE cursorPasajes CURSOR FOR SELECT CLI_COD,CLI_DNI,CLI_NOMBRE,CLI_APELLIDO,CLI_DIRECCION,CLI_TELEFONO,CLI_MAIL,CLI_FECHA_NAC,VIAJE_COD,IMPORTE,BUTACA,MATRICULA,ENCONTRADO,ACTUALIZAR,ES_COMPRADOR FROM @TablaPasajes
		DECLARE cursorEncomiendas CURSOR FOR SELECT CLI_COD,CLI_DNI,CLI_NOMBRE,CLI_APELLIDO,CLI_DIRECCION,CLI_TELEFONO,CLI_MAIL,CLI_FECHA_NAC,VIAJE_COD,IMPORTE,PESO,MATRICULA,ENCONTRADO,ACTUALIZAR,ES_COMPRADOR FROM @TablaEncomiendas
		DECLARE @cliCod int
		DECLARE @curDni decimal(10,0)
		DECLARE @curNom varchar(60)
		DECLARE @curApe varchar(60)
		DECLARE @curDir varchar(80)
		DECLARE @curTel int
		DECLARE @curMail varchar(60)
		DECLARE @curFechaNac datetime
		DECLARE @viajeCod int
		DECLARE @precio decimal
		DECLARE @but int
		DECLARE @matri varchar(8)
		DECLARE @peso decimal
		DECLARE @fechaCompra datetime
		DECLARE @clienteEncontrado BIT
		DECLARE @clienteActualizado BIT
		DECLARE @esComprador int
		DECLARE @codigoCli int
		SET @fechaCompra = [ABSTRACCIONX4].obtenerFechaDeHoy()		

		OPEN cursorPasajes
		FETCH NEXT FROM cursorPasajes INTO @cliCod,@curDni,@curNom,@curApe,@curDir,@curTel,@curMail,@curFechaNac,@viajeCod,@precio,@but,@matri,@clienteEncontrado,@clienteActualizado,@esComprador
		WHILE(@@FETCH_STATUS=0)
		BEGIN

			IF(@clienteEncontrado = 0) --hay que agregar al cliente
			BEGIN		
				
				IF(@esComprador=0)
				BEGIN	
					EXEC [ABSTRACCIONX4].ingresarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					SET @codigoCli = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @curDni AND CLI_APELLIDO = @curApe)
				

					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, PASAJE_FECHA_COMPRA, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@fechaCompra,@but,@matri,@precio/10)
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, PASAJE_FECHA_COMPRA, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@fechaCompra,@but,@matri,@precio/10)
				END

			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, PASAJE_FECHA_COMPRA, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@fechaCompra,@but,@matri,@precio/10)
			END	
											
										 
			FETCH NEXT FROM cursorPasajes INTO @cliCod,@curDni,@curNom,@curApe,@curDir,@curTel,@curMail,@curFechaNac,@viajeCod,@precio,@but,@matri,@clienteEncontrado,@clienteActualizado,@esComprador
		END
		-- cerrar cursor

		OPEN cursorEncomiendas
		FETCH NEXT FROM cursorEncomiendas INTO @cliCod,@curDni,@curNom,@curApe,@curDir,@curTel,@curMail,@curFechaNac,@viajeCod,@precio,@peso,@matri,@clienteEncontrado,@clienteActualizado,@esComprador
		WHILE(@@FETCH_STATUS=0)
		BEGIN
			IF(@clienteEncontrado = 0) --hay que agregar al cliente
			BEGIN		

				IF(@esComprador=0)
				BEGIN

					EXEC [ABSTRACCIONX4].ingresarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					SET @codigoCli = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @curDni AND CLI_APELLIDO = @curApe)
				
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_FECHA_COMPRA, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@fechaCompra,@peso,@matri,@precio/10) 
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_FECHA_COMPRA, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@fechaCompra,@peso,@matri,@precio/10) 
				END
			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel					
					
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_FECHA_COMPRA, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@fechaCompra,@peso,@matri,@precio/10) 
			END	

			
			FETCH NEXT FROM cursorEncomiendas INTO @cliCod,@curDni,@curNom,@curApe,@curDir,@curTel,@curMail,@curFechaNac,@viajeCod,@precio,@peso,@matri,@clienteEncontrado,@clienteActualizado,@esComprador
		END
		--cerrar cursor


		------------------
		COMMIT TRANSACTION
		------------------

	END TRY
	BEGIN CATCH
		
		--------------------
		ROLLBACK TRANSACTION
		--------------------

		DECLARE @Error varchar(30)
		SET @Error = 'Fallo la compra'
		RAISERROR(@Error, 16, 1)

	END CATCH


GO




--------------------------------Alta Tarjeta------------------------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].altaTarjeta
	@nroTarjeta numeric(16,0),
	@codSeg int,
	@vencMes int,
	@vencAnio int,
	@tipo_cod int
AS
	BEGIN
		INSERT INTO ABSTRACCIONX4.TARJETAS
				(TARJ_NRO,TARJ_CODSEG,TARJ_VTO,TIPO_COD)
				VALUES (@nroTarjeta,@codSeg,CAST(@vencMes AS VARCHAR)+ '/' + CAST(@vencAnio AS VARCHAR) ,@tipo_cod)
	END 
GO

----------------------------------Datos de tarjeta Válidos -------------------------------------

CREATE FUNCTION  [ABSTRACCIONX4].datosValidosDeTarjeta(@tarjNro numeric(16,0), @tarjVtoMes int, @tarjVtoAnio int, @tarjCodSeg int, @tarjTipo varchar(20))
RETURNS BIT
AS 
	BEGIN
	DECLARE @tipoCod int,@tipoDesc varchar(20),@nro numeric(16,0), @vto varchar(30), @codSeg int, @fechaVenc varchar(15)

	SELECT @vto = TARJ_VTO, @codSeg = TARJ_CODSEG, @tipoCod = TIPO_COD FROM [ABSTRACCIONX4].TARJETAS WHERE TARJ_NRO = @tarjNro	
	SELECT @tipoDesc=TIPO_DESC FROM [ABSTRACCIONX4].TIPOS WHERE TIPO_COD = @tipoCod 

	SET @fechaVenc = CAST(@tarjVtoMes AS VARCHAR)+ '/' + CAST(@tarjVtoAnio AS VARCHAR)
		
	BEGIN
		IF(@tipoDesc = @tarjTipo AND @vto = @fechaVenc  AND @codSeg = @tarjCodSeg)
		BEGIN
		RETURN 1
		END
	END	
	
	RETURN 0
	
	END
GO

