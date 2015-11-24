USE [GD2C2015]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--//////////// Creacion del esquema /////////////

CREATE SCHEMA [ABSTRACCIONX4] AUTHORIZATION [gd]
GO

--//////////////////////////////////////////////



--//////////// Creacion de tablas //////////////

--Rol: Si el estado es 0, se encuentra inactivo, si es 1 activo.
--Esta tabla contiene código, nombre y estado del rol (activo/inactivo)
CREATE TABLE [ABSTRACCIONX4].[ROLES](
	[ROL_COD] [tinyint] IDENTITY,
	[ROL_ESTADO] [bit] DEFAULT 1 NOT NULL, --para su baja lógica
	[ROL_NOMBRE] [varchar](30) UNIQUE NOT NULL, 
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


--Tabla Funcionalidad: Contiene los códigos, nombres de cada funcionalidad. 
CREATE TABLE [ABSTRACCIONX4].[FUNCIONALIDADES](
	[FUNC_COD] [tinyint] IDENTITY,
	[FUNC_DESC] [varchar] (60) UNIQUE NOT NULL,
CONSTRAINT [PK_FUNCIONALIDADES] PRIMARY KEY CLUSTERED 
(
	[FUNC_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


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


-- Tabla Usuarios: 
CREATE TABLE [ABSTRACCIONX4].[USUARIOS](
	[USERNAME] [varchar] (20),
	[PASSWORD] [varchar] (70) NOT NULL,
	[CANT_INTENTOS] [tinyint] DEFAULT 0
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[USERNAME] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO



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



-- Tabla Clientes:
CREATE TABLE [ABSTRACCIONX4].[CLIENTES](
	[CLI_COD] [int] IDENTITY ,
	[CLI_DNI] [numeric] (10,0) NOT NULL,
	[CLI_NOMBRE] [varchar] (60) NOT NULL,
	[CLI_APELLIDO] [varchar] (60) NOT NULL,
	[CLI_DIRECCION] [varchar] (80) NOT NULL,
	[CLI_TELEFONO] [int] NOT NULL,
	[CLI_MAIL] [varchar] (60) NULL,
	[CLI_FECHA_NAC] [datetime] NOT NULL 
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[CLI_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO



-- Tabla de Tipos de Tarjetas de Cerdito
CREATE TABLE [ABSTRACCIONX4].[TIPOS_TARJETAS](
	[TIPOTARJ_COD] [tinyint] IDENTITY,
	[TIPOTARJ_DESC] [varchar] (30),
CONSTRAINT [PK_TIPOS] PRIMARY KEY CLUSTERED 
(
	[TIPOTARJ_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

--Tabla de Tipos por Cuotas
CREATE TABLE [ABSTRACCIONX4].[TIPOS_CUOTAS](
	[TIPOTARJ_COD] [tinyint] NOT NULL,
	[CUO_NUM] [tinyint] NOT NULL,
CONSTRAINT [PK_TIPOS_CUOTAS] PRIMARY KEY CLUSTERED 
(
	[TIPOTARJ_COD],
	[CUO_NUM] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

ALTER TABLE [ABSTRACCIONX4].[TIPOS_CUOTAS]  WITH CHECK ADD  CONSTRAINT [FK_TIPOS_CUOTAS_TIPOS_TARJETAS] FOREIGN KEY([TIPOTARJ_COD])
REFERENCES [ABSTRACCIONX4].[TIPOS_TARJETAS] ([TIPOTARJ_COD])

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
	[TIPOTARJ_COD] [tinyint] NOT NULL,
CONSTRAINT [PK_TARJETAS] PRIMARY KEY CLUSTERED 
(
	[TARJ_NRO] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)  ON [PRIMARY]
GO	

ALTER TABLE [ABSTRACCIONX4].[TARJETAS]  WITH CHECK ADD  CONSTRAINT [FK_TARJETAS_TIPOS_TARJETAS] FOREIGN KEY([TIPOTARJ_COD])
REFERENCES [ABSTRACCIONX4].[TIPOS_TARJETAS] ([TIPOTARJ_COD])

GO

-- Tabla Compras:
CREATE TABLE [ABSTRACCIONX4].[COMPRAS](
	[COMP_PNR] [varchar] (12) UNIQUE NOT NULL,
	[COMP_FECHA] [datetime] NOT NULL,
	[COMP_EFECTIVO] [BIT] NOT NULL,
	[TARJ_NRO] [numeric] (16,0) NULL, 
	[COMP_CUOTAS] [tinyint] NULL, 
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


-- Tabla Ruta Aérea:
CREATE TABLE [ABSTRACCIONX4].[RUTAS_AEREAS](
	[RUTA_ID] [int] IDENTITY,
	[RUTA_COD] [int] NOT NULL,
	[CIU_COD_O] [smallint] NOT NULL,
	[CIU_COD_D] [smallint] NOT NULL,
	[RUTA_PRECIO_BASE_KG] [numeric] (5,2) NOT NULL,
	[RUTA_PRECIO_BASE_PASAJE] [numeric] (5,2) NOT NULL,
	[RUTA_ESTADO] [bit] DEFAULT 1,
	CONSTRAINT [UK_CIUDADES] UNIQUE (CIU_COD_O , CIU_COD_D , RUTA_COD ),
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


-- Tabla tipos de servicios por ruta
CREATE TABLE [ABSTRACCIONX4].[SERVICIOS_RUTAS](
	[SERV_COD] [tinyint] NOT NULL,
	[RUTA_ID] [int] NOT NULL
 CONSTRAINT [PK_SERVICIOS_RUTAS] PRIMARY KEY CLUSTERED 
(
	[SERV_COD] ASC,
	[RUTA_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[SERVICIOS_RUTAS]  WITH CHECK ADD  CONSTRAINT [FK_SERVICIOS_RUTAS_SERVICIO] FOREIGN KEY([SERV_COD])
REFERENCES [ABSTRACCIONX4].[SERVICIOS] ([SERV_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[SERVICIOS_RUTAS]  WITH CHECK ADD  CONSTRAINT [FK_SERVICIOS_RUTAS_RUTA] FOREIGN KEY([RUTA_ID])
REFERENCES [ABSTRACCIONX4].[RUTAS_AEREAS] ([RUTA_ID])

GO



-- Tabla de Aeronaves
CREATE TABLE [ABSTRACCIONX4].[AERONAVES](
	[AERO_FECHA_ALTA] [datetime] NULL,
	[AERO_MOD] [varchar] (30),
	[AERO_MATRI] [varchar] (8),
	[AERO_FAB] [varchar] (30) NOT NULL,	
	[SERV_COD] [tinyint] NOT NULL,	
	[AERO_FECHA_BAJA] [datetime] NULL,
	[AERO_CANT_BUTACAS] [smallint] NOT NULL,
	[AERO_CANT_KGS] [numeric] (6,2) NOT NULL,
	[CIU_COD_ORIGEN] [smallint] NULL,
 CONSTRAINT [PK_AERONAVES] PRIMARY KEY CLUSTERED 
(
	[AERO_MATRI] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_AERONAVES_SERVICIOS] FOREIGN KEY([SERV_COD])
REFERENCES [ABSTRACCIONX4].[SERVICIOS] ([SERV_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_AERONAVES_CIUDADES] FOREIGN KEY([CIU_COD_ORIGEN])
REFERENCES [ABSTRACCIONX4].[CIUDADES] ([CIU_COD])

GO


-- Tabla de Fueras de servicio por aeronave
CREATE TABLE [ABSTRACCIONX4].[FUERA_SERVICIO_AERONAVES](
	[FS_AERONAVE_ID] [int] IDENTITY,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[FECHA_FS] [datetime] NOT NULL,
	[FECHA_REINICIO] [datetime] NOT NULL,
 CONSTRAINT [PK_FUERA_SERVICIO_AERONAVES] PRIMARY KEY CLUSTERED 
(
	[FS_AERONAVE_ID] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ABSTRACCIONX4].[FUERA_SERVICIO_AERONAVES]  WITH CHECK ADD  CONSTRAINT [FK_FUERA_SERVICIO_AERONAVES] FOREIGN KEY([AERO_MATRI])
REFERENCES [ABSTRACCIONX4].[AERONAVES] ([AERO_MATRI])

GO


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


-- Tabla Encomiendas 
CREATE TABLE [ABSTRACCIONX4].[ENCOMIENDAS](
	[ENCOMIENDA_COD] [int] IDENTITY,
	[COMP_PNR] [varchar] (12) NULL, 
	[CLI_COD] [int] NOT NULL,
	[VIAJE_COD] [int] NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	[ENCOMIENDA_PRECIO] [numeric] (7,2) NOT NULL,	
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

--//////////////////////////////////////////////

--FUNCIONES, SP ETC. NECESARIAS PRE-MIGRACION 

CREATE VIEW [ABSTRACCIONX4].vwRandom
AS
SELECT RAND() as Rnd
GO 

CREATE FUNCTION [ABSTRACCIONX4].fnCustomPass 
(    
    @size AS INT, --Tamaño de la cadena aleatoria
    @op AS VARCHAR(2) --Opción para letras(ABC..), numeros(123...) o ambos.
)
RETURNS VARCHAR(62)
AS
BEGIN    

    DECLARE @chars AS VARCHAR(52),
            @numbers AS VARCHAR(10),
            @strChars AS VARCHAR(62),        
            @strPass AS VARCHAR(62),
            @index AS INT,
            @cont AS INT

    SET @strPass = ''
    SET @strChars = ''    
    SET @chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
    SET @numbers = '0123456789'

    SET @strChars = CASE @op WHEN 'C' THEN @chars --Letras
                        WHEN 'N' THEN @numbers --Números
                        WHEN 'CN' THEN @chars + @numbers --Ambos (Letras y Números)
                        ELSE '------'
                    END

    SET @cont = 0
    WHILE @cont < @size
    BEGIN
        SET @index = ceiling( ( SELECT rnd FROM vwRandom ) * (len(@strChars)))--Uso de la vista para el Rand() y no generar error.
        SET @strPass = @strPass + substring(@strChars, @index, 1)
        SET @cont = @cont + 1
    END    
        
    RETURN @strPass

END
GO




--//////////// Migración de tabla maestra /////////////

--Inserta las ciudades en la tabla de ciudades

INSERT INTO [ABSTRACCIONX4].[CIUDADES] (CIU_DESC)
SELECT DISTINCT SUBSTRING(Ruta_Ciudad_Origen,2,100) FROM gd_esquema.Maestra
GO



-- Inserta servicios en la tabla de servicios

INSERT INTO [ABSTRACCIONX4].[SERVICIOS]
	(
		[SERV_DESC],
		[SERV_PORC] 
	)
SELECT DISTINCT Tipo_Servicio , AVG((Ruta_Precio_BasePasaje * 100)/Pasaje_Precio)
FROM gd_esquema.Maestra WHERE Pasaje_Precio != 0
GROUP BY Tipo_Servicio

GO
 
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
INSERT INTO ABSTRACCIONX4.ROLES (ROL_NOMBRE)
	VALUES ('Administrador'),('Cliente')

GO

--Inserta las funciones del sistema en la Tabla FUNCIONALIDAD
--No se considera el login ya que se aclara que no es una funcionalidad asignable a un rol.
-- Insert de roles
INSERT INTO ABSTRACCIONX4.FUNCIONALIDADES (FUNC_DESC)
	VALUES ('ABM Rol'),
		   ('ABM Aeronave'),
		   ('ABM Ruta'),
		   ('Generación Viaje'),
		   ('Registro Llegada Destino'),
		   ('Consulta Millas'),
		   ('Consulta/Canje Millas'),
		   ('Compra'),
		   ('Devolución'),
		   ('Registro de Usuario'),
		   ('Listado Estadístico')

GO


-- Inserta tipo de tarjetas en la TABLA TIPOS

INSERT INTO [ABSTRACCIONX4].[TIPOS_TARJETAS] (TIPOTARJ_DESC) VALUES ('Visa')
INSERT INTO [ABSTRACCIONX4].[TIPOS_TARJETAS] (TIPOTARJ_DESC) VALUES ('Master Card')
INSERT INTO [ABSTRACCIONX4].[TIPOS_TARJETAS] (TIPOTARJ_DESC) VALUES ('American Express')

-- Inserta las cuotas posibles para los distintos tipos de tarjetas.

--Visa
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(1,1)
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(1,2)
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(1,3)

--MasterCard
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(2,2)
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(2,3)
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(2,6)

--American Express
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(3,6)
INSERT INTO [ABSTRACCIONX4].[TIPOS_CUOTAS] (TIPOTARJ_COD,CUO_NUM) VALUES(3,12)

GO

-- Inserta Productos en la TABLA PREMIOS
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(1500,'Televisor LED 40 pulgadas',160)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(2000,'Ciclomotor',23)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(900,'Remera',100)

GO

-- Inserta las rutas aereas en la tabla rutas aereas 
INSERT INTO [ABSTRACCIONX4].[RUTAS_AEREAS]

	(	RUTA_COD,
		CIU_COD_O,
		CIU_COD_D,
		RUTA_PRECIO_BASE_KG,
		RUTA_PRECIO_BASE_PASAJE
	)

SELECT	Ruta_Codigo,
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(Ruta_Ciudad_Origen,2,100)),
		(SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(Ruta_Ciudad_Destino,2,100)),
		MAX(Ruta_Precio_BaseKG) as Precio_BaseKG,
		MAX(Ruta_Precio_BasePasaje) as Precio_BasePasaje
	FROM gd_esquema.Maestra
	GROUP BY Ruta_Codigo,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino
GO

-- Inserta los tipos de servicios por cada ruta

INSERT INTO [ABSTRACCIONX4].[SERVICIOS_RUTAS]

	(	RUTA_ID,
		SERV_COD
	)
	
	SELECT DISTINCT (SELECT RUTA_ID 
						FROM ABSTRACCIONX4.RUTAS_AEREAS
						WHERE RUTA_COD = m.Ruta_Codigo AND
							  CIU_COD_O = (SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Origen,2,100)) AND
							  CIU_COD_D = (SELECT C.CIU_COD FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_DESC = SUBSTRING(m.Ruta_Ciudad_Destino,2,100))),
					(SELECT SERV_COD 
						FROM ABSTRACCIONX4.SERVICIOS
						WHERE SERV_DESC = m.Tipo_Servicio)
		FROM gd_esquema.Maestra m



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


SET IDENTITY_INSERT [ABSTRACCIONX4].[ENCOMIENDAS] ON

GO

--- TRIGGER PARA CREAR LA COMPRA DE LOS PASAJES/ENCOMIENDAS SOLO DURANTE LA MIGRACION. (Luego se elimina)
---------------------------------------------------------------------------------------------------------

CREATE TRIGGER [ABSTRACCIONX4].generadorCompraEncomiendas
ON [ABSTRACCIONX4].[ENCOMIENDAS]
AFTER INSERT
AS
BEGIN
	DECLARE @cli_cod int
	DECLARE @encomienda_cod int	
	DECLARE @fechaCompra datetime
	DECLARE cursorA CURSOR FOR (SELECT ENCOMIENDA_COD, CLI_COD FROM INSERTED)
	OPEN cursorA
	FETCH NEXT FROM cursorA INTO @encomienda_cod,@cli_cod
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @codigoPNR varchar(11)
		SELECT @codigoPNR = [ABSTRACCIONX4].fnCustomPass(10,'CN')
		SET @fechaCompra = (SELECT Paquete_FechaCompra FROM gd_esquema.Maestra WHERE Paquete_Codigo = @encomienda_cod)
		
		INSERT INTO [ABSTRACCIONX4].[COMPRAS] (COMP_PNR , COMP_FECHA ,COMP_EFECTIVO ,CLI_COD)
		VALUES (@codigoPNR,@fechaCompra,1,@cli_cod)

		UPDATE [ABSTRACCIONX4].[ENCOMIENDAS]
		SET COMP_PNR = @codigoPNR 
		WHERE ENCOMIENDA_COD = @encomienda_cod

		FETCH NEXT FROM cursorA INTO @encomienda_cod,@cli_cod
	END

	CLOSE cursorA
	DEALLOCATE cursorA

END
GO

---------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------

INSERT INTO ABSTRACCIONX4.ENCOMIENDAS
	(
		[ENCOMIENDA_COD],
		[CLI_COD],
		[VIAJE_COD],
		[AERO_MATRI],
		[ENCOMIENDA_PRECIO],	
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
	T.MAT_AERONAVE,T.PRECIO,T.CANT_KG,T.CANT_MILLAS
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

DROP TRIGGER [ABSTRACCIONX4].generadorCompraEncomiendas
GO


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

SET IDENTITY_INSERT [ABSTRACCIONX4].[PASAJES] ON
GO

--- TRIGGER PARA CREAR LA COMPRA DE LOS PASAJES/ENCOMIENDAS SOLO DURANTE LA MIGRACION. (Luego se elimina)
---------------------------------------------------------------------------------------------------------

/*
CREATE TRIGGER [ABSTRACCIONX4].generadorCompraPasajes
ON #tempPasajes
AFTER INSERT
AS
BEGIN
	DECLARE @cli_cod int
	DECLARE @pasaje_cod int
	DECLARE @fechaCompra datetime
	DECLARE cursorA CURSOR FOR (SELECT PASAJE_COD, CLI_COD,PASAJE_FECHA_COMPRA FROM INSERTED)
	OPEN cursorA
	FETCH NEXT FROM cursorA INTO @pasaje_cod,@cli_cod,@fechaCompra
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @codigoPNR varchar(11)
		SELECT @codigoPNR = [ABSTRACCIONX4].fnCustomPass(10,'CN')

		INSERT INTO [ABSTRACCIONX4].[COMPRAS] (COMP_PNR , COMP_FECHA ,COMP_EFECTIVO ,CLI_COD)
		VALUES (@codigoPNR,@fechaCompra,1,@cli_cod)

		UPDATE [ABSTRACCIONX4].[PASAJES]
		SET COMP_PNR = @codigoPNR 
		WHERE PASAJE_COD = @pasaje_cod

		FETCH NEXT FROM cursorA INTO @pasaje_cod,@cli_cod,@fechaCompra
	END

	CLOSE cursorA
	DEALLOCATE cursorA

END
GO


---------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------




-- Inserta pasajes en la tabla pasajes

INSERT INTO [ABSTRACCIONX4].PASAJES
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

DROP TRIGGER [ABSTRACCIONX4].generadorCompraPasajes
*/
-- Actualiza el valor de las butacas disponibles en viajes realizados 

UPDATE [ABSTRACCIONX4].[VIAJES]
	SET CANT_BUT_OCUPADAS = (SELECT COUNT(*) 
								FROM [ABSTRACCIONX4].PASAJES
								WHERE PASAJE_CANCELADO = 0 AND
									VIAJE_COD = v.VIAJE_COD)
	FROM [ABSTRACCIONX4].VIAJES v 

GO

-- Insert de usuario invitado y un administrador
-- el hash de la contraseña w23e está previamente calculado
DECLARE @Password VARCHAR(70)
SET @Password = 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7'
INSERT INTO ABSTRACCIONX4.USUARIOS (USERNAME,PASSWORD)
	VALUES ('Invitado',''),
		   ('usuarioX',@Password),
		   ('jorge256',@Password),
		   ('Alguien',@Password)

GO

-- Insert de roles por usuarios
INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
	SELECT 'Invitado',ROL_COD 
		FROM ABSTRACCIONX4.ROLES
		WHERE ROL_NOMBRE = 'Cliente'
INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
	SELECT USERNAME,(SELECT ROL_COD 
						FROM ABSTRACCIONX4.ROLES 
						WHERE ROL_NOMBRE='Administrador')
		FROM ABSTRACCIONX4.USUARIOS
		WHERE USERNAME != 'Invitado'

GO

-- Insert de funcionalidades por roles
INSERT INTO ABSTRACCIONX4.FUNCIONES_ROLES (ROL_COD,FUNC_COD)
	SELECT (SELECT ROL_COD
				FROM ABSTRACCIONX4.ROLES
				WHERE ROL_NOMBRE = 'Administrador'),FUNC_COD
		FROM ABSTRACCIONX4.FUNCIONALIDADES
		WHERE FUNC_DESC != 'Consulta Millas'
INSERT INTO ABSTRACCIONX4.FUNCIONES_ROLES (ROL_COD,FUNC_COD)
	SELECT (SELECT ROL_COD
				FROM ABSTRACCIONX4.ROLES
				WHERE ROL_NOMBRE = 'Cliente'),FUNC_COD
		FROM ABSTRACCIONX4.FUNCIONALIDADES
		WHERE FUNC_DESC IN ('Consulta Millas','Compra','Devolución')

GO

--Crea tabla para la fecha del sistema
CREATE TABLE [ABSTRACCIONX4].[FECHA]([HOY] [datetime])

GO

CREATE FUNCTION [ABSTRACCIONX4].obtenerFechaDeHoy()
RETURNS datetime
AS 
	begin
	return (select top 1 * from [ABSTRACCIONX4].[FECHA])
	end
GO

CREATE PROCEDURE [ABSTRACCIONX4].crearTablaFecha
	@fecha datetime
AS
	DELETE FROM [ABSTRACCIONX4].[FECHA]

	insert into [ABSTRACCIONX4].[FECHA] values (@fecha)
GO

--//////////////////////////////////////////////





--//////////// Creacion de stored procedures y funciones /////////////



--**************** LOGIN *************

-----------------------------Login---------------------------
CREATE PROCEDURE [ABSTRACCIONX4].LoginAdministrador
(@Usuario VARCHAR(20), @ContraseniaIngresada VARCHAR(70))
AS
BEGIN
	DECLARE @Contrasenia VARCHAR(70),
			@CantidadIntentos TINYINT,
			@ExisteUsuario BIT

	SELECT @ExisteUsuario = COUNT(*) 
		FROM ABSTRACCIONX4.USUARIOS
		WHERE USERNAME = @Usuario
	

	IF @ExisteUsuario = 0
	BEGIN
		RAISERROR('El nombre de usuario ingresado no existe.', 16, 1)
		RETURN
	END

	SELECT @Contrasenia = PASSWORD, @CantidadIntentos = CANT_INTENTOS
		FROM ABSTRACCIONX4.USUARIOS
		WHERE USERNAME = @Usuario

	IF @CantidadIntentos = 3
	BEGIN
		RAISERROR('Ha ingresado la contraseña 3 veces de forma incorrecta. Contáctese con un administrador para reestablecer su cuenta.', 16, 1)
		RETURN
	END

	IF @ContraseniaIngresada <> @Contrasenia
	BEGIN
		RAISERROR('Contraseña incorrecta.', 16, 1)
		
		UPDATE ABSTRACCIONX4.USUARIOS 
			SET CANT_INTENTOS = CANT_INTENTOS + 1
			WHERE USERNAME = @Usuario

		SELECT @CantidadIntentos = CANT_INTENTOS
			FROM ABSTRACCIONX4.USUARIOS
			WHERE USERNAME = @Usuario
		
		IF @CantidadIntentos = 3
		BEGIN
			RAISERROR('Ha ingresado la contraseña 3 veces de forma incorrecta. Contáctese con un administrador para reestablecer su cuenta.', 16, 1)
		END
		RETURN
	END

	UPDATE ABSTRACCIONX4.USUARIOS 
		SET CANT_INTENTOS = 0
		WHERE USERNAME = @Usuario
END

GO

--**************** MANEJO DE FECHAS *************


-------------------------------Fecha entre dos fechas----------------------------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_between

 (@fecha DATETIME, @fecha1 DATETIME, @fecha2 DATETIME)

RETURNS smallint

AS

BEGIN
	if(datediff(minute, '1900-01-01 00:00:00.0000000', @fecha) between datediff(minute, '1900-01-01 00:00:00.0000000', @fecha1) and datediff(minute, '1900-01-01 00:00:00.0000000', @fecha2))
	begin
		return 1
	end
	
	return 0
END
GO
-------------------------------Saber si un datetime esta entre otros dos datetime------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_is_between
 (@fecha DATETIME, @fecha1 DATETIME, @fecha2 DATETIME)

RETURNS smallint

AS

BEGIN
	if(@fecha1 <= @fecha2)
		begin
			if([ABSTRACCIONX4].datetime_between(@fecha, @fecha1, @fecha2) = 1)
				begin
					return 1
				end
		end
	else
		begin
			if([ABSTRACCIONX4].datetime_between(@fecha, @fecha2, @fecha1) = 1)
				begin
					return 1
				end
		end

	return 0
END

GO

-------------------------------Aeronaves disponibles para un vuelo-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_disponible

 (@matricula VARCHAR(8), @fecha_salida datetime, @fecha_llegada_estimada DATETIME)

RETURNS smallint

AS
	
BEGIN

	return (select case 
						when @matricula not in (select distinct AERO_MATRI
												from ABSTRACCIONX4.VIAJES v
												where 
												([ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_SALIDA, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_LLEGADAE, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_salida, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_llegada_estimada, v.VIAJE_FECHA_SALIDA, v.VIAJE_FECHA_LLEGADAE) = 1)
												)
						then 1
						else 0
					end)
END
GO

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


--**************** COMPRA PASAJES *************


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
				(TARJ_NRO,TARJ_CODSEG,TARJ_VTO,TIPOTARJ_COD)
				VALUES (@nroTarjeta,@codSeg,CAST(@vencMes AS VARCHAR)+ '/' + CAST(@vencAnio AS VARCHAR) ,@tipo_cod)
	END 
GO

-------------------------------Importe de una encomienda-----------------------------
CREATE FUNCTION [ABSTRACCIONX4].importeEncomienda(@kilos numeric(7,2), @origen varchar(80), @destino varchar(80))
RETURNS table

AS		
	return(
			select (r.RUTA_PRECIO_BASE_KG * @kilos) IMPORTE
				from ABSTRACCIONX4.RUTAS_AEREAS r
				where
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
	return(	select r.RUTA_PRECIO_BASE_PASAJE * (1 + s.SERV_PORC / 100) IMPORTE
				from ABSTRACCIONX4.RUTAS_AEREAS r, ABSTRACCIONX4.SERVICIOS s, ABSTRACCIONX4.servicios_rutas sr
				where r.RUTA_ID = sr.RUTA_ID and
				sr.SERV_COD = s.SERV_COD and
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
				ABSTRACCIONX4.CIUDADES c1, ABSTRACCIONX4.CIUDADES c2,ABSTRACCIONX4.SERVICIOS s,ABSTRACCIONX4.SERVICIOS_RUTAS rs
			where v.RUTA_ID = r1.RUTA_ID and
				v.RUTA_ID = r2.RUTA_ID and
				r1.CIU_COD_O = c1.CIU_COD and
				r2.CIU_COD_D = c2.CIU_COD and
				r1.RUTA_ID = rs.RUTA_ID and
				rs.SERV_COD = s.SERV_COD and
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
	IF NOT EXISTS (SELECT 1 FROM ABSTRACCIONX4.CLIENTES WHERE CLI_APELLIDO = @ape AND CLI_DNI = @dni)
	BEGIN
	INSERT INTO [ABSTRACCIONX4].CLIENTES
	(CLI_DNI,CLI_APELLIDO,CLI_NOMBRE,CLI_DIRECCION,CLI_MAIL,CLI_FECHA_NAC,CLI_TELEFONO) VALUES(@dni,@ape,@nombre,@direccion,@mail,@fechanac,@telefono)
	END

GO

--------------------------------ingresarCompra------------------------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].ingresarCompra
	@codigoPNR varchar(12),
	@nroTarjeta numeric(16,0),
	@formaDePago varchar(25),
	@codigoCli int,
	@cuotas smallint,
	@fechaCompra datetime
AS
	BEGIN
		IF(@formaDePago='Efectivo')
		BEGIN
			INSERT INTO ABSTRACCIONX4.COMPRAS
				(COMP_PNR,CLI_COD,COMP_EFECTIVO,COMP_FECHA)
				VALUES (@codigoPNR,@codigoCli,1,@fechaCompra)
		END
		ELSE
		BEGIN
			INSERT INTO ABSTRACCIONX4.COMPRAS
				(COMP_PNR,TARJ_NRO,CLI_COD,COMP_CUOTAS,COMP_EFECTIVO,COMP_FECHA)
				VALUES (@codigoPNR,@nroTarjeta,@codigoCli,@cuotas,0,@fechaCompra)
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
	@codigoPNR varchar(12),@cuotas smallint, @formaDePago varchar(25),@nroTarjeta numeric(16,0),@codSeg int,@vencMes int, @vencAnio int, @tipoTarjeta varchar(30),
	@agregarTarjeta BIT	
	)
AS
	BEGIN TRY
		
		DECLARE @fechaCompra datetime		
		SET @fechaCompra = [ABSTRACCIONX4].obtenerFechaDeHoy()

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
			SET @tipo_cod = (SELECT TIPOTARJ_COD FROM ABSTRACCIONX4.TIPOS_TARJETAS WHERE TIPOTARJ_DESC = @tipoTarjeta)
			EXEC [ABSTRACCIONX4].altaTarjeta @nroTarjeta,@codSeg,@vencMes,@vencAnio,@tipo_cod
		END

		DECLARE @cod_cli int
		SET @cod_cli = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @dni AND CLI_APELLIDO = @ape)
		EXEC [ABSTRACCIONX4].ingresarCompra @codigoPNR,@nroTarjeta,@formaDePago,@cod_cli,@cuotas,@fechaCompra
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
		DECLARE @clienteEncontrado BIT
		DECLARE @clienteActualizado BIT
		DECLARE @esComprador int
		DECLARE @codigoCli int		

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
				

					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@but,@matri,@precio/10)
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@but,@matri,@precio/10)
				END

			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_NRO, AERO_MATRI, PASAJE_MILLAS) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@but,@matri,@precio/10)
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
				
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@peso,@matri,@precio/10) 
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@peso,@matri,@precio/10) 
				END
			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel					
					
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG, AERO_MATRI, ENCOMIENDA_MILLAS) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@peso,@matri,@precio/10) 
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



----------------------------------Datos de tarjeta Válidos -------------------------------------

CREATE FUNCTION  [ABSTRACCIONX4].datosValidosDeTarjeta(@tarjNro numeric(16,0), @tarjVtoMes int, @tarjVtoAnio int, @tarjCodSeg int, @tarjTipo varchar(20))
RETURNS BIT
AS 
	BEGIN
	DECLARE @tipoCod int,@tipoDesc varchar(20),@nro numeric(16,0), @vto varchar(30), @codSeg int, @fechaVenc varchar(15)

	SELECT @vto = TARJ_VTO, @codSeg = TARJ_CODSEG, @tipoCod = TIPOTARJ_COD FROM [ABSTRACCIONX4].TARJETAS WHERE TARJ_NRO = @tarjNro	
	SELECT @tipoDesc=TIPOTARJ_DESC FROM [ABSTRACCIONX4].TIPOS_TARJETAS WHERE TIPOTARJ_COD = @tipoCod 

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


--************** ABM ROL *******************

-------------------------------Tipo Lista-------------------------------
CREATE TYPE [ABSTRACCIONX4].Lista AS TABLE 
( elemento VARCHAR(30) )

GO

-------------------------------Actualizar Funcionalidades-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ActualizarFuncionalidades
	@CodigoRol TINYINT,
	@FuncionalidadesNuevas Lista READONLY
AS
BEGIN
	INSERT INTO [ABSTRACCIONX4].FUNCIONES_ROLES (ROL_COD,FUNC_COD)
		SELECT @CodigoRol,
			   ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento)
		FROM @FuncionalidadesNuevas
		WHERE elemento NOT IN (SELECT * FROM [ABSTRACCIONX4].FuncionalidadesRol(@CodigoRol))
	
	DELETE FROM ABSTRACCIONX4.FUNCIONES_ROLES
		WHERE ROL_COD = @CodigoRol AND
			  FUNC_COD NOT IN
				(SELECT ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento)
				 FROM @FuncionalidadesNuevas)
END

GO



-------------------------------Alta Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AltaRol
	@Nombre VARCHAR(30),
	@Funcionalidades Lista Readonly
AS
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.ROLES (ROL_NOMBRE) VALUES (@Nombre)
		INSERT INTO ABSTRACCIONX4.FUNCIONES_ROLES (ROL_COD,FUNC_COD)
			SELECT ABSTRACCIONX4.DarCodigoDeRol(@Nombre),
			ABSTRACCIONX4.DarCodigoDeFuncionalidad(elemento) FROM @Funcionalidades
		INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
			VALUES ('Invitado',ABSTRACCIONX4.DarCodigoDeRol(@Nombre))
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH

GO

-------------------------------Existe Nombre Rol-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ExisteNombreRol (@Nombre VARCHAR(30))
	RETURNS BIT
AS
BEGIN
	DECLARE @Existe INT
	SELECT @Existe = COUNT(*) FROM ABSTRACCIONX4.ROLES WHERE ROL_NOMBRE = @Nombre
	IF(@Existe > 0)
	RETURN 1
	RETURN 0
END
GO


-------------------------------Dar Codigo De Rol-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DarCodigoDeRol (@Rol VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Rol_Cod TINYINT
	SELECT @Rol_Cod = R.ROL_COD 
	FROM ABSTRACCIONX4.ROLES R
	WHERE R.ROL_NOMBRE = @Rol
	IF(@Rol_Cod is NULL)
	SET @Rol_Cod = 0
	RETURN @Rol_Cod
END
GO

-------------------------------Dar Codigo De Funcionalidad-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].DarCodigoDeFuncionalidad (@Funcion VARCHAR(60))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Func_Cod TINYINT
	SELECT @Func_Cod = FUNC_COD 
	FROM ABSTRACCIONX4.FUNCIONALIDADES
	WHERE FUNC_DESC = @Funcion
	IF(@Func_Cod is NULL)
	SET @Func_Cod = 0
	RETURN @Func_Cod
END
GO
-------------------------------Baja Rol-------------------------------

--DROP PROCEDURE ABSTRACCIONX4.BajaRol
CREATE PROCEDURE [ABSTRACCIONX4].BajaRol
	@Nombre VARCHAR(30)	
AS
	-- VER SI VA TRANSACCION
		DECLARE @Codigo TINYINT
		SET @Codigo = [ABSTRACCIONX4].DarCodigoDeRol(@Nombre)
		
		UPDATE ABSTRACCIONX4.ROLES 
		SET ROL_ESTADO = 0 WHERE ROL_NOMBRE = @Nombre
		
GO

-------------------------------Modificar Rol-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarRol
	@NombreOriginal VARCHAR(30),
	@NombreNuevo VARCHAR(30),
	@Funcionalidades Lista READONLY,
	@Estado BIT
AS
	BEGIN TRY
		UPDATE ABSTRACCIONX4.ROLES 
			SET ROL_NOMBRE = @NombreNuevo, ROL_ESTADO = @Estado 
			WHERE ROL_NOMBRE = @NombreOriginal
		DECLARE @CodigoRol TINYINT
		SET @CodigoRol = [ABSTRACCIONX4].DarCodigoDeRol(@NombreNuevo)
		EXEC [ABSTRACCIONX4].ActualizarFuncionalidades @CodigoRol,@Funcionalidades
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre ' + @NombreNuevo + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO


-------------------------------Funcionalidades del rol-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].FuncionalidadesRol
	(@CodigoRol TINYINT)
RETURNS @FuncionalidadesRol TABLE (Funcionalidad VARCHAR(30))
AS
BEGIN
	INSERT INTO @FuncionalidadesRol
	SELECT FUNC_DESC 
		FROM FUNCIONALIDADES F JOIN FUNCIONES_ROLES FR ON (F.FUNC_COD = FR.FUNC_COD)
		WHERE ROL_COD = @CodigoRol
	RETURN
END

GO

-------------------------------Baja Funcionalidades-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BajaFuncionalidades
	@NombreRol VARCHAR(30)	
AS
	BEGIN TRY
		DECLARE @Codigo TINYINT
		SET @Codigo = [ABSTRACCIONX4].DarCodigoDeRol(@NombreRol)
		
		DELETE FROM [ABSTRACCIONX4].FUNCIONES_ROLES
		WHERE ROL_COD = @Codigo
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		--SET @Error = 'El nombre ' + @Nombre + ' ya esta en uso para otro rol'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO




-- ************ ABM AERONAVE *************

------------------------------Modificar Aeronave Viajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveViajes
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].VIAJES
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja AND
			  [ABSTRACCIONX4].ExisteViajeEntreFechas(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1
END
GO


-------------------------------Modificar Aeronave Pasajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronavePasajes
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	IF(@FechaBaja is NULL)
	BEGIN
		UPDATE [ABSTRACCIONX4].PASAJES
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja
	END
	ELSE BEGIN
	UPDATE [ABSTRACCIONX4].PASAJES
	SET AERO_MATRI = @MatriculaNueva
	WHERE AERO_MATRI = @MatriculaVieja AND 
			  [ABSTRACCIONX4].ExisteViajeEntreFechas(
			  [ABSTRACCIONX4].FechaSalidaDeViaje(VIAJE_COD),
			   @FechaBaja,@FechaReinicio) = 1
	END
END
GO
-------------------------------Modificar Aeronave Encomiendas-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveEncomiendas
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN 
	IF(@FechaBaja IS NULL)
	BEGIN
		UPDATE [ABSTRACCIONX4].ENCOMIENDAS
			SET AERO_MATRI = @MatriculaNueva
			WHERE AERO_MATRI = @MatriculaVieja
	END
	ELSE
	BEGIN
	UPDATE [ABSTRACCIONX4].ENCOMIENDAS
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja AND 
				  [ABSTRACCIONX4].ExisteViajeEntreFechas(
				  [ABSTRACCIONX4].FechaSalidaDeViaje(VIAJE_COD),
				  @FechaBaja,@FechaReinicio) = 1
	END
END
GO



-------------------------------Actualizar Butacas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AgregarButacas 
@Matricula VARCHAR(8), 
@CantidadPasillo TINYINT, 
@CantidadVentanilla TINYINT
AS
BEGIN
	DECLARE @i SMALLINT
	SET @i = 0
	DECLARE @CantidadButacas TINYINT
	SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
	WHILE (@i < @CantidadPasillo)
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_PISO , AERO_MATRI , BUT_TIPO)
		VALUES (@i , 1 , @Matricula , 'Pasillo')
		SET @i = @i + 1
	END

	WHILE (@i < @CantidadButacas)
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_PISO , AERO_MATRI , BUT_TIPO)
		VALUES (@i , 1 , @Matricula , 'Ventanilla')
		SET @i = @i + 1
	END
	
END

GO



-------------------------------Cantidad de fueras de servicio-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].CantidadFuerasDeServicioEntre
	(@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME)
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(*) 
			FROM [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
			WHERE AERO_MATRI = @Matricula AND
				  ([ABSTRACCIONX4].datetime_is_between(FECHA_FS,@FechaBaja,@FechaReinicio) = 1 OR
				   [ABSTRACCIONX4].datetime_is_between(FECHA_REINICIO,@FechaBaja,@FechaReinicio) = 1))
END
GO

-------------------------------Baja Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @HuboError BIT
	SET @HuboError = 0
	DECLARE @Error varchar(120)

	IF [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,@FechaReinicio) = 1
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		SET @HuboError = 1
	END

	IF [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(@Matricula,@FechaBaja,@FechaReinicio) > 0
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' ya se encuentra en fuera de servicio en esas fechas'
		RAISERROR(@Error, 16, 1)
		SET @HuboError = 1
	END

	IF @HuboError = 1
		RETURN
	
	INSERT INTO [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
		(AERO_MATRI,FECHA_FS,FECHA_REINICIO)
		VALUES (@Matricula,@FechaBaja,@FechaReinicio)
END
GO

-------------------------------Dar de Baja Logica-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].DarDeBajaLogica
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME
AS
	DECLARE @Error varchar(120)
	DECLARE @HuboError BIT
	SET @HuboError = 0

	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(NULL)

	IF [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,NULL) = 1
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		SET @HuboError = 1
	END

	IF [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(@Matricula,@FechaBaja,@FechaMaxima) > 0
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' ya se encuentra en fuera de servicio en esas fechas'
		RAISERROR(@Error, 16, 1)
		SET @HuboError = 1
	END

	IF @HuboError = 1
		RETURN

	UPDATE ABSTRACCIONX4.AERONAVES 
		SET AERO_FECHA_BAJA = @FechaBaja
		WHERE AERO_MATRI = @Matricula
GO


-------------------------------Tiene viaje comprado-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].TieneViajeComprado
	(@Matricula VARCHAR(8))
RETURNS BIT
AS
BEGIN
	DECLARE @ResultadoPasajes INT
	DECLARE @ResultadoEncomiendas INT 
	
	SELECT @ResultadoPasajes = COUNT(*) 
		FROM ABSTRACCIONX4.PASAJES
		   WHERE AERO_MATRI = @Matricula

	SELECT @ResultadoEncomiendas = COUNT(*) 
		FROM ABSTRACCIONX4.ENCOMIENDAS
		   WHERE AERO_MATRI = @Matricula

	IF @ResultadoPasajes > 0 OR @ResultadoEncomiendas > 0
		RETURN 1
	RETURN 0
END

GO

-------------------------------Tiene viaje entre fechas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].TieneViajeEntreFechas
	(@Matricula VARCHAR(8),@Fecha1 DATETIME,@Fecha2 DATETIME)
RETURNS BIT
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@Fecha2)

	DECLARE @Cantidad INT
	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.VIAJES
		WHERE AERO_MATRI = @Matricula
			AND ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@FechaMaxima) = 1

	IF @Cantidad > 0
		RETURN 1
	RETURN 0
END

GO

-------------------------------Cantidad Butacas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].CantidadButacas
	(@Matricula VARCHAR(8) , @Tipo VARCHAR(30))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Cantidad SMALLINT
	
	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.BUTACAS
		   WHERE AERO_MATRI = @Matricula
		   AND BUT_TIPO = @Tipo
	RETURN @Cantidad
END
GO

-------------------------------Cancelar Pasajes Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].CancelarPasajesEncomiendasAeronave
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)

	IF @FechaReinicio IS NULL
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_FECHA_BAJA = @FechaBaja
				WHERE AERO_MATRI = @Matricula
		END
	ELSE
		BEGIN
			INSERT INTO [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
				(AERO_MATRI,FECHA_FS,FECHA_REINICIO)
				VALUES (@Matricula,@FechaBaja,@FechaReinicio)
		END
		
		
		UPDATE ABSTRACCIONX4.PASAJES
			SET PASAJE_CANCELADO = 1 
			WHERE AERO_MATRI = @Matricula AND 
				  PASAJE_COD IN (SELECT * FROM [ABSTRACCIONX4].PasajesEntreFechas(@FechaBaja,@FechaMaxima))

		UPDATE ABSTRACCIONX4.ENCOMIENDAS
			SET ENCOMIENDA_CANCELADO = 1 
			WHERE AERO_MATRI = @Matricula AND 
				  ENCOMIENDA_COD IN (SELECT * FROM [ABSTRACCIONX4].EncomiendasEntreFechas(@FechaBaja,@FechaMaxima))
END

GO

-------------------------------Pasajes entre fechas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].PasajesEntreFechas
	(@Fecha1 DATETIME, @Fecha2 DATETIME)
RETURNS @Pasajes TABLE (PASAJE_COD INT)
AS
BEGIN
	INSERT INTO @Pasajes
	SELECT PASAJE_COD 
			FROM ABSTRACCIONX4.PASAJES P JOIN ABSTRACCIONX4.VIAJES V ON (P.VIAJE_COD = V.VIAJE_COD)
			WHERE ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@Fecha2) = 1

	RETURN
END

GO

-------------------------------Encomiendas entre fechas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].EncomiendasEntreFechas
	(@Fecha1 DATETIME, @Fecha2 DATETIME)
