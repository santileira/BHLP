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
	[USUA_COD] [tinyint] IDENTITY,
	[USERNAME] [varchar] (20),
	[PASSWORD] [varchar] (70) NOT NULL,
	[CANT_INT_FALL] [tinyint] DEFAULT 0,
	[HABILITADO] [bit] DEFAULT 1
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[USUA_COD] 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO



--Tabla roles por usuario: tiene los roles para cada usuario del sistema
CREATE TABLE [ABSTRACCIONX4].[ROLES_USUARIOS](
	[ROL_COD] [tinyint] NOT NULL,
	[USUA_COD] [tinyint] NOT NULL,
 CONSTRAINT [PK_ROLES_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ROL_COD] ASC,
	[USUA_COD] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [ABSTRACCIONX4].[ROLES_USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_ROLES_USUARIOS_ROL] FOREIGN KEY([ROL_COD])
REFERENCES [ABSTRACCIONX4].[ROLES] ([ROL_COD])

GO

ALTER TABLE [ABSTRACCIONX4].[ROLES_USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_ROLES_USUARIOS_USUARIO] FOREIGN KEY([USUA_COD])
REFERENCES [ABSTRACCIONX4].[USUARIOS] ([USUA_COD])

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
	[HABILITADA] [bit] DEFAULT 1
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
	CONSTRAINT [UK_VIAJES] UNIQUE (VIAJE_FECHA_SALIDA , VIAJE_FECHA_LLEGADAE , AERO_MATRI , RUTA_ID),
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
	[BUT_ID] [int] IDENTITY,
	[BUT_NRO] [smallint] NOT NULL,
	[BUT_PISO] [tinyint] NOT NULL,
	[BUT_TIPO] [varchar] (15) NOT NULL,
	[AERO_MATRI] [varchar] (8) NOT NULL,
	CONSTRAINT [UK_BUTACAS] UNIQUE (BUT_NRO,AERO_MATRI),
	CONSTRAINT [PK_BUTACAS] PRIMARY KEY CLUSTERED
(
	[BUT_ID]
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
	[BUT_ID] [int] NOT NULL,
	[PASAJE_CANCELADO] [bit] DEFAULT 0,
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

ALTER TABLE [ABSTRACCIONX4].[PASAJES]  WITH CHECK ADD  CONSTRAINT [FK_PASAJES_BUTACAS] FOREIGN KEY([BUT_ID])
REFERENCES [ABSTRACCIONX4].[BUTACAS] ([BUT_ID])

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
	[ENCOMIENDA_PRECIO] [numeric] (7,2) NOT NULL,	
	[ENCOMIENDA_PESO_KG] [numeric] (6,2) NOT NULL,
	[ENCOMIENDA_CANCELADO] [bit] DEFAULT 0,
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
		AERO_CANT_KGS , 
		AERO_FAB , 
		SERV_COD
	)
SELECT  Aeronave_Modelo , 
		Aeronave_Matricula,  
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
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(2000,'Ciclomotor',2)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(900,'Remera',100)
INSERT INTO [ABSTRACCIONX4].[PREMIOS] (PREMIO_PUNTOS,PREMIO_DETALLE,PREMIO_STOCK) VALUES(30,'Llavero',300)

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


-- Inserta las compras en la tabla compras
INSERT INTO ABSTRACCIONX4.COMPRAS
	(
		[COMP_PNR],
		[COMP_FECHA],
		[COMP_EFECTIVO],
		[CLI_COD]
	)
SELECT [ABSTRACCIONX4].fnCustomPass(10,'CN'),Tabla2.* FROM (
SELECT DISTINCT FechaCompra, Efectivo, CliCod FROM (
			SELECT m1.Paquete_FechaCompra as FechaCompra,1 as Efectivo,(SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = m1.Cli_Dni AND CLI_APELLIDO = m1.Cli_Apellido) as CliCod FROM gd_esquema.Maestra m1 WHERE Paquete_Codigo != 0
				UNION 
			SELECT m2.Pasaje_FechaCompra as FechaCompra,1 as Efectivo,(SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = m2.Cli_Dni AND CLI_APELLIDO = m2.Cli_Apellido) as CliCod FROM gd_esquema.Maestra m2 WHERE Pasaje_Codigo != 0) 
			as Tabla) as Tabla2

/*
CREATE FUNCTION [ABSTRACCIONX4].DevolverPNR (@Cli_Cod INT , @Fecha DATETIME)
RETURNS VARCHAR(15)
AS
BEGIN
	DECLARE @PNR VARCHAR(15)
	SELECT @PNR = COMP_PNR FROM [ABSTRACCIONX4].COMPRAS WHERE COMP_FECHA = @Fecha AND CLI_COD = @Cli_Cod
	RETURN @PNR
END
GO


INSERT INTO [ABSTRACCIONX4].[COMPRAS]

	(	
		COMP_PNR ,
		COMP_EFECTIVO,
		CLI_COD ,
		COMP_FECHA
		)
	SELECT [ABSTRACCIONX4].fnCustomPass(10,'CN') PNR, 1 EFECTIVO, T.CLIENTE , T.FECHA FROM (
	SELECT (SELECT c.CLI_COD 
			FROM [ABSTRACCIONX4].[CLIENTES] c 
			WHERE c.CLI_DNI = m.Cli_Dni 
				AND c.CLI_APELLIDO = m.Cli_Apellido 
				AND c.CLI_NOMBRE = m.Cli_Nombre  
		) AS CLIENTE, CASE m.Paquete_FechaCompra WHEN 0 THEN m.Pasaje_FechaCompra ELSE m.Paquete_FechaCompra END AS FECHA FROM gd_esquema.Maestra m
		) T
		GROUP BY T.FECHA , T.CLIENTE
GO*/


GO

INSERT INTO ABSTRACCIONX4.ENCOMIENDAS
	(
		[ENCOMIENDA_COD],
		[CLI_COD],
		[VIAJE_COD],
		[ENCOMIENDA_PRECIO],	
		[ENCOMIENDA_PESO_KG],
		[COMP_PNR]
	)

SELECT T.ENCOMIENDA_COD,T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.PRECIO,T.CANT_KG,
	(SELECT COMP_PNR FROM ABSTRACCIONX4.COMPRAS WHERE COMP_FECHA = FECHA_COMPRA AND CLI_COD = CLIENTE)
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

-- Inserta pasajes en la tabla pasajes

INSERT INTO [ABSTRACCIONX4].PASAJES
	(
		[PASAJE_COD],
		[CLI_COD],
		[VIAJE_COD],
		[PASAJE_PRECIO],
		[BUT_ID],
		[COMP_PNR]
		
	)
		
SELECT T.PASAJE_COD,T.CLIENTE,
	(SELECT v.VIAJE_COD 
	FROM [ABSTRACCIONX4].VIAJES v
	WHERE v.RUTA_ID = T.ID_RUTA
		AND v.AERO_MATRI = T.MAT_AERONAVE
		AND v.VIAJE_FECHA_SALIDA = T.FECHA_SALIDA
	) COD_VIAJE,
	T.PRECIO,T.NRO_BUTACA,
	(SELECT COMP_PNR FROM ABSTRACCIONX4.COMPRAS WHERE COMP_FECHA = FECHA_COMPRA AND CLI_COD = CLIENTE)
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
		(SELECT BUT_ID 
			FROM [ABSTRACCIONX4].BUTACAS B
			WHERE B.AERO_MATRI = m.Aeronave_Matricula AND
				  B.BUT_NRO = m.Butaca_Nro) NRO_BUTACA,
		m.FechaSalida FECHA_SALIDA,
		m.Aeronave_Matricula MAT_AERONAVE,
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
INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USUA_COD,ROL_COD)
	SELECT (SELECT USUA_COD FROM ABSTRACCIONX4.USUARIOS U WHERE U.USERNAME = 'Invitado') ,ROL_COD 
		FROM ABSTRACCIONX4.ROLES
		WHERE ROL_NOMBRE = 'Cliente'

INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USUA_COD,ROL_COD)
	SELECT (SELECT USUA_COD FROM ABSTRACCIONX4.USUARIOS U1 WHERE U1.USERNAME = U.USERNAME),
			(SELECT ROL_COD FROM ABSTRACCIONX4.ROLES WHERE ROL_NOMBRE='Administrador')
		FROM ABSTRACCIONX4.USUARIOS U
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
		WHERE FUNC_DESC IN ('Consulta Millas','Compra')

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

	SELECT @Contrasenia = PASSWORD, @CantidadIntentos = CANT_INT_FALL
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
			SET CANT_INT_FALL = CANT_INT_FALL + 1
			WHERE USERNAME = @Usuario

		SELECT @CantidadIntentos = CANT_INT_FALL
			FROM ABSTRACCIONX4.USUARIOS
			WHERE USERNAME = @Usuario
		
		IF @CantidadIntentos = 3
		BEGIN
			RAISERROR('Ha ingresado la contraseña 3 veces de forma incorrecta. Contáctese con un administrador para reestablecer su cuenta.', 16, 1)
		END
		RETURN
	END

	UPDATE ABSTRACCIONX4.USUARIOS 
		SET CANT_INT_FALL = 0
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

-------------------------------Aeronaves en servicio-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].aeronave_en_servicio

 (@matricula VARCHAR(8), @fecha_salida datetime, @fecha_llegada_estimada DATETIME)

RETURNS smallint

AS
	
BEGIN

	return (select case 
						when @matricula not in (select distinct AERO_MATRI
												from ABSTRACCIONX4.FUERA_SERVICIO_AERONAVES fs
												where 
												([ABSTRACCIONX4].datetime_is_between(fs.FECHA_FS, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(fs.FECHA_REINICIO, @fecha_salida, @fecha_llegada_estimada) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_salida, fs.FECHA_FS, fs.FECHA_REINICIO) = 1) or
												([ABSTRACCIONX4].datetime_is_between(@fecha_llegada_estimada, fs.FECHA_FS, fs.FECHA_REINICIO) = 1)
												)
						and (
							(select a.AERO_FECHA_BAJA from ABSTRACCIONX4.AERONAVES a where a.AERO_MATRI = @matricula) is NULL or
							[ABSTRACCIONX4].fecha_menor(@fecha_llegada_estimada, (select a.AERO_FECHA_BAJA from ABSTRACCIONX4.AERONAVES a where a.AERO_MATRI = @matricula)) = 1
						)
					then 1
					else 0
					end)
END
GO

CREATE FUNCTION [ABSTRACCIONX4].aeronave_en_servicio_para_comprar
(@matricula VARCHAR(8), @fecha datetime)

RETURNS smallint

AS
	
BEGIN

	if(@matricula in (select distinct v.AERO_MATRI
						from ABSTRACCIONX4.viajes v
						where day(v.viaje_fecha_salida) = day(@fecha)
						and month(v.viaje_fecha_salida) = month(@fecha)
						and year(v.viaje_fecha_salida) = year(@fecha)
						and [ABSTRACCIONX4].aeronave_en_servicio(v.AERO_MATRI, v.viaje_fecha_salida, v.viaje_fecha_llegadae) = 1
						)
		)
		return 1

	return 0
			
