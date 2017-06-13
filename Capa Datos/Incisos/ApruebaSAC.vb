Imports System.Data.SqlClient
Public Class ApruebaSAC
    Dim objConeccion As New ConectarService

    'Funcion que verifica si es posible realizar el proceso de aprobacion
    Public Function VerificaApruebaSAC(ByVal anioProcesa As Integer) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String

        Try
            sql_query = " select " +
                " count(1) " +
                " FROM " +
                " SAC_Versiones_Bitacora " +
                " where " +
                " anio_version = @anioProcesa AND " +
                " anio_inicia_enmienda = @anioProcesa AND " +
                " estado is null "

            Using conn = objConeccion.Conectar
                Dim command = New SqlCommand(sql_query, conn)
                command.Parameters.AddWithValue("anioProcesa", anioProcesa)

                conn.Open()

                If command.ExecuteScalar() > 0 Then
                    estado = True
                Else
                    estado = False
                End If

            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try

        Return estado
    End Function

    'Funcion que aprueba y genera la siguiente version del SAC
    Public Function ApruebaSAC(ByVal id_versionAct As Integer, ByVal anio_version As Integer, ByVal anio_inicia_enmienda As Integer, ByVal anio_final_enmienda As Integer, ByVal anio_version_new As Integer) As Boolean
        Dim estado As Boolean = True
        Dim sql_query As String
        Try
            '      Se Verifica previamente que no este "APROBADA" la Correlacion
            '       @AnioProcesa es pasado como parametro de la Versión del SAC por Aprobar

            'a) De SAC_Versiones_Bitacora obtener Anio_version y Anio_inicia_enmienda activa "ESTADO= 'A' 

            Dim AnioVERCorAct As Integer = anio_version
            Dim AnioIniciaEnmienda As Integer = anio_inicia_enmienda
            Dim AnioFinEnmienda As Integer = anio_final_enmienda

            'aa) De SAC_Versiones_Bitacora obtener Anio_version DE NUEVA CORRELACION "ESTADO= NULL 
            Dim id_verCorNew As Integer
            Dim AnioVerCorNew As Integer = anio_version_new

            sql_query = " SELECT  id_version " +
                " FROM SAC_Versiones_Bitacora " +
                " WHERE estado Is NULL "

            Using con1 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con1)
                con1.Open()
                id_verCorNew = command.ExecuteScalar()
                con1.Close()
            End Using

            'b) VERIFICA Y ELIMINA TODOS LOS REGISTROS DE LA TABLA: TEMP_INCISOS
            'Validar no aprobar 2 veces o sin terminar

            Dim CanReg As Integer = 0

            sql_query = "DELETE TEM_Incisos"
            Using con2 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con2)
                con2.Open()
                command.ExecuteNonQuery()
                con2.Close()
            End Using

            'c) COPIA de SAC_INCISOS los INCISOS con ESTADO = 'A'  a LA TABLA: TEMP_INCISOS 

            sql_query = " INSERT INTO TEM_Incisos (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, Estado ) " +
                " SELECT ID_Version, anio_version, Codigo_inciso, texto_inciso, dai_base, estado " +
                " FROM SAC_Incisos " +
                " WHERE Estado = 'A'  "

            Using con3 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con3)

                con3.Open()
                command.ExecuteNonQuery()
                con3.Close()

            End Using

            'd) Elimina de la tabla: TEMP_INCISOS, los incisos que en SAC_CORRELACION tienen Inciso_nuevo IS NULL
            ' -- Selecciona los incisos vigentes que tiene supresion y apertura (modificacion)
            ' -- Queda pendiente clasificar aparte las supreciones y aparte las modificaciones

            sql_query = "  SELECT distinct Inciso_origen " +
                " FROM SAC_CORRELACION " +
                " WHERE Inciso_nuevo Is NULL " +
                " AND version = @id_versionAct " +
                " AND Anio_Nueva_version =  @AnioVerCorNew " +
                " AND estado Is NULL  " +
                " and fin_vigencia = @AnioVerCorNew  "

            Dim dataSetTem_Incisos As New DataSet

            Using con4 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con4)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                Dim dataAdapter As New SqlDataAdapter(command)
                con4.Open()
                dataAdapter.Fill(dataSetTem_Incisos)
                con4.Close()

            End Using

            CanReg = 0
            For Each row As DataRow In dataSetTem_Incisos.Tables(0).Rows
                Dim IncisoOrigen As String = row("inciso_origen").ToString

                sql_query = " DELETE TEM_Incisos " +
                    " WHERE  Codigo_inciso = @IncisoOrigen; "

                Using con5 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con5)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    con5.Open()
                    command.ExecuteNonQuery()
                    con5.Close()
                End Using


                CanReg = CanReg + 1

            Next


            '   e) Inserta en tabla: TEMP_INCISOS todos los INCISOS  de correlacion con 
            '   "Inciso_Nuevo" IS NOT NULL en SAC_CORRELACION
            ' -- Agrega unicamente los vigentes en esta version

            sql_query = " SELECT version,anio_nueva_version,Inciso_nuevo, texto_inciso, dai_nuevo " +
                " FROM SAC_CORRELACION " +
                " WHERE Inciso_nuevo Is Not NULL " +
                " AND estado Is NULL " +
                " AND version = @id_versionAct " +
                " AND Anio_Nueva_version =  @AnioVerCorNew " +
                " and inicio_vigencia = @AnioVerCorNew " +
                " ORDER BY Inciso_nuevo "

            Dim dataSetINCI_NEW As New DataSet
            Using con6 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con6)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                Dim dataAdapter As New SqlDataAdapter(command)

                con6.Open()
                dataAdapter.Fill(dataSetINCI_NEW)
                con6.Close()

            End Using

            Dim inciso As String = "00"

            For Each row As DataRow In dataSetINCI_NEW.Tables(0).Rows
                Dim Id_Version As Integer = row("version")
                Dim AnioNuevaVer As Integer = row("anio_nueva_version")
                Dim IncisoNuevo As String = row("Inciso_nuevo").ToString
                Dim Texto_Inciso As String = row("texto_inciso").ToString
                Dim DAI As Double = row("dai_nuevo")

                If (inciso <> IncisoNuevo) Then

                    sql_query = "  INSERT INTO TEM_Incisos " +
                        " (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base ) " +
                        " VALUES " +
                        " (@ID_Version, @AnioNuevaVer, @IncisoNuevo, @Texto_Inciso, @DAI); "

                    Using con7 = objConeccion.Conectar
                        Dim command As New SqlCommand(sql_query, con7)
                        command.Parameters.AddWithValue("id_version", Id_Version)
                        command.Parameters.AddWithValue("AnioNuevaVer", AnioNuevaVer)
                        command.Parameters.AddWithValue("IncisoNuevo", IncisoNuevo)
                        command.Parameters.AddWithValue("Texto_Inciso", Texto_Inciso)
                        command.Parameters.AddWithValue("DAI", DAI)
                        con7.Open()
                        command.ExecuteNonQuery()
                        con7.Close()

                    End Using

                    inciso = IncisoNuevo


                End If

            Next

            'Elimina Incisos Repetidos en TEM_INCISOS

            sql_query = " SELECT CODIGO_INCISO, COUNT(1) AS TT FROM TEM_Incisos " +
                " GROUP BY CODIGO_INCISO " +
                " ORDER BY TT  DESC "

            Dim dataSetCOD As New DataSet
            Using con8 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con8)
                Dim dataAdapter As New SqlDataAdapter(command)
                con8.Open()
                dataAdapter.Fill(dataSetCOD)
                con8.Close()


            End Using

            Dim codi As String
            Dim repe As Integer = 0
            Dim i As Integer = 0

            For Each row As DataRow In dataSetCOD.Tables(0).Rows
                codi = row("CODIGO_INCISO")
                repe = row("TT")

                If repe > 1 Then

                    sql_query = " DELETE TEM_Incisos " +
                        " WHERE  CODIGO_INCISO = @Codi " +
                        " AND ESTADO ='A' "

                    Using con9 = objConeccion.Conectar
                        Dim command As New SqlCommand(sql_query, con9)
                        command.Parameters.AddWithValue("Codi", codi)
                        con9.Open()
                        command.ExecuteNonQuery()
                        con9.Close()

                    End Using

                    i = i + 1

                End If

            Next

            ' G) Verifica en cada Instrumentode Impacto generado por la Correlación e Ingresa los
            ' registros en tabla CORRELACION_INSTRUMENTOS con "INCISO_ORIGEN" de la tabla SAC_CORRELACION 

            ' f1)Identifica incisos Aperturados que afecten a un Instrumento
            ' NOTA Cambio Query ya que no aparecian todas las aperturas
            ' -- Seleccionando unicamente los incisos aplicados en la version actual

            sql_query = " select SAC_Corre.inciso_origen " +
                " from " +
                " (SELECT case when (Inciso_origen is null) then inciso_nuevo else inciso_origen end inciso_origen " +
                " FROM SAC_CORRELACION " +
                " WHERE estado Is NULL " +
                " AND version = @id_versionAct " +
                " AND Anio_Nueva_version =  @AnioVerCorNew " +
                " and inicio_vigencia = @AnioVerCorNew) SAC_Corre " +
                " GROUP by SAC_Corre.inciso_origen " +
                " ORDER BY SAC_Corre.Inciso_origen "

            Dim dataSet_IncisoCorre As New DataSet
            Using con10 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con10)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                Dim dataAdapter As New SqlDataAdapter(command)

                con10.Open()
                dataAdapter.Fill(dataSet_IncisoCorre)
                con10.Close()

            End Using

            '/**	BUSCA AFECTACION EN INSTRUMENTOS  **/
            For Each row As DataRow In dataSet_IncisoCorre.Tables(0).Rows

                '/**	CONTEO DE APERTURAS DE CADA INCISO CORRELACION **/
                CanReg = 0
                Dim situacion As String = Nothing
                Dim IncisoOrigen As String = row("Inciso_origen")


                sql_query = " SELECT count(1) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN =@IncisoOrigen " +
                    " or inciso_nuevo = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND version = @id_versionAct " +
                    " AND Anio_Nueva_version =  @AnioVerCorNew " +
                    " AND(INCISO_NUEVO Is Not NULL Or DATALENGTH(INCISO_NUEVO) > 0) "

                Using con11 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con11)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                    command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                    con11.Open()
                    CanReg = command.ExecuteScalar()
                    con11.Close()

                End Using

                If CanReg > 0 Then
                    situacion = " -APERTURADO- "
                End If

                '/**	IDETIFICA SI EL INCISO SE SUPRIME O NO EN LA CORRELACION **/
                Dim suprime As Integer = 0

                sql_query = " (SELECT count(1) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND version = @id_versionAct " +
                    " AND Anio_Nueva_version =  @AnioVerCorNew " +
                    " AND (INCISO_NUEVO IS NULL OR DATALENGTH(INCISO_NUEVO)= 0)) "

                Using con12 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con12)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                    command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                    con12.Open()
                    suprime = command.ExecuteScalar()
                    con12.Close()

                End Using

                If suprime > 0 Then
                    situacion = situacion + " -SUPRIMIDO- "
                End If

                '/**	OBTIENE DATOS DEL INCISO ORIGINAL  **/
                sql_query = " SELECT texto_inciso,dai_base " +
                    " FROM SAC_Incisos " +
                    " WHERE Estado = 'A' " +
                    " AND CODIGO_INCISO = @IncisoOrigen "

                Dim texto_inciso As String
                Dim DAI_Ori As Double
                Using con13 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con13)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)

                    con13.Open()
                    Dim dataReader As SqlDataReader = command.ExecuteReader()


                    If dataReader.HasRows Then
                        dataReader.Read()

                        texto_inciso = dataReader.GetString(0)
                        DAI_Ori = dataReader.GetDecimal(1)

                    Else
                        'Este bloque ya no se cumple su funcion original 
                        ' ya que cuando son nuevas partidas siempre entrara a este bloque

                        'Return estado = False
                        'estado = False
                    End If

                    con13.Close()

                End Using

                '/**	OBTIENE DATOS DE LA CORRELACION  **/

                sql_query = " (SELECT coalesce(max(dai_nuevo),0) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND version = @id_versionAct " +
                    " AND Anio_Nueva_version =  @AnioVerCorNew " +
                    " AND estado Is NULL " +
                    " AND (INCISO_NUEVO IS NOT NULL OR DATALENGTH(INCISO_NUEVO)> 0)) "

                Dim dai_max As Integer
                Using con14 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con14)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                    command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                    con14.Open()
                    dai_max = command.ExecuteScalar()
                    con14.Close()

                End Using

                sql_query = " (SELECT coalesce(min(dai_nuevo),0) " +
                    " FROM SAC_Correlacion " +
                    " WHERE INCISO_ORIGEN = @IncisoOrigen " +
                    " AND estado Is NULL " +
                    " AND version = @id_versionAct " +
                    " AND Anio_Nueva_version =  @AnioVerCorNew " +
                    " AND (INCISO_NUEVO IS NOT NULL OR DATALENGTH(INCISO_NUEVO)> 0)) "

                Dim dai_min As Integer
                Using con15 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con15)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                    command.Parameters.AddWithValue("id_versionAct", id_versionAct)
                    con15.Open()
                    dai_min = command.ExecuteScalar()
                    con15.Close()

                End Using


                '/**	OBTIENE DATOS DE LOS TRATADOS  E INSERTA DATOS EN TABLA DE AFECTA TRATADO   **/

                sql_query = " SELECT CODIGO_INCISO,id_instrumento,id_categoria " +
                    " FROM SAC_Asocia_Categoria " +
                    " WHERE estado = 'A' " +
                    " AND  CODIGO_INCISO =  @IncisoOrigen " +
                    " GROUP BY CODIGO_INCISO,id_instrumento,id_categoria; "

                Dim Codigo_Inciso_Inst As String
                Dim Id_instrumento As Integer
                Dim Id_Categoria As Integer
                Dim dTAsociaCat As New DataTable

                Using con16 = objConeccion.Conectar
                    Dim command As New SqlCommand(sql_query, con16)
                    command.Parameters.AddWithValue("IncisoOrigen", IncisoOrigen)
                    Dim dataAdapter As New SqlDataAdapter(command)
                    con16.Open()
                    dataAdapter.Fill(dTAsociaCat)
                    con16.Close()

                End Using

                If dTAsociaCat.Rows.Count > 0 Then
                    'Si el inciso afecta tratado

                    For Each rowAsociaCat As DataRow In dTAsociaCat.Rows
                        Codigo_Inciso_Inst = rowAsociaCat("codigo_inciso").ToString
                        Id_instrumento = rowAsociaCat("id_instrumento").ToString
                        Id_Categoria = rowAsociaCat("id_categoria").ToString

                        If Codigo_Inciso_Inst.Length > 4 Then
                            Dim nombre_instrumento As String
                            Dim codigo_categoria As String

                            sql_query = " SELECT Nombre_Instrumento FROM IC_INSTRUMENTOS " +
                                " WHERE id_instrumento = @ID_Instrumento "

                            Using con16 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con16)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)

                                con16.Open()
                                nombre_instrumento = command.ExecuteScalar()
                                con16.Close()

                            End Using


                            sql_query = " SELECT  Codigo_Categoria FROM IC_Categorias_Desgravacion  " +
                                " WHERE id_instrumento = @ID_Instrumento " +
                                " AND ID_Categoria   = @ID_Categoria "

                            Using con17 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con17)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)
                                command.Parameters.AddWithValue("ID_Categoria", Id_Categoria)

                                con17.Open()
                                codigo_categoria = command.ExecuteScalar()
                                con17.Close()

                            End Using

                            sql_query = " INSERT INTO Correlacion_Instrumentos " +
                                " (inciso_original,situacion, id_instrumento, " +
                                " texto_original, dai_original, " +
                                " anio_anterior_cor, canti_aperturas, " +
                                " nombre_instrumento,id_categoria,codigo_categoria, " +
                                " ver_nueva_cor, anio_nueva_cor, " +
                                " dai_max_nuevo,dai_min_nuevo, " +
                                " estado,accion_propuesta, " +
                                " fecha_generada,usuario_generada )   " +
                                " VALUES (@Codigo_Inciso_Inst,@Situacion,@ID_Instrumento, " +
                                " @Texto_Inciso, @DAI_Ori," +
                                " @AnioVerCorAct,@CanReg, " +
                                " @Nombre_Instrumento,@ID_Categoria,@Codigo_Categoria, " +
                                " @ID_VerCorNew, @AnioVerCorNew, " +
                                " @DAI_Max,@DAI_Min, " +
                                " 'PENDIENTE', 'Revisar lista de nuevos incisos aperturados y definir si requiere de asociar la respectiva categoría de desgravación para el tratado o acuerdo', " +
                                " SYSDATETIME(),'USER') "

                            Using con18 = objConeccion.Conectar
                                Dim command As New SqlCommand(sql_query, con18)
                                command.Parameters.AddWithValue("Codigo_Inciso_Inst", Codigo_Inciso_Inst)
                                command.Parameters.AddWithValue("Situacion", situacion)
                                command.Parameters.AddWithValue("ID_Instrumento", Id_instrumento)
                                command.Parameters.AddWithValue("Texto_Inciso", texto_inciso)
                                command.Parameters.AddWithValue("DAI_Ori", DAI_Ori)
                                command.Parameters.AddWithValue("AnioVerCorAct", AnioVERCorAct)
                                command.Parameters.AddWithValue("CanReg", CanReg)
                                command.Parameters.AddWithValue("Nombre_Instrumento", nombre_instrumento)
                                command.Parameters.AddWithValue("ID_Categoria", Id_Categoria)
                                command.Parameters.AddWithValue("Codigo_Categoria", codigo_categoria)
                                command.Parameters.AddWithValue("ID_VerCorNew", id_verCorNew)
                                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                                command.Parameters.AddWithValue("DAI_Max", dai_max)
                                command.Parameters.AddWithValue("DAI_Min", dai_min)

                                con18.Open()
                                command.ExecuteNonQuery()
                                con18.Close()

                            End Using

                        End If

                    Next
                Else
                    'Si el inciso no afecta a ningun tratado
                End If

                situacion = Nothing
                'IncisoCorre =  IncisoOrigen
            Next


            ' D1)  ACTUALIZA en SAC_CORRELACION ESTADO INDICANDO SUPRESION
            ' -- selecciona unicamente las supresiones vigentes

            sql_query = " UPDATE SAC_CORRELACION " +
                " SET ESTADO ='S' " +
                " WHERE Inciso_nuevo Is NULL " +
                " AND DATALENGTH(inciso_ORIGEN)>2 " +
                " AND ESTADO IS NULL " +
                " AND Anio_nueva_version = @AnioVerCorNew  " +
                " AND VERSION = @id_versionAct " +
                " and fin_vigencia = @AnioVerCorNew "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)

                con.Open()
                command.ExecuteNonQuery()
                con.Close()

            End Using


            'D2) ACTUALIZA en SAC_CORRELACION ESTADO INDICANDO NUEVO inciso
            '-- Actualiza incisos vigentes
            sql_query = " 	UPDATE SAC_CORRELACION " +
                " SET ESTADO ='N' " +
                " WHERE Inciso_origen Is NULL " +
                " AND DATALENGTH(inciso_nuevo)>2 " +
                " AND ESTADO IS NULL " +
                " AND Anio_nueva_version = @AnioVerCorNew  " +
                " and inicio_vigencia = @AnioVerCorNew " +
                " AND VERSION = @id_versionAct "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)

                con.Open()
                command.ExecuteNonQuery()
                con.Close()

            End Using

            'D3) ACTUALIZA en SAC_CORRELACION ESTADO INDICANDO  APERTURA
            '-- Actualiza incisos vigentes
            sql_query = " 	UPDATE SAC_CORRELACION " +
                " SET ESTADO ='A' " +
                " WHERE DATALENGTH(inciso_ORIGEN) > 2 " +
                " AND DATALENGTH(inciso_nuevo)>2 " +
                " AND Inciso_origen <> inciso_nuevo " +
                " AND ESTADO IS NULL " +
                " AND Anio_nueva_version = @AnioVerCorNew " +
                " and inicio_vigencia = @AnioVerCorNew " +
                " AND VERSION = @id_versionAct "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)

                con.Open()
                command.ExecuteNonQuery()
                con.Close()

            End Using

            'D4) ACTUALIZA en SAC_CORRELACION ESTADO INDICANDO  DAI MODIFICADO
            '-- Actualiza incisos vigentes
            sql_query = " 	UPDATE SAC_CORRELACION " +
                " SET ESTADO ='D' " +
                " WHERE DATALENGTH(inciso_ORIGEN) > 2 " +
                " AND DATALENGTH(inciso_nuevo)>2 " +
                " AND Inciso_origen = inciso_nuevo " +
                " AND DAI_BASE <> DAI_NUEVO " +
                " AND ESTADO IS NULL " +
                " AND Anio_nueva_version = @AnioVerCorNew " +
                " and inicio_vigencia = @AnioVerCorNew " +
                " AND VERSION = @id_versionAct "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)

                con.Open()
                command.ExecuteNonQuery()
                con.Close()

            End Using

            'D5) ACTUALIZA en SAC_CORRELACION ESTADO INDICANDO  TEXTO MODIFICADO
            '-- Actualiza incisos vigentes
            sql_query = " 	UPDATE SAC_CORRELACION " +
                " SET ESTADO ='T' " +
                " WHERE DATALENGTH(inciso_ORIGEN) > 2 " +
                " AND DATALENGTH(inciso_nuevo)>2 " +
                " AND Inciso_origen = inciso_nuevo " +
                " AND DAI_BASE = DAI_NUEVO " +
                " AND ESTADO IS NULL " +
                " AND Anio_nueva_version = @AnioVerCorNew " +
                " and inicio_vigencia = @AnioVerCorNew " +
                " AND VERSION = @id_versionAct "

            Using con = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)
                command.Parameters.AddWithValue("id_versionAct", id_versionAct)

                con.Open()
                command.ExecuteNonQuery()
                con.Close()

            End Using

            'h) En SAC_INCISOS cambia "el estado" de los registros CON ESTADO = 'I' al ESTADO='H'(historico)**/

            sql_query = " UPDATE SAC_INCISOS SET ESTADO = 'H' " +
                " WHERE  ESTADO = 'I' "

            Using con19 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con19)

                con19.Open()
                command.ExecuteNonQuery()
                con19.Close()

            End Using


            '/** I) En SAC_INCISOS cambia "el estado" de los registros CON ESTADO = 'A' al ESTADO='I'(Inactivo)**/
            sql_query = " UPDATE SAC_INCISOS SET ESTADO = 'I' " +
                " WHERE  ESTADO = 'A' "

            Using con20 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con20)

                con20.Open()
                command.ExecuteNonQuery()
                con20.Close()

            End Using

            '/** J) SE INSERTAN EN SAC_INCISOS los registros de nueva Version y cambia ESTADO = 'A' (Activo) **/


            sql_query = " UPDATE TEM_INCISOS " +
                " SET ID_VERSION = @ID_VerCorNew, anio_version = @AnioVerCorNew; "

            Using con21 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con21)
                command.Parameters.AddWithValue("ID_VerCorNew", id_verCorNew)
                command.Parameters.AddWithValue("AnioVerCorNew", AnioVerCorNew)

                con21.Open()
                command.ExecuteNonQuery()
                con21.Close()

            End Using

            sql_query = " INSERT INTO SAC_Incisos (ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, Estado ) " +
                " SELECT ID_Version, Anio_Version,Codigo_inciso,texto_inciso, dai_base, 'A'  " +
                " FROM TEM_Incisos "

            Using con22 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con22)

                con22.Open()
                command.ExecuteNonQuery()
                con22.Close()

            End Using

            sql_query = " UPDATE SAC_Versiones_Bitacora SET ESTADO = 'I' " +
                " WHERE  ESTADO = 'A'  "

            Using con23 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con23)

                con23.Open()
                command.ExecuteNonQuery()
                con23.Close()

            End Using

            sql_query = " UPDATE SAC_Versiones_Bitacora SET ESTADO = 'A' " +
                " WHERE estado Is NULL "

            Using con24 = objConeccion.Conectar
                Dim command As New SqlCommand(sql_query, con24)

                con24.Open()
                command.ExecuteNonQuery()
                con24.Close()

            End Using

        Catch ex As SqlException
            estado = False
        Catch ex As Exception
            estado = False
        End Try
        Return estado
    End Function

End Class