RETURNS @Encomiendas TABLE (ENCOMIENDA_COD INT)
AS
BEGIN
	INSERT INTO @Encomiendas
	SELECT ENCOMIENDA_COD 
			FROM ABSTRACCIONX4.ENCOMIENDAS E JOIN ABSTRACCIONX4.VIAJES V ON (E.VIAJE_COD = V.VIAJE_COD)
			WHERE ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@Fecha2) = 1

	RETURN
END

GO


-------------------------------Suplantar Aeronave Baja-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].SuplantarAeronave
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)
	
	DECLARE @MatriculaNueva VARCHAR(8)
	SET @MatriculaNueva = ABSTRACCIONX4.AeronaveDeMismasCaracteristicas(@Matricula,@FechaBaja,@FechaMaxima)
	
	IF @MatriculaNueva IS NULL
	BEGIN
		DECLARE @Error varchar(80)
		SET @Error = 'Ninguna aeronave de la flota tiene las mismas características'
		RAISERROR(@Error, 16, 1)
		RETURN
	END
	
	EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima
	EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima
	EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima

	IF @FechaReinicio IS NULL
		BEGIN
			UPDATE ABSTRACCIONX4.AERONAVES 
				SET AERO_FECHA_BAJA = @FechaBaja
				WHERE AERO_MATRI = @Matricula
		END
	ELSE
		BEGIN
			INSERT INTO [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
				(AERO_MATRI,FECHA_FS,FECHA_REINICIO)
				VALUES (@Matricula,@FechaBaja,@FechaReinicio)
		END
END

GO

-------------------------------Fecha de reinicio o maxima-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].FechaReinicioOMaxima
	(@FechaReinicio DATETIME)