END
GO

-------------------------------La aeronave sigue la ruta existente-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].sigue_la_ruta

 (@matricula VARCHAR(8), @ruta_id int, @fecha_salida datetime, @fecha_llegada_estimada DATETIME)

RETURNS smallint

AS
	
BEGIN
	declare @ciudad_actual smallint

	if((select count(*) from ABSTRACCIONX4.VIAJES v where v.aero_matri = @matricula) = 0)
		set @ciudad_actual = (select a.ciu_cod_origen from ABSTRACCIONX4.aeronaves a where a.aero_matri = @matricula)
	else
		set @ciudad_actual = (select top 1 r.CIU_COD_D
								from ABSTRACCIONX4.VIAJES v, ABSTRACCIONX4.RUTAS_AEREAS r
								where v.AERO_MATRI = @matricula
								and [ABSTRACCIONX4].fecha_menor(v.VIAJE_FECHA_LLEGADAE, @fecha_salida) = 1
								and v.RUTA_ID = r.RUTA_ID
								order by v.VIAJE_FECHA_LLEGADAE desc
								)
	
	if(@ciudad_actual = (select r.CIU_COD_O from ABSTRACCIONX4.RUTAS_AEREAS r where r.RUTA_ID = @ruta_id))
			return 1

	return 0
END
GO

CREATE FUNCTION [ABSTRACCIONX4].fecha_menor

 (@fecha1 datetime, @fecha2 DATETIME)

RETURNS smallint

AS
	
BEGIN
	if (datediff(minute, '1900-01-01 00:00:00.0000000', @fecha1) < datediff(minute, '1900-01-01 00:00:00.0000000', @fecha2))
		return 1
	
	return 0
END
GO

-------------------------------Pasajero disponibles para un vuelo-------------------------------
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
			where AERO_MATRI = @matricula) - (select coalesce(sum(e.ENCOMIENDA_PESO_KG),0)
												from ABSTRACCIONX4.ENCOMIENDAS e, ABSTRACCIONX4.VIAJES v
												where e.VIAJE_COD = v.VIAJE_COD and
												v.AERO_MATRI = @matricula and
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
			where b.AERO_MATRI = @matricula and
			b.BUT_ID not in(select b.BUT_ID
								from ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.BUTACAS b, ABSTRACCIONX4.VIAJES v
								where v.AERO_MATRI = @matricula and 
								p.VIAJE_COD = @viaje_cod and
								p.BUT_ID = b.BUT_ID and
								p.VIAJE_COD = v.VIAJE_COD and
								v.AERO_MATRI = b.AERO_MATRI and
								p.PASAJE_CANCELADO = 0)
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
				day(v.VIAJE_FECHA_SALIDA) = day(@fecha)	and
				[ABSTRACCIONX4].aeronave_en_servicio_para_comprar(v.aero_matri, @fecha) = 1						
			)
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
		DECLARE @codBut int		

		OPEN cursorPasajes
		FETCH NEXT FROM cursorPasajes INTO @cliCod,@curDni,@curNom,@curApe,@curDir,@curTel,@curMail,@curFechaNac,@viajeCod,@precio,@but,@matri,@clienteEncontrado,@clienteActualizado,@esComprador
		WHILE(@@FETCH_STATUS=0)
		BEGIN
			
			SET @codBut = (SELECT BUT_ID FROM ABSTRACCIONX4.BUTACAS WHERE BUT_NRO = @but AND AERO_MATRI = @matri)
			IF(@clienteEncontrado = 0) --hay que agregar al cliente
			BEGIN		
				
				IF(@esComprador=0)
				BEGIN	
					EXEC [ABSTRACCIONX4].ingresarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					SET @codigoCli = (SELECT CLI_COD FROM ABSTRACCIONX4.CLIENTES WHERE CLI_DNI = @curDni AND CLI_APELLIDO = @curApe)
					
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_ID) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@codBut)
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_ID) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@codBut)
				END

			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel

					
					INSERT INTO [ABSTRACCIONX4].PASAJES (COMP_PNR,CLI_COD, VIAJE_COD, PASAJE_PRECIO, BUT_ID) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@codBut)
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
				
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG) 
										VALUES(@codigoPNR,@codigoCli,@viajeCod,@precio,@peso) 
				END
				ELSE
				BEGIN
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG) 
										VALUES(@codigoPNR,@cod_cli,@viajeCod,@precio,@peso) 
				END
			END
			ELSE
			BEGIN
			IF(@clienteActualizado = 1) -- si existe y se modifico, hay que actualizarlo
					EXEC [ABSTRACCIONX4].actualizarDatosDelCliente @curDni,@curApe,@curNom,@curDir,@curMail,@curFechaNac,@curTel					
					
					INSERT INTO [ABSTRACCIONX4].ENCOMIENDAS (COMP_PNR,CLI_COD, VIAJE_COD, ENCOMIENDA_PRECIO, ENCOMIENDA_PESO_KG) 
										VALUES(@codigoPNR,@cliCod,@viajeCod,@precio,@peso) 
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
		INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USUA_COD,ROL_COD)
			VALUES (ABSTRACCIONX4.DarCodigoDeUsuario('Invitado'),ABSTRACCIONX4.DarCodigoDeRol(@Nombre))
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

