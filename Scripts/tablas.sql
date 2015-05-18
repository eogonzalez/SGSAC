USE [SGSACDB]
GO

/****** Object:  Table [dbo].[g_menu_opcion]    Script Date: 04/05/2015 14:33:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[g_menu_opcion](
	[id_opcion] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[descripcion] [varchar](300) NOT NULL,
	[url] [varchar](150) NOT NULL,
 CONSTRAINT [pk_g_menu_opcion_id_opcion] PRIMARY KEY CLUSTERED 
(
	[id_opcion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[g_usuarios]    Script Date: 04/05/2015 14:35:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[g_usuarios](
	[id_usuario] [int] NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[estado] [nchar](1) NOT NULL,
 CONSTRAINT [pk_g_usuarios_id_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[g_usuarios] ADD  CONSTRAINT [DF_g_usuarios_estado]  DEFAULT ((1)) FOR [estado]
GO

/****** Object:  Table [dbo].[g_usuarios_seguridad]    Script Date: 04/05/2015 19:06:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[g_usuarios_seguridad](
	[corr_usuarios_ingreso] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[fecha_ultimo_acceso] [datetime] NOT NULL,
	[direccion_ip] [varchar](100) NOT NULL,
 CONSTRAINT [PK_g_usuarios_seguridad] PRIMARY KEY CLUSTERED 
(
	[corr_usuarios_ingreso] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[g_usuarios_seguridad]  WITH CHECK ADD  CONSTRAINT [FK_g_usuarios_seguridad_g_usuarios1] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[g_usuarios] ([id_usuario])
GO

ALTER TABLE [dbo].[g_usuarios_seguridad] CHECK CONSTRAINT [FK_g_usuarios_seguridad_g_usuarios1]
GO

/****** Object:  Table [dbo].[IC_Tipo_Instrumento]    Script Date: 04/08/2015 23:51:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Tipo_Instrumento](
	[id_tipo_instrumento] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[observaciones] [varchar](300) NOT NULL,
 CONSTRAINT [PK_IC_Tipo_Instrumento] PRIMARY KEY CLUSTERED 
(
	[id_tipo_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IC_Tipo_Relacion_Instrumento]    Script Date: 04/08/2015 23:53:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Tipo_Relacion_Instrumento](
	[id_tipo_relacion_instrumento] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[observaciones] [varchar](300) NOT NULL,
 CONSTRAINT [PK_IC_Tipo_Relacion_Instrumento] PRIMARY KEY CLUSTERED 
(
	[id_tipo_relacion_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IC_Instrumentos]    Script Date: 04/26/2015 21:59:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Instrumentos](
	[id_instrumento] [int] NOT NULL,
	[id_tipo_instrumento] [int] NOT NULL,
	[id_tipo_relacion_instrumento] [int] NOT NULL,
	[nombre_instrumento] [varchar](150) NOT NULL,
	[sigla] [varchar](100) NOT NULL,
	[sigla_alternativa] [varchar](100) NOT NULL,
	[observaciones] [varchar](500) NOT NULL,
	[fecha_firma] [datetime] NOT NULL,
	[fecha_ratificada] [datetime] NOT NULL,
	[fecha_vigencia] [datetime] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_IC_Instrumentos] PRIMARY KEY CLUSTERED 
(
	[id_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UX_IC_Instrumentos] UNIQUE NONCLUSTERED 
(
	[id_instrumento] ASC,
	[sigla] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IC_Instrumentos]  WITH CHECK ADD  CONSTRAINT [FK_IC_Instrumentos_IC_Tipo_Instrumento] FOREIGN KEY([id_tipo_instrumento])
REFERENCES [dbo].[IC_Tipo_Instrumento] ([id_tipo_instrumento])
GO

ALTER TABLE [dbo].[IC_Instrumentos] CHECK CONSTRAINT [FK_IC_Instrumentos_IC_Tipo_Instrumento]
GO

ALTER TABLE [dbo].[IC_Instrumentos]  WITH CHECK ADD  CONSTRAINT [FK_IC_Instrumentos_IC_Tipo_Relacion_Instrumento] FOREIGN KEY([id_tipo_relacion_instrumento])
REFERENCES [dbo].[IC_Tipo_Relacion_Instrumento] ([id_tipo_relacion_instrumento])
GO

ALTER TABLE [dbo].[IC_Instrumentos] CHECK CONSTRAINT [FK_IC_Instrumentos_IC_Tipo_Relacion_Instrumento]
GO

ALTER TABLE [dbo].[IC_Instrumentos] ADD  CONSTRAINT [DF_IC_Instrumentos_estado]  DEFAULT ((1)) FOR [estado]
GO



/****** Object:  Table [dbo].[IC_Tipo_Desgravacion]    Script Date: 04/26/2015 21:01:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Tipo_Desgravacion](
	[id_tipo_desgrava] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[observaciones] [varchar](300) NOT NULL,
 CONSTRAINT [PK_IC_Tipo_Desgravacion] PRIMARY KEY CLUSTERED 
(
	[id_tipo_desgrava] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IC_Categorias_Desgravacion]    Script Date: 04/26/2015 21:03:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Categorias_Desgravacion](
	[id_categoria] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[id_tipo_desgrava] [int] NOT NULL,
	[codigo_categoria] [varchar](10) NOT NULL,
	[cantidad_tramos] [int] NOT NULL,
	[observaciones] [varchar](300) NOT NULL,
 CONSTRAINT [PK_IC_Categorias_Desgravacion] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC,
	[id_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion]  WITH CHECK ADD  CONSTRAINT [FK_IC_Categorias_Desgravacion_IC_Instrumentos] FOREIGN KEY([id_instrumento])
REFERENCES [dbo].[IC_Instrumentos] ([id_instrumento])
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion] CHECK CONSTRAINT [FK_IC_Categorias_Desgravacion_IC_Instrumentos]
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion]  WITH CHECK ADD  CONSTRAINT [FK_IC_Categorias_Desgravacion_IC_Tipo_Desgravacion] FOREIGN KEY([id_tipo_desgrava])
REFERENCES [dbo].[IC_Tipo_Desgravacion] ([id_tipo_desgrava])
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion] CHECK CONSTRAINT [FK_IC_Categorias_Desgravacion_IC_Tipo_Desgravacion]
GO

/****** Object:  Table [dbo].[IC_Categorias_Desgravacion_Tramos]    Script Date: 04/26/2015 21:14:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Categorias_Desgravacion_Tramos](
	[id_tramo] [int] NOT NULL,
	[id_categoria] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[cantidad_cortes] [int] NULL,
	[periodo_corte] [char](10) NULL,
	[desgrava_tramo_anterior] [numeric](28, 8) NULL,
	[desgrava_final_tramo] [numeric](28, 8) NULL,
	[factor_desgrava] [numeric](28, 8) NULL,
	[cortes_ejecutados] [int] NULL,
	[activo] [char](10) NULL,
 CONSTRAINT [PK_IC_Categorias_Desgravacion_Tramos] PRIMARY KEY CLUSTERED 
(
	[id_tramo] ASC,
	[id_categoria] ASC,
	[id_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos]  WITH CHECK ADD  CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Categorias_Desgravacion] FOREIGN KEY([id_categoria], [id_instrumento])
REFERENCES [dbo].[IC_Categorias_Desgravacion] ([id_categoria], [id_instrumento])
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos] CHECK CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Categorias_Desgravacion]
GO


/****** Object:  Table [dbo].[G_Paises]    Script Date: 04/26/2015 21:31:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[G_Paises](
	[id_pais] [int] NOT NULL,
	[codigo_alfa] [varchar](2) NOT NULL,
	[nombre_pais] [varchar](250) NOT NULL,
	[codigo_num] [int] NOT NULL,
	[codigo_alfa3] [varchar](3) NULL,
	[fecha_inicio_vig] [datetime] NULL,
	[fecha_fin_vig] [datetime] NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_G_Paises] PRIMARY KEY CLUSTERED 
(
	[id_pais] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UX_G_Paises] UNIQUE NONCLUSTERED 
(
	[id_pais] ASC,
	[codigo_alfa] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IC_Tipo_Periodo_Corte]    Script Date: 05/17/2015 20:59:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Tipo_Periodo_Corte](
	[id_tipo_periodo] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[observaciones] [varchar](300) NOT NULL,
 CONSTRAINT [PK_IC_Tipo_Periodo_Corte] PRIMARY KEY CLUSTERED 
(
	[id_tipo_periodo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