RETURNS DATETIME
AS
BEGIN
	IF @FechaReinicio IS NULL
	BEGIN
		DECLARE @FechaMaxima DATETIME
		SELECT @FechaMaxima = MAX(VIAJE_FECHA_LLEGADA) FROM ABSTRACCIONX4.VIAJES
		RETURN @FechaMaxima
	END 
	RETURN @FechaReinicio
END

GO


-------------------------------Aeronave de mismas caracteristicas-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].AeronaveDeMismasCaracteristicas
	(@Matricula VARCHAR(8),@FechaBaja DATETIME,@FechaReinicio DATETIME)
RETURNS VARCHAR(8)
AS
BEGIN
	DECLARE @TipoServicio TINYINT
	DECLARE @Fabricante VARCHAR(30)
	DECLARE @Modelo VARCHAR(30)
	DECLARE @CantidadKG NUMERIC(6,2)

	SELECT @TipoServicio = SERV_COD, @Fabricante = AERO_FAB,
		   @Modelo = AERO_MOD, 
		   @CantidadKG = AERO_CANT_KGS
		   FROM ABSTRACCIONX4.AERONAVES
		   WHERE AERO_MATRI = @Matricula

	DECLARE @MatriculaNueva VARCHAR(8)
	SET @MatriculaNueva = NULL
	SELECT TOP 1 @MatriculaNueva = AERO_MATRI
		FROM ABSTRACCIONX4.AERONAVES A
		WHERE AERO_MATRI <> @Matricula AND
			  SERV_COD = @TipoServicio AND 
			  AERO_FAB = @Fabricante AND
			  AERO_MOD = @Modelo AND
			  [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_ALTA,@FechaBaja,@FechaReinicio) = 0 AND
			  AERO_CANT_KGS >= @CantidadKG AND
			  (SELECT CASE AERO_FECHA_BAJA 
					  WHEN NULL THEN 0
					  ELSE [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_BAJA,@FechaBaja,@FechaReinicio)
					  END) = 0 AND
			  [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(AERO_MATRI,@FechaBaja,@FechaReinicio) = 0 AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Pasillo') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Pasillo') AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Ventanilla') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Ventanilla') AND
			  ABSTRACCIONX4.DisponibleParaTodosLosVuelosDe(AERO_MATRI,@Matricula,@FechaBaja,@FechaReinicio) = 1
			  

	RETURN @MatriculaNueva