-------------------------------Dar Codigo De Usuario-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].DarCodigoDeUsuario(@Username VARCHAR(30))
RETURNS TINYINT
AS
BEGIN
	DECLARE @Usua_Cod TINYINT
	SELECT @Usua_Cod = U.USUA_COD
	FROM ABSTRACCIONX4.USUARIOS U
	WHERE U.USERNAME = @Username
	IF(@Usua_cod is NULL)
	SET @Usua_cod = 0
	RETURN @Usua_cod
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

-------------------------------Actualizar Butacas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].AgregarButacas 
@Matricula VARCHAR(8), 
@CantidadPasillo SMALLINT, 
@CantidadVentanilla SMALLINT
AS
BEGIN
	DECLARE @i SMALLINT
	SET @i = 1
	DECLARE @CantidadButacas SMALLINT
	SET @CantidadButacas = @CantidadPasillo + @CantidadVentanilla
	WHILE (@i <= @CantidadPasillo)
	BEGIN
		INSERT INTO [ABSTRACCIONX4].BUTACAS (BUT_NRO , BUT_PISO , AERO_MATRI , BUT_TIPO)
		VALUES (@i , 1 , @Matricula , 'Pasillo')
		SET @i = @i + 1
	END

	WHILE (@i <= @CantidadButacas)
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

-------------------------------Fecha de reinicio o maxima-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].FechaReinicioOMaxima
	(@FechaReinicio DATETIME)
RETURNS DATETIME
AS
BEGIN
	IF @FechaReinicio IS NULL
	BEGIN
		DECLARE @FechaMaxima DATETIME
		SET @FechaMaxima = CONVERT(DATETIME,'31-12-2999')
		RETURN @FechaMaxima
	END 
	RETURN @FechaReinicio
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
			AND (ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,@Fecha1,@FechaMaxima) = 1 OR
				 ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_LLEGADAE,@Fecha1,@FechaMaxima) = 1)

	IF @Cantidad > 0
		RETURN 1
	RETURN 0
END

GO

-------------------------------Baja Aeronave-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].DejarAeronaveFueraDeServicio
	@Matricula VARCHAR(8),
	@FechaBaja DATETIME,
	@FechaReinicio DATETIME
AS
BEGIN
	DECLARE @Error varchar(120)

	IF [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(@Matricula,@FechaBaja,@FechaReinicio) > 0
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' ya se encuentra en fuera de servicio en esas fechas'
		RAISERROR(@Error, 16, 1)
		RETURN
	END

	IF [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,@FechaReinicio) = 1
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		RETURN
	END
	
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

	DECLARE @FechaMaxima DATETIME
	SET @FechaMaxima = [ABSTRACCIONX4].FechaReinicioOMaxima(NULL)

	IF [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(@Matricula,@FechaBaja,@FechaMaxima) > 0
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' ya se encuentra en fuera de servicio en esas fechas'
		RAISERROR(@Error, 16, 1)
		RETURN
	END

	IF [ABSTRACCIONX4].TieneViajeEntreFechas(@Matricula,@FechaBaja,NULL) = 1
	BEGIN
		SET @Error = 'La aeronave de matrícula ' + @Matricula + ' tiene viajes programados'
		RAISERROR(@Error, 16, 1)
		RETURN
	END

	UPDATE ABSTRACCIONX4.AERONAVES 
		SET AERO_FECHA_BAJA = @FechaBaja
		WHERE AERO_MATRI = @Matricula
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


-------------------------------Cancelar Pasajes y Encomiendas Aeronave-------------------------------
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
			FROM ABSTRACCIONX4.PASAJES P JOIN ABSTRACCIONX4.VIAJES V ON (P.VIAJE_COD = V.VIAJE_COD)
			WHERE AERO_MATRI = @Matricula AND 
				  PASAJE_COD IN (SELECT * FROM [ABSTRACCIONX4].PasajesEntreFechas(@FechaBaja,@FechaMaxima))

		UPDATE ABSTRACCIONX4.ENCOMIENDAS
			SET ENCOMIENDA_CANCELADO = 1
			FROM ABSTRACCIONX4.ENCOMIENDAS E JOIN ABSTRACCIONX4.VIAJES V ON (E.VIAJE_COD = V.VIAJE_COD)
			WHERE AERO_MATRI = @Matricula AND 
				  ENCOMIENDA_COD IN (SELECT * FROM [ABSTRACCIONX4].EncomiendasEntreFechas(@FechaBaja,@FechaMaxima))
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

-------------------------------Ciudad en la que se encuentra-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula VARCHAR(8),@FechaBaja DATETIME)
RETURNS VARCHAR(80)
AS
BEGIN
	DECLARE @FechaAlta DATETIME
	SELECT @FechaAlta = AERO_FECHA_ALTA FROM ABSTRACCIONX4.AERONAVES WHERE AERO_MATRI = @Matricula
	DECLARE @Ciudad VARCHAR(80)
	IF @FechaAlta IS NULL
	BEGIN
		SET @FechaAlta = CONVERT(DATETIME, '01/01/1900')
	END

	SELECT TOP 1 @Ciudad = c.CIU_DESC 
		FROM ABSTRACCIONX4.VIAJES v JOIN ABSTRACCIONX4.RUTAS_AEREAS r ON (v.RUTA_ID=r.RUTA_ID)
									JOIN ABSTRACCIONX4.CIUDADES c ON (r.CIU_COD_D=C.CIU_COD)
		WHERE v.AERO_MATRI = @Matricula AND 
			  ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_LLEGADAE,@FechaAlta,@FechaBaja) = 1
		ORDER BY v.VIAJE_FECHA_LLEGADAE DESC

	IF @Ciudad IS NULL
	BEGIN
		RETURN (SELECT C.CIU_DESC FROM ABSTRACCIONX4.AERONAVES A JOIN ABSTRACCIONX4.CIUDADES C ON (A.CIU_COD_ORIGEN = C.CIU_COD)
					WHERE AERO_MATRI = @Matricula)
	END

	RETURN @Ciudad
