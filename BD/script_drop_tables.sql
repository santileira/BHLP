DROP PROCEDURE ABSTRACCIONX4.LoginAdministrador

DROP FUNCTION [ABSTRACCIONX4].datetime_between
DROP FUNCTION [ABSTRACCIONX4].datetime_is_between
DROP FUNCTION [ABSTRACCIONX4].aeronave_disponible
DROP FUNCTION [ABSTRACCIONX4].pasajero_disponible

DROP PROCEDURE [ABSTRACCIONX4].altaTarjeta
DROP FUNCTION [ABSTRACCIONX4].importeEncomienda
DROP FUNCTION [ABSTRACCIONX4].importePasaje
DROP FUNCTION [ABSTRACCIONX4].buscarCliente
DROP FUNCTION [ABSTRACCIONX4].kilosDisponibles
DROP FUNCTION [ABSTRACCIONX4].butacasDisponibles
DROP FUNCTION [ABSTRACCIONX4].butacasDisponiblesCantidad
DROP FUNCTION [ABSTRACCIONX4].buscarViajesDisponibles
DROP PROCEDURE [ABSTRACCIONX4].actualizarDatosDelCliente
DROP PROCEDURE [ABSTRACCIONX4].ingresarDatosDelCliente
DROP PROCEDURE [ABSTRACCIONX4].ingresarCompra
DROP PROCEDURE [ABSTRACCIONX4].ingresarDatos
DROP PROCEDURE [ABSTRACCIONX4].ingresarDatosDePasajes
DROP PROCEDURE [ABSTRACCIONX4].ingresarDatosDeEncomiendas
DROP PROCEDURE [ABSTRACCIONX4].ingresarDatosDeCompra
DROP FUNCTION  [ABSTRACCIONX4].datosValidosDeTarjeta
DROP TYPE [ABSTRACCIONX4].TablePasajesType
DROP TYPE [ABSTRACCIONX4].TableEncomiendasType

DROP PROCEDURE [ABSTRACCIONX4].ActualizarFuncionalidades
DROP PROCEDURE [ABSTRACCIONX4].AltaRol
DROP FUNCTION [ABSTRACCIONX4].ExisteNombreRol
DROP FUNCTION [ABSTRACCIONX4].DarCodigoDeRol
DROP FUNCTION [ABSTRACCIONX4].DarCodigoDeUsuario
DROP FUNCTION [ABSTRACCIONX4].DarCodigoDeFuncionalidad
DROP PROCEDURE [ABSTRACCIONX4].BajaRol
DROP PROCEDURE [ABSTRACCIONX4].ModificarRol
DROP FUNCTION [ABSTRACCIONX4].FuncionalidadesRol
DROP PROCEDURE [ABSTRACCIONX4].BajaFuncionalidades

DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronavePasajes
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveEncomiendas
DROP PROCEDURE [ABSTRACCIONX4].AgregarButacas 
DROP PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
DROP PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
DROP FUNCTION [ABSTRACCIONX4].TieneViajeComprado
DROP FUNCTION [ABSTRACCIONX4].TieneViajeEntreFechas
DROP FUNCTION [ABSTRACCIONX4].CantidadButacas
DROP PROCEDURE [ABSTRACCIONX4].CancelarPasajesEncomiendasAeronave
DROP FUNCTION [ABSTRACCIONX4].PasajesEntreFechas
DROP FUNCTION [ABSTRACCIONX4].EncomiendasEntreFechas
DROP PROCEDURE [ABSTRACCIONX4].SuplantarAeronave
DROP FUNCTION [ABSTRACCIONX4].FechaReinicioOMaxima
DROP FUNCTION [ABSTRACCIONX4].AeronaveDeMismasCaracteristicas
DROP FUNCTION [ABSTRACCIONX4].DisponibleParaTodosLosVuelosDe
DROP PROCEDURE [ABSTRACCIONX4].BorrarButacas 
DROP FUNCTION [ABSTRACCIONX4].FechaSalidaDeViaje
DROP PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveButacas
DROP FUNCTION [ABSTRACCIONX4].ExisteViajeEntreFechas
DROP FUNCTION [ABSTRACCIONX4].TieneViajeAsignado
DROP PROCEDURE [ABSTRACCIONX4].BorrarPasajes
DROP PROCEDURE [ABSTRACCIONX4].BorrarEncomiendas
DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigoServicio
DROP PROCEDURE [ABSTRACCIONX4].ModificarAeronave
DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad
DROP PROCEDURE [ABSTRACCIONX4].AltaAeronave
DROP FUNCTION [ABSTRACCIONX4].DatosDeAeronaveASuplantar
DROP FUNCTION [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra
DROP FUNCTION [ABSTRACCIONX4].CantidadFuerasDeServicioEntre

DROP PROCEDURE [ABSTRACCIONX4].AltaRuta
DROP PROCEDURE [ABSTRACCIONX4].BajaRuta
DROP FUNCTION [ABSTRACCIONX4].TieneViajeProgramado 
DROP FUNCTION [ABSTRACCIONX4].TieneViajeVendidos
DROP FUNCTION [ABSTRACCIONX4].EstaSiendoUsada 
DROP PROCEDURE [ABSTRACCIONX4].ModificarRuta
DROP PROCEDURE [ABSTRACCIONX4].ActualizarServiciosRuta
DROP FUNCTION [ABSTRACCIONX4].ServiciosDeRuta
DROP TYPE [ABSTRACCIONX4].Lista

DROP PROCEDURE [ABSTRACCIONX4].generarNuevoViaje

DROP PROCEDURE [ABSTRACCIONX4].RegistrarUsuario

DROP FUNCTION [ABSTRACCIONX4].llegaADestinoCorrecto
DROP FUNCTION [ABSTRACCIONX4].esOrigenCorrecto
DROP FUNCTION [ABSTRACCIONX4].obtenerFechaSalidaDeUnViaje
DROP PROCEDURE [ABSTRACCIONX4].agregarFechaLlegada

DROP FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasPasajes
DROP FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas
DROP PROCEDURE [ABSTRACCIONX4].DescontarMillas
DROP FUNCTION [ABSTRACCIONX4].ObtenerCiudadDesc
DROP FUNCTION [ABSTRACCIONX4].obtenerPuntosDePremio
DROP PROCEDURE [ABSTRACCIONX4].reducirStockDePremio
DROP PROCEDURE [ABSTRACCIONX4].insertarCanje

DROP FUNCTION [ABSTRACCIONX4].ObtenerCodigo
DROP FUNCTION [ABSTRACCIONX4].EstaEnViajePasaje
DROP FUNCTION [ABSTRACCIONX4].EstaEnViajeEncomienda
DROP FUNCTION [ABSTRACCIONX4].datetime_esMayor
DROP FUNCTION [ABSTRACCIONX4].LlenarEncomiendas
DROP FUNCTION [ABSTRACCIONX4].LlenarPasajes
DROP PROCEDURE [ABSTRACCIONX4].CancelarPasajesYEncomiendas
DROP TYPE [ABSTRACCIONX4].Lista1

DROP FUNCTION [ABSTRACCIONX4].destinosConMasPasajesVendidos
DROP FUNCTION [ABSTRACCIONX4].destinosConAeronaveMasVacia
DROP FUNCTION [ABSTRACCIONX4].clientesConMasMillas
DROP FUNCTION [ABSTRACCIONX4].destinosConMasPasajesCancelados
DROP FUNCTION [ABSTRACCIONX4].cantidadDiasFueraDeServicio
DROP FUNCTION [ABSTRACCIONX4].aeronavesConMayorFueraDeServicio
DROP FUNCTION [ABSTRACCIONX4].fecha_dado_datetime

DROP FUNCTION [ABSTRACCIONX4].obtenerFechaDeHoy
DROP PROCEDURE [ABSTRACCIONX4].crearTablaFecha


DROP TABLE [ABSTRACCIONX4].FUNCIONES_ROLES
DROP TABLE [ABSTRACCIONX4].DEVOLUCIONES
DROP TABLE [ABSTRACCIONX4].ROLES_USUARIOS
DROP TABLE [ABSTRACCIONX4].USUARIOS
DROP TABLE [ABSTRACCIONX4].ROLES
DROP TABLE [ABSTRACCIONX4].PASAJES
DROP TABLE [ABSTRACCIONX4].FUNCIONALIDADES
DROP TABLE [ABSTRACCIONX4].ENCOMIENDAS
DROP TABLE [ABSTRACCIONX4].VIAJES
DROP TABLE [ABSTRACCIONX4].COMPRAS
DROP TABLE [ABSTRACCIONX4].CANJES
DROP TABLE [ABSTRACCIONX4].SERVICIOS_RUTAS
DROP TABLE [ABSTRACCIONX4].RUTAS_AEREAS
DROP TABLE [ABSTRACCIONX4].TARJETAS
DROP TABLE [ABSTRACCIONX4].TIPOS_CUOTAS
DROP TABLE [ABSTRACCIONX4].TIPOS_TARJETAS
DROP TABLE [ABSTRACCIONX4].PREMIOS
DROP TABLE [ABSTRACCIONX4].CLIENTES
DROP TABLE [ABSTRACCIONX4].BUTACAS
DROP TABLE [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
DROP TABLE [ABSTRACCIONX4].AERONAVES
DROP TABLE [ABSTRACCIONX4].SERVICIOS
DROP TABLE [ABSTRACCIONX4].CIUDADES
DROP TABLE [ABSTRACCIONX4].FECHA

DROP FUNCTION [ABSTRACCIONX4].fnCustomPass
DROP VIEW [ABSTRACCIONX4].vwRandom

DROP SCHEMA ABSTRACCIONX4