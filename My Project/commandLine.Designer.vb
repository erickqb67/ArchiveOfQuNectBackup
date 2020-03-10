<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCommandLine
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
        Me.txtProgramScript = New System.Windows.Forms.TextBox()
        Me.lblProgramScript = New System.Windows.Forms.Label()
        Me.lblArguments = New System.Windows.Forms.Label()
        Me.txtArguments = New System.Windows.Forms.TextBox()
        Me.btnProgramScript = New System.Windows.Forms.Button()
        Me.btnArguments = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtProgramScript
        '
        Me.txtProgramScript.Location = New System.Drawing.Point(15, 36)
        Me.txtProgramScript.Name = "txtProgramScript"
        Me.txtProgramScript.Size = New System.Drawing.Size(776, 20)
        Me.txtProgramScript.TabIndex = 0
        '
        'lblProgramScript
        '
        Me.lblProgramScript.AutoSize = True
        Me.lblProgramScript.Location = New System.Drawing.Point(12, 12)
        Me.lblProgramScript.Name = "lblProgramScript"
        Me.lblProgramScript.Size = New System.Drawing.Size(78, 13)
        Me.lblProgramScript.TabIndex = 1
        Me.lblProgramScript.Text = "Program/Script"
        '
        'lblArguments
        '
        Me.lblArguments.AutoSize = True
        Me.lblArguments.Location = New System.Drawing.Point(12, 90)
        Me.lblArguments.Name = "lblArguments"
        Me.lblArguments.Size = New System.Drawing.Size(57, 13)
        Me.lblArguments.TabIndex = 3
        Me.lblArguments.Text = "Arguments"
        '
        'txtArguments
        '
        Me.txtArguments.Location = New System.Drawing.Point(15, 109)
        Me.txtArguments.Multiline = True
        Me.txtArguments.Name = "txtArguments"
        Me.txtArguments.Size = New System.Drawing.Size(776, 324)
        Me.txtArguments.TabIndex = 2
        '
        'btnProgramScript
        '
        Me.btnProgramScript.Location = New System.Drawing.Point(108, 6)
        Me.btnProgramScript.Name = "btnProgramScript"
        Me.btnProgramScript.Size = New System.Drawing.Size(203, 25)
        Me.btnProgramScript.TabIndex = 4
        Me.btnProgramScript.Text = "Copy Program/Script to Clipboard"
        Me.btnProgramScript.UseVisualStyleBackColor = True
        '
        'btnArguments
        '
        Me.btnArguments.Location = New System.Drawing.Point(108, 78)
        Me.btnArguments.Name = "btnArguments"
        Me.btnArguments.Size = New System.Drawing.Size(203, 25)
        Me.btnArguments.TabIndex = 5
        Me.btnArguments.Text = "Copy Arguments to Clipboard"
        Me.btnArguments.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(375, 456)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(51, 29)
        Me.btnOk.TabIndex = 6
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'frmCommandLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 497)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnArguments)
        Me.Controls.Add(Me.btnProgramScript)
        Me.Controls.Add(Me.lblArguments)
        Me.Controls.Add(Me.txtArguments)
        Me.Controls.Add(Me.lblProgramScript)
        Me.Controls.Add(Me.txtProgramScript)
        Me.Name = "frmCommandLine"
        Me.Text = "Command Line"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtProgramScript As TextBox
    Friend WithEvents lblProgramScript As Label
    Friend WithEvents lblArguments As Label
    Friend WithEvents txtArguments As TextBox
    Friend WithEvents btnProgramScript As Button
    Friend WithEvents btnArguments As Button
    Friend WithEvents btnOk As Button
End Class
