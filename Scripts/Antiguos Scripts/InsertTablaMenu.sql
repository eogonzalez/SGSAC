USE [SGSACDB]
GO
/****** Object:  Table [dbo].[g_menu_opcion]    Script Date: 04/07/2015 22:39:09 ******/
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (1, N'Administración y Seguridad', N'Administración', N'/frmAdministracion.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (2, N'Tratados y Acuerdos', N'Tratados', N'/frmTratadosyAcuerdos.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (3, N'Enmiendas', N'Enmiendas', N'/frmEnmiendas.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (4, N'Consultas y Reportes', N'Consultas', N'/frmConsultasReportes.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (5, N'Transmisión', N'Transmisión', N'/frmTransmision.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (6, N'Ayuda', N'Ayuda', N'/frmAyuda.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url]) VALUES (7, N'Salir', N'Salir', N'/Login.aspx')
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (8, N'Gestion Instrumento', N'Gestion Instrumento', N'/frmTratadosyAcuerdos.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (9, N'Tipo Instrumento', N'Tipo Instrumento', N'/frmTipoInstrumento.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (10, N'Tipo Relación Instrumento', N'Tipo Relación Instrumento', N'/frmTipoRelacionInstrumento.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (11, N'Tipo de Desgravación', N'Tipo de Desgravación', N'/frmTipoDesgravacion.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (12, N'Tipo de Periodo de Corte', N'Tipo de Periodo de Corte', N'/frmTipoPeriodo.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (13, N'Consuta SAC', N'Consuta SAC', N'/frmConsultaSAC.aspx', 4)