END
GO


-------------------------------Respeta origenes y destinos-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].RespetaOrigenesDestinos
	(@MatriculaNueva VARCHAR(8),@MatriculaVieja VARCHAR(8),@FechaBaja DATETIME,@FechaReinicio DATETIME)
RETURNS BIT
AS
BEGIN
	DECLARE @UnaTabla TABLE (orden INT IDENTITY, origen SMALLINT, destino SMALLINT)
	INSERT INTO @UnaTabla
	SELECT CIU_COD_O,CIU_COD_D
		FROM ABSTRACCIONX4.VIAJES V JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON (V.RUTA_ID = R.RUTA_ID)
		WHERE AERO_MATRI IN (@MatriculaNueva,@MatriculaVieja) AND
			  [ABSTRACCIONX4].datetime_is_between(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1
		ORDER BY VIAJE_FECHA_SALIDA

	IF (SELECT COUNT(*)
			FROM @UnaTabla T1 JOIN @UnaTabla T2 ON (T1.orden = (T2.orden - 1))
			WHERE T1.destino <> T2.origen) > 0
		RETURN 0
	RETURN 1
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
			  [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_ALTA,@FechaBaja,[ABSTRACCIONX4].FechaReinicioOMaxima(NULL)) = 0 AND
			  AERO_CANT_KGS >= @CantidadKG AND
			  (SELECT CASE AERO_FECHA_BAJA 
					  WHEN NULL THEN 0
					  ELSE [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_BAJA,AERO_FECHA_ALTA,@FechaReinicio)
					  END) = 0 AND
			  [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(AERO_MATRI,@FechaBaja) = [ABSTRACCIONX4].CiudadEnLaQueSeEncuentra(@Matricula,@FechaBaja) AND
			  [ABSTRACCIONX4].CantidadFuerasDeServicioEntre(AERO_MATRI,@FechaBaja,@FechaReinicio) = 0 AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Pasillo') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Pasillo') AND
			  [ABSTRACCIONX4].CantidadButacas(AERO_MATRI,'Ventanilla') >= [ABSTRACCIONX4].CantidadButacas(@Matricula,'Ventanilla') AND
			  [ABSTRACCIONX4].DisponibleParaTodosLosVuelosDe(AERO_MATRI,@Matricula,@FechaBaja,@FechaReinicio) = 1 AND
			  [ABSTRACCIONX4].RespetaOrigenesDestinos(AERO_MATRI,@Matricula,@FechaBaja,@FechaReinicio) = 1
			  
	RETURN @MatriculaNueva
END

GO


-------------------------------Reasignar butacas a pasajes-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ReasignarButacas
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8),
@FechaBaja DATETIME,
@FechaReinicio DATETIME
AS
BEGIN
	UPDATE ABSTRACCIONX4.PASAJES
	SET BUT_ID = (SELECT BUT_ID
					FROM ABSTRACCIONX4.BUTACAS B
					WHERE AERO_MATRI = @MatriculaNueva
					AND B.BUT_NRO = (SELECT BUT_NRO FROM ABSTRACCIONX4.BUTACAS B1
										WHERE B1.BUT_ID = P.BUT_ID))
	FROM ABSTRACCIONX4.PASAJES P
	WHERE VIAJE_COD IN 
	(SELECT VIAJE_COD 
		FROM ABSTRACCIONX4.VIAJES
		WHERE AERO_MATRI = @MatriculaVieja AND
			 ([ABSTRACCIONX4].ExisteViajeEntreFechas(VIAJE_FECHA_SALIDA,@FechaBaja,@FechaReinicio) = 1 OR
			  [ABSTRACCIONX4].ExisteViajeEntreFechas(VIAJE_FECHA_LLEGADAE,@FechaBaja,@FechaReinicio) = 1))

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
	
	EXECUTE [ABSTRACCIONX4].ReasignarButacas @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima
	EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @Matricula,@MatriculaNueva,@FechaBaja,@FechaMaxima

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


-------------------------------Modificar Períodos fuera de servicio-------------------------------
CREATE PROCEDURE  [ABSTRACCIONX4].ModificarPeriodosFS
@MatriculaVieja VARCHAR(8), 
@MatriculaNueva VARCHAR(8)
AS
BEGIN 
	UPDATE [ABSTRACCIONX4].FUERA_SERVICIO_AERONAVES
		SET AERO_MATRI = @MatriculaNueva
		WHERE AERO_MATRI = @MatriculaVieja
END
GO

/*
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
		
		DELETE FROM [ABSTRACCIONX4].BUTACAS WHERE AERO_MATRI = @MatriculaVieja
	END TRY
	BEGIN CATCH
	SET @Error = 'Error butaca no aceptada' + @MatriculaNueva 
				RAISERROR(@Error, 16, 1)
	END CATCH
END
GO*/

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
WHERE P.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta AND
	  ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,[ABSTRACCIONX4].obtenerFechaDeHoy(),[ABSTRACCIONX4].FechaReinicioOMaxima(NULL)) = 1)

GO


