CREATE TABLE IC_tipo_instrumento (
	[id_tipo_instrumento] [int] NOT NULL,
	[descripcion] [varchar] (100) NOT NULL,
	[observaciones] varchar (300) NULL,
	CONSTRAINT [PK_IC_tipo_instrumento_id_tipo_instrumento] PRIMARY KEY
)

CREATE TABLE IC_tipo_relacion_instrumento(
	id_tipo_relacion_instrumento int NOT NULL, 
	descripción varchar (100) NOT NULL,
	observaciones varchar (300) NULL,
	CONSTRAINT [PK_relacion_istrumento_id_relacion_instrumento] PRIMARY KEY
)

CREATE TABLE IC_instrumentos (
	id_instrumento int NOT NULL,
	id_tipo_instrumento int NOT NULL,
	id_tipo_relacion_instrumento int NOT NULL,
	nombre_instrumento varchar (150) NOT NULL,
	sigla varchar (100) NOT NULL,
	sigla_alt varchar (100) NOT NULL,
	observaciones varchar (500) NOT NULL,
	fecha_firma DateTime NOT NULL,
	fecha_ratifica DateTime NOT NULL,
	fecha_vigencia DateTime NOT NULL,
	
	
)


CREATE TABLE IC_tipo_desgravacion(
	[id_tipo_desgrava] [int] NOT NULL,
	[descripcion] [varchar] (100) NOT NULL,
	[observaciones] [varchar] (300) NOT NULL,

)
CREATE TABLE IC_Categorias_Desgravacion(
	[id_categoria] int NOT NULL,
	[id_instrumento] int NOT NULL,
	[id_tipo_desgrava] [int] NOT NULL,
	[codigo_categoria] varchar(10) NOT NULL,
	[cantidad_tramos] int NOT NULL,
	[observaciones] [varchar] (300) NULL,
	
)	

CREATE TABLE IC_Categorias_Desgravacion_Tramos(
	[id_tramo] int NOT NULL,
	[id_categoria] int NOT NULL,
	[id_instrumento] int NOT NULL,
	[cantidad_cortes] int NULL,
	[periodo_corte] CHAR NULL,
	[desgrava_tramo_anterior] numeric(28,8) NULL,
	[desgrava_final_tramo] NUMERIC(28,8) NULL,
	[factor_desgrava] NUMERIC(28,8) NULL,
	[cortes_ejecutados] INT NULL,
	[activo] CHAR NULL,

)

CREATE TABLE G_PAISES(
	[ID_PAIS] [int] IDENTITY(1,1) NOT NULL,
	[CODIGO_ALFA] [varchar](2) NOT NULL,
	[NOMBRE_PAIS] [varchar](200) NULL,
	[CODIGO_NUM] [int] NULL,
	[CODIGO_ALFA3] [varchar](3) NULL,
	[FECHA_INICIO_VIG] [date] NULL,
	[FECHA_FIN_VIGE] [date] NULL,
	[ESTADO] [varchar](10) NULL,
	[USUARIO] [varchar](200) NULL,
	[FECHA_MODIFICA] [date] NULL,

)
id_pais, codigo_alfa 

/**/
CREATE TABLE IC_instrumento_pais(
	[corr_instrumento_pais] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[id_pais] [int] NOT NULL,
	[fecha_firma] [datetime] NOT NULL,
	[fecha_ratifica] [datetime] NOT NULL,
	[fecha_vigencia] [datetime] NOT NULL,
)


CREATE TABLE [dbo].[SAC_PAISES_TLC](
	[ID_ROW] [int] IDENTITY(1,1) NOT NULL,
	[ID_TRATADO] [int] NOT NULL,
	[CODIGO_ALFA] [varchar](2) NOT NULL,
	[SIGLA1] [varchar](50) NOT NULL,
	[NOMBRE_PAIS] [varchar](200) NULL,
	[CODIGO_NUM_PAIS] [int] NULL,
	[ID_TIPO_INTEGRANTE] [int],
	[CODIGO_BLOQUE] [int] NULL,
	[SIGLAS_BLOQUE] [varchar](10) NULL,
	[FECHA_INICIO_VIG] [date] NULL,
	[FECHA_FIN_VIGE] [date] NULL,
	[ESTADO] [varchar](10) NULL,
	[USUARIO] [varchar](200) NULL,
	[FECHA_MODIFICA] [date] NULL,
PRIMARY KEY CLUSTERED 
(	[ID_ROW] ASC,
	[ID_TRATADO] ASC,
	[CODIGO_ALFA] ASC

CREATE TABLE IC_Bloques_Pais (
    [ID_BLOQUE] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DESCRIPCION] [varchar](100) NOT NULL,
	[SIGLA] [varchar](50) NULL,
	[OBSERVACIONES] [varchar](500) NULL,
	[USUARIO] [varchar](200) NULL,
	[FECHA_MODIFICA] [date] NULL,
)