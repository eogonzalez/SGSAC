USE [SGSACDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_SEL_MENU]    Script Date: 04/21/2015 20:54:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: 07/04/2015
-- Description:	Mostrar el listado de opciones del menú principal
-- =============================================
ALTER PROCEDURE [dbo].[SP_SEL_MENU] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT id_opcion
		,nombre
		,descripcion
		,url
		,id_padre
    FROM dbo.g_menu_opcion
    
END
