Delete 
FROM g_menu_opcion
where id_opcion >=8;

INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (8, N'Gestion Instrumentos', N'Gestion Instrumentos', N'/frmTratadosyAcuerdos.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (9, N'Tipo Instrumento', N'Tipo Instrumento', N'/frmTipoInstrumento.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (10, N'Tipo Relación Instrumento', N'Tipo Relación Instrumento', N'/frmTipoRelacionInstrumento.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (11, N'Tipo de Desgravación', N'Tipo de Desgravación', N'/frmTipoDesgravacion.aspx', 2)
INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (12, N'Tipo de Periodo de Corte', N'Tipo de Periodo de Corte', N'/frmTipoPeriodo.aspx', 2)

Update g_menu_opcion
set url='/Inicio.aspx'
where id_opcion = 2;

Update g_menu_opcion
set url='/Inicio.aspx'
where id_opcion = 4;

INSERT [dbo].[g_menu_opcion] ([id_opcion], [nombre], [descripcion], [url], [id_padre] ) VALUES (13, N'Consuta SAC', N'Consulta SAC', N'/frmConsultaSAC.aspx', 4)


