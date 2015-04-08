SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: 07/04/2015
-- Description:	Mostrar el listado de opciones del menú principal
-- =============================================
CREATE PROCEDURE dbo.SP_SEL_MENU 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT id_opcion
		,nombre
		,descripcion
		,url
    FROM dbo.g_menu_opcion
    
END
GO