END

GO


-------------------------------Disponible para todos los vuelos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DisponibleParaTodosLosVuelosDe
	(@MatriculaNueva VARCHAR(8),@MatriculaVieja VARCHAR(8),@FechaBaja DATETIME,@FechaReinicio DATETIME)
RETURNS BIT
AS
BEGIN
	DECLARE @Cantidad INT
	SET @Cantidad = 0

	/*DECLARE @MaximaFechaSalida DATETIME
	SELECT @MaximaFechaSalida = [ABSTRACCIONX4].FechaReinicioOMaxima(@FechaReinicio)*/

	SELECT @Cantidad = COUNT(*) 
		FROM ABSTRACCIONX4.VIAJES v
		WHERE v.AERO_MATRI = @MatriculaVieja AND
			  [ABSTRACCIONX4].datetime_is_between(v.VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1
			  AND
			  [ABSTRACCIONX4].aeronave_disponible(@MatriculaNueva,v.VIAJE_FECHA_SALIDA,v.VIAJE_FECHA_LLEGADAE) = 0

	IF @Cantidad > 0
		RETURN 0
	RETURN 1
END

GO



-------------------------------Borrar Butacas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarButacas 
@Matricula VARCHAR(8)
AS
BEGIN
	DELETE FROM [ABSTRACCIONX4].BUTACAS 
	WHERE AERO_MATRI = @Matricula
END
GO



-------------------------------Fecha de salida de viaje-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].FechaSalidaDeViaje
	(@ViajeCod INT)
