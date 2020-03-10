Public Class frmCommandLine
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub

    Private Sub btnProgramScript_Click(sender As Object, e As EventArgs) Handles btnProgramScript.Click
        My.Computer.Clipboard.SetText(txtProgramScript.Text)
    End Sub

    Private Sub btnArguments_Click(sender As Object, e As EventArgs) Handles btnArguments.Click
        My.Computer.Clipboard.SetText(txtArguments.Text)
    End Sub

    Private Sub frmCommandLine_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        txtArguments.Width = Me.ClientSize.Width - 2 * txtArguments.Left
        txtProgramScript.Width = Me.ClientSize.Width - 2 * txtArguments.Left
        txtArguments.Height = Me.ClientSize.Height - (btnArguments.Top + btnArguments.Height + (3 * txtArguments.Left) + btnOk.Height)
        btnOk.Top = Me.ClientSize.Height - btnOk.Height - txtArguments.Left
        btnOk.Left = (Me.ClientSize.Width - btnOk.Width) / 2
    End Sub
End Class