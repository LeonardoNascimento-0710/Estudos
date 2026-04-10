<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmInicio
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInicio))
        Button1 = New Button()
        BtnErp = New Button()
        Panel1 = New Panel()
        Button3 = New Button()
        Panel2 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(23, 30)
        Button1.Name = "Button1"
        Button1.Size = New Size(104, 55)
        Button1.TabIndex = 0
        Button1.Text = "CAIXA"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' BtnErp
        ' 
        BtnErp.Location = New Point(103, 27)
        BtnErp.Name = "BtnErp"
        BtnErp.Size = New Size(104, 55)
        BtnErp.TabIndex = 1
        BtnErp.Text = "RETAGUARDA"
        BtnErp.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(Button3)
        Panel1.Controls.Add(Button1)
        Panel1.Location = New Point(31, 43)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(317, 118)
        Panel1.TabIndex = 2
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(177, 30)
        Button3.Name = "Button3"
        Button3.Size = New Size(115, 55)
        Button3.TabIndex = 1
        Button3.Text = "CONFIGURAÇÕES"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.White
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(BtnErp)
        Panel2.Location = New Point(31, 184)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(317, 118)
        Panel2.TabIndex = 3
        ' 
        ' FrmInicio
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(134), CByte(29), CByte(29))
        ClientSize = New Size(380, 351)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        Name = "FrmInicio"
        StartPosition = FormStartPosition.CenterScreen
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents BtnErp As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Panel2 As Panel

End Class
