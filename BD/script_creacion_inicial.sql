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
	[ROL_COD] [tinyint] IDENTITY,
	[ROL_ESTADO] [bit] DEFAULT 1 NOT NULL, --para su baja l�gica
	[ROL_NOMBRE] [varchar](30) UNIQUE NOT NULL, 
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
--Tabla Funcionalidad: Contiene los c�digos, nombres de cada funcionalidad. 
CREATE TABLE [ABSTRACCIONX4].[FUNCIONALIDADES](
	[FUNC_COD] [tinyint] IDENTITY,
	[FUNC_DESC] [varchar] (60) UNIQUE NOT NULL,
CONSTRAINT [PK_FUNCIONALIDADES] PRIMARY KEY CLUSTERED 
(
	[FUNC_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--SIN INSERT
--Tabla Funciones por rol: Contiene las funcionalidades habilitadas para cada rol del sistema.
CREATE TABLE [ABSTRACCIONX4].[FUNCIONES_ROLES](
	[ROL_COD] [tinyint] NOT NULL,
	[FUNC_COD] [tinyint] NOT NULL,
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
	[USERNAME] [varchar] (20),
	[PASSWORD] [varchar] (70) NOT NULL,
	[ROL_COD] [tinyint] NULL,
	[CANT_INTENTOS] [tinyint] DEFAULT 0,
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
	[CLI_COD] [int] IDENTITY ,
	[CLI_DNI] [numeric] (10,0) NOT NULL,
	[CLI_NOMBRE] [varchar] (60) NOT NULL,
	[CLI_APELLIDO] [varchar] (60) NOT NULL,
	[CLI_DIRECCION] [varchar] (80) NOT NULL,
	[CLI_TELEFONO] [int] NOT NULL,
	[CLI_MAIL] [varchar] (60) NULL,
	[CLI_FECHA_NAC] [datetime] NOT NULL,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[CLI_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO



-- Tabla de Tipos de Tarjetas de Cerdito
CREATE TABLE [ABSTRACCIONX4].[TIPOS](
	[TIPO_COD] [tinyint] IDENTITY,
	[TIPO_DESC] [varchar] (30),
	[TIPO_CUO] [bit] DEFAULT 0,
CONSTRAINT [PK_TIPOS] PRIMARY KEY CLUSTERED 
(
	[TIPO_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

-- Tabla Servicios:
CREATE TABLE [ABSTRACCIONX4].[SERVICIOS](
	[SERV_COD] [tinyint] IDENTITY,
	[SERV_DESC] [varchar] (30) NOT NULL, 
	[SERV_PORC] [numeric] (5,2) NOT NULL,
CONSTRAINT [PK_SERVICIOS] PRIMARY KEY CLUSTERED 
(
	[SERV_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Tabla de Tarjetas
CREATE TABLE [ABSTRACCIONX4].[TARJETAS](
	[TARJ_NRO] [numeric] (16,0),
	[TARJ_VTO] [datetime] NOT NULL,
	[TARJ_CODSEG] [numeric] (4,0) NOT NULL,
	[TIPO_COD] [tinyint] NOT NULL,
CONSTRAINT [PK_TARJETAS] PRIMARY KEY CLUSTERED 
(
	[TARJ_NRO] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

ALTER TABLE [ABSTRACCIONX4].[TARJETAS]  WITH CHECK ADD  CONSTRAINT [FK_TARJETAS_TIPOS] FOREIGN KEY([TIPO_COD])
REFERENCES [ABSTRACCIONX4].[TIPOS] ([TIPO_COD])

GO


-- Tabla Compras:
CREATE TABLE [ABSTRACCIONX4].[COMPRAS](
	[COMP_COD] [int] IDENTITY,
	[TARJ_NRO] [numeric] (16,0) NULL,  
	[CLI_COD] [int] NOT NULL,
	[COMP_PNR] [int] UNIQUE NOT NULL,
CONSTRAINT [PK_COMPRAS] PRIMARY KEY CLUSTERED 
(
	[COMP_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO

ALTER TABLE [ABSTRACCIONX4].[COMPRAS]  WITH CHECK ADD  CONSTRAINT [FK_COMPRAS_TARJETAS] FOREIGN KEY([TARJ_NRO])
REFERENCES [ABSTRACCIONX4].[TARJETAS] ([TARJ_NRO])

GO

ALTER TABLE [ABSTRACCIONX4].[COMPRAS]  WITH CHECK ADD  CONSTRAINT [FK_COMPRAS_CLIENTES] FOREIGN KEY([CLI_COD])
REFERENCES [ABSTRACCIONX4].[CLIENTES] ([CLI_COD])

GO





/*select DISTINCT Ruta_Codigo , Ruta_Ciudad_Destino , Ruta_Ciudad_Origen,Ruta_Precio_BasePasaje , Pasaje_Precio , Tipo_Servicio , (Ruta_Precio_BasePasaje * 100)/Pasaje_Precio
from gd_esquema.Maestra where Pasaje_Precio != 0
order by Tipo_Servicio*/

--TIENE INSERT
-- Tabla Ciudades:

CREATE TABLE [ABSTRACCIONX4].[CIUDADES](
	[CIU_COD] [smallint] IDENTITY,
	[CIU_DESC] [varchar] (80) UNIQUE NOT NULL,
 CONSTRAINT [PK_CIUDADES] PRIMARY KEY CLUSTERED 
(
	[CIU_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--TIENE INSERT
-- Tabla Ruta A�rea:

CREATE TABLE [ABSTRACCIONX4].[RUTAS_AEREAS](
	[RUTA_ID] [int] IDENTITY,
	[RUTA_COD] [int] NOT NULL,
	[SERV_COD] [tinyint] NOT NULL,
	[CIU_COD_O] [smallint] NOT NULL,
	[CIU_COD_D] [smallint] NOT NULL,
	[RUTA_PRECIO_BASE_KG] [numeric] (5,2) NOT NULL,
	[RUTA_PRECIO_BASE_PASAJE] [numeric] (5,2) NOT NULL,
	[RUTA_ESTADO] [bit] DEFAULT 1, --para su baja l�gica
	CONSTRAINT [UK_CIUDADES] UNIQUE (CIU_COD_O , CIU_COD_D , RUTA_COD , SERV_COD),
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

ALTER TABLE [ABSTRACCIONX4].[RUTAS_AEREAS]  WITH CHECK ADD  CONSTRAINT [FK_RUTAS_AEREAS_SERVICIOS] FOREIGN KEY([SERV_COD])
REFERENCES [ABSTRACCIONX4].[SERVICIOS] ([SERV_COD])

GO
--TIENE INSERT
-- Tabla de Aeronaves

CREATE TABLE [ABSTRACCIONX4].[AERONAVES](
	[AERO_FECHA_ALTA] [datetime] NULL,
	[AERO_MOD] [varchar] (30),
	[AERO_MATRI] [varchar] (8),
	[AERO_FAB] [varchar] (30) NOT NULL,	
	[SERV_COD] [tinyint] NOT NULL,	
	[AERO_BAJA_FS] [bit] DEFAULT 0,
	[AERO_BAJA_VU] [bit] DEFAULT 0,
	[AERO_FECHA_FS] [datetime] NULL,
	[AERO_FECHA_RS] [datetime] NULL,
	[AERO_FECHA_BAJA] [datetime] NULL,
	[AERO_CANT_BUTACAS] [smallint] NOT NULL,
	[AERO_CANT_KGS] [numeric] (6,2) NOT NULL,
 CONSTRAINT [PK_AERONAVES] PRIMARY KEY CLUSTERED 
(
	[AERO_MATRI] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_AERONAVES_SERVICIOS] FOREIGN KEY([SERV_COD])
REFERENCES [ABSTRACCIONX4].[SERVICIOS] ([SERV_COD])

GO

--TIENE INSERT
-- Tabla De Viajes

CREATE TABLE [ABSTRACCIONX4].[VIAJES](
	[VIAJE_COD] [int] IDENTITY,
	[VIAJE_FECHA_SALIDA] [datetime] NOT NULL,
	[VIAJE_FECHA_LLEGADA] [datetime] NULL,
	[VIAJE_FECHA_LLEGADAE] [datetime] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[RUTA_ID] [int] NOT NULL,
	CONSTRAINT [UK_VIAJES] UNIQUE (VIAJE_FECHA_SALIDA , VIAJE_FECHA_LLEGADA , AERO_MATRI , RUTA_ID),
	CONSTRAINT [CK_FECHA] CHECK (VIAJE_FECHA_LLEGADA > VIAJE_FECHA_SALIDA),
	CONSTRAINT [CK_FECHAE] CHECK (VIAJE_FECHA_LLEGADAE > VIAJE_FECHA_SALIDA),
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

--Tabla De Butacas 

CREATE TABLE [ABSTRACCIONX4].[BUTACAS] (
	[BUT_NRO] [smallint] NOT NULL,
	[BUT_PISO] [tinyint] NOT NULL,
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



-- Tabla Pasajes

CREATE TABLE [ABSTRACCIONX4].[PASAJES](
	[PASAJE_COD] [int] IDENTITY,
	[COMP_COD] [int] NULL, -- TO DO!!!!!!!!!!!!!!!!!!!!!!!!!!!! ESTO DEBE SER NOT NULL.
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[PASAJE_PRECIO] [numeric] (7,2) NOT NULL,
	[PASAJE_FECHA_COMPRA] [datetime] NOT NULL,
	[BUT_NRO] [smallint] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[PASAJE_CANCELADO] [bit] DEFAULT 0,
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

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([BUT_NRO],[AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([BUT_NRO],[AERO_MATRI])

GO

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_COMPRAS] FOREIGN KEY([COMP_COD])
REFERENCES [ABSTRACCIONX4].[COMPRAS] ([COMP_COD])

GO
/*
ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([AERO_MATRI])

GO*/


-- Tabla Encomiendas 

CREATE TABLE [ABSTRACCIONX4].[ENCOMIENDAS](
	[ENCOMIENDA_COD] [int] IDENTITY,
	[COMP_COD] [int] NULL, -- TO DO!!!!!!!!!!!!!!!!!!!!!!!!!!!! ESTO DEBE SER NOT NULL
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[ENCOMIENDA_PRECIO] [numeric] (7,2) NOT NULL,	
	[ENCOMIENDA_FECHA_COMPRA] [datetime] NOT NULL,
	[ENCOMIENDA_PESO_KG] [numeric] (6,2) NOT NULL,
	[ENCOMIENDA_CANCELADO] [bit] DEFAULT 0,
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

ALTER TABLE [ABSTRACCIONX4].[ENCOMIENDAS]  WITH CHECK ADD  CONSTRAINT [FK_ENCOMIENDAS_COMPRAS] FOREIGN KEY([COMP_COD])
REFERENCES [ABSTRACCIONX4].[COMPRAS] ([COMP_COD])

GO

-- Tabla Devoluciones

CREATE TABLE [ABSTRACCIONX4].[DEVOLUCIONES](
	[DEVOLUC_COD] [int] IDENTITY,
	[DEVOLUC_FECHA] [datetime] NOT NULL,
	[ENCOMIENDA_COD] [int] NOT NULL,	
	[PASAJE_COD] [int] NOT NULL,
	[DEVOLUC_MOTIVO] [char] (255),
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
	[PREMIO_COD] [smallint] IDENTITY,	
	[PREMIO_PUNTOS] [smallint] NOT NULL,
	[PREMIO_DETALLE] [varchar] (60) NOT NULL,
 CONSTRAINT [PK_PREMIOS] PRIMARY KEY CLUSTERED 
(
	[PREMIO_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Tabla de Canjes

CREATE TABLE [ABSTRACCIONX4].[CANJES](
	[CANJE_COD] [int] IDENTITY,
	[CANJE_NRO_DOC] [numeric] (10,0) NOT NULL,
	[CANJE_FECHA] [datetime] NOT NULL,	
	[PREMIO_COD] [smallint] NOT NULL,
	[CANJE_CANTIDAD] [tinyint] NOT NULL,
	[CLI_COD] [int] NOT NULL,
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


-- Inserta servicios en la tabla de servicios

INSERT INTO [ABSTRACCIONX4].[SERVICIOS]
	(
		[SERV_DESC],
		[SERV_PORC] 
	)
SELECT DISTINCT Tipo_Servicio , AVG((Ruta_Precio_BasePasaje * 100)/Pasaje_Precio)
FROM gd_esquema.Maestra WHERE Pasaje_Precio != 0
GROUP BY Tipo_Servicio

--SELECT * FROM [ABSTRACCIONX4].[SERVICIOS]
 
--Inserta las aeronaves en la tabla de aeronaves
INSERT INTO [ABSTRACCIONX4].[AERONAVES] 
	(	AERO_MOD , 
		AERO_MATRI , 
		AERO_CANT_BUTACAS ,  
		AERO_CANT_KGS , 
		AERO_FAB , 
		SERV_COD
	)
SELECT  Aeronave_Modelo , 
		Aeronave_Matricula, 
		MAX(Butaca_Nro), 
		Aeronave_KG_Disponibles , 
		Aeronave_Fabricante , 		
		(SELECT S.SERV_COD FROM [ABSTRACCIONX4].[SERVICIOS] S WHERE S.SERV_DESC = Tipo_Servicio)		
FROM gd_esquema.Maestra
GROUP BY Aeronave_Modelo , Aeronave_Matricula , Aeronave_KG_Disponibles , Aeronave_Fabricante , Tipo_Servicio
GO

--SELECT * FROM [ABSTRACCIONX4].[AERONAVES]
--SELECT COUNT(distinct Aeronave_Matricula) FROM gd_esquema.Maestra

-- Inserta los roles en la tabla roles
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('CLIENTE')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('ADMINISTRADOR')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('ADMINIdaasdASDR')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('ADMI12312')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('ADMINISTSDASDASDASRADOR')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('BORIS JUDIO')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('MUERTE A ISRAEL')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('MUEasdasdasdASDAS')
INSERT [ABSTRACCIONX4].[ROLES] (ROL_NOMBRE)
VALUES ('AGUANTE FUTURAMA')

--Inserta las funciones del sistema en la Tabla FUNCIONALIDAD
--No se considera el login ya que se aclara que no es una funcionalidad asignable a un rol.
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Aqui ira el nombre de una funcionalidad')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Hacer jabon a los judios')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Hacesdasdasdasdsan a los judios')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Hacer 312312312s')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Matar musulmanes')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('Aguante boca')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('FORROS')
INSERT INTO [ABSTRACCIONX4].[FUNCIONALIDADES] (FUNC_DESC) VALUES ('PUTOS')
--GO


-- Inserta las rutas aereas en la tabla rutas aereas 

INSERT INTO [ABSTRACCIONX4].[RUTAS_AEREAS]

	(	RUTA_COD,
		SERV_COD,
		CIU_COD_O,
		CIU_COD_D,
		RUTA_PRECIO_BASE_KG,
		RUTA_PRECIO_BASE_PASAJE,
		RUTA_ESTADO
	)

SELECT	Ruta_Codigo,
		(SELECT S.SERV_COD FROM [ABSTRACCIONX4].[SERVICIOS] S WHERE S.SERV_DESC = Tipo_Servicio),
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = Ruta_Ciudad_Origen),
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = Ruta_Ciudad_Destino),
		MAX(Ruta_Precio_BaseKG) as Precio_BaseKG,
		MAX(Ruta_Precio_BasePasaje) as Precio_BasePasaje,
		1 
	FROM gd_esquema.Maestra
	GROUP BY Ruta_Codigo,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino,Tipo_Servicio
GO

--SELECT * FROM [ABSTRACCIONX4].[RUTAS_AEREAS] 

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
	GROUP BY FechaSalida,Fecha_LLegada_Estimada,FechaLLegada,Ruta_Codigo,Aeronave_Matricula,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino
GO
--SELECT * FROM [ABSTRACCIONX4].[VIAJES] 

-- Inserta los clientes en la tabla clientes

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

--SELECT * FROM [ABSTRACCIONX4].[CLIENTES]

-- Inserta encomiendas en la tabla encomiendas 

INSERT INTO [ABSTRACCIONX4].[ENCOMIENDAS]
	(
		[CLI_COD],
		[VIAJE_COD],
		[AERO_MATRI],
		[ENCOMIENDA_PRECIO],	
		[ENCOMIENDA_FECHA_COMPRA],
		[ENCOMIENDA_PESO_KG]
	)

SELECT T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.MAT_AERONAVE,T.PRECIO,T.FECHA_COMPRA,T.CANT_KG
FROM
(SELECT (SELECT c.CLI_COD 
			FROM [ABSTRACCIONX4].[CLIENTES] c 
			WHERE c.CLI_DNI = m.Cli_Dni 
				AND c.CLI_APELLIDO = m.Cli_Apellido 
				AND c.CLI_NOMBRE = m.Cli_Nombre  
		) CLIENTE,
		m.Paquete_Precio PRECIO,
		m.Paquete_KG CANT_KG,
		m.Paquete_FechaCompra FECHA_COMPRA,
		m.FechaSalida FECHA_SALIDA,
		m.Aeronave_Matricula MAT_AERONAVE,
		(SELECT r.RUTA_ID 
			FROM [ABSTRACCIONX4].RUTAS_AEREAS r
				JOIN [ABSTRACCIONX4].CIUDADES c1 ON (c1.CIU_COD = r.CIU_COD_O)
				JOIN [ABSTRACCIONX4].CIUDADES c2 ON (c2.CIU_COD = r.CIU_COD_D)
			WHERE r.RUTA_COD = m.Ruta_Codigo 
					AND c1.CIU_DESC = m.Ruta_Ciudad_Origen 
					AND c2.CIU_DESC = m.Ruta_Ciudad_Destino
		) ID_RUTA
FROM gd_esquema.Maestra m
WHERE Paquete_Precio > 0) T
GO

--SELECT * FROM [ABSTRACCIONX4].[ENCOMIENDAS]

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
	WHERE Pasaje_Codigo!=0
GO

-- Inserta pasajes en la tabla pasajes

INSERT INTO [ABSTRACCIONX4].[PASAJES]
	(
		[CLI_COD],
		[VIAJE_COD],
		[PASAJE_PRECIO],
		[PASAJE_FECHA_COMPRA],
		[BUT_NRO],
		[AERO_MATRI] 
		
	)
		
SELECT T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.PRECIO,T.FECHA_COMPRA,T.NRO_BUTACA,T.MAT_AERONAVE
FROM
(SELECT (SELECT c.CLI_COD 
			FROM [ABSTRACCIONX4].[CLIENTES] c 
			WHERE c.CLI_DNI = m.Cli_Dni 
				AND c.CLI_APELLIDO = m.Cli_Apellido 
				AND c.CLI_NOMBRE = m.Cli_Nombre  
		) CLIENTE,
		m.Pasaje_Precio PRECIO,
		m.Pasaje_FechaCompra FECHA_COMPRA,
		m.Butaca_Nro NRO_BUTACA,
		m.FechaSalida FECHA_SALIDA,
		m.Aeronave_Matricula MAT_AERONAVE,
		(SELECT r.RUTA_ID 
			FROM [ABSTRACCIONX4].RUTAS_AEREAS r
				JOIN [ABSTRACCIONX4].CIUDADES c1 ON (c1.CIU_COD = r.CIU_COD_O)
				JOIN [ABSTRACCIONX4].CIUDADES c2 ON (c2.CIU_COD = r.CIU_COD_D)
			WHERE r.RUTA_COD = m.Ruta_Codigo 
					AND c1.CIU_DESC = m.Ruta_Ciudad_Origen 
					AND c2.CIU_DESC = m.Ruta_Ciudad_Destino
		) ID_RUTA
FROM gd_esquema.Maestra m 
WHERE Pasaje_Precio > 0) T 

-- SELECT * FROM [ABSTRACCIONX4].[PASAJES]