-------------------------------Borrar Encomiendas-------------------------------
CREATE PROCEDURE [ABSTRACCIONX4].BorrarEncomiendas
@IdRuta INT
AS
UPDATE ABSTRACCIONX4.ENCOMIENDAS
SET ENCOMIENDA_CANCELADO = 1
WHERE ENCOMIENDA_COD IN (SELECT E.ENCOMIENDA_COD FROM [ABSTRACCIONX4].ENCOMIENDAS E , [ABSTRACCIONX4].VIAJES V
WHERE E.VIAJE_COD = V.VIAJE_COD AND V.RUTA_ID = @IdRuta AND
	  ABSTRACCIONX4.datetime_is_between(VIAJE_FECHA_SALIDA,[ABSTRACCIONX4].obtenerFechaDeHoy(),[ABSTRACCIONX4].FechaReinicioOMaxima(NULL)) = 1) 

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
		DECLARE @CodigoServicio TINYINT
		DECLARE @ViajeAsignado BIT
		SELECT @ExisteMatricula = COUNT(*) FROM [ABSTRACCIONX4].AERONAVES WHERE AERO_MATRI = @Matricula AND AERO_MATRI <> @MatriculaActual
		SET @ViajeAsignado = [ABSTRACCIONX4].TieneViajeAsignado(@MatriculaActual)
		--EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual	
		
		IF(@ExisteMatricula = 0)
		BEGIN
			SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
			--si tiene viaje comprado solo modifico su nombre, no se puede otra cosa EN AERONAVES
			

			IF(@Matricula = @MatriculaActual)
			BEGIN
				IF( @ViajeAsignado = 0)
				BEGIN
					EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual
					EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
					UPDATE [ABSTRACCIONX4].AERONAVES 
						SET AERO_MOD = @Modelo, AERO_FAB = @Fabricante, SERV_COD = @CodigoServicio,
							AERO_CANT_KGS = @CantidadKG
						WHERE AERO_MATRI = @Matricula
				END
			END
			ELSE
			BEGIN
				INSERT INTO [ABSTRACCIONX4].AERONAVES 
						(AERO_MOD , AERO_MATRI , AERO_FAB , SERV_COD  , AERO_CANT_KGS) VALUES
						(@Modelo , @Matricula , @Fabricante , @CodigoServicio  , @CantidadKG)
				IF( @ViajeAsignado = 1)
				BEGIN
					EXECUTE [ABSTRACCIONX4].ModificarAeronaveButacas @MatriculaActual , @Matricula	
					EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @MatriculaActual , @Matricula , NULL , NULL
					EXECUTE [ABSTRACCIONX4].ModificarPeriodosFS @MatriculaActual , @Matricula
				END
				ELSE
				BEGIN
					EXECUTE [ABSTRACCIONX4].ModificarPeriodosFS @MatriculaActual , @Matricula
					EXECUTE [ABSTRACCIONX4].ModificarAeronaveViajes @MatriculaActual , @Matricula , NULL , NULL
					EXECUTE [ABSTRACCIONX4].BorrarButacas @MatriculaActual
					EXECUTE [ABSTRACCIONX4].AgregarButacas @Matricula , @CantidadPasillo , @CantidadVentanilla
				END
				DELETE FROM [ABSTRACCIONX4].AERONAVES
					WHERE AERO_MATRI = @MatriculaActual
					
			END
			
		END	
		ELSE
		BEGIN
			SET @Error = 'Ya existe una aeronave con matrícula ' + @Matricula
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
		DECLARE @CodigoCiudad SMALLINT
		SET @CodigoServicio = [ABSTRACCIONX4].ObtenerCodigoServicio(@TipoDeServicio)
		SET @CodigoCiudad = [ABSTRACCIONX4].ObtenerCodigoCiudad(@CiudadPrincipal)

		INSERT INTO ABSTRACCIONX4.AERONAVES 
			(AERO_MOD,AERO_MATRI,AERO_FAB,SERV_COD,AERO_CANT_KGS,AERO_FECHA_ALTA , CIU_COD_ORIGEN)
			VALUES (@Modelo,@Matricula,@Fabricante,@CodigoServicio,@CantidadKG,@FechaAlta,@CodigoCiudad)
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
	DECLARE @Error varchar(80)
	SET @Error = 'La aeronave no se encuentra disponible en el periodo ingresado'

	IF(ABSTRACCIONX4.aeronave_disponible(@matricula, @salida, @llegadaEstimada) = 0)
	BEGIN
		RAISERROR(@Error, 16, 1)
		RETURN
	END

	BEGIN TRY
		INSERT INTO ABSTRACCIONX4.VIAJES 
			(VIAJE_FECHA_SALIDA, VIAJE_FECHA_LLEGADA, VIAJE_FECHA_LLEGADAE, AERO_MATRI, RUTA_ID)
			VALUES (@salida, null, @llegadaEstimada, @matricula, @ruta)
	END TRY
	BEGIN CATCH
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
		INSERT INTO ABSTRACCIONX4.ROLES_USUARIOS (USUA_COD,ROL_COD)
			SELECT (SELECT USUA_COD FROM ABSTRACCIONX4.USUARIOS WHERE USERNAME = @Usuario),ROL_COD
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

--------------debeHaberLlegadoAntes-----------------

CREATE FUNCTION [ABSTRACCIONX4].debeHaberLlegadoAntes(@fechaSalida datetime, @fechaLlegada datetime, @aero_matri varchar(8))
RETURNS BIT
AS
BEGIN
	DECLARE @salidaSigVuelo datetime
	SELECT TOP 1 @salidaSigVuelo = VIAJE_FECHA_SALIDA FROM ABSTRACCIONX4.VIAJES WHERE AERO_MATRI = @aero_matri AND VIAJE_FECHA_SALIDA > @fechaSalida ORDER BY VIAJE_FECHA_SALIDA
	
	IF @fechaLlegada < @salidaSigVuelo
	BEGIN
		RETURN 1
	END

	RETURN 0
