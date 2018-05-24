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
        Me.lblPassword = New System.Windows.Forms.Label()
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
        Me.ckbSSO = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ckbAppFolders = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(19, 24)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(120, 20)
        Me.txtUsername.TabIndex = 0
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(22, 5)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(110, 13)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "QuickBase Username"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(160, 5)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(108, 13)
        Me.lblPassword.TabIndex = 3
        Me.lblPassword.Text = "QuickBase Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(157, 24)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(120, 20)
        Me.txtPassword.TabIndex = 2
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(296, 5)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(93, 13)
        Me.lblServer.TabIndex = 5
        Me.lblServer.Text = "QuickBase Server"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(293, 24)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(237, 20)
        Me.txtServer.TabIndex = 4
        '
        'lblAppToken
        '
        Me.lblAppToken.AutoSize = True
        Me.lblAppToken.Location = New System.Drawing.Point(22, 58)
        Me.lblAppToken.Name = "lblAppToken"
        Me.lblAppToken.Size = New System.Drawing.Size(148, 13)
        Me.lblAppToken.TabIndex = 7
        Me.lblAppToken.Text = "QuickBase Application Token"
        '
        'txtAppToken
        '
        Me.txtAppToken.Location = New System.Drawing.Point(19, 77)
        Me.txtAppToken.Name = "txtAppToken"
        Me.txtAppToken.Size = New System.Drawing.Size(258, 20)
        Me.txtAppToken.TabIndex = 6
        '
        'tvAppsTables
        '
        Me.tvAppsTables.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tvAppsTables.Location = New System.Drawing.Point(21, 176)
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
        Me.btnListTables.Location = New System.Drawing.Point(210, 147)
        Me.btnListTables.Name = "btnListTables"
        Me.btnListTables.Size = New System.Drawing.Size(76, 23)
        Me.btnListTables.TabIndex = 9
        Me.btnListTables.Text = "List Tables"
        Me.btnListTables.UseVisualStyleBackColor = True
        '
        'btnFolder
        '
        Me.btnFolder.Location = New System.Drawing.Point(802, 74)
        Me.btnFolder.Name = "btnFolder"
        Me.btnFolder.Size = New System.Drawing.Size(28, 23)
        Me.btnFolder.TabIndex = 10
        Me.btnFolder.Text = "..."
        Me.btnFolder.UseVisualStyleBackColor = True
        '
        'lblBackupFolder
        '
        Me.lblBackupFolder.AutoSize = True
        Me.lblBackupFolder.Location = New System.Drawing.Point(565, 57)
        Me.lblBackupFolder.Name = "lblBackupFolder"
        Me.lblBackupFolder.Size = New System.Drawing.Size(104, 13)
        Me.lblBackupFolder.TabIndex = 11
        Me.lblBackupFolder.Text = "Folder to Backup To"
        '
        'lstBackup
        '
        Me.lstBackup.FormattingEnabled = True
        Me.lstBackup.Location = New System.Drawing.Point(433, 177)
        Me.lstBackup.Name = "lstBackup"
        Me.lstBackup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstBackup.Size = New System.Drawing.Size(397, 576)
        Me.lstBackup.TabIndex = 12
        Me.lstBackup.Visible = False
        '
        'btnAddToBackupList
        '
        Me.btnAddToBackupList.Location = New System.Drawing.Point(396, 388)
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
        Me.btnBackup.Location = New System.Drawing.Point(559, 148)
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
        Me.lblAttachments.Location = New System.Drawing.Point(296, 58)
        Me.lblAttachments.Name = "lblAttachments"
        Me.lblAttachments.Size = New System.Drawing.Size(85, 13)
        Me.lblAttachments.TabIndex = 15
        Me.lblAttachments.Text = "File Attachments"
        '
        'cmbAttachments
        '
        Me.cmbAttachments.FormattingEnabled = True
        Me.cmbAttachments.Items.AddRange(New Object() {"Do not download", "Download current revision and list file URL", "Download all revisions and current rev file URL", "Download all revisions and all file URLs"})
        Me.cmbAttachments.Location = New System.Drawing.Point(292, 76)
        Me.cmbAttachments.Name = "cmbAttachments"
        Me.cmbAttachments.Size = New System.Drawing.Size(238, 21)
        Me.cmbAttachments.TabIndex = 16
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.Enabled = False
        Me.txtBackupFolder.Location = New System.Drawing.Point(559, 76)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(237, 20)
        Me.txtBackupFolder.TabIndex = 17
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(396, 436)
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
        Me.lblBackup.Location = New System.Drawing.Point(439, 152)
        Me.lblBackup.Name = "lblBackup"
        Me.lblBackup.Size = New System.Drawing.Size(91, 13)
        Me.lblBackup.TabIndex = 19
        Me.lblBackup.Text = "Tables to Backup"
        Me.lblBackup.Visible = False
        '
        'lblTables
        '
        Me.lblTables.AutoSize = True
        Me.lblTables.Location = New System.Drawing.Point(25, 152)
        Me.lblTables.Name = "lblTables"
        Me.lblTables.Size = New System.Drawing.Size(135, 13)
        Me.lblTables.TabIndex = 20
        Me.lblTables.Text = "Tables you have access to"
        '
        'ckbDateFolders
        '
        Me.ckbDateFolders.AutoSize = True
        Me.ckbDateFolders.Location = New System.Drawing.Point(559, 105)
        Me.ckbDateFolders.Name = "ckbDateFolders"
        Me.ckbDateFolders.Size = New System.Drawing.Size(188, 17)
        Me.ckbDateFolders.TabIndex = 21
        Me.ckbDateFolders.Text = "Create new subfolder for each day"
        Me.ckbDateFolders.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(19, 110)
        Me.pb.Maximum = 1000
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(511, 23)
        Me.pb.TabIndex = 22
        Me.pb.Visible = False
        '
        'ckbDetectProxy
        '
        Me.ckbDetectProxy.AutoSize = True
        Me.ckbDetectProxy.Location = New System.Drawing.Point(642, 24)
        Me.ckbDetectProxy.Name = "ckbDetectProxy"
        Me.ckbDetectProxy.Size = New System.Drawing.Size(188, 17)
        Me.ckbDetectProxy.TabIndex = 23
        Me.ckbDetectProxy.Text = "Automatically detect proxy settings"
        Me.ckbDetectProxy.UseVisualStyleBackColor = True
        '
        'ckbSSO
        '
        Me.ckbSSO.AutoSize = True
        Me.ckbSSO.Location = New System.Drawing.Point(550, 24)
        Me.ckbSSO.Name = "ckbSSO"
        Me.ckbSSO.Size = New System.Drawing.Size(70, 17)
        Me.ckbSSO.TabIndex = 24
        Me.ckbSSO.Text = "Use SSO"
        Me.ckbSSO.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(648, 152)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(0, 13)
        Me.lblProgress.TabIndex = 25
        '
        'ckbAppFolders
        '
        Me.ckbAppFolders.AutoSize = True
        Me.ckbAppFolders.Location = New System.Drawing.Point(559, 124)
        Me.ckbAppFolders.Name = "ckbAppFolders"
        Me.ckbAppFolders.Size = New System.Drawing.Size(199, 17)
        Me.ckbAppFolders.TabIndex = 26
        Me.ckbAppFolders.Text = "Put each application in its own folder"
        Me.ckbAppFolders.UseVisualStyleBackColor = True
        '
        'backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 773)
        Me.Controls.Add(Me.ckbAppFolders)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ckbSSO)
        Me.Controls.Add(Me.ckbDetectProxy)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.ckbDateFolders)
        Me.Controls.Add(Me.lblTables)
        Me.Controls.Add(Me.lblBackup)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.txtBackupFolder)
        Me.Controls.Add(Me.cmbAttachments)
        Me.Controls.Add(Me.lblAttachments)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.btnAddToBackupList)
        Me.Controls.Add(Me.lstBackup)
        Me.Controls.Add(Me.lblBackupFolder)
        Me.Controls.Add(Me.btnFolder)
        Me.Controls.Add(Me.btnListTables)
        Me.Controls.Add(Me.tvAppsTables)
        Me.Controls.Add(Me.lblAppToken)
        Me.Controls.Add(Me.txtAppToken)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtUsername)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "backup"
        Me.Text = "QuNect Backup"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
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
    Friend WithEvents ckbSSO As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RetrieveTheTableReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ckbAppFolders As CheckBox
End Class
