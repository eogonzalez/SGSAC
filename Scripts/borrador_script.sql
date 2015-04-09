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

/*----*/
CREATE TABLE IC_instrumento_pais(
	[corr_instrumento_pais] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[id_pais] [int] NOT NULL,
	[fecha_firma] [datetime] NOT NULL,
	[fecha_ratifica] [datetime] NOT NULL,
	[fecha_vigencia] [datetime] NOT NULL,
)

CREATE TABLE IC_tipo_desgravacion(
	[id_tipo_desgrava] [int] NOT NULL,
	[descripcion] [varchar] (100) NOT NULL,
	[observaciones] [varchar] (300) NOT NULL,

)
CREATE TABLE IC_Categorias_Desgrava(
	[id_tipo_categoria] int NOT NULL,
	[id_tipo_desgrava] [int] NOT NULL,
	[codigo_categoria] varchar(10) NOT NULL,
	[cantidad_tramos] int NOT NULL,
	[observaciones] [varchar] (300) NULL,
	
)

CREATE TABLE IC_Categoria_Tramo(
	id_tramo
	id_tipo_categoria
)

CREATE TABLE IC_Instrumento_Categoria(

)