END

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
				CO.COMP_FECHA as "Fecha de Compra",E.ENCOMIENDA_PRECIO as Precio
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
				CO.COMP_FECHA as "Fecha de Compra",P.PASAJE_PRECIO as Precio
			FROM ABSTRACCIONX4.CLIENTES C
			JOIN ABSTRACCIONX4.PASAJES P ON C.CLI_COD=P.CLI_COD
			JOIN ABSTRACCIONX4.VIAJES V ON P.VIAJE_COD = V.VIAJE_COD 
			JOIN ABSTRACCIONX4.RUTAS_AEREAS R ON R.RUTA_ID = V.RUTA_ID
			JOIN ABSTRACCIONX4.COMPRAS CO ON CO.COMP_PNR = P.COMP_PNR
		WHERE C.CLI_DNI = @dni AND C.CLI_APELLIDO = @ape AND VIAJE_FECHA_LLEGADA is NOT NULL AND VIAJE_FECHA_LLEGADA BETWEEN 
			[ABSTRACCIONX4].obtenerFechaDeHoy() - 365 AND [ABSTRACCIONX4].obtenerFechaDeHoy() AND PASAJE_CANCELADO = 0

		)
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
	SET @Esta  = 0
	ELSE
	SET @Esta  = 1
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
	RETURN 0
	RETURN 1
END
GO


--------------------------------Llenar Encomiendas--------------------------------