RETURNS DATETIME
AS
BEGIN
	DECLARE @FechaSalida DATETIME
	SELECT @FechaSalida = VIAJE_FECHA_SALIDA FROM ABSTRACCIONX4.VIAJES WHERE VIAJE_COD = @ViajeCod
	RETURN @FechaSalida
END

GO

-------------------------------Modificar Aeronave Butacas-------------------------------

CREATE PROCEDURE  [ABSTRACCIONX4].ModificarAeronaveButacas
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].BUTACAS
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja
END
GO


-------------------------------Viajes asignados a aeronave-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ExisteViajeEntreFechas
	(@FechaViaje DATETIME,@Fecha1 DATETIME,@Fecha2 DATETIME)
RETURNS BIT
AS
BEGIN
	IF @Fecha1 IS NULL
		RETURN 1

	DECLARE @Resultado BIT
	SET @Resultado = [ABSTRACCIONX4].datetime_is_between(@FechaViaje,@Fecha1,@Fecha2)

	RETURN @Resultado
END

GO


-------------------------------Modificacion Butaca-------------------------------
CREATE TRIGGER [ABSTRACCIONX4].ModificacionButaca
ON [ABSTRACCIONX4].BUTACAS
INSTEAD OF UPDATE
AS
DECLARE @MatriculaVieja VARCHAR(8)
DECLARE @MatriculaNueva VARCHAR(8)
declaRE @Error varchar(100)
BEGIN
	SELECT TOP 1 @MatriculaVieja = AERO_MATRI FROM DELETED
	SELECT TOP 1 @MatriculaNueva = AERO_MATRI FROM INSERTED
	
	IF(UPDATE(AERO_MATRI))
	BEGIN TRY
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_TIPO , AERO_MATRI , BUT_PISO)
		SELECT BUT_NRO , BUT_TIPO , AERO_MATRI , BUT_PISO FROM INSERTED
		
		EXECUTE [ABSTRACCIONX4].ModificarAeronavePasajes @MatriculaVieja , @MatriculaNueva , NULL , NULL
		EXECUTE [ABSTRACCIONX4].ModificarAeronaveEncomiendas @MatriculaVieja , @MatriculaNueva , NULL , NULL
		
		DELETE FROM [ABSTRACCIONX4].BUTACAS WHERE AERO_MATRI = @MatriculaVieja
	END TRY
	BEGIN CATCH
	SET @Error = 'ENTRE A ERROR TRIGGER BUTACA' + @MatriculaNueva 
				RAISERROR(@Error, 16, 1)
	END CATCH
END
GO

-------------------------------Viajes asignados a aeronave-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].TieneViajeAsignado
	(@Matricula VARCHAR(8))
RETURNS BIT
AS
BEGIN
	DECLARE @Resultado INT 
	SELECT @Resultado = COUNT(*) FROM ABSTRACCIONX4.VIAJES WHERE AERO_MATRI=@Matricula
	IF @Resultado > 0
		RETURN 1
	RETURN 0
END

GO


-------------------------------Borrar Pasajes-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarPasajes
@IdRuta INT
AS
UPDATE ABSTRACCIONX4.PASAJES 
SET PASAJE_CANCELADO = 1
WHERE PASAJE_COD IN (SELECT P.PASAJE_COD FROM [ABSTRACCIONX4].PASAJES P , [ABSTRACCIONX4].VIAJES V
WHERE P.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta)

GO


-------------------------------Borrar Encomiendas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarEncomiendas
@IdRuta INT
AS
UPDATE ABSTRACCIONX4.ENCOMIENDAS
SET ENCOMIENDA_CANCELADO = 1
WHERE ENCOMIENDA_COD IN (SELECT E.ENCOMIENDA_COD FROM [ABSTRACCIONX4].ENCOMIENDAS E , [ABSTRACCIONX4].VIAJES V
WHERE E.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta)

GO

-------------------------------Obter Codigo de Servicio-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Codigo TINYINT
	SELECT @Codigo = SERV_COD FROM [ABSTRACCIONX4].SERVICIOS WHERE SERV_DESC = @TipoDeServicio
	RETURN @Codigo
