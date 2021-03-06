USE [SGSACDB]
GO
/****** Object:  Table [dbo].[IC_Tipo_Socio]    Script Date: 06/30/2015 23:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IC_Tipo_Socio](
	[id_tipo_socio] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[sigla] [varchar](50) NULL,
	[OBSERVACIONES] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipo_socio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IC_Instrumento_Paises]    Script Date: 06/30/2015 23:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IC_Instrumento_Paises](
	[ID_INSTRUMENTO] [int] NOT NULL,
	[ID_PAIS] [int] NOT NULL,
	[ID_TIPO_SOCIO] [int] NOT NULL,
	[CODIGO_BLOQUE_PAIS] [int] NULL,
	[OBSERVACIONES] [varchar](500) NULL,
	[FECHA_FIRMA] [date] NULL,
	[FECHA_RATIFICACION] [date] NULL,
	[FECHA_VIGENCIA] [date] NULL,
	[ESTADO] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_INSTRUMENTO] ASC,
	[ID_PAIS] ASC,
	[ID_TIPO_SOCIO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IC_Bloque_Pais]    Script Date: 06/30/2015 23:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IC_Bloque_Pais](
	[ID_BLOQUE_PAIS] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [varchar](100) NOT NULL,
	[SIGLA] [varchar](50) NULL,
	[OBSERVACIONES] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_BLOQUE_PAIS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G_Paises]    Script Date: 06/30/2015 23:06:58 ******/
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
/****** Object:  Default [DF_IC_Instrumento_Paises_ESTADO]    Script Date: 06/30/2015 23:06:58 ******/
ALTER TABLE [dbo].[IC_Instrumento_Paises] ADD  CONSTRAINT [DF_IC_Instrumento_Paises_ESTADO]  DEFAULT ((1)) FOR [ESTADO]
GO
