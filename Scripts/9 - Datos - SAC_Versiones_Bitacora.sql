USE [SGSACDB]
GO
INSERT [dbo].[SAC_Versiones_Bitacora] ([id_version], [anio_version], [enmienda], [anio_inicia_enmienda], [anio_fin_enmieda], [observaciones], [fecha_generada], [fecha_inicia_vigencia], [fecha_fin_vigencia], [usuario_reviso], [fecha_revisasa], [usuario_aprueba], [fecha_aprueba], [estado]) VALUES (3, 2002, N'Tercera', 2002, 2006, N'', NULL, CAST(N'2002-01-01' AS Date), CAST(N'2006-12-31' AS Date), NULL, NULL, NULL, NULL, N'A')
