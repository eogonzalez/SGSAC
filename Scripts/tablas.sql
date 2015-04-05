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