CREATE FUNCTION [ABSTRACCIONX4].LlenarEncomiendas(@Codigo VARCHAR(12))
RETURNS TABLE 
AS

	RETURN(SELECT	[ENCOMIENDA_COD] AS 'Código',
					[COMP_PNR] AS 'Código Compra',
					[CLI_COD]  AS 'Cliente',
					E.[VIAJE_COD] AS 'Viaje' ,
					[ENCOMIENDA_PRECIO] AS 'Precio' ,
					(SELECT COMP_FECHA FROM ABSTRACCIONX4.COMPRAS WHERE COMP_PNR = @Codigo) AS 'Fecha Compra',
					[ENCOMIENDA_PESO_KG] AS 'Peso' ,
					V.[AERO_MATRI] AS 'Aeronave'  
			FROM ABSTRACCIONX4.ENCOMIENDAS E , ABSTRACCIONX4.VIAJES V
			WHERE E.VIAJE_COD = V.VIAJE_COD AND COMP_PNR = @Codigo AND
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
					(SELECT COMP_FECHA FROM ABSTRACCIONX4.COMPRAS WHERE COMP_PNR = @Codigo) AS 'Fecha Compra',
					B.[BUT_NRO] AS 'Butaca',
					B.[AERO_MATRI] AS 'Aeronave' 
			FROM ABSTRACCIONX4.PASAJES P , ABSTRACCIONX4.BUTACAS B
			WHERE B.BUT_ID = P.BUT_ID AND COMP_PNR = @Codigo AND
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
		[ABSTRACCIONX4].EstaEnViajePasaje(PASAJE_COD) = 0 AND
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
			from (select ciu.ciu_desc Descripcion, com.COMP_FECHA Fecha, count(p.pasaje_cod) Cantidad
					from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades ciu, ABSTRACCIONX4.COMPRAS com
					where com.COMP_PNR = p.COMP_PNR  and
					p.viaje_cod = v.viaje_cod and
					v.ruta_id = r.ruta_id and
					r.ciu_cod_d = ciu.ciu_cod and
					p.pasaje_cancelado = 0
					group by ciu.ciu_desc, com.COMP_FECHA) t
			where year(t.Fecha) = @anio and month(t.Fecha) between 1 and 6
			group by t.Descripcion
			order by coalesce(sum(t.cantidad),0) desc
else
	insert @variable_tabla 
			select top 5 t.Descripcion
			from (select ciu.ciu_desc Descripcion, com.COMP_FECHA Fecha, count(p.pasaje_cod) Cantidad
					from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades ciu, ABSTRACCIONX4.COMPRAS com
					where com.COMP_PNR = p.COMP_PNR  and
					p.viaje_cod = v.viaje_cod and
					v.ruta_id = r.ruta_id and
					r.ciu_cod_d = ciu.ciu_cod and
					p.pasaje_cancelado = 0
					group by ciu.ciu_desc, com.COMP_FECHA) t
			where year(t.Fecha) = @anio and month(t.Fecha) between 7 and 12
			group by t.Descripcion
			order by coalesce(sum(t.cantidad),0) desc
		
return;
end
GO

-------------------------------Estadistica destino con aeronaves mas vacias-------------------------------
CREATE FUNCTION [ABSTRACCIONX4].cantidadButacasAeronave(@matricula varchar(8))

RETURNS smallint
AS
begin
	return (select count(*) from ABSTRACCIONX4.BUTACAS b where b.AERO_MATRI = @matricula)
end
GO

CREATE FUNCTION [ABSTRACCIONX4].destinosConAeronaveMasVacia(@semestre tinyint, @anio smallint)

RETURNS @variable_tabla TABLE (Descripcion varchar(80), Cantidad smallint)

AS
begin
if(@semestre = 1)
	insert @variable_tabla 
		select top 5 t.Descripcion, coalesce(sum(t.Cantidad),0) Cantidad
		from (select c.ciu_desc Descripcion, ([ABSTRACCIONX4].cantidadButacasAeronave(a.AERO_MATRI) - v.CANT_BUT_OCUPADAS) Cantidad
				from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c, ABSTRACCIONX4.AERONAVES a
				where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6
				and v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				a.AERO_MATRI = v.AERO_MATRI
						) t
		group by t.Descripcion
		order by coalesce(sum(t.Cantidad),0) desc
else
	insert @variable_tabla 
		select top 5 t.Descripcion, coalesce(sum(t.Cantidad),0) Cantidad
		from (select c.ciu_desc Descripcion, ([ABSTRACCIONX4].cantidadButacasAeronave(a.AERO_MATRI) - v.CANT_BUT_OCUPADAS) Cantidad
				from abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades c, ABSTRACCIONX4.AERONAVES a
				where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 7 and 12
				and v.ruta_id = r.ruta_id and
				r.ciu_cod_d = c.ciu_cod and
				a.AERO_MATRI = v.AERO_MATRI) t
		group by t.Descripcion
		order by coalesce(sum(t.Cantidad),0) desc
		
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
			(select coalesce(sum(CAST(Precio/10 as Int)),0) from [ABSTRACCIONX4].obtenerHistorialMillasPasajes(c.cli_dni, c.cli_apellido)) MillasPasajes,
			(select coalesce(sum(CAST(Precio/10 as Int)),0) from [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(c.cli_dni, c.cli_apellido)) MillasEncomiendas
		from ABSTRACCIONX4.CLIENTES c, ABSTRACCIONX4.PASAJES p, ABSTRACCIONX4.VIAJES v
		where year(v.viaje_fecha_salida) = @anio and month(v.viaje_fecha_salida) between 1 and 6 and
		v.VIAJE_COD = p.viaje_cod and p.cli_cod = c.cli_cod) t
		order by (t.MillasEncomiendas + t.MillasPasajes) desc
else
	insert @variable_tabla 
		select top 5 t.nombre, t.apellido, (t.MillasEncomiendas + t.MillasPasajes) Millas
		from
		(select distinct c.cli_nombre nombre, c.cli_apellido apellido, 
			(select coalesce(sum(CAST(Precio/10 as Int)),0) from [ABSTRACCIONX4].obtenerHistorialMillasPasajes(c.cli_dni, c.cli_apellido)) MillasPasajes,
			(select coalesce(sum(CAST(Precio/10 as Int)),0) from [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(c.cli_dni, c.cli_apellido)) MillasEncomiendas
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
		from (select ciu.ciu_desc Descripcion, com.COMP_FECHA Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades ciu, ABSTRACCIONX4.COMPRAS com
				where com.COMP_PNR = p.COMP_PNR and
				p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = ciu.ciu_cod and
				p.pasaje_cancelado = 1
				group by ciu.ciu_desc, com.COMP_FECHA) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 1 and 6
		group by t.Descripcion
		order by coalesce(sum(t.cantidad),0) desc
else
	insert @variable_tabla 
		select top 5 t.Descripcion
		from (select ciu.ciu_desc Descripcion, com.COMP_FECHA Fecha, count(p.pasaje_cod) Cantidad
				from abstraccionx4.pasajes p, abstraccionx4.viajes v, abstraccionx4.rutas_aereas r, abstraccionx4.ciudades ciu, ABSTRACCIONX4.COMPRAS com
				where com.COMP_PNR = p.COMP_PNR and
				p.viaje_cod = v.viaje_cod and
				v.ruta_id = r.ruta_id and
				r.ciu_cod_d = ciu.ciu_cod and
				p.pasaje_cancelado = 1
				group by ciu.ciu_desc, com.COMP_FECHA) t
		where year(t.Fecha) = @anio and month(t.Fecha) between 7 and 12
		group by t.Descripcion
		order by coalesce(sum(t.cantidad),0) desc

return;
end
GO


-------------------------------Estadistica aeronaves con mayor cantidad de dias fuera de servicio-----------------
CREATE FUNCTION [ABSTRACCIONX4].cantidadDiasFueraDeServicio(@matricula varchar(8))

RETURNS smallint

AS

begin
	declare @fechas table (fuera_servicio datetime, fecha_reinicio datetime)

	insert into @fechas select fs.FECHA_FS fuera_servicio, fs.FECHA_REINICIO fecha_reinicio
						from ABSTRACCIONX4.FUERA_SERVICIO_AERONAVES fs 
						where fs.AERO_MATRI = @matricula

	if((select count(*) from @fechas) <> 0)
		begin
		return (select coalesce(sum(t2.cantidad_dias),0)
				from (select datediff(day, t.fecha_reinicio, t.fuera_servicio) cantidad_dias
						from @fechas t) t2)
		end

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
		from [ABSTRACCIONX4].aeronaves a, ABSTRACCIONX4.FUERA_SERVICIO_AERONAVES fs
		where year(fs.FECHA_FS) = @anio and month(fs.FECHA_FS) between 1 and 6
		and a.AERO_MATRI = fs.AERO_MATRI
		order by a.aero_matri desc
else
	insert @variable_tabla	
		select top 5 a.aero_matri, [ABSTRACCIONX4].cantidadDiasFueraDeServicio(a.aero_matri) CantidadDias
		from [ABSTRACCIONX4].aeronaves a, ABSTRACCIONX4.FUERA_SERVICIO_AERONAVES fs
		where year(fs.FECHA_FS) = @anio and month(fs.FECHA_FS) between 7 and 12
		and a.AERO_MATRI = fs.AERO_MATRI
		order by a.aero_matri desc

return;
end	
GO
