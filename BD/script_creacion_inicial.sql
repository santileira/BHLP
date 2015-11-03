USE [GD2C2015]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*select AERO_MATRI from ABSTRACCIONX4.AERONAVES WHERE AERO_MATRI LIKE 'L%' ORDER BY AERO_MATRI
delete from ABSTRACCIONX4.BUTACAS
where AERO_MATRI = 'SANTI'
delete from ABSTRACCIONX4.AERONAVES
where AERO_MATRI = 'SANTI'*/
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
	[CANT_INTENTOS] [tinyint] DEFAULT 0,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[USERNAME] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO


--SIN INSERT
--Tabla roles por usuario: tiene los roles para cada usuario del sistema
CREATE TABLE [ABSTRACCIONX4].[ROLES_USUARIOS](
	[ROL_COD] [tinyint] NOT NULL,
	[USERNAME] [varchar] (20) NOT NULL,
 CONSTRAINT [PK_ROLES_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC,
	[USERNAME] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[ROLES_USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_ROLES_USUARIOS_ROL] FOREIGN KEY([ROL_COD])
REFERENCES [ABSTRACCIONX4].[ROLES] ([ROL_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[ROLES_USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_ROLES_USUARIOS_USUARIO] FOREIGN KEY([USERNAME])
REFERENCES [ABSTRACCIONX4].[USUARIOS] ([USERNAME])

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
	[CLI_MILLAS_ACUMULADAS] [int] DEFAULT 0, 
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
CONSTRAINT [PK_TIPOS] PRIMARY KEY CLUSTERED 
(
	[TIPO_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

--Tabla de Tipos por Cuotas
CREATE TABLE [ABSTRACCIONX4].[TIPOS_CUOTAS](
	[TIPO_COD] [tinyint] NOT NULL,
	[CUO_NUM] [tinyint] NOT NULL,
CONSTRAINT [PK_TIPOS_CUOTAS] PRIMARY KEY CLUSTERED 
(
	[TIPO_COD],
	[CUO_NUM] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

ALTER TABLE [ABSTRACCIONX4].[TIPOS_CUOTAS]  WITH CHECK ADD  CONSTRAINT [FK_TIPOS_CUOTAS_TIPOS] FOREIGN KEY([TIPO_COD])
REFERENCES [ABSTRACCIONX4].[TIPOS] ([TIPO_COD])

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
	[TARJ_VTO] [varchar] (10) NOT NULL,
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
	[COMP_PNR] [varchar] (12) UNIQUE NOT NULL,
	[TARJ_NRO] [numeric] (16,0) NULL,  
	[CLI_COD] [int] NOT NULL,
	
CONSTRAINT [PK_COMPRAS] PRIMARY KEY CLUSTERED 
(
	[COMP_PNR] 
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
	[CIU_COD_P] [smallint] NULL,
 CONSTRAINT [PK_AERONAVES] PRIMARY KEY CLUSTERED 
(
	[AERO_MATRI] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_AERONAVES_SERVICIOS] FOREIGN KEY([SERV_COD])
REFERENCES [ABSTRACCIONX4].[SERVICIOS] ([SERV_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_AERONAVES_CIUDADES] FOREIGN KEY([CIU_COD_P])
REFERENCES [ABSTRACCIONX4].[CIUDADES] ([CIU_COD])

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
	[CANT_BUT_OCUPADAS] [smallint] DEFAULT 0
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

-- Tabla Devoluciones

CREATE TABLE [ABSTRACCIONX4].[DEVOLUCIONES](
	[DEVOLUC_COD] [int] IDENTITY,
	[DEVOLUC_FECHA] [datetime] NOT NULL,
	[DEVOLUC_MOTIVO] [char] (255),
 CONSTRAINT [PK_DEVOLUCIONES] PRIMARY KEY CLUSTERED 
(
	[DEVOLUC_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


-- Tabla Pasajes

CREATE TABLE [ABSTRACCIONX4].[PASAJES](
	[PASAJE_COD] [int] IDENTITY,
	[COMP_PNR] [varchar] (12) NULL, 
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[PASAJE_PRECIO] [numeric] (7,2) NOT NULL,
	[PASAJE_FECHA_COMPRA] [datetime] NOT NULL,
	[BUT_NRO] [smallint] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[PASAJE_CANCELADO] [bit] DEFAULT 0,
	[PASAJE_MILLAS] [int],
	[DEVOLUC_COD] [int]
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

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_COMPRAS] FOREIGN KEY([COMP_PNR])
REFERENCES [ABSTRACCIONX4].[COMPRAS] ([COMP_PNR])

GO

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_DEVOLUCIONES] FOREIGN KEY([DEVOLUC_COD])
REFERENCES [ABSTRACCIONX4].[DEVOLUCIONES] ([DEVOLUC_COD])

GO
/*
ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([AERO_MATRI])

GO*/


-- Tabla Encomiendas 

CREATE TABLE [ABSTRACCIONX4].[ENCOMIENDAS](
	[ENCOMIENDA_COD] [int] IDENTITY,
	[COMP_PNR] [varchar] (12) NULL, 
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[ENCOMIENDA_PRECIO] [numeric] (7,2) NOT NULL,	
	[ENCOMIENDA_FECHA_COMPRA] [datetime] NOT NULL,
	[ENCOMIENDA_PESO_KG] [numeric] (6,2) NOT NULL,
	[ENCOMIENDA_CANCELADO] [bit] DEFAULT 0,
	[ENCOMIENDA_MILLAS] [int],
	[DEVOLUC_COD] [int] NULL
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

ALTER TABLE [ABSTRACCIONX4].[ENCOMIENDAS]  WITH CHECK ADD  CONSTRAINT [FK_ENCOMIENDAS_COMPRAS] FOREIGN KEY([COMP_PNR])
REFERENCES [ABSTRACCIONX4].[COMPRAS] ([COMP_PNR])

GO

ALTER TABLE [ABSTRACCIONX4].[ENCOMIENDAS]  WITH CHECK ADD  CONSTRAINT [FK_ENCOMIENDAS_DEVOLUCIONES] FOREIGN KEY([DEVOLUC_COD])
REFERENCES [ABSTRACCIONX4].[DEVOLUCIONES] ([DEVOLUC_COD])

GO




-- Tabla de Premios

CREATE TABLE [ABSTRACCIONX4].[PREMIOS](
	[PREMIO_COD] [smallint] IDENTITY,	
	[PREMIO_PUNTOS] [smallint] NOT NULL,
	[PREMIO_DETALLE] [varchar] (60) NOT NULL,
	[PREMIO_STOCK] [int] NOT NULL,
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
SELECT DISTINCT SUBSTRING(Ruta_Ciudad_Origen,2,100) FROM gd_esquema.Maestra
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

-- Inserta los roles en la tabla roles

--Inserta las funciones del sistema en la Tabla FUNCIONALIDAD
--No se considera el login ya que se aclara que no es una funcionalidad asignable a un rol.

GO

-- Inserta tipo de tarjetas en la TABLA TIPOS

INSERT INTO [ABSTRACCIONX4].[TIPOS] (TIPO_DESC) VALUES ('Visa')
INSERT INTO [ABSTRACCIONX4].[TIPOS] (TIPO_DESC) VALUES ('Master Card')
INSERT INTO [ABSTRACCIONX4].[TIPOS] (TIPO_DESC) VALUES ('American Express')

-- Inserta Productos en la TABLA PREMIOS

INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(1500,'Consolador Aleman de 18 velocidades',160)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(2000,'Quip� + Tor� ',23)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(900,'Cinturonga',100)

INSERT INTO [ABSTRACCIONX4].FUNCIONALIDADES (FUNC_DESC) VALUES ('HOLA')
INSERT INTO [ABSTRACCIONX4].FUNCIONALIDADES (FUNC_DESC) VALUES ('chau')
INSERT INTO [ABSTRACCIONX4].ROLES (ROL_NOMBRE) VALUES ('BORIS')

INSERT INTO [ABSTRACCIONX4].ROLES (ROL_NOMBRE) VALUES ('jor')
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
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(Ruta_Ciudad_Origen,2,100)),
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(Ruta_Ciudad_Destino,2,100)),
		MAX(Ruta_Precio_BaseKG) as Precio_BaseKG,
		MAX(Ruta_Precio_BasePasaje) as Precio_BasePasaje,
		1 
	FROM gd_esquema.Maestra
	GROUP BY Ruta_Codigo,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino,Tipo_Servicio
GO


-- Inserta los viajes en la tabla viajes (el insert de las butacas disponibles se hace al final)

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

-- PARA PROBAR REGISTRO LLEGADA DESTINO - INSERT INTO [ABSTRACCIONX4].[VIAJES] values(GETDATE(),NULL,(GETDATE() + 1),'ZBV-508',12)
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
		CLI_FECHA_NAC,
		CLI_MILLAS_ACUMULADAS
	)

	SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Dir, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac,0 FROM gd_esquema.Maestra

GO

--SELECT * FROM [ABSTRACCIONX4].[CLIENTES]

-- Inserta encomiendas en la tabla encomiendas 
SET IDENTITY_INSERT [ABSTRACCIONX4].[ENCOMIENDAS] ON
GO

INSERT INTO [ABSTRACCIONX4].[ENCOMIENDAS]
	(
		[ENCOMIENDA_COD],
		[CLI_COD],
		[VIAJE_COD],
		[AERO_MATRI],
		[ENCOMIENDA_PRECIO],	
		[ENCOMIENDA_FECHA_COMPRA],
		[ENCOMIENDA_PESO_KG],
		[ENCOMIENDA_MILLAS]
	)

SELECT T.ENCOMIENDA_COD,T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.MAT_AERONAVE,T.PRECIO,T.FECHA_COMPRA,T.CANT_KG,T.CANT_MILLAS
FROM
(SELECT (SELECT c.CLI_COD 
			FROM [ABSTRACCIONX4].[CLIENTES] c 
			WHERE c.CLI_DNI = m.Cli_Dni 
				AND c.CLI_APELLIDO = m.Cli_Apellido 
				AND c.CLI_NOMBRE = m.Cli_Nombre  
		) CLIENTE,
		m.Paquete_Codigo ENCOMIENDA_COD,
		m.Paquete_Precio PRECIO,
		m.Paquete_KG CANT_KG,
		m.Paquete_FechaCompra FECHA_COMPRA,
		m.FechaSalida FECHA_SALIDA,
		m.Aeronave_Matricula MAT_AERONAVE,
		CAST((m.Paquete_Precio / 10) as INT) CANT_MILLAS,
		(SELECT r.RUTA_ID 
			FROM [ABSTRACCIONX4].RUTAS_AEREAS r
				JOIN [ABSTRACCIONX4].CIUDADES c1 ON (c1.CIU_COD = r.CIU_COD_O)
				JOIN [ABSTRACCIONX4].CIUDADES c2 ON (c2.CIU_COD = r.CIU_COD_D)
			WHERE r.RUTA_COD = m.Ruta_Codigo 
					AND c1.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Origen,2,100) 
					AND c2.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Destino,2,100) 
		) ID_RUTA
FROM gd_esquema.Maestra m
WHERE Paquete_Precio > 0) T
GO

SET IDENTITY_INSERT [ABSTRACCIONX4].[ENCOMIENDAS] OFF

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

SET IDENTITY_INSERT [ABSTRACCIONX4].[PASAJES] ON
GO

INSERT INTO [ABSTRACCIONX4].[PASAJES]
	(
		[PASAJE_COD],
		[CLI_COD],
		[VIAJE_COD],
		[PASAJE_PRECIO],
		[PASAJE_FECHA_COMPRA],
		[BUT_NRO],
		[AERO_MATRI],
		[PASAJE_MILLAS]
		
	)
		
SELECT T.PASAJE_COD,T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.PRECIO,T.FECHA_COMPRA,T.NRO_BUTACA,T.MAT_AERONAVE,T.CANT_MILLAS
FROM
(SELECT (SELECT c.CLI_COD 
			FROM [ABSTRACCIONX4].[CLIENTES] c 
			WHERE c.CLI_DNI = m.Cli_Dni 
				AND c.CLI_APELLIDO = m.Cli_Apellido 
				AND c.CLI_NOMBRE = m.Cli_Nombre  
		) CLIENTE,
		m.Pasaje_Codigo PASAJE_COD,
		m.Pasaje_Precio PRECIO,
		m.Pasaje_FechaCompra FECHA_COMPRA,
		m.Butaca_Nro NRO_BUTACA,
		m.FechaSalida FECHA_SALIDA,
		m.Aeronave_Matricula MAT_AERONAVE,
		CAST((m.Pasaje_Precio / 10) as INT) CANT_MILLAS,
		(SELECT r.RUTA_ID 
			FROM [ABSTRACCIONX4].RUTAS_AEREAS r
				JOIN [ABSTRACCIONX4].CIUDADES c1 ON (c1.CIU_COD = r.CIU_COD_O)
				JOIN [ABSTRACCIONX4].CIUDADES c2 ON (c2.CIU_COD = r.CIU_COD_D)
			WHERE r.RUTA_COD = m.Ruta_Codigo 
					AND c1.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Origen,2,100) 
					AND c2.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Destino,2,100)
		) ID_RUTA
FROM gd_esquema.Maestra m 
WHERE Pasaje_Precio > 0) T 

GO
SET IDENTITY_INSERT [ABSTRACCIONX4].[PASAJES] OFF



-- Actualiza el valor de las butacas disponibles en viajes realizados 

UPDATE [ABSTRACCIONX4].[VIAJES]
	SET CANT_BUT_OCUPADAS = (SELECT COUNT(*) 
								FROM [ABSTRACCIONX4].PASAJES
								WHERE PASAJE_CANCELADO = 0 AND
									VIAJE_COD = v.VIAJE_COD)
	FROM [ABSTRACCIONX4].VIAJES v 



-- Insert de usuario invitado y un administrador

INSERT INTO ABSTRACCIONX4.USUARIOS (USERNAME,PASSWORD)
	VALUES ('INVITADO',''),('UnAdmin','x')


-- Insert de roles

INSERT INTO ABSTRACCIONX4.ROLES (ROL_NOMBRE)
	VALUES ('ADMINISTRADOR'),('CLIENTE')


-- Insert de funcionalidades

INSERT INTO ABSTRACCIONX4.FUNCIONALIDADES (FUNC_DESC)
	VALUES ('ABM Rol'),
		   ('ABM Aeronave'),
		   ('ABM Ruta'),
		   ('Generaci�n Viaje'),
		   ('Registro Llegada Destino'),
		   ('Canje Millas'),
		   ('Consulta Millas'),
		   ('Compra'),
		   ('Devoluci�n'),
		   ('Registro de Usuario'),
		   ('Listado Estad�stico')


-- !!!!!!! ACTUALIZAR AL FINAL !!!!!
-- Insert de roles por usuarios

INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
	SELECT 'INVITADO',ROL_COD 
		FROM ABSTRACCIONX4.ROLES
		WHERE ROL_NOMBRE = 'Cliente'