END
GO

-------------------------------Modificar Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarAeronave
	@MatriculaActual VARCHAR(8),
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadPasillo SMALLINT,
	@CantidadVentanilla SMALLINT,
	@CantidadKG NUMERIC(6,2)
AS
		DECLARE @Error varchar(80)
		DECLARE @ExisteMatricula BIT
BEGIN 
		DECLARE @CodigoServicio SMALLINT
		DECLARE @viajeComprado BIT
		DECLARE @CantidadButacas SMALLINT
		SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
		SELECT @ExisteMatricula = COUNT(*) FROM [ABSTRACCIONX4].AERONAVES WHERE AERO_MATRI = @Matricula AND AERO_MATRI <> @MatriculaActual
		SET @viajeComprado = [ABSTRACCIONX4].tieneViajeComprado(@MatriculaActual)
		--EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual	
		
		IF(@ExisteMatricula = 0)
		BEGIN
			SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
			--si tiene viaje comprado solo modifico su nombre, no se puede otra cosa EN AERONAVES
			IF( @viajeComprado = 1)
			BEGIN
				IF(@Matricula != @MatriculaActual)
				BEGIN	
					INSERT INTO [ABSTRACCIONX4].AERONAVES 
					(AERO_MOD , AERO_MATRI , AERO_FAB , SERV_COD , AERO_CANT_BUTACAS , AERO_CANT_KGS) VALUES
					(@Modelo , @Matricula , @Fabricante , @CodigoServicio , @CantidadButacas , @CantidadKG)

					EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @MatriculaActual , @Matricula , NULL , NULL
					EXECUTE [ABSTRACCIONX4].ModificarAeronaveButacas @MatriculaActual , @Matricula

					DELETE FROM [ABSTRACCIONX4].AERONAVES
					WHERE AERO_MATRI = @MatriculaActual
				END
			END
			ELSE
			BEGIN
					EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual
				
					UPDATE ABSTRACCIONX4.AERONAVES
					SET AERO_MOD = @Modelo , AERO_FAB = @Fabricante, AERO_MATRI = @Matricula ,
					SERV_COD = @CodigoServicio,
					AERO_CANT_BUTACAS = @CantidadButacas, AERO_CANT_KGS = @CantidadKG
					WHERE AERO_MATRI = @MatriculaActual

					EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
			END
		END	
		ELSE
		BEGIN
			SET @Error = '(modificar aeronave)Ya existe una aeronave con matrícula ' + @Matricula
			RAISERROR(@Error, 16, 1)
		END	
		
END
GO


-------------------------------Obter Codigo de Ciudad-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigoCiudad(@Ciudad VARCHAR(80))
RETURNS SMALLINT
AS
BEGIN
	DECLARE @Codigo SMALLINT
	SELECT @Codigo = CIU_COD FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_DESC = @Ciudad
	RETURN @Codigo
END
GO


-------------------------------Alta Aeronave-------------------------------

create PROCEDURE [ABSTRACCIONX4].AltaAeronave
	@Modelo VARCHAR(30),
	@Matricula VARCHAR(8),
	@Fabricante VARCHAR(30),
	@TipoDeServicio VARCHAR(30),
	@CantidadPasillo SMALLINT,
	@CantidadVentanilla SMALLINT,
	@CantidadKG NUMERIC(6,2),
	@FechaAlta DATETIME,
	@CiudadPrincipal VARCHAR(80)
AS
	BEGIN TRY
		DECLARE @CodigoServicio TINYINT
		DECLARE @CantidadButacas TINYINT
		DECLARE @CodigoCiudad SMALLINT
		SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
		SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
		SET @CodigoCiudad = [ABSTRACCIONX4].ObtenerCodigoCiudad(@CiudadPrincipal)

		INSERT INTO ABSTRACCIONX4.AERONAVES 
			(AERO_MOD,AERO_MATRI,AERO_FAB,SERV_COD,AERO_CANT_BUTACAS,AERO_CANT_KGS,AERO_FECHA_ALTA , CIU_COD_ORIGEN)
			VALUES (@Modelo,@Matricula,@Fabricante,@CodigoServicio,@CantidadButacas,@CantidadKG,@FechaAlta,@CodigoCiudad)
		EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe una aeronave con matrícula ' + @Matricula
		RAISERROR(@Error, 16, 1)
	END CATCH


GO

-------------------------------Obtener Codigo de Ciudad-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DatosDeAeronaveASuplantar(@Matricula VARCHAR(8),@FechaBaja DATETIME)
RETURNS @Datos TABLE (AERO_MOD VARCHAR(30),AERO_FAB VARCHAR(30),SERV_DESC VARCHAR(30),CIU_DESC VARCHAR(80),
				BUT_PASILLO SMALLINT, BUT_VENTANILLA SMALLINT, CANT_KGS NUMERIC(6,2))
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = ABSTRACCIONX4.FechaReinicioOMaxima(NULL)

	INSERT INTO @Datos
		SELECT AERO_MOD, AERO_FAB, s.SERV_DESC, 
			   [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula,@FechaBaja),
			   (SELECT COUNT(*) 
					FROM ABSTRACCIONX4.BUTACAS 
					WHERE AERO_MATRI = @Matricula AND BUT_TIPO = 'Pasillo'),
				(SELECT COUNT(*) 
					FROM ABSTRACCIONX4.BUTACAS 
					WHERE AERO_MATRI = @Matricula AND BUT_TIPO = 'Ventanilla'),
				AERO_CANT_KGS
			FROM ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (s.SERV_COD = a.SERV_COD)
			WHERE AERO_MATRI = @Matricula

	RETURN
END
GO


-------------------------------Ciudad en la que se encuentra-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula VARCHAR(8),@FechaBaja DATETIME)
RETURNS VARCHAR(80)
AS
BEGIN
	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = ABSTRACCIONX4.FechaReinicioOMaxima(NULL)

	DECLARE @Ciudad VARCHAR(80)

	SELECT TOP 1 @Ciudad = c.CIU_DESC 
		FROM ABSTRACCIONX4.VIAJES v JOIN ABSTRACCIONX4.RUTAS_AEREAS r ON (v.RUTA_ID=r.RUTA_ID)
									JOIN ABSTRACCIONX4.CIUDADES c ON (r.CIU_COD_O=C.CIU_COD)
		WHERE v.AERO_MATRI = @Matricula AND 
			  ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaMaxima) = 1
		ORDER BY v.VIAJE_FECHA_SALIDA

	RETURN @Ciudad
END
GO










-- ******** ABM RUTA **********


-------------------------------Alta Ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AltaRuta
	@Codigo INT,
	@Servicios Lista Readonly,
	@CiudadOrigen VARCHAR(80),
	@CiudadDestino VARCHAR(80),
	@PrecioPasaje NUMERIC(5,2),
	@PrecioeEncomienda NUMERIC(5,2)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.RUTAS_AEREAS
			(RUTA_COD,CIU_COD_O,CIU_COD_D,RUTA_PRECIO_BASE_PASAJE,RUTA_PRECIO_BASE_KG)
			VALUES (@Codigo,
				ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadOrigen),
				ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadDestino),
				@PrecioPasaje,
				@PrecioeEncomienda)

		INSERT INTO ABSTRACCIONX4.SERVICIOS_RUTAS
		(RUTA_ID,SERV_COD)
		SELECT SCOPE_IDENTITY(),ABSTRACCIONX4.ObtenerCodigoServicio(S.elemento)
		FROM @Servicios S

	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(255)
		SET @Error = 'Ya existe una ruta de ' + @CiudadOrigen + ' a ' + @CiudadDestino +
			' con el código ' + CONVERT(VARCHAR,@Codigo)
		RAISERROR(@Error, 16, 1)
	END CATCH
GO

-------------------------------Baja Ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BajaRuta
	@IdRuta INT
AS
	IF [ABSTRACCIONX4].EstaSiendoUsada(@IdRuta) = 1
	BEGIN
		DECLARE @Error varchar(255)
		SET @Error = 'No puede darse de baja a la ruta, tiene un viaje en este momento'
		RAISERROR(@Error, 16, 1)
		RETURN
	END
	
	UPDATE ABSTRACCIONX4.RUTAS_AEREAS
		SET RUTA_ESTADO = 0
		WHERE RUTA_ID=@IdRuta

	EXECUTE [ABSTRACCIONX4].BorrarPasajes @IdRuta
	EXECUTE [ABSTRACCIONX4].BorrarEncomiendas @IdRuta
GO

-------------------------------Tiene Viaje Programado-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].TieneViajeProgramado (@IdRuta INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Tiene INT
	SELECT @Tiene = COUNT(*) FROM [ABSTRACCIONX4].VIAJES V WHERE V.RUTA_ID = @IdRuta
	
	IF(@Tiene > 0)
		RETURN 1
	RETURN 0
END
GO

-------------------------------Tiene Viaje Vendidos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].TieneViajeVendidos (@IdRuta INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Tiene INT
	DECLARE @Tiene1 INT
	SELECT @Tiene = COUNT(*) FROM [ABSTRACCIONX4].PASAJES P JOIN [ABSTRACCIONX4].VIAJES V ON P.VIAJE_COD = V.VIAJE_COD WHERE V.RUTA_ID = @IdRuta
	SELECT @Tiene1 = COUNT(*) FROM [ABSTRACCIONX4].ENCOMIENDAS E JOIN [ABSTRACCIONX4].VIAJES V ON E.VIAJE_COD = V.VIAJE_COD WHERE V.RUTA_ID = @IdRuta

	
	
	IF(@Tiene + @Tiene1 > 0)
		RETURN 1
	RETURN 0
END
GO

-------------------------------Esta Siendo Usada-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].EstaSiendoUsada (@IdRuta INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Tiene INT
	SELECT @Tiene = COUNT(*) 
		FROM [ABSTRACCIONX4].VIAJES V 
		WHERE V.RUTA_ID = @IdRuta AND
			  [ABSTRACCIONX4].datetime_is_between([ABSTRACCIONX4].obtenerFechaDeHoy(),VIAJE_FECHA_SALIDA,VIAJE_FECHA_LLEGADAE) = 1
	
	IF(@Tiene > 0)
	RETURN 1
		RETURN 0
END
GO

-------------------------------Servicios de una ruta-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].ServiciosDeRuta
	(@IdRuta TINYINT)
RETURNS @Servicios TABLE (tipoServicio VARCHAR(30))
AS
BEGIN
	INSERT INTO @Servicios
	SELECT SERV_DESC 
		FROM ABSTRACCIONX4.SERVICIOS S JOIN ABSTRACCIONX4.SERVICIOS_RUTAS SR ON (S.SERV_COD= SR.SERV_COD)
		WHERE RUTA_ID = @IdRuta
	RETURN
END

GO

