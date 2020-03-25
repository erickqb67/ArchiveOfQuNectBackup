
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Data.Odbc
Imports System.Text.RegularExpressions
Imports System.Configuration



Public Class backup

    Private Const AppName = "QuNectBackup"
    Private Const yearForAllFileURLs = 18
    Private cmdLineArgs() As String
    Private automode As Boolean = True
    Private appdbid As String = ""
    Private qdbAppName As String = ""
    Private dbidToAppName As New Dictionary(Of String, String)
    Dim cAppConfig As Configuration = ConfigurationManager.OpenExeConfiguration(Application.StartupPath & "\QuNectBackup.exe")
    Dim appSettings As AppSettingsSection = cAppConfig.AppSettings
    Private Class qdbVersion
        Public year As Integer
        Public major As Integer
        Public minor As Integer
    End Class
    Private Class backupResult
        Public result As Boolean
        Public okayCancel As DialogResult
    End Class
    Enum PasswordOrToken
        Neither = 0
        password = 1
        token = 2
    End Enum
    Private qdbVer As qdbVersion = New qdbVersion

    Private Sub backup_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub
    Function createDBIDList() As String
        Dim i As Integer
        Dim dbids As String = ""
        Dim delimiter As String = ""
        For i = 0 To lstBackup.Items.Count - 1
            dbids &= delimiter & lstBackup.Items(i).Substring(lstBackup.Items(i).LastIndexOf(" ") + 1)
            delimiter = ";"
        Next
        Return dbids
    End Function
    Function createTableList() As String
        Dim i As Integer
        Dim tables As String = ""
        Dim delimiter As String = ""
        For i = 0 To lstBackup.Items.Count - 1
            tables &= delimiter & lstBackup.Items(i)
            delimiter = vbCrLf
        Next
        Return tables
    End Function

    Private Sub backup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtUsername.Text = GetSetting(AppName, "Credentials", "username")
        cmbPassword.SelectedIndex = CInt(GetSetting(AppName, "Credentials", "passwordOrToken", CStr(PasswordOrToken.Neither)))
        txtPassword.Text = GetSetting(AppName, "Credentials", "password")
        txtServer.Text = GetSetting(AppName, "Credentials", "server", "")
        txtAppToken.Text = GetSetting(AppName, "Credentials", "apptoken", "")
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


        txtBackupFolder.Text = GetSetting(AppName, "location", "path")
        If appSettings.Settings.Item("location") IsNot Nothing AndAlso appSettings.Settings.Item("location").Value.Length > 0 Then
            txtBackupFolder.Text = appSettings.Settings.Item("location").Value
        Else
            SaveSettings()
        End If
        cmdLineArgs = System.Environment.GetCommandLineArgs()
        If cmdLineArgs.Length > 1 Then
            qdbAppName = ""
            appdbid = ""
            Dim dbids As String = ""
            If cmdLineArgs.Length >= 10 Then
                txtBackupFolder.Text = cmdLineArgs(1)
                dbids = cmdLineArgs(2)
                txtUsername.Text = cmdLineArgs(3)
                txtPassword.Text = cmdLineArgs(4)
                txtServer.Text = cmdLineArgs(5)
                If cmdLineArgs(6) = "1" Then
                    ckbDetectProxy.Checked = True
                Else
                    ckbDetectProxy.Checked = False
                End If
                If cmdLineArgs(7) = "1" Then
                    ckbDateFolders.Checked = True
                Else
                    ckbDateFolders.Checked = False
                End If
                If cmdLineArgs(8) = "1" Then
                    ckbAppFolders.Checked = True
                Else
                    ckbAppFolders.Checked = False
                End If
                cmbAttachments.SelectedIndex = CInt(cmdLineArgs(9))
                cmbPassword.SelectedIndex = PasswordOrToken.token
            End If
            automode = True
            listTables(dbids)
            backup()
            Me.Close()
        Else
            automode = False
            Dim myBuildInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
            Me.Text = "QuNect Backup " & myBuildInfo.ProductVersion
            If txtUsername.Text.Length > 0 And txtPassword.Text.Length > 0 And txtServer.Text.Length > 0 Then
                If (cmbPassword.SelectedIndex = PasswordOrToken.password And txtAppToken.Text.Length > 0) Or cmbPassword.SelectedIndex = PasswordOrToken.token Then
                    tabs.SelectedIndex = 1
                End If
            End If
        End If
    End Sub
    Sub showHideControls()
        Try
            Dim usernamePresent As Boolean = txtUsername.Text.Length > 0
            cmbPassword.Visible = usernamePresent
            txtPassword.Visible = cmbPassword.SelectedIndex <> PasswordOrToken.Neither And usernamePresent
            txtServer.Visible = txtPassword.Visible And txtPassword.Text.Length > 0
            lblServer.Visible = txtServer.Visible
            lblAppToken.Visible = usernamePresent And cmbPassword.SelectedIndex = PasswordOrToken.password
            txtAppToken.Visible = lblAppToken.Visible
            btnAppToken.Visible = lblAppToken.Visible
            btnUserToken.Visible = usernamePresent And cmbPassword.SelectedIndex = PasswordOrToken.token
            ckbDetectProxy.Visible = txtServer.Text.Length > 0 And txtServer.Visible
            txtServer.Visible = txtUsername.Text.Length > 0 And txtPassword.Text.Length > 0 And cmbPassword.SelectedIndex <> PasswordOrToken.Neither
            lblServer.Visible = txtServer.Visible
            txtAppToken.Visible = txtUsername.Text.Length > 0 And cmbPassword.SelectedIndex = PasswordOrToken.password And txtPassword.Text.Length > 0 And txtServer.Text.Length > 0
            lblAppToken.Visible = txtAppToken.Visible
            btnAppToken.Visible = txtAppToken.Visible
            btnUserToken.Visible = txtUsername.Text.Length > 0 And cmbPassword.SelectedIndex = PasswordOrToken.token
            Dim showListTables As Boolean = (txtUsername.Text.Length > 0) And (cmbPassword.SelectedIndex <> PasswordOrToken.Neither) And (txtPassword.Text.Length > 0) And (txtServer.Text.Length > 0)
            btnListTables.Visible = showListTables
            btnTest.Visible = showListTables
            cmbAttachments.Visible = showListTables
        Catch excpt As Exception
            MsgBox(excpt.Message, MsgBoxStyle.OkOnly, AppName)
        End Try

    End Sub

    Sub SaveSettings()
        Try
            If automode Then Exit Sub
            SaveSetting(AppName, "backup", "dbids", createDBIDList())
            SaveSetting(AppName, "Credentials", "username", txtUsername.Text)
            SaveSetting(AppName, "Credentials", "password", txtPassword.Text)
            SaveSetting(AppName, "Credentials", "server", txtServer.Text)
            SaveSetting(AppName, "location", "path", txtBackupFolder.Text)
            SaveSetting(AppName, "Credentials", "apptoken", txtAppToken.Text)
            If (cmbAttachments.SelectedIndex = 3 And qdbVer.year >= yearForAllFileURLs) Or cmbAttachments.SelectedIndex < 3 Then
                SaveSetting(AppName, "attachments", "mode", cmbAttachments.Text)
            End If
            If ckbDateFolders.Checked Then
                SaveSetting(AppName, "datefolders", "mode", "1")
            Else
                SaveSetting(AppName, "datefolders", "mode", "0")
            End If
            If ckbAppFolders.Checked Then
                SaveSetting(AppName, "appfolders", "mode", "1")
            Else
                SaveSetting(AppName, "appfolders", "mode", "0")
            End If
            If ckbDetectProxy.Checked Then
                SaveSetting(AppName, "Credentials", "detectproxysettings", "1")
            Else
                SaveSetting(AppName, "Credentials", "detectproxysettings", "0")
            End If
            SaveSetting(AppName, "Credentials", "passwordOrToken", cmbPassword.SelectedIndex)
            If appSettings.Settings.Item("tables") Is Nothing Then
                appSettings.Settings.Add("tables", createTableList())
            Else
                appSettings.Settings.Item("tables").Value = createTableList()
            End If
            If appSettings.Settings.Item("location") Is Nothing Then
                appSettings.Settings.Add("location", txtBackupFolder.Text)
            Else
                appSettings.Settings.Item("location").Value = txtBackupFolder.Text
            End If
            cAppConfig.Save(ConfigurationSaveMode.Modified)
        Catch excpt As Exception
            MsgBox(excpt.Message, MsgBoxStyle.OkOnly, AppName)
        End Try
    End Sub
    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        SaveSettings()
        showHideControls()
    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
        SaveSettings()
        showHideControls()
    End Sub

    Private Sub btnListTables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListTables.Click
        qdbAppName = ""
        appdbid = ""
        listTables("")
    End Sub
    Private Sub listTables(dbids As String)
        Me.Cursor = Cursors.WaitCursor
        tvAppsTables.Visible = True
        btnCommandLine.Visible = True
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
                MsgBox("You are running the 20" & qdbVer.year & " version of QuNect ODBC for QuickBase. Please install the latest version from https://qunect.com/download/QuNect.exe", MsgBoxStyle.OkOnly, AppName)
                quNectConn.Dispose()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim tableOfTables As DataTable = quNectConn.GetSchema("Tables")
            listTablesFromGetSchema(tableOfTables, dbids)
            Me.Cursor = Cursors.Default
            quNectConn.Close()
            quNectConn.Dispose()
        Catch excpt As Exception
            Me.Cursor = Cursors.Default
            If excpt.Message.Contains("Data source name not found") Then
                MsgBox("Please install QuNect ODBC for QuickBase from http://qunect.com/download/QuNect.exe and try again.", MsgBoxStyle.OkOnly, AppName)
            Else
                MsgBox(excpt.Message, MsgBoxStyle.OkOnly, AppName)
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
    Sub listTablesFromGetSchema(tables As DataTable, dbids As String)
        Dim configTableList As String = ""
        Dim getDBIDfromdbName As New Regex("([a-z0-9~]+)$")
        Dim dbid As String

        Dim i As Integer
        If dbids = "" Then
            dbids = GetSetting(AppName, "backup", "dbids")
            If appSettings.Settings.Item("tables") IsNot Nothing Then
                configTableList = appSettings.Settings.Item("tables").Value
            End If
        End If
        Dim dbidArray As New ArrayList
        If dbids.ToLower() = "all" Then
            For i = 0 To tables.Rows.Count - 1
                Dim table As String = tables.Rows(i)(2)
                Dim dbidMatch As Match = getDBIDfromdbName.Match(table)
                dbid = dbidMatch.Value
                dbidArray.Add(dbid)
            Next

        ElseIf configTableList.Length > dbids.Length Then
            dbids = configTableList
            Dim separator() As String = {vbCrLf}
            dbidArray.AddRange(dbids.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        Else
            dbidArray.AddRange(dbids.Split(";"c))
        End If
        Dim dbidCollection As New Collection
        For i = 0 To dbidArray.Count - 1
            Try
                Dim dbidMatch As Match = getDBIDfromdbName.Match(dbidArray(i))
                dbid = dbidMatch.Value
                dbidCollection.Add(dbid, dbid)
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
        pb.Value = 0
        pb.Visible = True
        pb.Maximum = tables.Rows.Count
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
        showHideControls()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub txtServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged
        SaveSettings()
        showHideControls()
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
            SaveSettings()

        End If
    End Sub
    Private Sub btnAddToBackupList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToBackupList.Click
        addNodeToBackupList(tvAppsTables.SelectedNode)
    End Sub
    Sub addNodeToBackupList(tnode As System.Windows.Forms.TreeNode)
        If tnode Is Nothing Then Exit Sub

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
                If tnode.Level <> 1 Then
                    If tnode.Level = 0 Then
                        For j As Integer = 0 To tnode.Nodes.Count - 1
                            addToBackupList(tnode.Nodes(j).FullPath())
                        Next
                    End If
                    Exit Sub
                End If
                addToBackupList(tnode.FullPath())
            Catch excpt As Exception
                MsgBox("Please select a table first", MsgBoxStyle.OkOnly, AppName)
            End Try
        End If
        showHideControls()
        SaveSettings()
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
        SaveSettings()

    End Sub
    Private Sub tvAppsTables_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvAppsTables.DoubleClick
        If tvAppsTables.SelectedNode.Level <> 1 Then
            Exit Sub
        End If
        addToBackupList(tvAppsTables.SelectedNode.FullPath())
    End Sub

    Private Sub backup_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        lstBackup.Width = tabs.DisplayRectangle.Width - lstBackup.Left
        lstBackup.Height = tabs.DisplayRectangle.Height - lstBackup.Top
        tvAppsTables.Height = lstBackup.Height
    End Sub

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        backup()
    End Sub
    Private Function buildConnectionString(additionalFolders As String) As String
        buildConnectionString = "FIELDNAMECHARACTERS=all;uid=" & txtUsername.Text
        buildConnectionString &= ";pwd=" & txtPassword.Text
        buildConnectionString &= ";driver={QuNect ODBC for QuickBase};IGNOREDUPEFIELDNAMES=1;"
        buildConnectionString &= ";quickbaseserver=" & txtServer.Text
        If ckbDetectProxy.Checked Then
            buildConnectionString &= ";DETECTPROXY=1"
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
        If cmbPassword.SelectedIndex = PasswordOrToken.Neither Then
            cmbPassword.Focus()
            Throw New System.Exception("Please indicate whether you are using a password or a user token.")
            Return ""
        ElseIf cmbPassword.SelectedIndex = PasswordOrToken.password Then
            buildConnectionString &= ";PWDISPASSWORD=1"
            buildConnectionString &= ";APPTOKEN=" & txtAppToken.Text
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

            If quNectCmd IsNot Nothing Then
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

        quickBaseSQL = "select fid, field_type, formula, mode, label from """ & dbid & "~fields"""
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
            Dim label As String = dr.GetString(4)
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
            Dim dbidWithoutQID As String = Regex.Replace(dbid, "~-?\d+", "")
            folderPath &= "\" & makeFileNameCompatible(dbidToAppName(dbidWithoutQID))
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
        SaveSettings()
    End Sub

    Private Sub cmbAttachments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAttachments.SelectedIndexChanged
        SaveSettings()
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
        showHideControls()
        SaveSettings()

    End Sub

    Private Sub ckbDateFolders_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbDateFolders.CheckStateChanged
        SaveSettings()
    End Sub
    Private Sub ckbAppFolders_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbAppFolders.CheckStateChanged
        SaveSettings()
    End Sub
    Private Sub ckbDetectProxy_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbDetectProxy.CheckStateChanged
        SaveSettings()
    End Sub

    Private Sub lstBackup_DoubleClick(sender As Object, e As System.EventArgs) Handles lstBackup.DoubleClick
        If lstBackup.SelectedIndex = -1 Then
            Exit Sub
        End If
        lstBackup.Items.RemoveAt(lstBackup.SelectedIndex)
        SaveSettings()

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
        listTables("")
    End Sub

    Private Sub tvAppsTables_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvAppsTables.NodeMouseClick
        If e.Node.Level <> 0 Then
            Exit Sub
        End If
        tvAppsTables.SelectedNode = e.Node
    End Sub
    Private Sub cmbPassword_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPassword.SelectedIndexChanged
        SaveSettings()
        showHideControls()
    End Sub
    Private Sub btnAppToken_Click(sender As Object, e As EventArgs) Handles btnAppToken.Click
        Process.Start("https://qunect.com/flash/AppToken.html")
    End Sub

    Private Sub btnUserToken_Click(sender As Object, e As EventArgs) Handles btnUserToken.Click
        Process.Start("https://qunect.com/flash/UserToken.html")
    End Sub
    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim connectionString As String = buildConnectionString("")
            Dim quNectConn As OdbcConnection = New OdbcConnection(connectionString)
            quNectConn.Open()
        Catch excpt As Exception
            Me.Cursor = Cursors.Default
            If excpt.Message.Contains("Data source name not found") Then
                MsgBox("Please install QuNect ODBC for QuickBase from http://qunect.com/download/QuNect.exe and try again.", MsgBoxStyle.OkOnly, AppName)
            Else
                MsgBox(excpt.Message, MsgBoxStyle.OkOnly, AppName)
            End If
            Exit Sub
        End Try
        Me.Cursor = Cursors.Default
        MsgBox("Success", MsgBoxStyle.OkOnly, AppName)
    End Sub
    Public Sub tvAppsTables_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvAppsTables.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub lstBackup_DragEnter(sender As Object, e As DragEventArgs) Handles lstBackup.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub
    Public Sub lstBackup_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstBackup.DragDrop
        Dim draggedNode As System.Windows.Forms.TreeNode = e.Data.GetData(GetType(System.Windows.Forms.TreeNode))
        addNodeToBackupList(draggedNode)
    End Sub
    Public Sub lstBackup_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles lstBackup.MouseDown
        DoDragDrop(sender, DragDropEffects.Move)
    End Sub

    Private Sub tvAppsTables_DragEnter(sender As Object, e As DragEventArgs) Handles tvAppsTables.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub
    Public Sub tvAppsTables_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvAppsTables.DragDrop
        btnRemove_Click(sender, e)
    End Sub

    Private Sub btnCommandLine_Click(sender As Object, e As EventArgs) Handles btnCommandLine.Click
        If cmbPassword.SelectedIndex <> PasswordOrToken.token Then
            MsgBox("This feature is only avialable when using a user token instead of a password.", MsgBoxStyle.OkOnly, AppName)
            Exit Sub
        End If
        Dim dbids As String = createDBIDList()
        If dbids = "" Then
            dbids = "all"
        End If
        Dim programScript As String = """" & cmdLineArgs(0) & """"
        Dim arguments As String = ""
        arguments &= """" & txtBackupFolder.Text & """"
        arguments &= " """ & dbids & """"
        arguments &= " """ & txtUsername.Text & """"
        arguments &= " """ & txtPassword.Text & """"
        arguments &= " """ & txtServer.Text & """"

        If ckbDetectProxy.Checked Then
            arguments &= " ""1"""
        Else
            arguments &= " ""0"""
        End If
        If ckbDateFolders.Checked Then
            arguments &= " ""1"""
        Else
            arguments &= " ""0"""
        End If
        If ckbAppFolders.Checked Then
            arguments &= " ""1"""
        Else
            arguments &= " ""0"""
        End If
        arguments &= " """ & cmbAttachments.SelectedIndex & """"

        frmCommandLine.txtArguments.Text = arguments
        frmCommandLine.txtProgramScript.Text = programScript
        frmCommandLine.ShowDialog()

    End Sub
End Class


