Public Class FrmInicio
    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#Region "Botões"

    Private Sub BtnErp_Click(sender As Object, e As EventArgs) Handles BtnErp.Click
        Dim frm As New FrmRetaguarda
        frm.Show()
    End Sub

#End Region


End Class