-------------------------------Actualizar servicios de ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ActualizarServiciosRuta
	@IdRuta TINYINT,
	@ServiciosNuevos Lista READONLY
AS
BEGIN
	INSERT INTO [ABSTRACCIONX4].SERVICIOS_RUTAS(RUTA_ID,SERV_COD)
		SELECT @IdRuta,
			   ABSTRACCIONX4.ObtenerCodigoServicio(elemento)
		FROM @ServiciosNuevos
		WHERE elemento NOT IN (SELECT * FROM [ABSTRACCIONX4].ServiciosDeRuta(@IdRuta))
	
	DELETE FROM ABSTRACCIONX4.SERVICIOS_RUTAS
		WHERE RUTA_ID = @IdRuta AND
			  SERV_COD NOT IN
				(SELECT ABSTRACCIONX4.ObtenerCodigoServicio(elemento)
				 FROM @ServiciosNuevos)
END

GO


-------------------------------Modificacion ruta-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].ModificarRuta
	@IdRuta INT,
	@Codigo INT,
	@Servicios Lista Readonly,
	@CiudadOrigen VARCHAR(80),
	@CiudadDestino VARCHAR(80),
	@PrecioPasaje NUMERIC(5,2),
	@PrecioeEncomienda NUMERIC(5,2)
AS
BEGIN
	BEGIN TRY
		UPDATE ABSTRACCIONX4.RUTAS_AEREAS
			SET RUTA_COD = @Codigo,
				CIU_COD_O = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadOrigen),
				CIU_COD_D = ABSTRACCIONX4.ObtenerCodigoCiudad(@CiudadDestino),
				RUTA_PRECIO_BASE_PASAJE = @PrecioPasaje,
				RUTA_PRECIO_BASE_KG = @PrecioeEncomienda
			WHERE RUTA_ID = @IdRuta

		EXEC ABSTRACCIONX4.ActualizarServiciosRuta @IdRuta,@Servicios
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(255)
		SET @Error = 'Ya existe una ruta de ' + @CiudadOrigen + ' a ' + @CiudadDestino +
			' con el código ' + CONVERT(VARCHAR,@Codigo)
		RAISERROR(@Error, 16, 1)
	END CATCH
END

GO


-- **************** GENERAR VIAJE **************

CREATE PROCEDURE [ABSTRACCIONX4].generarNuevoViaje
	@salida datetime, 
	@llegadaEstimada datetime, 
	@ruta int, 
	@matricula varchar(8)
AS
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.VIAJES 
			(VIAJE_FECHA_SALIDA, VIAJE_FECHA_LLEGADA, VIAJE_FECHA_LLEGADAE, AERO_MATRI, RUTA_ID)
			VALUES (@salida, null, @llegadaEstimada, @matricula, @ruta)
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'Ya existe un viaje programado con los mismos detalles'
		RAISERROR(@Error, 16, 1)
	END CATCH
GO


-- ************ REGISTRO USUARIO **************

CREATE PROCEDURE [ABSTRACCIONX4].RegistrarUsuario
(@Usuario VARCHAR(20),@Contrasenia VARCHAR(70))
AS
BEGIN
	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.USUARIOS (USERNAME,PASSWORD)
			VALUES (@Usuario,@Contrasenia)
		INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USERNAME,ROL_COD)
			SELECT @Usuario,ROL_COD
				FROM ABSTRACCIONX4.ROLES 
				WHERE ROL_NOMBRE = 'ADMINISTRADOR'
	END TRY
	BEGIN CATCH
		DECLARE @Error varchar(80)
		SET @Error = 'El nombre de usuario ' + @Usuario + ' ya existe.'
		RAISERROR(@Error, 16, 1)
	END CATCH
END

GO


-- ************** REGISTRO LLEGADA ****************

------------------------llegaADestinoCorrecto ----------------------------
CREATE FUNCTION [ABSTRACCIONX4].llegaADestinoCorrecto(@MatriculaTxt varchar(8), @ciuDestinoTxt varchar(80))
RETURNS INT --NO PONER SMALLINT 
AS 
	BEGIN
	DECLARE @rutaId int,@ciuDestinoId int, @ciuDestino varchar(80), @viajecod int 
	SELECT TOP 1 @rutaId=RUTA_ID, @viajecod = VIAJE_COD FROM [ABSTRACCIONX4].VIAJES WHERE AERO_MATRI=@MatriculaTxt AND VIAJE_FECHA_LLEGADA IS NULL ORDER BY VIAJE_FECHA_SALIDA
	SELECT @ciuDestinoId = CIU_COD_D FROM [ABSTRACCIONX4].RUTAS_AEREAS WHERE RUTA_ID = @rutaId 	
	SELECT @ciuDestino = CIU_DESC FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_COD = @ciuDestinoId

	BEGIN
		IF(@ciuDestino = @ciuDestinoTxt)
		BEGIN
		RETURN @viajecod
		END
	END	
	
	RETURN -1
	
	END
GO


-----------------esOrigenCorrecto ---------------------

CREATE FUNCTION [ABSTRACCIONX4].esOrigenCorrecto(@MatriculaTxt varchar(8), @ciuOrigenTxt varchar(80))
RETURNS BIT
AS 
	BEGIN
	DECLARE @rutaId int,@ciuOrigenId int, @ciuOrigen varchar(80) 
	SELECT TOP 1 @rutaId=RUTA_ID FROM [ABSTRACCIONX4].VIAJES WHERE AERO_MATRI=@MatriculaTxt AND VIAJE_FECHA_LLEGADA IS NULL ORDER BY VIAJE_FECHA_SALIDA
	SELECT @ciuOrigenId = CIU_COD_O FROM [ABSTRACCIONX4].RUTAS_AEREAS WHERE RUTA_ID = @rutaId 	
	SELECT @ciuOrigen = CIU_DESC FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_COD = @ciuOrigenId

	BEGIN
		IF(@ciuOrigen = @ciuOrigenTxt)
		BEGIN
		RETURN 1
		END
	END	
	
	RETURN 0
	
	END
GO


---------------obtenerFechaSalidaDeUnViaje ----------------------


CREATE FUNCTION [ABSTRACCIONX4].obtenerFechaSalidaDeUnViaje(@ViajeCod int)
RETURNS datetime
AS 
	BEGIN
	DECLARE @fechaSalida datetime
	SELECT @fechaSalida = VIAJE_FECHA_SALIDA FROM [ABSTRACCIONX4].VIAJES WHERE VIAJE_COD = @ViajeCod

	RETURN @fechaSalida
	
	END
GO


---------------agregarFechaLlegada -----------------

CREATE PROCEDURE [ABSTRACCIONX4].agregarFechaLlegada
	@fecha datetime, @viajecod int
AS
	UPDATE [ABSTRACCIONX4].VIAJES
	SET VIAJE_FECHA_LLEGADA = @fecha
	WHERE VIAJE_COD = @viajecod
GO


-- ************** MILLAS ****************

-------------------------------Obtener Ciudad dado el Codigo-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCiudadDesc(@Cod_Ciu int)
RETURNS varchar(15)
AS
BEGIN
	DECLARE @ciudad varchar(15)
	SELECT @ciudad = CIU_DESC FROM CIUDADES WHERE CIU_COD =@Cod_Ciu
	RETURN @ciudad
END

GO
-------------------------------Obtener Historial de Millas Encomiendas-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(@dni numeric (10,0), @ape varchar(30))
RETURNS TABLE
AS 
	RETURN(
		SELECT	E.ENCOMIENDA_COD as Codigo,
				'Encomienda' as Tipo,
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_O) as Origen,  
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_D) as Destino, 
				CO.COMP_FECHA as "Fecha de Compra",E.ENCOMIENDA_PRECIO as Precio,E.ENCOMIENDA_MILLAS as "Cant. de Millas"
			FROM ABSTRACCIONX4.CLIENTES C
			JOIN ABSTRACCIONX4.ENCOMIENDAS E ON C.CLI_COD=E.CLI_COD
			JOIN ABSTRACCIONX4.VIAJES V ON E.VIAJE_COD = V.VIAJE_COD 
			JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON R.RUTA_ID = V.RUTA_ID
			JOIN ABSTRACCIONX4.COMPRAS CO ON CO.COMP_PNR = E.COMP_PNR 
		WHERE C.CLI_DNI = @dni AND C.CLI_APELLIDO = @ape AND VIAJE_FECHA_LLEGADA is NOT NULL AND VIAJE_FECHA_LLEGADA BETWEEN 
			[ABSTRACCIONX4].obtenerFechaDeHoy() + 365  - 365 AND [ABSTRACCIONX4].obtenerFechaDeHoy() + 730 AND ENCOMIENDA_CANCELADO = 0

		)
GO

-------------------------------Obtener Historial de Millas Pasajes-------------------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerHistorialMillasPasajes(@dni numeric (10,0), @ape varchar(30))
RETURNS TABLE
AS 
	RETURN(
		SELECT	P.PASAJE_COD Codigo,
				'Pasaje' as Tipo,
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_O) as Origen,  
				ABSTRACCIONX4.ObtenerCiudadDesc(R.CIU_COD_D) as Destino, 
				CO.COMP_FECHA as "Fecha de Compra",P.PASAJE_PRECIO as Precio,P.PASAJE_MILLAS as "Cant. de Millas"
			FROM ABSTRACCIONX4.CLIENTES C
			JOIN ABSTRACCIONX4.PASAJES P ON C.CLI_COD=P.CLI_COD
			JOIN ABSTRACCIONX4.VIAJES V ON P.VIAJE_COD = V.VIAJE_COD 
			JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON R.RUTA_ID = V.RUTA_ID
			JOIN ABSTRACCIONX4.COMPRAS CO ON CO.COMP_PNR = P.COMP_PNR
		WHERE C.CLI_DNI = @dni AND C.CLI_APELLIDO = @ape AND VIAJE_FECHA_LLEGADA is NOT NULL AND VIAJE_FECHA_LLEGADA BETWEEN 
			[ABSTRACCIONX4].obtenerFechaDeHoy() - 365 AND [ABSTRACCIONX4].obtenerFechaDeHoy() AND PASAJE_CANCELADO = 0

		)
GO



-------------------------------Descontar millas-------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].DescontarMillas
	@cantMillas int,
	@dni int,
	@ape varchar(30)
