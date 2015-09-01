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

/****** Object:  Table [dbo].[IC_Categorias_Desgravacion_Tramos]    Script Date: 05/18/2015 22:06:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IC_Categorias_Desgravacion_Tramos](
	[id_tramo] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[id_categoria] [int] NOT NULL,
	[id_tipo_periodo] [int] NULL,
	[cantidad_cortes] [int] NULL,
	[desgrava_tramo_anterior] [numeric](28, 8) NULL,
	[desgrava_tramo_final] [numeric](28, 8) NULL,
	[factor_desgrava] [numeric](28, 8) NULL,
	[cortes_ejecutados] [int] NULL,
	[activo] [char](10) NULL,
 CONSTRAINT [PK_IC_Categorias_Desgravacion_Tramos] PRIMARY KEY CLUSTERED 
(
	[id_tramo] ASC,
	[id_instrumento] ASC,
	[id_categoria] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos]  WITH CHECK ADD  CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Categorias_Desgravacion1] FOREIGN KEY([id_categoria], [id_instrumento])
REFERENCES [dbo].[IC_Categorias_Desgravacion] ([id_categoria], [id_instrumento])
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos] CHECK CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Categorias_Desgravacion1]
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos]  WITH CHECK ADD  CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Tipo_Periodo_Corte1] FOREIGN KEY([id_tipo_periodo])
REFERENCES [dbo].[IC_Tipo_Periodo_Corte] ([id_tipo_periodo])
GO

ALTER TABLE [dbo].[IC_Categorias_Desgravacion_Tramos] CHECK CONSTRAINT [FK_IC_Categorias_Desgravacion_Tramos_IC_Tipo_Periodo_Corte1]
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

/****** Object:  Table [dbo].[SAC_Versiones_Bitacora]    Script Date: 05/24/2015 18:58:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Versiones_Bitacora](
	[id_version] [int] NOT NULL,
	[anio_version] [int] NOT NULL,
	[enmienda] [varchar](200) NULL,
	[anio_inicia_enmienda] [int] NULL,
	[anio_fin_enmieda] [int] NULL,
	[observaciones] [varchar](500) NULL,
	[fecha_generada] [datetime] NULL,
	[fecha_inicia_vigencia] [date] NULL,
	[fecha_fin_vigencia] [date] NULL,
	[usuario_reviso] [varchar](200) NULL,
	[fecha_revisasa] [date] NULL,
	[usuario_aprueba] [varchar](200) NULL,
	[fecha_aprueba] [date] NULL,
	[estado] [varchar](10) NULL,
 CONSTRAINT [PK_SAC_VERSIONES_BITACORA] PRIMARY KEY CLUSTERED 
(
	[id_version] ASC,
	[anio_version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SAC_Seccion]    Script Date: 05/24/2015 19:04:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Seccion](
	[Seccion] [varchar](5) NOT NULL,
	[descripcion_seccion] [varchar](500) NOT NULL,
 CONSTRAINT [PK_SAC_SECCION] PRIMARY KEY CLUSTERED 
(
	[Seccion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SAC_Capitulos]    Script Date: 05/24/2015 19:08:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Capitulos](
	[Seccion] [varchar](5) NULL,
	[Capitulo] [varchar](3) NOT NULL,
	[descripcion_capitulo] [varchar](500) NULL,
	[activo] [varchar](2) NULL,
 CONSTRAINT [PK_SAC_Capitulos] PRIMARY KEY CLUSTERED 
(
	[Capitulo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SAC_Partidas]    Script Date: 05/24/2015 19:14:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Partidas](
	[Capitulo] [varchar](3) NOT NULL,
	[Partida] [varchar](3) NOT NULL,
	[Descripcion_Partida] [varchar](500) NULL,
	[activo] [varchar](2) NULL,
	[fecha_fin_vigencia] [date] NULL,
	[usuario_modifica] [varchar](500) NULL,
 CONSTRAINT [PK_SAC_Partidas] PRIMARY KEY CLUSTERED 
(
	[Capitulo] ASC,
	[Partida] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAC_Partidas]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Partidas_SAC_Capitulos1] FOREIGN KEY([Capitulo])
REFERENCES [dbo].[SAC_Capitulos] ([Capitulo])
GO

ALTER TABLE [dbo].[SAC_Partidas] CHECK CONSTRAINT [FK_SAC_Partidas_SAC_Capitulos1]
GO

/****** Object:  Table [dbo].[SAC_Subpartidas]    Script Date: 05/24/2015 19:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Subpartidas](
	[capitulo] [varchar](3) NOT NULL,
	[partida] [varchar](3) NOT NULL,
	[subpartida] [nchar](10) NOT NULL,
	[texto_subpartida] [nchar](1000) NULL,
	[activo] [varchar](2) NULL,
	[fecha_inicio_vigencia] [date] NULL,
	[fecha_fin_vigencia] [date] NULL,
	[usuario_modifica] [varchar](500) NULL,
 CONSTRAINT [PK_SAC_Subpartidas] PRIMARY KEY CLUSTERED 
(
	[capitulo] ASC,
	[partida] ASC,
	[subpartida] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAC_Subpartidas]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Subpartidas_SAC_Partidas1] FOREIGN KEY([capitulo], [partida])
REFERENCES [dbo].[SAC_Partidas] ([Capitulo], [Partida])
GO

ALTER TABLE [dbo].[SAC_Subpartidas] CHECK CONSTRAINT [FK_SAC_Subpartidas_SAC_Partidas1]
GO


/****** Object:  Table [dbo].[SAC_Incisos]    Script Date: 05/24/2015 19:27:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Incisos](
	[id_version] [int] NOT NULL,
	[anio_version] [int] NOT NULL,
	[codigo_inciso] [varchar](12) NOT NULL,
	[texto_inciso] [varchar](1000) NULL,
	[dai_base] [decimal](28, 8) NOT NULL,
	[estado] [varchar](10) NULL,
 CONSTRAINT [PK_SAC_Incisos] PRIMARY KEY CLUSTERED 
(
	[id_version] ASC,
	[anio_version] ASC,
	[codigo_inciso] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAC_Incisos]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Incisos_SAC_Versiones_Bitacora] FOREIGN KEY([id_version], [anio_version])
REFERENCES [dbo].[SAC_Versiones_Bitacora] ([id_version], [anio_version])
GO

ALTER TABLE [dbo].[SAC_Incisos] CHECK CONSTRAINT [FK_SAC_Incisos_SAC_Versiones_Bitacora]
GO

/****** Object:  Table [dbo].[SAC_Asocia_Categoria]    Script Date: 05/24/2015 19:56:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Asocia_Categoria](
	[id_instrumento] [int] NOT NULL,
	[id_categoria] [int] NOT NULL,
	[id_version] [int] NOT NULL,
	[anio_version] [int] NOT NULL,
	[codigo_inciso] [varchar](12) NOT NULL,
	[inciso_presicion] [varchar](15) NULL,
	[texto_precision] [varchar](200) NULL,
	[observaciones] [varchar](300) NULL,
	[salvaguardia] [varchar](10) NULL,
	[contingente] [varchar](10) NULL,
	[usuario_reviso] [varchar](200) NULL,
	[fecha_aprueba] [date] NULL,
	[fecha_inicia_vigencia] [date] NULL,
	[fecha_fin_vigencia] [date] NULL,
	[estado] [varchar](10) NULL,
 CONSTRAINT [PK_SAC_Asocia_Categoria] PRIMARY KEY CLUSTERED 
(
	[id_instrumento] ASC,
	[id_categoria] ASC,
	[id_version] ASC,
	[anio_version] ASC,
	[codigo_inciso] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAC_Asocia_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Asocia_Categoria_IC_Categorias_Desgravacion] FOREIGN KEY([id_categoria], [id_instrumento])
REFERENCES [dbo].[IC_Categorias_Desgravacion] ([id_categoria], [id_instrumento])
GO

ALTER TABLE [dbo].[SAC_Asocia_Categoria] CHECK CONSTRAINT [FK_SAC_Asocia_Categoria_IC_Categorias_Desgravacion]
GO

ALTER TABLE [dbo].[SAC_Asocia_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Asocia_Categoria_SAC_Incisos] FOREIGN KEY([id_version], [anio_version], [codigo_inciso])
REFERENCES [dbo].[SAC_Incisos] ([id_version], [anio_version], [codigo_inciso])
GO

ALTER TABLE [dbo].[SAC_Asocia_Categoria] CHECK CONSTRAINT [FK_SAC_Asocia_Categoria_SAC_Incisos]
GO


/****** Object:  Table [dbo].[SAC_Tratados_Bitacora]    Script Date: 06/07/2015 21:55:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Tratados_Bitacora](
	[id_version] [int] NOT NULL,
	[id_corte_version] [int] NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[anio_version] [int] NULL,
	[incisos_procesados] [int] NULL,
	[cantidad_categoria] [int] NULL,
	[fecha_generada] [date] NULL,
	[fecha_inicia_vigencia] [date] NULL,
	[usuario_reviso] [varchar](200) NULL,
	[fecha_revisada] [date] NULL,
	[usuario_aprueba] [varchar](200) NULL,
	[fecha_aprueba] [date] NULL,
	[estado] [varchar](10) NULL,
	[usuario] [varchar](200) NULL,
	[fecha_modifica] [date] NULL,
 CONSTRAINT [PK_SAC_Tratados_Bitacora] PRIMARY KEY CLUSTERED 
(
	[id_version] ASC,
	[id_corte_version] ASC,
	[id_instrumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAC_Tratados_Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_SAC_Tratados_Bitacora_IC_Instrumentos1] FOREIGN KEY([id_instrumento])
REFERENCES [dbo].[IC_Instrumentos] ([id_instrumento])
GO

ALTER TABLE [dbo].[SAC_Tratados_Bitacora] CHECK CONSTRAINT [FK_SAC_Tratados_Bitacora_IC_Instrumentos1]
GO

/****** Object:  Table [dbo].[SAC_Dai_Instrumento]    Script Date: 06/30/2015 18:31:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Dai_Instrumento](
	[id_dai_instrumento] [int] IDENTITY(1,1) NOT NULL,
	[id_instrumento] [int] NOT NULL,
	[inciso] [varchar](15) NOT NULL,
	[categoria] [varchar](500) NOT NULL,
	[factor_desgrava] [decimal](10, 4) NULL,
	[desgrava_tramos_antes] [decimal](10, 2) NULL,
	[nuevo_dai] [decimal](8, 2) NULL,
	[dai_calc_relativo] [decimal](8, 2) NULL,
	[dai_calc_absoluto] [decimal](8, 2) NULL,
	[dai_base] [decimal](8, 2) NULL,
	[sigla1_instrumento] [varchar](50) NULL,
	[version_sac_calculo] [int] NULL,
	[id_corte_nuevo] [int] NULL,
	[inciso_anterior] [varchar](15) NULL,
	[usuario_genero] [varchar](200) NULL,
	[fecha_generada] [date] NULL,
	[usuario_reviso] [varchar](200) NULL,
	[fecha_revisada] [date] NULL,
	[usuario_aprueba] [varchar](200) NULL,
	[fecha_aprueba] [date] NULL,
	[fecha_inicia_vigencia] [date] NULL,
	[fecha_fin_vigencia] [date] NULL,
	[observaciones] [varchar](500) NULL,
	[dai_rela_vs_abso] [decimal](8, 2) NULL,
	[estado] [varchar](10) NULL,
 CONSTRAINT [PK_SAC_Dai_Instrumento] PRIMARY KEY CLUSTERED 
(
	[id_dai_instrumento] ASC,
	[id_instrumento] ASC,
	[inciso] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SAC_Correlacion]    Script Date: 08/24/2015 18:48:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SAC_Correlacion](
	[inciso_origen] [varchar](20) NULL,
	[inciso_nuevo] [varchar](20) NULL,
	[texto_inciso] [varchar](1000) NULL,
	[comentarios] [varchar](500) NULL,
	[normativa] [varchar](500) NULL,
	[dai_base] [numeric](6, 2) NULL,
	[dai_nuevo] [numeric](6, 2) NULL,
	[anio_version] [smallint] NULL,
	[anio_nueva_version] [smallint] NULL,
	[version] [smallint] NULL,
	[fin_vigencia] [smallint] NULL,
	[inicio_vigencia] [smallint] NULL,
	[estado] [nchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
