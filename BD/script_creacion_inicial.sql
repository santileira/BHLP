USE [GD2C2015]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================================================================
-- Descripci�n:	Creaci�n de Esquema
-- =============================================================================================

--CREATE SCHEMA [ABSTRACCIONX4] AUTHORIZATION [gd]
--GO

-- =============================================================================================
-- Descripci�n:	Creaci�n de Tablas
-- =============================================================================================


--Rol: Si el estado es 0, se encuentra inactivo, si es 1 activo.

--TIENE INSERT
--Esta tabla contiene c�digo, nombre y estado del rol (activo/inactivo)
CREATE TABLE [ABSTRACCIONX4].[ROLES](
	[ROL_COD] [int] IDENTITY NOT NULL,
	[ROL_ESTADO] [numeric] (2,0) NOT NULL, --para su baja l�gica
	[ROL_NOMBRE] [varchar](255) COLLATE Modern_Spanish_CI_AS NOT NULL, 
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
--Tabla Funcionalidad: Contiene los c�digos, nombres de cada funcionalidad. 
CREATE TABLE [ABSTRACCIONX4].[FUNCIONALIDADES](
	[FUNC_COD] [numeric] (18,0) NOT NULL, --IDENTITY O NO??? POR ALGO ESTA NO RECORDAMOS
	[FUNC_DESC] [varchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
CONSTRAINT [PK_FUNCIONALIDADES] PRIMARY KEY CLUSTERED 
(
	[FUNC_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--SIN INSERT
--Tabla Funciones por rol: Contiene las funcionalidades habilitadas para cada rol del sistema.
CREATE TABLE [ABSTRACCIONX4].[FUNCIONES_ROLES](
	[ROL_COD] [int] NOT NULL,
	[FUNC_COD] [numeric] (18,0) NOT NULL,
 CONSTRAINT [PK_FUNCIONES_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC,
	[FUNC_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[FUNCIONES_ROLES]  WITH CHECK ADD  CONSTRAINT [FK_FUNCIONES_ROLES_ROLES] FOREIGN KEY([ROL_COD])
REFERENCES [ABSTRACCIONX4].[ROLES] ([ROL_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[FUNCIONES_ROLES]  WITH CHECK ADD  CONSTRAINT [FK_FUNCIONES_ROLES_FUNCIONALIDADES] FOREIGN KEY([FUNC_COD])
REFERENCES [ABSTRACCIONX4].[FUNCIONALIDADES] ([FUNC_COD])

GO

--SIN INSERT
-- Tabla Usuarios: 

CREATE TABLE [ABSTRACCIONX4].[USUARIOS](
	[USERNAME] [varchar] (100) NOT NULL,
	[PASSWORD] [varchar] (100) NOT NULL,
	[ROL_COD] [int] NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[USERNAME] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOS_ROLES] FOREIGN KEY([ROL_COD])
REFERENCES [ABSTRACCIONX4].[ROLES] ([ROL_COD])

GO

--TIENE INSERT
-- Tabla Clientes:

CREATE TABLE [ABSTRACCIONX4].[CLIENTES](
	[CLI_COD] [int] IDENTITY NOT NULL,
	[CLI_DNI] [numeric] (18,0) NOT NULL,
	[CLI_NOMBRE] [varchar] (100) NOT NULL,
	[CLI_APELLIDO] [varchar] (100) NOT NULL,
	[CLI_DIRECCION] [varchar] (255) NOT NULL,
	[CLI_TELEFONO] [int] NOT NULL,
	[CLI_MAIL] [varchar] (100) NULL,
	[CLI_FECHA_NAC] [datetime] NOT NULL,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[CLI_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
-- Tabla Ciudades:

CREATE TABLE [ABSTRACCIONX4].[CIUDADES](
	[CIU_COD] [int] IDENTITY NOT NULL,
	[CIU_DESC] [varchar] (255) NOT NULL,
 CONSTRAINT [PK_CIUDADES] PRIMARY KEY CLUSTERED 
(
	[CIU_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
-- Tabla Ruta A�rea:

CREATE TABLE [ABSTRACCIONX4].[RUTAS_AEREAS](
	[RUTA_ID] [int] IDENTITY NOT NULL,
	[RUTA_COD] [int] NOT NULL,
	[RUTA_SERV] [varchar] (100) NOT NULL,
	[CIU_COD_O] [int] NOT NULL,
	[CIU_COD_D] [int] NOT NULL,
	[RUTA_PRECIO_BASE_KG] [numeric] (10,2) NOT NULL,
	[RUTA_PRECIO_BASE_PASAJE] [numeric] (10,2) NOT NULL,
	[RUTA_ESTADO] [int] NOT NULL, --para su baja l�gica
 CONSTRAINT [PK_RUTAS_AEREAS] PRIMARY KEY CLUSTERED 
(
	[RUTA_ID] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[RUTAS_AEREAS]  WITH CHECK ADD  CONSTRAINT [FK_RUTAS_AEREAS_CIUDADES_O] FOREIGN KEY([CIU_COD_O])
REFERENCES [ABSTRACCIONX4].[CIUDADES] ([CIU_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[RUTAS_AEREAS]  WITH CHECK ADD  CONSTRAINT [FK_RUTAS_AEREAS_CIUDADES_D] FOREIGN KEY([CIU_COD_D])
REFERENCES [ABSTRACCIONX4].[CIUDADES] ([CIU_COD])

GO
--TIENE INSERT
-- Tabla de Aeronaves

CREATE TABLE [ABSTRACCIONX4].[AERONAVES](
	[AERO_FECHA_ALTA] [datetime] NULL,
	[AERO_MOD] [varchar] (255) NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[AERO_FAB] [varchar] (255) NOT NULL,	
	[AERO_SERV] [varchar] (100) NOT NULL,	
	[AERO_BAJA_FS] [int] NULL,
	[AERO_BAJA_VU] [int] NULL,
	[AERO_FECHA_FS] [int] NULL,
	[AERO_FECHA_RS] [int] NULL,
	[AERO_FECHA_BAJA] [int] NULL,
	[AERO_CANT_BUTACAS] [int] NOT NULL,
	[AERO_CANT_KGS] [int] NOT NULL,
 CONSTRAINT [PK_AERONAVES] PRIMARY KEY CLUSTERED 
(
	[AERO_MATRI] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
-- Tabla De Viajes

CREATE TABLE [ABSTRACCIONX4].[VIAJES](
	[VIAJE_COD] [int] IDENTITY NOT NULL,
	[VIAJE_FECHA_SALIDA] [datetime] NOT NULL,
	[VIAJE_FECHA_LLEGADA] [datetime] NOT NULL,
	[VIAJE_FECHA_LLEGADAE] [datetime] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[RUTA_ID] [int] NOT NULL,
	[VIAJE_OK] [int] NULL,
 CONSTRAINT [PK_VIAJES] PRIMARY KEY CLUSTERED 
(
	[VIAJE_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[VIAJES]  WITH CHECK ADD  CONSTRAINT [FK_VIAJES_AERONAVES] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[AERONAVES] ([AERO_MATRI])

GO

ALTER TABLE [ABSTRACCIONX4].[VIAJES]  WITH CHECK ADD  CONSTRAINT [FK_VIAJES_RUTAS_AEREAS] FOREIGN KEY([RUTA_ID])
REFERENCES [ABSTRACCIONX4].[RUTAS_AEREAS] ([RUTA_ID])

GO

--Tabla De Butacas (SANTI HACER INSERT)

CREATE TABLE [ABSTRACCIONX4].[BUTACAS] (
	[BUT_NRO] [int] NOT NULL,
	[BUT_PISO] [int] NOT NULL,
	[BUT_TIPO] [varchar] (15) NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,

	CONSTRAINT [PK_BUTACAS] PRIMARY KEY CLUSTERED 
(
	[BUT_NRO],
	[AERO_MATRI] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ABSTRACCIONX4].[BUTACAS]  WITH CHECK ADD  CONSTRAINT [FK_BUTACAS_AERONAVES] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[AERONAVES] ([AERO_MATRI])

GO



-- Tabla Pasajes (SANTI HACER INSERT)

CREATE TABLE [ABSTRACCIONX4].[PASAJES](
	[PASAJE_COD] [int] IDENTITY NOT NULL,
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[PASAJE_PRECIO] [int] NOT NULL,
	[PASAJE_FECHA_COMPRA] [datetime] NOT NULL,
	[BUT_NRO] [int] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[PASAJE_CANCELADO] [int] NULL,
 CONSTRAINT [PK_PASAJES] PRIMARY KEY CLUSTERED 
(
	[PASAJE_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_CLIENTES] FOREIGN KEY([CLI_COD])
REFERENCES [ABSTRACCIONX4].[CLIENTES] ([CLI_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_VIAJES] FOREIGN KEY([VIAJE_COD])
REFERENCES [ABSTRACCIONX4].[VIAJES] ([VIAJE_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([BUT_NRO] , [AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([BUT_NRO] , [AERO_MATRI])

GO
/*
ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([AERO_MATRI])

GO*/


-- Tabla Encomiendas (FRAN HACER INSERT)

CREATE TABLE [ABSTRACCIONX4].[ENCOMIENDAS](
	[ENCOMIENDA_COD] [int] IDENTITY NOT NULL,
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[ENCOMIENDA_PRECIO] [int] NOT NULL,	
	[ENCOMIENDA_FECHA_COMPRA] [datetime] NOT NULL,
	[ENCOMIENDA_PESO_KG] [int] NOT NULL,
	[ENCOMIENDA_CANCELADO] [int] NULL,
 CONSTRAINT [PK_ENCOMIENDAS] PRIMARY KEY CLUSTERED 
(
	[ENCOMIENDA_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[ENCOMIENDAS]  WITH CHECK ADD  CONSTRAINT [FK_ENCOMIENDAS_CLIENTES] FOREIGN KEY([CLI_COD])
REFERENCES [ABSTRACCIONX4].[CLIENTES] ([CLI_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[ENCOMIENDAS]  WITH CHECK ADD  CONSTRAINT [FK_ENCOMIENDAS_VIAJES] FOREIGN KEY([VIAJE_COD])
REFERENCES [ABSTRACCIONX4].[VIAJES] ([VIAJE_COD])

GO

-- Tabla Devoluciones

CREATE TABLE [ABSTRACCIONX4].[DEVOLUCIONES](
	[DEVOLUC_COD] [int] IDENTITY NOT NULL,
	[DEVOLUC_FECHA] [datetime] NOT NULL,
	[DEVOLUC_NRO_COMPRA] [int] NOT NULL,
	[ENCOMIENDA_COD] [int] NULL,	
	[PASAJE_COD] [int] NULL,
	[DEVOLUC_MOTIVO] [char] (255) NULL,
 CONSTRAINT [PK_DEVOLUCIONES] PRIMARY KEY CLUSTERED 
(
	[DEVOLUC_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[DEVOLUCIONES]  WITH CHECK ADD  CONSTRAINT [FK_DEVOLUCIONES_PASAJES] FOREIGN KEY([PASAJE_COD])
REFERENCES [ABSTRACCIONX4].[PASAJES] ([PASAJE_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[DEVOLUCIONES]  WITH CHECK ADD  CONSTRAINT [FK_DEVOLUCIONES_ENCOMIENDAS] FOREIGN KEY([ENCOMIENDA_COD])
REFERENCES [ABSTRACCIONX4].[ENCOMIENDAS] ([ENCOMIENDA_COD])

GO


-- Tabla de Premios

CREATE TABLE [ABSTRACCIONX4].[PREMIOS](
	[PREMIO_COD] [int] IDENTITY NOT NULL,	
	[PREMIO_PUNTOS] [int] NOT NULL,
	[PREMIO_DETALLE] [char] (255) NOT NULL,
 CONSTRAINT [PK_PREMIOS] PRIMARY KEY CLUSTERED 
(
	[PREMIO_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Tabla de Canjes

CREATE TABLE [ABSTRACCIONX4].[CANJES](
	[CANJE_COD] [int] IDENTITY NOT NULL,
	[CANJE_NRO_DOC] [int] NOT NULL,
	[CANJE_FECHA] [datetime] NOT NULL,	
	[PREMIO_COD] [int] NOT NULL,
	[CANJE_CANTIDAD] [int] NOT NULL,
	[CLI_COD] [int]  NOT NULL,
 CONSTRAINT [PK_CANJES] PRIMARY KEY CLUSTERED 
(
	[CANJE_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[CANJES]  WITH CHECK ADD  CONSTRAINT [FK_CANJES_PREMIOS] FOREIGN KEY([PREMIO_COD])
REFERENCES [ABSTRACCIONX4].[PREMIOS] ([PREMIO_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[CANJES]  WITH CHECK ADD  CONSTRAINT [FK_CANJES_CLIENTES] FOREIGN KEY([CLI_COD])
REFERENCES [ABSTRACCIONX4].[CLIENTES] ([CLI_COD])

GO




-- =============================================================================================
-- Descripci�n:	Migraci�n de Tabla Maestra
-- =============================================================================================

--Inserta las ciudades en la tabla de ciudades

INSERT INTO [ABSTRACCIONX4].[CIUDADES] (CIU_DESC)
SELECT DISTINCT RUTA_CIUDAD_ORIGEN FROM gd_esquema.Maestra
GO

--SELECT * FROM [ABSTRACCIONX4].[CIUDADES]

 
--Inserta las aeronaves en la tabla de aeronaves
INSERT INTO [ABSTRACCIONX4].[AERONAVES] 
	(	AERO_MOD , 
		AERO_MATRI , 
		AERO_CANT_BUTACAS ,  
		AERO_CANT_KGS , 
		AERO_FAB , 
		AERO_SERV
	)
SELECT Aeronave_Modelo , Aeronave_Matricula matricula, MAX(Butaca_Nro), Aeronave_KG_Disponibles , Aeronave_Fabricante , Tipo_Servicio 
FROM gd_esquema.Maestra
GROUP BY Aeronave_Modelo , Aeronave_Matricula , Aeronave_KG_Disponibles , Aeronave_Fabricante , Tipo_Servicio
GO

--SELECT * FROM [ABSTRACCIONX4].[AERONAVES]
--SELECT COUNT(distinct Aeronave_Matricula) FROM gd_esquema.Maestra

-- Inserta los roles en la tabla roles
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE , ROL_ESTADO)
VALUES ('CLIENTE' , 1)
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE , ROL_ESTADO)
VALUES ('ADMINISTRADOR' , 1)
GO

--Inserta las funciones del sistema en la Tabla FUNCIONALIDAD
--No se considera el login ya que se aclara que no es una funcionalidad asignable a un rol.
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_COD, FUNC_DESC) VALUES (1,'Aqui ira el nombre de una funcionalidad')

GO


-- Inserta las rutas aereas en la tabla rutas aereas 

INSERT INTO [ABSTRACCIONX4].[RUTAS_AEREAS]

	(	RUTA_COD,
		RUTA_SERV,
		CIU_COD_O,
		CIU_COD_D,
		RUTA_PRECIO_BASE_KG,
		RUTA_PRECIO_BASE_PASAJE,
		RUTA_ESTADO
	)

SELECT	Ruta_Codigo,
		Tipo_Servicio,
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = Ruta_Ciudad_Origen),
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = Ruta_Ciudad_Destino),
		MAX(Ruta_Precio_BaseKG) as Precio_BaseKG,
		MAX(Ruta_Precio_BasePasaje) as Precio_BasePasaje,
		1 
	FROM gd_esquema.Maestra
	GROUP BY Ruta_Codigo,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino,Tipo_Servicio
GO

SELECT * FROM [ABSTRACCIONX4].RUTAS_AEREAS

-- Inserta los viajes en la tabla viajes 

INSERT INTO[ABSTRACCIONX4].[VIAJES]

	(	
		VIAJE_FECHA_SALIDA ,
		VIAJE_FECHA_LLEGADA ,
		VIAJE_FECHA_LLEGADAE ,
		AERO_MATRI ,
		RUTA_ID
	)

	SELECT	FechaSalida,
			FechaLLegada,
			Fecha_LLegada_Estimada,
			Aeronave_Matricula, 
			(SELECT R.RUTA_ID FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R WHERE R.RUTA_COD = Ruta_Codigo AND R.RUTA_PRECIO_BASE_KG = MAX(Ruta_Precio_BaseKG) 
			AND R.RUTA_PRECIO_BASE_PASAJE = MAX(Ruta_Precio_BasePasaje) )
			FROM gd_esquema.Maestra 
	GROUP BY FechaSalida,Fecha_LLegada_Estimada,FechaLLegada,Ruta_Codigo,Aeronave_Matricula

GO



-- Inserta los clientes en la tabla clientes
-- FALLA PORQUE HAY 2 FORROS QUE TIENEN EL MISMO DNI. HIJO DE SU MADRE.

INSERT INTO [ABSTRACCIONX4].[CLIENTES]

	(	
		CLI_DNI ,
		CLI_NOMBRE ,
		CLI_APELLIDO ,
		CLI_DIRECCION ,
		CLI_TELEFONO ,
		CLI_MAIL ,
		CLI_FECHA_NAC
	)

	SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Dir, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac FROM gd_esquema.Maestra

GO

-- Inserta encomiendas en la tabla encomiendas

-- Inserta butacas en la tabla butacas

INSERT INTO [ABSTRACCIONX4].[BUTACAS]
	(
		BUT_NRO ,
		BUT_PISO ,
		BUT_TIPO ,
		AERO_MATRI 
	)

	SELECT DISTINCT Butaca_Nro , Butaca_Piso , Butaca_Tipo , Aeronave_Matricula
	FROM gd_esquema.Maestra
	WHERE Butaca_Nro != 0 AND Butaca_Piso != 0 
GO

-- Inserta pasajes en la tabla pasajes--SOLUCIONAR

INSERT INTO [ABSTRACCIONX4].[PASAJES]
	(
		[CLI_COD] ,
		[VIAJE_COD] ,
		[PASAJE_PRECIO] ,
		[PASAJE_FECHA_COMPRA] ,
		[BUT_NRO] ,
		[AERO_MATRI] 
	)

SELECT 1,--(SELECT CLI_COD FROM [ABSTRACCIONX4].[CLIENTES] WHERE Cli_Dni = CLI_DNI AND Cli_Apellido = CLI_APELLIDO AND Cli_Nombre = CLI_NOMBRE 
		--AND CLI_DIRECCION = Cli_Dir AND CLI_FECHA_NAC = Cli_Fecha_Nac),
		1,--(SELECT VIAJE_COD FROM [ABSTRACCIONX4].[VIAJES] WHERE VIAJE_FECHA_LLEGADA = FechaLLegada AND VIAJE_FECHA_LLEGADAE = Fecha_LLegada_Estimada
		--AND VIAJE_FECHA_SALIDA = FechaSalida AND AERO_MATRI = Aeronave_Matricula),
		Pasaje_Precio , Pasaje_FechaCompra , Butaca_Nro , Aeronave_Matricula 
FROM gd_esquema.Maestra
WHERE Pasaje_Codigo != 0
		
select * from [ABSTRACCIONX4].[VIAJES]

