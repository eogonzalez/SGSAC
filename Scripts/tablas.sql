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

/****** Object:  Table [dbo].[IC_Instrumentos]    Script Date: 04/08/2015 23:58:22 ******/
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
 CONSTRAINT [PK_IC_Instrumentos] PRIMARY KEY CLUSTERED 
(
	[id_instrumento] ASC
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

