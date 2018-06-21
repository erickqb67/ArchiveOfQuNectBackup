
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Data.Odbc
Imports System.Text.RegularExpressions



Public Class backup

    Private Const AppName = "QuNectBackup"
    Private Const qunectBackupVersion = "1.0.0.72"
    Private Const yearForAllFileURLs = 18
    Private cmdLineArgs() As String
    Private automode As Boolean = False
    Private appdbid As String = ""
    Private qdbAppName As String = ""
    Private dbidToAppName As New Dictionary(Of String, String)
    Private Class qdbVersion
        Public year As Integer
        Public major As Integer
        Public minor As Integer
    End Class
    Private Class backupResult
        Public result As Boolean
        Public okayCancel As DialogResult
    End Class
    Private qdbVer As qdbVersion = New qdbVersion

    Private Sub backup_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If lstBackup.Visible = True Then
            Dim i As Integer
            Dim dbids As String = ""
            Dim semicolon As String = ""
            For i = 0 To lstBackup.Items.Count - 1
                dbids &= semicolon & lstBackup.Items(i).Substring(lstBackup.Items(i).LastIndexOf(" ") + 1)
                semicolon = ";"
            Next

            SaveSetting(AppName, "backup", "dbids", dbids)
        End If
    End Sub


    Private Sub backup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUsername.Text = GetSetting(AppName, "Credentials", "username")
        cmbPassword.SelectedIndex = CInt(GetSetting(AppName, "Credentials", "passwordOrToken", "0"))
        txtPassword.Text = GetSetting(AppName, "Credentials", "password")
        txtServer.Text = GetSetting(AppName, "Credentials", "server", "www.quickbase.com")
        txtAppToken.Text = GetSetting(AppName, "Credentials", "apptoken", "b2fr52jcykx3tnbwj8s74b8ed55b")
        cmbAttachments.Text = GetSetting(AppName, "attachments", "mode", "Do not download")
        Dim dateFoldersSetting As String = GetSetting(AppName, "datefolders", "mode", "0")
        If dateFoldersSetting = "1" Then
            ckbDateFolders.Checked = True
        Else
            ckbDateFolders.Checked = False
        End If
        Dim appFoldersSetting As String = GetSetting(AppName, "appfolders", "mode", "0")
        If appFoldersSetting = "1" Then
            ckbAppFolders.Checked = True
        Else
            ckbAppFolders.Checked = False
        End If
        Dim detectProxySetting As String = GetSetting(AppName, "Credentials", "detectproxysettings", "0")
        If detectProxySetting = "1" Then
            ckbDetectProxy.Checked = True
        Else
            ckbDetectProxy.Checked = False
        End If
        Dim samlSetting As String = GetSetting(AppName, "Credentials", "samlsetting", "0")
        If samlSetting = "1" Then
            ckbSSO.Checked = True
        Else
            ckbSSO.Checked = False
        End If

        txtBackupFolder.Text = GetSetting(AppName, "location", "path")
        cmdLineArgs = System.Environment.GetCommandLineArgs()
        If cmdLineArgs.Length > 1 Then
            If cmdLineArgs(1) = "auto" Then
                automode = True
                qdbAppName = ""
                appdbid = ""
                listTables()
                backup()
                Me.Close()
            End If
        End If
        Dim myBuildInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
        Me.Text = "QuNect Backup " & qunectBackupVersion
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        SaveSetting(AppName, "Credentials", "username", txtUsername.Text)
    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
        SaveSetting(AppName, "Credentials", "password", txtPassword.Text)
    End Sub

    Private Sub btnListTables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListTables.Click
        qdbAppName = ""
        appdbid = ""
        listTables()
    End Sub
    Private Sub listTables()
        Me.Cursor = Cursors.WaitCursor
        tvAppsTables.Visible = True
        Try
            Dim connectionString As String = buildConnectionString("")
            Dim quNectConn As OdbcConnection = New OdbcConnection(connectionString)
            quNectConn.Open()
            Dim ver As String = quNectConn.ServerVersion
            Dim m As Match = Regex.Match(ver, "\d+\.(\d+)\.(\d+)\.(\d+)")
            qdbVer.year = CInt(m.Groups(1).Value)
            qdbVer.major = CInt(m.Groups(2).Value)
            qdbVer.minor = CInt(m.Groups(3).Value)
            If qdbVer.year < yearForAllFileURLs Then
                cmbAttachments.Items(3) = "Please upgrade to latest version of QuNect ODBC for QuickBase to list all file URLs"
            End If

            If qdbVer.year < 17 Then
                MsgBox("You are running the 20" & qdbVer.year & " version of QuNect ODBC for QuickBase. Please install the latest version from https://qunectllc.com/download/QuNect.exe", MsgBoxStyle.OkOnly, AppName)
                quNectConn.Dispose()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim tableOfTables As DataTable = quNectConn.GetSchema("Tables")
            listTablesFromGetSchema(tableOfTables)
            Me.Cursor = Cursors.Default
            quNectConn.Close()
            quNectConn.Dispose()
        Catch excpt As Exception
            Me.Cursor = Cursors.Default
            If excpt.Message.StartsWith("ERROR [IM003]") Or excpt.Message.Contains("Data source name not found") Then
                MsgBox("Please install QuNect ODBC for QuickBase from http://qunect.com/download/QuNect.exe and try again.", MsgBoxStyle.OkOnly, AppName)
            Else
                MsgBox(excpt.Message.Substring(13), MsgBoxStyle.OkOnly, AppName)
            End If
            Exit Sub
        End Try
    End Sub
    Sub timeoutCallback(ByVal result As System.IAsyncResult)
        If Not automode Then
            Me.Cursor = Cursors.Default
            MsgBox("Operation timed out. Please try again.", MsgBoxStyle.OkOnly, AppName)
        End If
    End Sub
    Sub listTablesFromGetSchema(tables As DataTable)

        Dim dbids As String = GetSetting(AppName, "backup", "dbids")
        Dim dbidArray As New ArrayList
        dbidArray.AddRange(dbids.Split(";"c))
        Dim i As Integer
        Dim dbidCollection As New Collection
        For i = 0 To dbidArray.Count - 1
            Try
                dbidCollection.Add(dbidArray(i), dbidArray(i))
            Catch excpt As Exception
                'ignore dupes
            End Try
        Next

        tvAppsTables.BeginUpdate()
        tvAppsTables.Nodes.Clear()
        tvAppsTables.ShowNodeToolTips = True
        Dim dbName As String
        Dim applicationName As String = ""
        Dim prevAppName As String = ""
        Dim dbid As String
        pb.Value = 0
        pb.Visible = True
        pb.Maximum = tables.Rows.Count
        Dim getDBIDfromdbName As New Regex("([a-z0-9~]+)$")
        'need to make a collection of current items in lstBackup
        Dim backupDBIDs As New Collection
        For i = 0 To lstBackup.Items.Count - 1
            backupDBIDs.Add(i, lstBackup.Items(i).Substring(lstBackup.Items(i).LastIndexOf(" ") + 1))
        Next
        dbidToAppName.Clear()
        For i = 0 To tables.Rows.Count - 1
            pb.Value = i
            Application.DoEvents()
            dbName = tables.Rows(i)(2)
            applicationName = tables.Rows(i)(0)
            Dim dbidMatch As Match = getDBIDfromdbName.Match(dbName)
            dbid = dbidMatch.Value
            If Not dbidToAppName.ContainsKey(dbid) Then
                dbidToAppName.Add(dbid, applicationName)
            End If
            If applicationName <> prevAppName Then

                Dim appNode As TreeNode = tvAppsTables.Nodes.Add(applicationName)
                appNode.ToolTipText = "Right click me to get all my table reports as well as tables!"
                appNode.Tag = dbid
                prevAppName = applicationName
            End If
            Dim tableName As String = dbName
            If appdbid.Length = 0 And dbName.Length > applicationName.Length Then
                tableName = dbName.Substring(applicationName.Length + 2)
            End If
            Dim tableNode As TreeNode = tvAppsTables.Nodes(tvAppsTables.Nodes.Count - 1).Nodes.Add(tableName)
            tableNode.ToolTipText = "Right click me to get all my table reports as well as tables!"
            tableNode.Tag = dbid
            If dbidCollection.Contains(dbid) Then
                If backupDBIDs.Contains(dbid) Then
                    lstBackup.Items(backupDBIDs(dbid)) = tvAppsTables.Nodes(tvAppsTables.Nodes.Count - 1).Nodes(tvAppsTables.Nodes(tvAppsTables.Nodes.Count - 1).Nodes.Count - 1).FullPath()
                Else
                    lstBackup.Items.Add(tvAppsTables.Nodes(tvAppsTables.Nodes.Count - 1).Nodes(tvAppsTables.Nodes(tvAppsTables.Nodes.Count - 1).Nodes.Count - 1).FullPath())
                End If
                'remove this from the collection so we can see what's missing after we find all the ones that are in the list to the left
                dbidCollection.Remove(dbid)
            End If
        Next
        For Each dbid In dbidCollection
            If Not backupDBIDs.Contains(dbid) And dbid <> "" Then
                lstBackup.Items.Add(dbid)
            End If

        Next
        pb.Visible = False
        tvAppsTables.EndUpdate()
        pb.Value = 0
        btnBackup.Visible = True
        btnAddToBackupList.Visible = True
        btnRemove.Visible = True
        lstBackup.Visible = True
        lblBackup.Visible = True
        Me.Cursor = Cursors.Default
    End Sub
    

    Private Sub txtServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged
        SaveSetting(AppName, "Credentials", "server", txtServer.Text)
    End Sub
    Private Sub btnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolder.Click
        Dim MyFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        ' Description that displays above the dialog box control. 

        MyFolderBrowser.Description = "Select the Folder"
        ' Sets the root folder where the browsing starts from 
        MyFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer
        Dim dlgResult As DialogResult = MyFolderBrowser.ShowDialog()

        If dlgResult = Windows.Forms.DialogResult.OK Then
            txtBackupFolder.Text = MyFolderBrowser.SelectedPath
            SaveSetting(AppName, "location", "path", txtBackupFolder.Text)
        End If
    End Sub
    Private Sub btnAddToBackupList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToBackupList.Click
        If My.Computer.Keyboard.ShiftKeyDown Then
            lstBackup.Items.Clear()
            Dim i As Integer
            For i = 0 To tvAppsTables.Nodes.Count - 1
                For j As Integer = 0 To tvAppsTables.Nodes(i).Nodes.Count - 1
                    lstBackup.Items.Add(tvAppsTables.Nodes(i).Nodes(j).FullPath())
                Next
            Next
        Else
            Try
                If tvAppsTables.SelectedNode.Level <> 1 Then
                    If tvAppsTables.SelectedNode.Level = 0 Then
                        For j As Integer = 0 To tvAppsTables.SelectedNode.Nodes.Count - 1
                            addToBackupList(tvAppsTables.SelectedNode.Nodes(j).FullPath())
                        Next
                    End If
                    Exit Sub
                End If
                addToBackupList(tvAppsTables.SelectedNode.FullPath())
            Catch excpt As Exception
                MsgBox("Please select a table first", MsgBoxStyle.OkOnly, AppName)
            End Try
        End If
    End Sub
    Private Sub addToBackupList(ByVal appTable As String)
        If appTable.Length = 0 Then
            Exit Sub
        End If
        Try
            Dim dbid As String = appTable.Substring(appTable.LastIndexOf(" ") + 1)
            Dim i As Integer

            For i = 0 To lstBackup.Items.Count - 1
                Application.DoEvents()
                Dim existingDBID As String = lstBackup.Items(i).Substring(lstBackup.Items(i).LastIndexOf(" ") + 1)
                If existingDBID = dbid Then
                    lstBackup.Items(i) = appTable
                    Exit Sub
                End If
            Next
            lstBackup.Items.Add(appTable)
        Catch excpt As Exception
        End Try
    End Sub
    Private Sub tvAppsTables_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvAppsTables.DoubleClick
        If tvAppsTables.SelectedNode.Level <> 1 Then
            Exit Sub
        End If
        addToBackupList(tvAppsTables.SelectedNode.FullPath())
    End Sub

    Private Sub backup_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        lstBackup.Width = Me.Width - 20 - lstBackup.Left
    End Sub

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        backup()
    End Sub
    Private Function buildConnectionString(additionalFolders As String) As String
        buildConnectionString = "FIELDNAMECHARACTERS=all;uid=" & txtUsername.Text
        buildConnectionString &= ";pwd=" & txtPassword.Text
        buildConnectionString &= ";driver={QuNect ODBC for QuickBase};IGNOREDUPEFIELDNAMES=1;"
        buildConnectionString &= ";quickbaseserver=" & txtServer.Text
        buildConnectionString &= ";APPTOKEN=" & txtAppToken.Text
        If ckbDetectProxy.Checked Then
            buildConnectionString &= ";DETECTPROXY=1"
        End If
        If ckbSSO.Checked Then
            buildConnectionString &= ";SAML=1"
        End If
        If appdbid.Length Then
            buildConnectionString &= ";APPID=" & appdbid & ";APPNAME=" & qdbAppName
        End If

        If cmbAttachments.SelectedIndex = 2 Then
            buildConnectionString &= ";allrevisions=1"
        ElseIf cmbAttachments.SelectedIndex = 3 Then
            buildConnectionString &= ";allrevisions=ALL"
            'filepath needs to be the last thing on the connection string
        End If
        If cmbAttachments.SelectedIndex >= 1 Then
            Dim folderPath As String = txtBackupFolder.Text
            If ckbDateFolders.Checked Then
                folderPath &= "\" & DateTime.Now.ToString("yyyy-MM-dd")
            End If
            If additionalFolders <> "" Then
                folderPath &= "\" & makeFileNameCompatible(additionalFolders)
            End If
            Directory.CreateDirectory(folderPath)
            buildConnectionString &= ";filepath=" & folderPath
        End If
        If cmbPassword.SelectedIndex = 0 Then
            cmbPassword.Focus()
            Throw New System.Exception("Please indicate whether you are using a password or a user token.")
            Return ""
        ElseIf cmbPassword.SelectedIndex = 1 Then
            buildConnectionString &= ";PWDISPASSWORD=1"
        Else
            buildConnectionString &= ";PWDISPASSWORD=0"
        End If
    End Function
    Private Sub backup()
        'here we need to go through the list and backup
        If cmbAttachments.SelectedIndex = 3 And qdbVer.year < yearForAllFileURLs Then
            MsgBox("Please upgrade to the latest version of QuNect ODBC for QuickBase to list all file URLs instead of just the current revision file URL.", MsgBoxStyle.OkOnly, AppName)
            Return
        End If
        Dim i As Integer
        Me.Cursor = Cursors.WaitCursor
        Dim connectionString As String = buildConnectionString("")

        Dim quNectConn As OdbcConnection = New OdbcConnection(connectionString)
        Try
            quNectConn.Open()
        Catch excpt As Exception
            If Not automode Then
                MsgBox(excpt.Message(), MsgBoxStyle.OkOnly, AppName)
            End If
            quNectConn.Dispose()
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try
        Dim backupCounter As Integer = 0
        Dim currentApplication As String = ""
        For i = 0 To lstBackup.Items.Count - 1
            If Not automode Then
                lblProgress.Text = ""
                lstBackup.SelectedIndex = i
            End If
            Dim dbName As String = lstBackup.Items(i).ToString()
            Dim dbid As String = dbName.Substring(dbName.LastIndexOf(" ") + 1)
            If ckbAppFolders.Checked AndAlso dbidToAppName.ContainsKey(dbid) AndAlso dbidToAppName.Item(dbid) <> currentApplication Then
                currentApplication = dbidToAppName.Item(dbid)
                connectionString = buildConnectionString(dbidToAppName(dbid))
                quNectConn.Close()
                quNectConn = New OdbcConnection(connectionString)
                Try
                    quNectConn.Open()
                Catch excpt As Exception
                    If Not automode Then
                        MsgBox(excpt.Message(), MsgBoxStyle.OkOnly, AppName)
                    End If
                    quNectConn.Dispose()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End Try
            End If
            Dim successFailure As backupResult = backupTable(dbName, dbid, quNectConn)
            If successFailure.okayCancel = DialogResult.Cancel Then
                Exit For
            End If
            If successFailure.result Then
                backupCounter += 1
            End If
        Next
        quNectConn.Close()
        quNectConn.Dispose()
        Me.Cursor = Cursors.Default
        If Not automode Then
            If lstBackup.Items.Count = 1 And backupCounter = 1 Then
                MsgBox("Your table has been backed up!", MsgBoxStyle.OkOnly, AppName)
            ElseIf lstBackup.Items.Count = 1 And backupCounter = 0 Then
                MsgBox("Sorry, your table was not backed up.", MsgBoxStyle.OkOnly, AppName)
            ElseIf backupCounter = 0 Then
                MsgBox("Sorry, none of your tables were  backed up.", MsgBoxStyle.OkOnly, AppName)
            Else
                MsgBox(backupCounter & " of " & lstBackup.Items.Count & " tables were backed up.", MsgBoxStyle.OkOnly, AppName)
            End If
        End If
    End Sub
    Private Function backupTable(ByVal dbName As String, ByVal dbid As String, ByRef quNectConn As OdbcConnection) As backupResult
        'we need to get the schema of the table
        Dim restrictions(2) As String
        restrictions(2) = dbid
        Dim columns As DataTable = quNectConn.GetSchema("Columns", restrictions)
        'now we can look for formula fileURL fields
        backupTable = New backupResult
        backupTable.okayCancel = DialogResult.OK
        backupTable.result = True
        Dim quickBaseSQL As String = "Select count(1) from """ & dbid & """"

        Dim quNectCmd As OdbcCommand = Nothing
        Dim dr As OdbcDataReader
        Try
            quNectCmd = New OdbcCommand(quickBaseSQL, quNectConn)
            dr = quNectCmd.ExecuteReader()
        Catch excpt As Exception
            If Not automode Then
                backupTable.okayCancel = MsgBox("Could Not Get record count For table " & dbid & " because " & excpt.Message() & vbCrLf & "Would you Like To Continue?", MsgBoxStyle.OkCancel, AppName)
                backupTable.result = False
            End If

            If Not quNectCmd Is Nothing Then
                quNectCmd.Dispose()
            End If
            Exit Function
        End Try
        If Not dr.HasRows Then
            backupTable.okayCancel = MsgBox("Could Not Get record count For table " & dbid & " perhaps because either the report's, criteria, sort order or columns refer to fields you do not have access to." & vbCrLf & "Would you like to continue?", MsgBoxStyle.OkCancel, AppName)
            backupTable.result = False
            Exit Function
        End If

        Dim recordCount As Integer = dr.GetValue(0)
        quNectCmd.Dispose()

        quickBaseSQL = "select fid, field_type, formula, mode from """ & dbid & "~fields"""
        Try
            quNectCmd = New OdbcCommand(quickBaseSQL, quNectConn)
            dr = quNectCmd.ExecuteReader()
        Catch excpt As Exception
            If Not automode Then
                backupTable.okayCancel = MsgBox("Could not get field identifiers and types for table " & dbid & " because " & excpt.Message() & vbCrLf & "Would you like to continue?", MsgBoxStyle.OkCancel, AppName)
                backupTable.result = False
            End If
            quNectCmd.Dispose()
            Exit Function
        End Try
        If Not dr.HasRows Then
            Exit Function
        End If




        Dim i
        Dim clist As String = ""
        Dim fieldTypes As String = ""
        Dim period As String = ""
        While (dr.Read())
            Dim mode As String = dr.GetString(3)
            Dim formula As String = dr.GetString(2)
            Dim field_type As String = dr.GetString(1)
            If (field_type = "url" Or field_type = "dblink") And mode = "virtual" And Not formula.Contains("/AmazonS3/download.aspx?") Then
                Continue While
            End If
            clist &= period & dr.GetString(0)
            fieldTypes &= period & field_type
            period = "."
        End While
        quNectCmd.Dispose()

        Dim folderPath As String = txtBackupFolder.Text
        If ckbDateFolders.Checked Then
            folderPath &= "\" & DateTime.Now.ToString("yyyy-MM-dd")
            Directory.CreateDirectory(folderPath)
        End If
        If ckbAppFolders.Checked Then
            folderPath &= "\" & makeFileNameCompatible(dbidToAppName(dbid))
            Directory.CreateDirectory(folderPath)
        End If
        Dim filenamePrefix As String = dbName
        If ckbAppFolders.Checked Then
            filenamePrefix = Regex.Replace(filenamePrefix, "\A[^\\]+\\", "")
        End If
        filenamePrefix = makeFileNameCompatible(filenamePrefix)

        If filenamePrefix.Length > 229 Then
            filenamePrefix = filenamePrefix.Substring(filenamePrefix.Length - 229)
        End If
        Dim filepath As String = folderPath & "\" & filenamePrefix & ".fids"
        Dim objWriter As System.IO.StreamWriter
        Try
            objWriter = New System.IO.StreamWriter(filepath)
        Catch excpt As Exception
            If Not automode Then
                backupTable.okayCancel = MsgBox("Could not open file " & filepath & " because " & excpt.Message() & vbCrLf & "Would you like to continue?", MsgBoxStyle.OkCancel, AppName)
                backupTable.result = False
            End If
            Exit Function
        End Try
        objWriter.Write(clist & vbCrLf & fieldTypes)
        objWriter.Close()

        'here we need to open a file
        'filename prefix can only be 229 characters in length
        quickBaseSQL = "select * from """ & dbid & """"



        Try
            quNectCmd = New OdbcCommand(quickBaseSQL, quNectConn)
            dr = quNectCmd.ExecuteReader()
        Catch excpt As Exception
            If Not automode Then
                backupTable.okayCancel = MsgBox("Could not backup table " & filenamePrefix & " because " & excpt.Message() & vbCrLf & "Would you like to continue?", MsgBoxStyle.OkCancel, AppName)
                backupTable.result = False
            End If
            quNectCmd.Dispose()
            Exit Function
        End Try
        If Not dr.HasRows Then
            Exit Function
        End If

        filepath = folderPath & "\" & filenamePrefix & ".csv"
        Try
            objWriter = New System.IO.StreamWriter(filepath)
        Catch excpt As Exception
            If Not automode Then
                backupTable.okayCancel = MsgBox("Could not open file " & filepath & " because " & excpt.Message() & vbCrLf & "Would you like to continue?", MsgBoxStyle.OkCancel, AppName)
                backupTable.result = False
            End If
            Exit Function
        End Try

        For i = 0 To dr.FieldCount - 1
            objWriter.Write("""")
            objWriter.Write(Replace(CStr(dr.GetName(i)), """", """"""))
            objWriter.Write(""",")
        Next

        objWriter.Write(vbCrLf)
        Dim k As Integer = 0
        pb.Visible = True
        pb.Maximum = recordCount
        While (dr.Read())
            pb.Value = Math.Min(k, recordCount)
            lblProgress.Text = "Backing up " & k & " of " & recordCount
            Application.DoEvents()
            k += 1
            For i = 0 To dr.FieldCount - 1
                If dr.GetValue(i) Is Nothing Or IsDBNull(dr.GetValue(i)) Then
                    objWriter.Write(",")
                Else
                    Dim strCell As String = dr.GetValue(i).ToString()
                    Try
                        If Not (IsDBNull(columns.Rows(i).Item("REMARKS"))) And (cmbAttachments.SelectedIndex >= 1) And (strCell.Length > 0) And columns.Rows(i).Item("REMARKS").StartsWith("Formula URL") Then
                            Dim remarks As String = columns.Rows(i).Item("REMARKS")
                            'here we need to fetch the file that is pointed to
                            Using wc As New WebClient
                                Using rawStream As Stream = wc.OpenRead(strCell)
                                    Dim fileName As String = String.Empty
                                    Dim contentDisposition As String = wc.ResponseHeaders("content-disposition")
                                    If Not String.IsNullOrEmpty(contentDisposition) Then
                                        Dim lookFor As String = "filename="
                                        Dim index As Integer = contentDisposition.IndexOf(lookFor, StringComparison.CurrentCultureIgnoreCase)
                                        If index >= 0 Then
                                            fileName = contentDisposition.Substring(index + lookFor.Length)
                                        End If
                                        If fileName.Length > 0 Then
                                            Dim fileExt As String = ""
                                            Dim lastPeriod As Integer = fileName.LastIndexOf(".")
                                            If lastPeriod <> -1 Then
                                                fileExt = fileName.Substring(lastPeriod + 1)
                                            End If
                                            Dim filePrefix As String = fileName.Substring(0, fileName.Length() - fileExt.Length() - 1)
                                            filePrefix = UrlDecode(filePrefix)
                                            'need to get rid of unprintable characters
                                            For j As Integer = 0 To filePrefix.Length() - 1
                                                If Asc(filePrefix.Chars(j)) < Asc(" ") Then
                                                    filePrefix = ChangeCharacter(filePrefix, " ", j)
                                                End If
                                            Next
                                            'get the record id and fid
                                            Dim fileSuffix As String = "." & dr.GetValue(2).ToString() & "_" & remarks.Substring(remarks.LastIndexOf(" ") + 4) & "_" & "." + fileExt
                                            If (filePrefix.Length() + fileSuffix.Length() > 255) Then
                                                filePrefix = filePrefix.Remove(255 - fileSuffix.Length())
                                            End If
                                            Directory.CreateDirectory(folderPath & "\" & dbid)
                                            filepath = folderPath & "\" & dbid & "\" & filePrefix + fileSuffix

                                            Using reader As BinaryReader = New BinaryReader(rawStream)
                                                File.WriteAllBytes(filepath, reader.ReadBytes(CInt(wc.ResponseHeaders("content-length"))))
                                                reader.Close()
                                            End Using
                                            strCell = "file:///" + filepath
                                        End If
                                    End If
                                    rawStream.Close()
                                End Using
                            End Using
                        End If
                    Catch excpt As Exception
                        strCell = "Could not download " & strCell & " because " & excpt.Message
                    End Try
                    objWriter.Write("""")
                    objWriter.Write(Replace(strCell, """", """"""))
                    objWriter.Write(""",")
                End If
            Next
            objWriter.Write(vbCrLf)
        End While
        pb.Visible = False
        lblProgress.Text = ""
        objWriter.Close()
        dr.Close()
        quNectCmd.Dispose()
    End Function
    Private Function makeFileNameCompatible(fileName As String) As String
        Return fileName.Replace("/", "").Replace("\", "").Replace(":", "").Replace(":", "_").Replace("?", "").Replace("""", "").Replace("<", "").Replace(">", "").Replace("|", "")
    End Function
    Private Function ChangeCharacter(s As String, replaceWith As Char, idx As Integer) As String
        Dim sb As New StringBuilder(s)
        sb(idx) = replaceWith
        Return sb.ToString()
    End Function

    Private Function UrlDecode(text As String) As String
        text = text.Replace("+", " ")
        Return System.Uri.UnescapeDataString(text)
    End Function
    Private Sub txtAppToken_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAppToken.TextChanged
        SaveSetting(AppName, "Credentials", "apptoken", txtAppToken.Text)
    End Sub

    Private Sub cmbAttachments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAttachments.SelectedIndexChanged
        If (cmbAttachments.SelectedIndex = 3 And qdbVer.year >= yearForAllFileURLs) Or cmbAttachments.SelectedIndex < 3 Then
            SaveSetting(AppName, "attachments", "mode", cmbAttachments.Text)
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If My.Computer.Keyboard.ShiftKeyDown Then
            lstBackup.Items.Clear()
        Else
            If lstBackup.SelectedIndex = -1 Then
                Exit Sub
            End If
            If lstBackup.SelectedItems.Count > 1 Then
                For i As Integer = lstBackup.Items.Count - 1 To 0 Step -1
                    If lstBackup.GetSelected(i) = True Then
                        lstBackup.Items.RemoveAt(i)
                    End If
                Next i
            Else
                Dim removedIndex As Integer = lstBackup.SelectedIndex
                lstBackup.Items.RemoveAt(lstBackup.SelectedIndex)
                If lstBackup.Items.Count > removedIndex Then
                    lstBackup.SelectedIndex = removedIndex
                ElseIf lstBackup.Items.Count > 0 Then
                    lstBackup.SelectedIndex = lstBackup.Items.Count - 1
                End If
            End If
        End If
    End Sub

    Private Sub ckbDateFolders_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbDateFolders.CheckStateChanged
        If ckbDateFolders.Checked Then
            SaveSetting(AppName, "datefolders", "mode", "1")
        Else
            SaveSetting(AppName, "datefolders", "mode", "0")
        End If
    End Sub
    Private Sub ckbAppFolders_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbAppFolders.CheckStateChanged
        If ckbAppFolders.Checked Then
            SaveSetting(AppName, "appfolders", "mode", "1")
        Else
            SaveSetting(AppName, "appfolders", "mode", "0")
        End If
    End Sub
    Private Sub ckbDetectProxy_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbDetectProxy.CheckStateChanged
        If ckbDetectProxy.Checked Then
            SaveSetting(AppName, "Credentials", "detectproxysettings", "1")
        Else
            SaveSetting(AppName, "Credentials", "detectproxysettings", "0")
        End If
    End Sub

    Private Sub lstBackup_DoubleClick(sender As Object, e As System.EventArgs) Handles lstBackup.DoubleClick
        If lstBackup.SelectedIndex = -1 Then
            Exit Sub
        End If
        lstBackup.Items.RemoveAt(lstBackup.SelectedIndex)
    End Sub


    Private Sub ckbSSO_CheckStateChanged(sender As Object, e As EventArgs) Handles ckbSSO.CheckStateChanged
        If ckbSSO.Checked Then
            SaveSetting(AppName, "Credentials", "samlsetting", "1")
        Else
            SaveSetting(AppName, "Credentials", "samlsetting", "0")
        End If
    End Sub
    Private Sub ContextMenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
        If (qdbVer.year < 16) Or ((qdbVer.year = 16) And ((qdbVer.major <= 6) And (qdbVer.minor < 20))) Then
            MsgBox("To access this feature please install the latest version from http://qunect.com/download/QuNect.exe", MsgBoxStyle.OkOnly, AppName)
            Exit Sub
        End If
        'here we need to reconnect with the appid in the connection string
        If tvAppsTables.SelectedNode Is Nothing Then
            Exit Sub
        End If
        appdbid = tvAppsTables.SelectedNode.Tag
        If tvAppsTables.SelectedNode.Level = 1 Then
            qdbAppName = tvAppsTables.SelectedNode.Parent.Text
        Else
            qdbAppName = tvAppsTables.SelectedNode.Text
        End If
        listTables()
    End Sub

    Private Sub tvAppsTables_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvAppsTables.NodeMouseClick
        If e.Node.Level <> 0 Then
            Exit Sub
        End If
        tvAppsTables.SelectedNode = e.Node
    End Sub
    Private Sub cmbPassword_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPassword.SelectedIndexChanged
        SaveSetting(AppName, "Credentials", "passwordOrToken", cmbPassword.SelectedIndex)
        If cmbPassword.SelectedIndex = 0 Then
            txtPassword.Enabled = False
        Else
            txtPassword.Enabled = True
        End If
    End Sub
End Class