AS
BEGIN
	DECLARE @MillasPasaje int
	DECLARE @seguir bit
	DECLARE @Cod int
	DECLARE @tipo varchar(15)
	DECLARE @Fecha datetime
	SET @seguir = 1
	DECLARE cursorPasajes CURSOR FOR (SELECT Codigo,Tipo,[Cant. de Millas],[Fecha de Compra] FROM ABSTRACCIONX4.obtenerHistorialMillasPasajes(@dni,@ape) UNION SELECT Codigo,Tipo,[Cant. de Millas],[Fecha de Compra] FROM ABSTRACCIONX4.obtenerHistorialMillasEncomiendas(@dni,@ape)) ORDER BY [Fecha de Compra]
	OPEN cursorPasajes
	FETCH NEXT FROM cursorPasajes INTO @Cod,@tipo,@MillasPasaje,@Fecha
	WHILE(@seguir=1)
	BEGIN
	
		
		IF(@cantMillas <= @MillasPasaje)
		BEGIN

			IF(@tipo = 'Pasaje')
			UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_MILLAS = @MillasPasaje - @cantMillas WHERE PASAJE_COD = @Cod
			ELSE
			UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_MILLAS = @MillasPasaje - @cantMillas WHERE ENCOMIENDA_COD = @Cod

		SET @seguir = 0
		END

		IF(@cantMillas > @MillasPasaje)
		BEGIN

			IF(@tipo = 'Pasaje')
			UPDATE ABSTRACCIONX4.PASAJES SET PASAJE_MILLAS = 0 WHERE PASAJE_COD = @Cod
			ELSE
			UPDATE ABSTRACCIONX4.ENCOMIENDAS SET ENCOMIENDA_MILLAS = 0 WHERE ENCOMIENDA_COD = @Cod

		SET @cantMillas = @cantMillas - @MillasPasaje 
		END


	FETCH NEXT FROM cursorPasajes INTO @Cod,@tipo,@MillasPasaje,@Fecha
	END
END

GO

--------------------------------Obtener Puntos de un Premio ----------------------

CREATE FUNCTION [ABSTRACCIONX4].obtenerPuntosDePremio(@descripcion varchar(100))
RETURNS int
AS 
	BEGIN
	DECLARE @puntos int
	SELECT @puntos = PREMIO_PUNTOS FROM [ABSTRACCIONX4].PREMIOS WHERE PREMIO_DETALLE = @descripcion

	RETURN @puntos
	
	END
GO

--------------------------------Reducir Cantidad de Stock en un Premio ----------------------

CREATE PROCEDURE [ABSTRACCIONX4].reducirStockDePremio
	@descripcion varchar(100),
	@cantidadSolicitada int
AS
BEGIN
	UPDATE [ABSTRACCIONX4].PREMIOS
	SET PREMIO_STOCK = PREMIO_STOCK - @cantidadSolicitada
	WHERE PREMIO_DETALLE = @descripcion
END

GO

-------------------------------Insertar Canje ----------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].insertarCanje
	@canje_fecha datetime,
	@canje_cantidad smallint,
	@descripcion varchar(100),
	@dni int,
	@ape varchar(30)
AS 
BEGIN
	DECLARE @cli_cod int, @premio_cod smallint
	SET @cli_cod = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @dni AND CLI_APELLIDO = @ape)	
	SET @premio_cod = (SELECT PREMIO_COD FROM ABSTRACCIONX4.PREMIOS WHERE PREMIO_DETALLE = @descripcion)

	INSERT INTO [ABSTRACCIONX4].CANJES (CANJE_FECHA,PREMIO_COD,CANJE_CANTIDAD,CLI_COD) 
	VALUES(@canje_fecha,@premio_cod,@canje_cantidad,@cli_cod)
END

GO

	


--******************* DEVOLUCIONES ****************
--------------------------------Obtener Codigo--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].ObtenerCodigo(@PNR VARCHAR(12))
RETURNS INT
AS
BEGIN
	DECLARE @Codigo INT
	SELECT @Codigo = COMP_PNR FROM [ABSTRACCIONX4].COMPRAS WHERE @PNR = COMP_PNR
	RETURN @Codigo
END
GO

--------------------------------Esta en viaje pasaje--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].EstaEnViajePasaje(@Codigo INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Esta BIT
	DECLARE @Cantidad INT

	SELECT @Cantidad = COUNT(*) FROM PASAJES P , VIAJES V
	WHERE P.VIAJE_COD = V.VIAJE_COD AND P.PASAJE_COD = @Codigo
	AND [ABSTRACCIONX4].datetime_is_between([ABSTRACCIONX4].obtenerFechaDeHoy(),V.VIAJE_FECHA_SALIDA,V.VIAJE_FECHA_LLEGADAE) = 0
	AND [ABSTRACCIONX4].datetime_esMayor([ABSTRACCIONX4].obtenerFechaDeHoy() , V.VIAJE_FECHA_LLEGADAE) = 1

	IF(@Cantidad > 0)
	SET @Esta  = 1
	ELSE
	SET @Esta  = 0
	RETURN @Esta
END
GO

--------------------------------Esta en viaje encomienda--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].EstaEnViajeEncomienda(@Codigo INT)
RETURNS BIT
AS
BEGIN
	DECLARE @Esta BIT
	DECLARE @Cantidad INT

	SELECT @Cantidad = COUNT(*) FROM ENCOMIENDAS E , VIAJES V
	WHERE E.VIAJE_COD = V.VIAJE_COD AND E.ENCOMIENDA_COD = @Codigo
	AND [ABSTRACCIONX4].datetime_is_between([ABSTRACCIONX4].obtenerFechaDeHoy(),V.VIAJE_FECHA_SALIDA,V.VIAJE_FECHA_LLEGADAE) = 0
	AND [ABSTRACCIONX4].datetime_esMayor([ABSTRACCIONX4].obtenerFechaDeHoy() , V.VIAJE_FECHA_LLEGADAE) = 1
	IF(@Cantidad > 0)
	RETURN 1
	RETURN 0
END
GO

--------------------------------Fecha es mayor que Fecha1--------------------------------
CREATE FUNCTION [ABSTRACCIONX4].datetime_esMayor(@Fecha DATETIME , @Fecha1 DATETIME)
RETURNS BIT
AS
BEGIN

IF(datediff(minute, '1900-01-01 00:00:00.0000000', @Fecha) > datediff(minute, '1900-01-01 00:00:00.0000000',  @Fecha1))
	BEGIN
		RETURN 1
	END
		RETURN 0
END
GO


--------------------------------Llenar Encomiendas--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].LlenarEncomiendas(@Codigo VARCHAR(12))
RETURNS TABLE 
AS

	RETURN(SELECT	[ENCOMIENDA_COD] AS 'Código',
					[COMP_PNR] AS 'Código Compra',
					[CLI_COD]  AS 'Cliente',
					[VIAJE_COD] AS 'Viaje' ,
					[ENCOMIENDA_PRECIO] AS 'Precio' ,
					[ENCOMIENDA_FECHA_COMPRA] AS 'Fecha Compra',
					[ENCOMIENDA_PESO_KG] AS 'Peso' ,
					[AERO_MATRI] AS 'Aeronave'  
			FROM ABSTRACCIONX4.ENCOMIENDAS
			WHERE COMP_PNR = @Codigo AND
			[ABSTRACCIONX4].EstaEnViajeEncomienda(ENCOMIENDA_COD) = 0 AND
			ENCOMIENDA_CANCELADO = 0
		  )

GO





--------------------------------Llenar Pasajes--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].LlenarPasajes(@Codigo VARCHAR(12))
RETURNS TABLE /*([PASAJE_COD] INT,
			   [COMP_COD] INT,
			   [CLI_COD] INT,
			   [VIAJE_COD] INT ,
	           [PASAJE_PRECIO] NUMERIC(7,2),
	           [PASAJE_FECHA_COMPRA] DATETIME,
	           [BUT_NRO] SMALLINT,
	           [AERO_MATRI] VARCHAR(8),
	           [PASAJE_CANCELADO] BIT)*/
AS

	RETURN(SELECT	[PASAJE_COD] AS 'Código',
					[COMP_PNR] AS 'Código Compra' ,
					[CLI_COD] AS 'Cliente',
					[VIAJE_COD] AS 'Viaje' ,
					[PASAJE_PRECIO] AS 'Precio' ,
					[PASAJE_FECHA_COMPRA] AS 'Fecha Compra',
					[BUT_NRO] AS 'Butaca',
					[AERO_MATRI] AS 'Aeronave' 
			FROM ABSTRACCIONX4.PASAJES 
			WHERE COMP_PNR = @Codigo AND
			[ABSTRACCIONX4].EstaEnViajePasaje(PASAJE_COD) = 0 AND
			PASAJE_CANCELADO = 0
		  )

GO

-------------------------------Tipo Lista1-------------------------------
CREATE TYPE [ABSTRACCIONX4].Lista1 AS TABLE 
( elemento VARCHAR(30) )

GO


--------------------------------Cancelar Pasajes y Encomiendas--------------------------------

CREATE PROCEDURE [ABSTRACCIONX4].CancelarPasajesYEncomiendas
	@Codigo VARCHAR(12),
	@Pasajes Lista1 readonly,
	@Encomiendas Lista1 readonly,
	@FechaDevolucion DATETIME,
	@Motivo VARCHAR(255)
AS
BEGIN
	DECLARE @CodigoDev INT
	DECLARE @Cod INT
	DECLARE @Cod1 INT
	INSERT INTO [ABSTRACCIONX4].DEVOLUCIONES (DEVOLUC_FECHA , DEVOLUC_MOTIVO)
	VALUES (@FechaDevolucion , @Motivo)
	SET @CodigoDev = @@IDENTITY
	DECLARE cursorPasajes CURSOR FOR SELECT * FROM @Pasajes
	OPEN cursorPasajes
	FETCH cursorPasajes INTO @Cod 
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		UPDATE  [ABSTRACCIONX4].PASAJES
		SET DEVOLUC_COD = @CodigoDev , PASAJE_CANCELADO = 1
		WHERE COMP_PNR = @Codigo AND
		@Cod = PASAJE_COD AND 
		[ABSTRACCIONX4].EstaEnViajeEncomienda(PASAJE_COD) = 0 AND
		PASAJE_CANCELADO = 0

		FETCH cursorPasajes INTO @Cod
	END
	CLOSE cursorPasajes
	DEALLOCATE cursorPasajes


	DECLARE cursorEncomiendas CURSOR FOR SELECT * FROM @Encomiendas
	OPEN cursorEncomiendas
	FETCH cursorEncomiendas INTO @Cod1 
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		UPDATE [ABSTRACCIONX4].ENCOMIENDAS
		SET DEVOLUC_COD = @CodigoDev , ENCOMIENDA_CANCELADO = 1
		WHERE COMP_PNR = @Codigo AND
		@Cod1 = ENCOMIENDA_COD AND 
		[ABSTRACCIONX4].EstaEnViajeEncomienda(ENCOMIENDA_COD) = 0 AND
		ENCOMIENDA_CANCELADO = 0

		FETCH cursorEncomiendas INTO @Cod1
	END
	CLOSE cursorEncomiendas
	DEALLOCATE cursorEncomiendas
END
GO


--******************* ESTADÍSTICAS ****************

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

