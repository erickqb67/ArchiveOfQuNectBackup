<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class backup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(backup))
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.lblAppToken = New System.Windows.Forms.Label()
        Me.txtAppToken = New System.Windows.Forms.TextBox()
        Me.tvAppsTables = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RetrieveTheTableReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnListTables = New System.Windows.Forms.Button()
        Me.btnFolder = New System.Windows.Forms.Button()
        Me.lblBackupFolder = New System.Windows.Forms.Label()
        Me.lstBackup = New System.Windows.Forms.ListBox()
        Me.btnAddToBackupList = New System.Windows.Forms.Button()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.lblAttachments = New System.Windows.Forms.Label()
        Me.cmbAttachments = New System.Windows.Forms.ComboBox()
        Me.txtBackupFolder = New System.Windows.Forms.TextBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblBackup = New System.Windows.Forms.Label()
        Me.lblTables = New System.Windows.Forms.Label()
        Me.ckbDateFolders = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.ckbDetectProxy = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ckbAppFolders = New System.Windows.Forms.CheckBox()
        Me.cmbPassword = New System.Windows.Forms.ComboBox()
        Me.btnAppToken = New System.Windows.Forms.Button()
        Me.btnUserToken = New System.Windows.Forms.Button()
        Me.tabs = New System.Windows.Forms.TabControl()
        Me.tabAuth = New System.Windows.Forms.TabPage()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.tabBackup = New System.Windows.Forms.TabPage()
        Me.cmbAttachmentFolders = New System.Windows.Forms.ComboBox()
        Me.ckbFilesByField = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.tabs.SuspendLayout()
        Me.tabAuth.SuspendLayout()
        Me.tabBackup.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(13, 34)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(197, 20)
        Me.txtUsername.TabIndex = 0
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(16, 15)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(110, 13)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "QuickBase Username"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(13, 96)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(184, 20)
        Me.txtPassword.TabIndex = 2
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(12, 186)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(93, 13)
        Me.lblServer.TabIndex = 5
        Me.lblServer.Text = "QuickBase Server"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(13, 204)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(203, 20)
        Me.txtServer.TabIndex = 4
        '
        'lblAppToken
        '
        Me.lblAppToken.AutoSize = True
        Me.lblAppToken.Location = New System.Drawing.Point(16, 131)
        Me.lblAppToken.Name = "lblAppToken"
        Me.lblAppToken.Size = New System.Drawing.Size(148, 13)
        Me.lblAppToken.TabIndex = 7
        Me.lblAppToken.Text = "QuickBase Application Token"
        '
        'txtAppToken
        '
        Me.txtAppToken.Location = New System.Drawing.Point(13, 150)
        Me.txtAppToken.Name = "txtAppToken"
        Me.txtAppToken.Size = New System.Drawing.Size(258, 20)
        Me.txtAppToken.TabIndex = 6
        '
        'tvAppsTables
        '
        Me.tvAppsTables.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tvAppsTables.Location = New System.Drawing.Point(5, 137)
        Me.tvAppsTables.Name = "tvAppsTables"
        Me.tvAppsTables.Size = New System.Drawing.Size(369, 577)
        Me.tvAppsTables.TabIndex = 8
        Me.tvAppsTables.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RetrieveTheTableReportsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(355, 26)
        '
        'RetrieveTheTableReportsToolStripMenuItem
        '
        Me.RetrieveTheTableReportsToolStripMenuItem.Name = "RetrieveTheTableReportsToolStripMenuItem"
        Me.RetrieveTheTableReportsToolStripMenuItem.Size = New System.Drawing.Size(354, 22)
        Me.RetrieveTheTableReportsToolStripMenuItem.Text = "Retrieve the table reports for the selected application."
        '
        'btnListTables
        '
        Me.btnListTables.Location = New System.Drawing.Point(194, 108)
        Me.btnListTables.Name = "btnListTables"
        Me.btnListTables.Size = New System.Drawing.Size(76, 23)
        Me.btnListTables.TabIndex = 9
        Me.btnListTables.Text = "List Tables"
        Me.btnListTables.UseVisualStyleBackColor = True
        '
        'btnFolder
        '
        Me.btnFolder.Location = New System.Drawing.Point(506, 25)
        Me.btnFolder.Name = "btnFolder"
        Me.btnFolder.Size = New System.Drawing.Size(28, 23)
        Me.btnFolder.TabIndex = 10
        Me.btnFolder.Text = "..."
        Me.btnFolder.UseVisualStyleBackColor = True
        '
        'lblBackupFolder
        '
        Me.lblBackupFolder.AutoSize = True
        Me.lblBackupFolder.Location = New System.Drawing.Point(269, 8)
        Me.lblBackupFolder.Name = "lblBackupFolder"
        Me.lblBackupFolder.Size = New System.Drawing.Size(104, 13)
        Me.lblBackupFolder.TabIndex = 11
        Me.lblBackupFolder.Text = "Folder to Backup To"
        '
        'lstBackup
        '
        Me.lstBackup.AllowDrop = True
        Me.lstBackup.FormattingEnabled = True
        Me.lstBackup.Location = New System.Drawing.Point(417, 138)
        Me.lstBackup.Name = "lstBackup"
        Me.lstBackup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstBackup.Size = New System.Drawing.Size(397, 576)
        Me.lstBackup.TabIndex = 12
        Me.lstBackup.Visible = False
        '
        'btnAddToBackupList
        '
        Me.btnAddToBackupList.Location = New System.Drawing.Point(380, 334)
        Me.btnAddToBackupList.Name = "btnAddToBackupList"
        Me.btnAddToBackupList.Size = New System.Drawing.Size(31, 24)
        Me.btnAddToBackupList.TabIndex = 13
        Me.btnAddToBackupList.Text = "->"
        Me.ToolTip1.SetToolTip(Me.btnAddToBackupList, "Hold down the shift key and click me to move over all tables, without the shift k" &
        "ey I add one table per click.")
        Me.btnAddToBackupList.UseVisualStyleBackColor = True
        Me.btnAddToBackupList.Visible = False
        '
        'btnBackup
        '
        Me.btnBackup.Location = New System.Drawing.Point(543, 109)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(83, 23)
        Me.btnBackup.TabIndex = 14
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.UseVisualStyleBackColor = True
        Me.btnBackup.Visible = False
        '
        'lblAttachments
        '
        Me.lblAttachments.AutoSize = True
        Me.lblAttachments.Location = New System.Drawing.Point(10, 9)
        Me.lblAttachments.Name = "lblAttachments"
        Me.lblAttachments.Size = New System.Drawing.Size(85, 13)
        Me.lblAttachments.TabIndex = 15
        Me.lblAttachments.Text = "File Attachments"
        '
        'cmbAttachments
        '
        Me.cmbAttachments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAttachments.FormattingEnabled = True
        Me.cmbAttachments.Items.AddRange(New Object() {"Do not download", "Download current revision and list file URL", "Download all revisions and current rev file URL", "Download all revisions and all file URLs"})
        Me.cmbAttachments.Location = New System.Drawing.Point(6, 27)
        Me.cmbAttachments.Name = "cmbAttachments"
        Me.cmbAttachments.Size = New System.Drawing.Size(238, 21)
        Me.cmbAttachments.TabIndex = 16
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.Enabled = False
        Me.txtBackupFolder.Location = New System.Drawing.Point(263, 27)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(237, 20)
        Me.txtBackupFolder.TabIndex = 17
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(380, 364)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(31, 24)
        Me.btnRemove.TabIndex = 18
        Me.btnRemove.Text = "<-"
        Me.ToolTip1.SetToolTip(Me.btnRemove, "Hold down the shift key and click me to remove all the tables, without the shift " &
        "key I remove one table per click.")
        Me.btnRemove.UseVisualStyleBackColor = True
        Me.btnRemove.Visible = False
        '
        'lblBackup
        '
        Me.lblBackup.AutoSize = True
        Me.lblBackup.Location = New System.Drawing.Point(423, 113)
        Me.lblBackup.Name = "lblBackup"
        Me.lblBackup.Size = New System.Drawing.Size(91, 13)
        Me.lblBackup.TabIndex = 19
        Me.lblBackup.Text = "Tables to Backup"
        Me.lblBackup.Visible = False
        '
        'lblTables
        '
        Me.lblTables.AutoSize = True
        Me.lblTables.Location = New System.Drawing.Point(9, 113)
        Me.lblTables.Name = "lblTables"
        Me.lblTables.Size = New System.Drawing.Size(135, 13)
        Me.lblTables.TabIndex = 20
        Me.lblTables.Text = "Tables you have access to"
        '
        'ckbDateFolders
        '
        Me.ckbDateFolders.AutoSize = True
        Me.ckbDateFolders.Location = New System.Drawing.Point(543, 10)
        Me.ckbDateFolders.Name = "ckbDateFolders"
        Me.ckbDateFolders.Size = New System.Drawing.Size(188, 17)
        Me.ckbDateFolders.TabIndex = 21
        Me.ckbDateFolders.Text = "Create new subfolder for each day"
        Me.ckbDateFolders.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(3, 71)
        Me.pb.Maximum = 1000
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(511, 23)
        Me.pb.TabIndex = 22
        Me.pb.Visible = False
        '
        'ckbDetectProxy
        '
        Me.ckbDetectProxy.AutoSize = True
        Me.ckbDetectProxy.Location = New System.Drawing.Point(15, 235)
        Me.ckbDetectProxy.Name = "ckbDetectProxy"
        Me.ckbDetectProxy.Size = New System.Drawing.Size(188, 17)
        Me.ckbDetectProxy.TabIndex = 23
        Me.ckbDetectProxy.Text = "Automatically detect proxy settings"
        Me.ckbDetectProxy.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(632, 113)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(0, 13)
        Me.lblProgress.TabIndex = 25
        '
        'ckbAppFolders
        '
        Me.ckbAppFolders.AutoSize = True
        Me.ckbAppFolders.Location = New System.Drawing.Point(543, 29)
        Me.ckbAppFolders.Name = "ckbAppFolders"
        Me.ckbAppFolders.Size = New System.Drawing.Size(199, 17)
        Me.ckbAppFolders.TabIndex = 26
        Me.ckbAppFolders.Text = "Put each application in its own folder"
        Me.ckbAppFolders.UseVisualStyleBackColor = True
        '
        'cmbPassword
        '
        Me.cmbPassword.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPassword.FormattingEnabled = True
        Me.cmbPassword.Items.AddRange(New Object() {"Please choose...", "QuickBase Password", "QuickBase User Token"})
        Me.cmbPassword.Location = New System.Drawing.Point(13, 69)
        Me.cmbPassword.Name = "cmbPassword"
        Me.cmbPassword.Size = New System.Drawing.Size(184, 21)
        Me.cmbPassword.TabIndex = 47
        '
        'btnAppToken
        '
        Me.btnAppToken.Location = New System.Drawing.Point(165, 127)
        Me.btnAppToken.Name = "btnAppToken"
        Me.btnAppToken.Size = New System.Drawing.Size(19, 20)
        Me.btnAppToken.TabIndex = 80
        Me.btnAppToken.Text = "?"
        Me.btnAppToken.UseVisualStyleBackColor = True
        '
        'btnUserToken
        '
        Me.btnUserToken.Location = New System.Drawing.Point(198, 70)
        Me.btnUserToken.Name = "btnUserToken"
        Me.btnUserToken.Size = New System.Drawing.Size(19, 20)
        Me.btnUserToken.TabIndex = 81
        Me.btnUserToken.Text = "?"
        Me.btnUserToken.UseVisualStyleBackColor = True
        '
        'tabs
        '
        Me.tabs.Controls.Add(Me.tabAuth)
        Me.tabs.Controls.Add(Me.tabBackup)
        Me.tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabs.Location = New System.Drawing.Point(0, 0)
        Me.tabs.Name = "tabs"
        Me.tabs.SelectedIndex = 0
        Me.tabs.Size = New System.Drawing.Size(992, 876)
        Me.tabs.TabIndex = 82
        '
        'tabAuth
        '
        Me.tabAuth.Controls.Add(Me.btnTest)
        Me.tabAuth.Controls.Add(Me.txtPassword)
        Me.tabAuth.Controls.Add(Me.txtUsername)
        Me.tabAuth.Controls.Add(Me.btnUserToken)
        Me.tabAuth.Controls.Add(Me.lblUsername)
        Me.tabAuth.Controls.Add(Me.btnAppToken)
        Me.tabAuth.Controls.Add(Me.txtServer)
        Me.tabAuth.Controls.Add(Me.cmbPassword)
        Me.tabAuth.Controls.Add(Me.lblServer)
        Me.tabAuth.Controls.Add(Me.txtAppToken)
        Me.tabAuth.Controls.Add(Me.lblAppToken)
        Me.tabAuth.Controls.Add(Me.ckbDetectProxy)
        Me.tabAuth.Location = New System.Drawing.Point(4, 22)
        Me.tabAuth.Name = "tabAuth"
        Me.tabAuth.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAuth.Size = New System.Drawing.Size(984, 850)
        Me.tabAuth.TabIndex = 0
        Me.tabAuth.Text = "Authentication"
        Me.tabAuth.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(13, 258)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(111, 23)
        Me.btnTest.TabIndex = 82
        Me.btnTest.Text = "Test Connection"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'tabBackup
        '
        Me.tabBackup.Controls.Add(Me.cmbAttachmentFolders)
        Me.tabBackup.Controls.Add(Me.ckbFilesByField)
        Me.tabBackup.Controls.Add(Me.btnRemove)
        Me.tabBackup.Controls.Add(Me.cmbAttachments)
        Me.tabBackup.Controls.Add(Me.btnAddToBackupList)
        Me.tabBackup.Controls.Add(Me.tvAppsTables)
        Me.tabBackup.Controls.Add(Me.ckbAppFolders)
        Me.tabBackup.Controls.Add(Me.btnListTables)
        Me.tabBackup.Controls.Add(Me.lblProgress)
        Me.tabBackup.Controls.Add(Me.btnFolder)
        Me.tabBackup.Controls.Add(Me.pb)
        Me.tabBackup.Controls.Add(Me.lblBackupFolder)
        Me.tabBackup.Controls.Add(Me.ckbDateFolders)
        Me.tabBackup.Controls.Add(Me.lstBackup)
        Me.tabBackup.Controls.Add(Me.lblTables)
        Me.tabBackup.Controls.Add(Me.lblAttachments)
        Me.tabBackup.Controls.Add(Me.lblBackup)
        Me.tabBackup.Controls.Add(Me.btnBackup)
        Me.tabBackup.Controls.Add(Me.txtBackupFolder)
        Me.tabBackup.Location = New System.Drawing.Point(4, 22)
        Me.tabBackup.Name = "tabBackup"
        Me.tabBackup.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBackup.Size = New System.Drawing.Size(984, 850)
        Me.tabBackup.TabIndex = 1
        Me.tabBackup.Text = "Backup"
        Me.tabBackup.UseVisualStyleBackColor = True
        '
        'cmbAttachmentFolders
        '
        Me.cmbAttachmentFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAttachmentFolders.FormattingEnabled = True
        Me.cmbAttachmentFolders.Location = New System.Drawing.Point(541, 72)
        Me.cmbAttachmentFolders.Name = "cmbAttachmentFolders"
        Me.cmbAttachmentFolders.Size = New System.Drawing.Size(285, 21)
        Me.cmbAttachmentFolders.TabIndex = 28
        Me.cmbAttachmentFolders.Visible = False
        '
        'ckbFilesByField
        '
        Me.ckbFilesByField.AutoSize = True
        Me.ckbFilesByField.Location = New System.Drawing.Point(543, 49)
        Me.ckbFilesByField.Name = "ckbFilesByField"
        Me.ckbFilesByField.Size = New System.Drawing.Size(290, 17)
        Me.ckbFilesByField.TabIndex = 27
        Me.ckbFilesByField.Text = "Put attachments in a folder named by the field values of:"
        Me.ckbFilesByField.UseVisualStyleBackColor = True
        Me.ckbFilesByField.Visible = False
        '
        'backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 876)
        Me.Controls.Add(Me.tabs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "backup"
        Me.Text = "QuNect Backup"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.tabs.ResumeLayout(False)
        Me.tabAuth.ResumeLayout(False)
        Me.tabAuth.PerformLayout()
        Me.tabBackup.ResumeLayout(False)
        Me.tabBackup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents lblAppToken As System.Windows.Forms.Label
    Friend WithEvents txtAppToken As System.Windows.Forms.TextBox
    Friend WithEvents tvAppsTables As System.Windows.Forms.TreeView
    Friend WithEvents btnListTables As System.Windows.Forms.Button
    Friend WithEvents btnFolder As System.Windows.Forms.Button
    Friend WithEvents lblBackupFolder As System.Windows.Forms.Label
    Friend WithEvents lstBackup As System.Windows.Forms.ListBox
    Friend WithEvents btnAddToBackupList As System.Windows.Forms.Button
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents lblAttachments As System.Windows.Forms.Label
    Friend WithEvents cmbAttachments As System.Windows.Forms.ComboBox
    Friend WithEvents txtBackupFolder As System.Windows.Forms.TextBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblBackup As System.Windows.Forms.Label
    Friend WithEvents lblTables As System.Windows.Forms.Label
    Friend WithEvents ckbDateFolders As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents ckbDetectProxy As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RetrieveTheTableReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ckbAppFolders As CheckBox
    Friend WithEvents cmbPassword As ComboBox
    Friend WithEvents btnAppToken As Button
    Friend WithEvents btnUserToken As Button
    Friend WithEvents tabs As TabControl
    Friend WithEvents tabAuth As TabPage
    Friend WithEvents tabBackup As TabPage
    Friend WithEvents btnTest As Button
    Friend WithEvents cmbAttachmentFolders As ComboBox
    Friend WithEvents ckbFilesByField As CheckBox
End Class
