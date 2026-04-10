<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRetaguarda
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRetaguarda))
        TbErp = New TabControl()
        TbCadatsros = New TabPage()
        TbCads = New TabControl()
        TabPage1 = New TabPage()
        CmbGrupos = New ComboBox()
        CmbFiltroGrp = New ComboBox()
        LblFiltro = New Label()
        LblTipoFiltro = New Label()
        CmbFiltro = New ComboBox()
        TxtFiltro = New TextBox()
        LblGrpProdutos = New Label()
        TxtQtdeProd = New TextBox()
        BtnLoadProds = New Button()
        CmbProdutos = New ComboBox()
        BtnExcluirProd = New Button()
        BtnGravar = New Button()
        DgvProdutos = New DataGridView()
        LblEstoque = New Label()
        TxtVenda = New TextBox()
        LblPrecoV = New Label()
        TxtCusto = New TextBox()
        LblPrecoC = New Label()
        TxtCodProd = New TextBox()
        LblCodProd = New Label()
        LblNomeProd = New Label()
        TabPage2 = New TabPage()
        TbEstoque = New TabPage()
        TbRelatorios = New TabPage()
        BtnLimpar = New Button()
        TbErp.SuspendLayout()
        TbCadatsros.SuspendLayout()
        TbCads.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(DgvProdutos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TbErp
        ' 
        TbErp.Controls.Add(TbCadatsros)
        TbErp.Controls.Add(TbEstoque)
        TbErp.Controls.Add(TbRelatorios)
        TbErp.Location = New Point(12, 12)
        TbErp.Name = "TbErp"
        TbErp.SelectedIndex = 0
        TbErp.Size = New Size(946, 579)
        TbErp.TabIndex = 0
        ' 
        ' TbCadatsros
        ' 
        TbCadatsros.BackColor = Color.White
        TbCadatsros.Controls.Add(TbCads)
        TbCadatsros.Location = New Point(4, 24)
        TbCadatsros.Name = "TbCadatsros"
        TbCadatsros.Padding = New Padding(3)
        TbCadatsros.Size = New Size(938, 551)
        TbCadatsros.TabIndex = 0
        TbCadatsros.Text = "CADASTROS"
        ' 
        ' TbCads
        ' 
        TbCads.Controls.Add(TabPage1)
        TbCads.Controls.Add(TabPage2)
        TbCads.Location = New Point(6, 6)
        TbCads.Name = "TbCads"
        TbCads.SelectedIndex = 0
        TbCads.Size = New Size(926, 539)
        TbCads.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(BtnLimpar)
        TabPage1.Controls.Add(CmbGrupos)
        TabPage1.Controls.Add(CmbFiltroGrp)
        TabPage1.Controls.Add(LblFiltro)
        TabPage1.Controls.Add(LblTipoFiltro)
        TabPage1.Controls.Add(CmbFiltro)
        TabPage1.Controls.Add(TxtFiltro)
        TabPage1.Controls.Add(LblGrpProdutos)
        TabPage1.Controls.Add(TxtQtdeProd)
        TabPage1.Controls.Add(BtnLoadProds)
        TabPage1.Controls.Add(CmbProdutos)
        TabPage1.Controls.Add(BtnExcluirProd)
        TabPage1.Controls.Add(BtnGravar)
        TabPage1.Controls.Add(DgvProdutos)
        TabPage1.Controls.Add(LblEstoque)
        TabPage1.Controls.Add(TxtVenda)
        TabPage1.Controls.Add(LblPrecoV)
        TabPage1.Controls.Add(TxtCusto)
        TabPage1.Controls.Add(LblPrecoC)
        TabPage1.Controls.Add(TxtCodProd)
        TabPage1.Controls.Add(LblCodProd)
        TabPage1.Controls.Add(LblNomeProd)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(918, 511)
        TabPage1.TabIndex = 0
        TabPage1.Text = "PRODUTOS"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' CmbGrupos
        ' 
        CmbGrupos.FormattingEnabled = True
        CmbGrupos.Location = New Point(353, 39)
        CmbGrupos.Name = "CmbGrupos"
        CmbGrupos.Size = New Size(161, 23)
        CmbGrupos.TabIndex = 26
        ' 
        ' CmbFiltroGrp
        ' 
        CmbFiltroGrp.FormattingEnabled = True
        CmbFiltroGrp.Location = New Point(186, 156)
        CmbFiltroGrp.Name = "CmbFiltroGrp"
        CmbFiltroGrp.Size = New Size(161, 23)
        CmbFiltroGrp.TabIndex = 25
        CmbFiltroGrp.Visible = False
        ' 
        ' LblFiltro
        ' 
        LblFiltro.AutoSize = True
        LblFiltro.Location = New Point(186, 138)
        LblFiltro.Name = "LblFiltro"
        LblFiltro.Size = New Size(53, 15)
        LblFiltro.TabIndex = 24
        LblFiltro.Text = "Pesquisa"
        ' 
        ' LblTipoFiltro
        ' 
        LblTipoFiltro.AutoSize = True
        LblTipoFiltro.Location = New Point(23, 138)
        LblTipoFiltro.Name = "LblTipoFiltro"
        LblTipoFiltro.Size = New Size(99, 15)
        LblTipoFiltro.TabIndex = 23
        LblTipoFiltro.Text = "Filtro de pesquisa"
        ' 
        ' CmbFiltro
        ' 
        CmbFiltro.FormattingEnabled = True
        CmbFiltro.Items.AddRange(New Object() {"Grupo", "Custo", "Venda"})
        CmbFiltro.Location = New Point(24, 156)
        CmbFiltro.Name = "CmbFiltro"
        CmbFiltro.Size = New Size(156, 23)
        CmbFiltro.TabIndex = 22
        ' 
        ' TxtFiltro
        ' 
        TxtFiltro.Enabled = False
        TxtFiltro.Location = New Point(187, 156)
        TxtFiltro.Name = "TxtFiltro"
        TxtFiltro.Size = New Size(160, 23)
        TxtFiltro.TabIndex = 21
        ' 
        ' LblGrpProdutos
        ' 
        LblGrpProdutos.AutoSize = True
        LblGrpProdutos.Location = New Point(351, 21)
        LblGrpProdutos.Name = "LblGrpProdutos"
        LblGrpProdutos.Size = New Size(40, 15)
        LblGrpProdutos.TabIndex = 19
        LblGrpProdutos.Text = "Grupo"
        ' 
        ' TxtQtdeProd
        ' 
        TxtQtdeProd.Location = New Point(268, 82)
        TxtQtdeProd.Name = "TxtQtdeProd"
        TxtQtdeProd.Size = New Size(79, 23)
        TxtQtdeProd.TabIndex = 18
        ' 
        ' BtnLoadProds
        ' 
        BtnLoadProds.Location = New Point(24, 193)
        BtnLoadProds.Name = "BtnLoadProds"
        BtnLoadProds.Size = New Size(102, 23)
        BtnLoadProds.TabIndex = 17
        BtnLoadProds.Text = "Carregar produtos"
        BtnLoadProds.UseVisualStyleBackColor = True
        ' 
        ' CmbProdutos
        ' 
        CmbProdutos.FormattingEnabled = True
        CmbProdutos.Location = New Point(24, 39)
        CmbProdutos.Name = "CmbProdutos"
        CmbProdutos.Size = New Size(323, 23)
        CmbProdutos.TabIndex = 16
        ' 
        ' BtnExcluirProd
        ' 
        BtnExcluirProd.Location = New Point(131, 111)
        BtnExcluirProd.Name = "BtnExcluirProd"
        BtnExcluirProd.Size = New Size(101, 23)
        BtnExcluirProd.TabIndex = 15
        BtnExcluirProd.Text = "Excluir Produto"
        BtnExcluirProd.UseVisualStyleBackColor = True
        ' 
        ' BtnGravar
        ' 
        BtnGravar.Location = New Point(22, 111)
        BtnGravar.Name = "BtnGravar"
        BtnGravar.Size = New Size(95, 23)
        BtnGravar.TabIndex = 14
        BtnGravar.Text = "Gravar dados"
        BtnGravar.UseVisualStyleBackColor = True
        ' 
        ' DgvProdutos
        ' 
        DgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvProdutos.Location = New Point(24, 222)
        DgvProdutos.Name = "DgvProdutos"
        DgvProdutos.Size = New Size(877, 283)
        DgvProdutos.TabIndex = 12
        ' 
        ' LblEstoque
        ' 
        LblEstoque.AutoSize = True
        LblEstoque.Location = New Point(264, 64)
        LblEstoque.Name = "LblEstoque"
        LblEstoque.Size = New Size(78, 15)
        LblEstoque.TabIndex = 8
        LblEstoque.Text = "Qtde Estoque"
        ' 
        ' TxtVenda
        ' 
        TxtVenda.Location = New Point(186, 82)
        TxtVenda.Name = "TxtVenda"
        TxtVenda.Size = New Size(75, 23)
        TxtVenda.TabIndex = 7
        ' 
        ' LblPrecoV
        ' 
        LblPrecoV.AutoSize = True
        LblPrecoV.Location = New Point(186, 64)
        LblPrecoV.Name = "LblPrecoV"
        LblPrecoV.Size = New Size(39, 15)
        LblPrecoV.TabIndex = 6
        LblPrecoV.Text = "Venda"
        ' 
        ' TxtCusto
        ' 
        TxtCusto.Location = New Point(104, 82)
        TxtCusto.Name = "TxtCusto"
        TxtCusto.Size = New Size(75, 23)
        TxtCusto.TabIndex = 5
        ' 
        ' LblPrecoC
        ' 
        LblPrecoC.AutoSize = True
        LblPrecoC.Location = New Point(104, 64)
        LblPrecoC.Name = "LblPrecoC"
        LblPrecoC.Size = New Size(38, 15)
        LblPrecoC.TabIndex = 4
        LblPrecoC.Text = "Custo"
        ' 
        ' TxtCodProd
        ' 
        TxtCodProd.ImeMode = ImeMode.Off
        TxtCodProd.Location = New Point(24, 82)
        TxtCodProd.MaxLength = 14
        TxtCodProd.Name = "TxtCodProd"
        TxtCodProd.Size = New Size(75, 23)
        TxtCodProd.TabIndex = 3
        ' 
        ' LblCodProd
        ' 
        LblCodProd.AutoSize = True
        LblCodProd.Location = New Point(22, 64)
        LblCodProd.Name = "LblCodProd"
        LblCodProd.Size = New Size(46, 15)
        LblCodProd.TabIndex = 1
        LblCodProd.Text = "Codigo"
        ' 
        ' LblNomeProd
        ' 
        LblNomeProd.AutoSize = True
        LblNomeProd.Location = New Point(22, 21)
        LblNomeProd.Name = "LblNomeProd"
        LblNomeProd.Size = New Size(50, 15)
        LblNomeProd.TabIndex = 0
        LblNomeProd.Text = "Produto"
        ' 
        ' TabPage2
        ' 
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(918, 511)
        TabPage2.TabIndex = 1
        TabPage2.Text = "CLIENTES"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' TbEstoque
        ' 
        TbEstoque.BackColor = Color.White
        TbEstoque.Location = New Point(4, 24)
        TbEstoque.Name = "TbEstoque"
        TbEstoque.Padding = New Padding(3)
        TbEstoque.Size = New Size(938, 551)
        TbEstoque.TabIndex = 1
        TbEstoque.Text = "ESTOQUE"
        ' 
        ' TbRelatorios
        ' 
        TbRelatorios.BackColor = Color.White
        TbRelatorios.Location = New Point(4, 24)
        TbRelatorios.Name = "TbRelatorios"
        TbRelatorios.Size = New Size(938, 551)
        TbRelatorios.TabIndex = 2
        TbRelatorios.Text = "RELATÓRIOS"
        ' 
        ' BtnLimpar
        ' 
        BtnLimpar.Location = New Point(246, 111)
        BtnLimpar.Name = "BtnLimpar"
        BtnLimpar.Size = New Size(101, 23)
        BtnLimpar.TabIndex = 27
        BtnLimpar.Text = "Limpar campos"
        BtnLimpar.UseVisualStyleBackColor = True
        ' 
        ' FrmRetaguarda
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(134), CByte(29), CByte(29))
        ClientSize = New Size(970, 603)
        Controls.Add(TbErp)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        Name = "FrmRetaguarda"
        StartPosition = FormStartPosition.CenterScreen
        TbErp.ResumeLayout(False)
        TbCadatsros.ResumeLayout(False)
        TbCads.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(DgvProdutos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TbCads As TabControl
    Friend WithEvents TbRelatorios As TabPage
    Friend WithEvents TbEstoque As TabPage
    Friend WithEvents TbErp As TabControl
    Friend WithEvents TbCadatsros As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TxtCodProd As TextBox
    Friend WithEvents LblCodProd As Label
    Friend WithEvents LblNomeProd As Label
    Friend WithEvents DgvProdutos As DataGridView
    Friend WithEvents LblEstoque As Label
    Friend WithEvents TxtVenda As TextBox
    Friend WithEvents LblPrecoV As Label
    Friend WithEvents TxtCusto As TextBox
    Friend WithEvents LblPrecoC As Label
    Friend WithEvents BtnExcluirProd As Button
    Friend WithEvents BtnGravar As Button
    Friend WithEvents CmbProdutos As ComboBox
    Friend WithEvents BtnLoadProds As Button
    Friend WithEvents TxtQtdeProd As TextBox
    Friend WithEvents LblGrpProdutos As Label
    Friend WithEvents CmbFiltro As ComboBox
    Friend WithEvents TxtFiltro As TextBox
    Friend WithEvents LblFiltro As Label
    Friend WithEvents LblTipoFiltro As Label
    Friend WithEvents CmbFiltroGrp As ComboBox
    Friend WithEvents CmbGrupos As ComboBox
    Friend WithEvents BtnLimpar As Button
End Class
