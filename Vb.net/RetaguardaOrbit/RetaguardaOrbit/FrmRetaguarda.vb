Imports System.Drawing.Printing
Imports System.IO
Imports System.Windows.Forms.VisualStyles
Imports MySql.Data.MySqlClient
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports iText.IO.Image

Imports ImgWin = System.Drawing.Image
Imports ImgPdf = iText.Layout.Element.Image

Public Class FrmRetaguarda

    Private Sub FrmRetaguarda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarCmbProdutos()
        CarregarCmbGrupos()
    End Sub

#Region "Produtos"


#Region "Design e containers"
    Private Sub BtnGravar_Click(sender As Object, e As EventArgs) Handles BtnGravar.Click

        If ValidarCampos() = False Then Exit Sub

        SalvarProduto()

    End Sub
    Private Sub BtnSelecionarImagem_Click(sender As Object, e As EventArgs) Handles BtnSelecionarImagem.Click

        Dim ofd As New OpenFileDialog

        ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png"

        If ofd.ShowDialog() = DialogResult.OK Then
            PicProds.Image = ImgWin.FromFile(ofd.FileName)
            PicProds.Tag = ofd.FileName
        End If

    End Sub
    Private Sub TxtCodProd_Leave(sender As Object, e As EventArgs) Handles TxtCodProd.Leave

        If ConfirmaCodigo() = True Then
            MessageBox.Show("O código já pertence a um produto!")
            TxtCodProd.Clear()
            TxtCodProd.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub BtnLimpar_Click(sender As Object, e As EventArgs) Handles BtnLimpar.Click
        LimparDados()
    End Sub

    Private Sub CmbFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFiltro.SelectedIndexChanged

        If CmbFiltro.Text = "Grupo" Then
            LblFiltro.Text = "Grupo:"
            CarregarCmbFiltroGrupos()
            CmbTipoFiltro.Visible = True
            TxtFiltro.Visible = False
        End If

        If CmbFiltro.Text = "Tipo estoque" Then
            LblFiltro.Text = "Tipo estoque:"
            CarregarCmbFiltroTiposEtq()
            CmbTipoFiltro.Visible = True
            TxtFiltro.Visible = False
        End If

        If CmbFiltro.Text = "Custo" Then
            LblFiltro.Text = "Custo:"
            CmbTipoFiltro.Visible = False
            TxtFiltro.Enabled = True
            TxtFiltro.Visible = True
        End If

        If CmbFiltro.Text = "Venda" Then
            LblFiltro.Text = "Venda:"
            CmbTipoFiltro.Visible = False
            TxtFiltro.Enabled = True
            TxtFiltro.Visible = True
        End If

    End Sub

    Private Sub dgvDados_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) _
Handles DgvProdutos.CellEndEdit

        AtualizarRegistro(e.RowIndex)

    End Sub

    Private Sub BtnLoadProds_Click(sender As Object, e As EventArgs) Handles BtnLoadProds.Click

        CarregarDgvProdutos()
        LimparDados()
        DgvProdutos.AllowUserToAddRows = False

    End Sub

    Private Sub CmbProdutos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbProdutos.SelectedIndexChanged

        If CmbProdutos.SelectedValue IsNot Nothing Then
            CarregarProdutoPorCodigo()
            TxtCodProd.Enabled = False
        Else
            TxtCodProd.Enabled = True
        End If

    End Sub
    Private Sub BtnExcluirProd_Click(sender As Object, e As EventArgs) Handles BtnExcluirProd.Click

        If TxtCodProd.Text = "" Then
            MessageBox.Show("Selecione um produto para excluir.")
            Exit Sub
        End If
        ExcluirProduto(TxtCodProd.Text)

    End Sub

    Private Sub TxtCusto_Leave(sender As Object, e As EventArgs) Handles TxtCusto.Leave
        FormatarDecimal(TxtCusto, "custo")
    End Sub

    Private Sub TxtVenda_Leave(sender As Object, e As EventArgs) Handles TxtVenda.Leave
        FormatarDecimal(TxtVenda, "venda")
    End Sub

    Private Sub TxtQtdeProd_Leave(sender As Object, e As EventArgs) Handles TxtQtdeProd.Leave

        If CmbTipoEtq.Text = "KG" Then
            FormatarDecimal(TxtQtdeProd, "KG")
        Else
            FormataInteiros(TxtQtdeProd)
        End If

    End Sub
    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
        ImprimirProdutos()
    End Sub

#End Region



#Region "Funções"

    Private Sub FormataInteiros(txt As TextBox)

        Dim valor As String = txt.Text.Trim()

        If valor = "" Then Exit Sub

        Dim numero As Decimal

        If Decimal.TryParse(valor, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, numero) Then

            If numero = Math.Truncate(numero) Then
                txt.Text = Convert.ToInt32(numero).ToString()
            Else
                CmbTipoEtq.SelectedIndex = 1
                txt.Text = numero.ToString("F3", Globalization.CultureInfo.InvariantCulture)
            End If

        Else
            txt.Clear()
        End If

    End Sub
    Private Sub FormatarDecimal(txt As TextBox, correcao As String)

        Dim valor As String = txt.Text.Trim()

        If valor = "" Then Exit Sub

        Dim numero As Decimal

        If Decimal.TryParse(valor, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, numero) Then

            If correcao = "custo" Or correcao = "venda" Then
                txt.Text = numero.ToString("F2", Globalization.CultureInfo.InvariantCulture)
            Else
                txt.Text = numero.ToString("F3", Globalization.CultureInfo.InvariantCulture)
            End If

        Else
            MessageBox.Show("Digite um valor numérico válido.")
            txt.Clear()
        End If

    End Sub
    Private Function TestarConexao() As Boolean
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=projects;user id=root;password=root;"
        )
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("Falha na conexão com o banco:" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Private Sub CarregarCmbProdutos()
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim dt As New DataTable()

                Using da As New MySqlDataAdapter("SELECT codigo, nome FROM produtos ORDER BY nome", conn)
                    da.Fill(dt)
                End Using

                CmbProdutos.DataSource = dt
                CmbProdutos.DisplayMember = "nome"
                CmbProdutos.ValueMember = "codigo"

                CmbProdutos.SelectedIndex = -1

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar os produtos:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub CarregarCmbGrupos()
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim dt As New DataTable()

                Using da As New MySqlDataAdapter("SELECT DISTINCT grupo FROM produtos ORDER BY grupo", conn)
                    da.Fill(dt)
                End Using

                CmbGrupos.DataSource = dt
                CmbGrupos.DisplayMember = "grupo"
                CmbGrupos.ValueMember = "grupo"

                CmbGrupos.SelectedIndex = -1

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar os produtos:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub CarregarCmbFiltroGrupos()
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim dt As New DataTable()

                Using da As New MySqlDataAdapter("SELECT DISTINCT grupo FROM produtos ORDER BY grupo", conn)
                    da.Fill(dt)
                End Using

                CmbTipoFiltro.DataSource = dt
                CmbTipoFiltro.DisplayMember = "grupo"
                CmbTipoFiltro.ValueMember = "grupo"

                CmbTipoFiltro.SelectedIndex = -1

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar os produtos:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub CarregarCmbFiltroTiposEtq()
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim dt As New DataTable()

                Using da As New MySqlDataAdapter("SELECT DISTINCT tipo_etq FROM produtos ORDER BY tipo_etq", conn)
                    da.Fill(dt)
                End Using

                CmbTipoFiltro.DataSource = dt
                CmbTipoFiltro.DisplayMember = "tipo_etq"
                CmbTipoFiltro.ValueMember = "tipo_etq"

                CmbTipoFiltro.SelectedIndex = -1

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar os tipos de estoque:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub CarregarProdutoPorCodigo()
        Try
            If CmbProdutos.SelectedValue Is Nothing Then Exit Sub

            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim sql As String =
            "SELECT codigo, custo, venda, qtde_estoque, grupo, tipo_etq, imagem
             FROM produtos
             WHERE codigo = @codigo"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", CmbProdutos.SelectedValue)

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            TxtCodProd.Text = dr("codigo").ToString()
                            TxtCusto.Text = dr("custo").ToString()
                            TxtVenda.Text = dr("venda").ToString()
                            TxtQtdeProd.Text = dr("qtde_estoque").ToString()
                            CmbGrupos.Text = dr("grupo").ToString()
                            CmbTipoEtq.Text = dr("tipo_etq").ToString()

                            If Not IsDBNull(dr("imagem")) Then
                                Dim imgBytes() As Byte = CType(dr("imagem"), Byte())

                                Using ms As New MemoryStream(imgBytes)
                                    PicProds.Image = ImgWin.FromStream(ms)
                                End Using
                            Else
                                PicProds.Image = Nothing
                            End If

                        Else
                            PicProds.Image = Nothing
                        End If
                    End Using
                End Using

            End Using

            TxtCodProd.Enabled = False
            DgvProdutos.DataSource = Nothing

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar produto: " & ex.Message)
        End Try
    End Sub

    Private Sub SalvarProduto()

        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim existe As Boolean = False

                Dim sqlCheck As String = "SELECT COUNT(*) FROM produtos WHERE codigo = @codigo"
                Using cmdCheck As New MySqlCommand(sqlCheck, conn)
                    cmdCheck.Parameters.AddWithValue("@codigo", TxtCodProd.Text)
                    existe = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0
                End Using

                Dim sql As String = ""

                If existe Then
                    sql = "UPDATE produtos SET 
                        custo = @custo,
                        venda = @venda,
                        qtde_estoque = @qtde,
                        grupo = @grupo,
                        tipo_etq = @tipo_etq,
                        imagem = @imagem
                       WHERE codigo = @codigo"
                Else

                    sql = "INSERT INTO produtos 
                        (codigo, custo, venda, qtde_estoque, grupo, tipo_etq, imagem)
                       VALUES
                        (@codigo, @custo, @venda, @qtde, @grupo, @tipo_etq, @imagem)"
                End If

                Using cmd As New MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@codigo", TxtCodProd.Text)
                    cmd.Parameters.AddWithValue("@custo", TxtCusto.Text)
                    cmd.Parameters.AddWithValue("@venda", TxtVenda.Text)
                    cmd.Parameters.AddWithValue("@qtde", TxtQtdeProd.Text)
                    cmd.Parameters.AddWithValue("@grupo", CmbGrupos.Text)
                    cmd.Parameters.AddWithValue("@tipo_etq", CmbTipoEtq.Text)

                    If PicProds.Image IsNot Nothing Then
                        Using ms As New MemoryStream()
                            PicProds.Image.Save(ms, Imaging.ImageFormat.Jpeg)
                            Dim imgBytes() As Byte = ms.ToArray()
                            cmd.Parameters.AddWithValue("@imagem", imgBytes)
                        End Using
                    Else
                        cmd.Parameters.AddWithValue("@imagem", DBNull.Value)
                    End If

                    cmd.ExecuteNonQuery()
                End Using

            End Using

            MessageBox.Show("Produto salvo com sucesso!")

        Catch ex As Exception
            MessageBox.Show("Erro ao salvar: " & ex.Message)
        End Try

    End Sub
    Private Sub AtualizarRegistro(rowIndex As Integer)

        If rowIndex < 0 Then Exit Sub
        If DgvProdutos.Rows(rowIndex).IsNewRow Then Exit Sub
        If Not TestarConexao() Then Exit Sub

        Try
            Dim row = DgvProdutos.Rows(rowIndex)

            Dim codigo As Integer = Convert.ToInt32(row.Cells("cod").Value)

            Dim nome As String =
            If(row.Cells("nome").Value Is Nothing OrElse IsDBNull(row.Cells("nome").Value),
               "",
               row.Cells("nome").Value.ToString())

            Dim custo As String =
            If(row.Cells("custo").Value Is Nothing OrElse IsDBNull(row.Cells("custo").Value),
               "",
               row.Cells("custo").Value.ToString())

            Dim venda As String =
            If(row.Cells("venda").Value Is Nothing OrElse IsDBNull(row.Cells("venda").Value),
               "",
               row.Cells("venda").Value.ToString())

            Dim tipo_etq As String =
            If(row.Cells("tipo_etq").Value Is Nothing OrElse IsDBNull(row.Cells("tipo_etq").Value),
               "",
               row.Cells("tipo_etq").Value.ToString())

            Dim qtde_estoque As String =
            If(row.Cells("qtde_estoque").Value Is Nothing OrElse IsDBNull(row.Cells("qtde_estoque").Value),
               "",
               row.Cells("qtde_estoque").Value.ToString())

            Dim grupo As String =
            If(row.Cells("grupo").Value Is Nothing OrElse IsDBNull(row.Cells("grupo").Value),
               "",
               row.Cells("grupo").Value.ToString())

            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim sql As String =
                "UPDATE produtos SET
                    codigo = @codigo,
                    nome = @nome,
                    custo = @custo,
                    venda = @venda,
                    qtde_estoque = @qtde_estoque,
                    grupo = @grupo,
                    tipo_etq = @tipo_etq
                 WHERE codigo = @codigo"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@nome", nome)
                    cmd.Parameters.AddWithValue("@custo", custo)
                    cmd.Parameters.AddWithValue("@venda", venda)
                    cmd.Parameters.AddWithValue("@qtde_estoque", qtde_estoque)
                    cmd.Parameters.AddWithValue("@grupo", grupo)
                    cmd.Parameters.AddWithValue("@tipo_etq", tipo_etq)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            CarregarCmbProdutos()
            CarregarDgvProdutos()
            LimparDados()

        Catch ex As Exception
            MessageBox.Show("Erro ao salvar:" & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub CarregarDgvProdutos()

        DgvProdutos.DataSource = Nothing

        Dim textoFiltro As String = TxtFiltro.Text.Trim()
        Dim tipo_filtro As String = CmbTipoFiltro.Text.Trim()


        Dim sql As String = "SELECT codigo, nome, custo, venda, qtde_estoque, grupo, tipo_etq FROM produtos WHERE 1=1"

        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim cmd As New MySqlCommand()
                cmd.Connection = conn

                If CmbFiltro.Text = "Venda" AndAlso textoFiltro <> "" Then

                    Dim valorVenda As Decimal

                    If Decimal.TryParse(textoFiltro, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, valorVenda) Then
                        sql &= " AND CAST(venda AS DECIMAL(10,2)) = @venda"
                        cmd.Parameters.AddWithValue("@venda", valorVenda)
                    Else
                        MessageBox.Show("Valor de venda inválido.")
                        Exit Sub
                    End If

                End If

                If CmbFiltro.Text = "Custo" AndAlso textoFiltro <> "" Then

                    Dim valorCusto As Decimal

                    If Decimal.TryParse(textoFiltro, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, valorCusto) Then
                        sql &= " AND CAST(custo AS DECIMAL(10,2)) = @custo"
                        cmd.Parameters.AddWithValue("@custo", valorCusto)
                    Else
                        MessageBox.Show("Valor de custo inválido.")
                        Exit Sub
                    End If

                End If

                If CmbFiltro.Text = "Grupo" AndAlso tipo_filtro <> "" Then
                    sql &= " AND grupo = @grupo"
                    cmd.Parameters.AddWithValue("@grupo", tipo_filtro)
                End If

                If CmbFiltro.Text = "Tipo estoque" AndAlso tipo_filtro <> "" Then
                    sql &= " AND tipo_etq = @tipo_etq"
                    cmd.Parameters.AddWithValue("@tipo_etq", tipo_filtro)
                End If
                cmd.CommandText = sql

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If DgvProdutos.Columns.Count = 0 Then
                    DgvProdutos.AutoGenerateColumns = False

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "cod",
                    .HeaderText = "Código",
                    .DataPropertyName = "codigo"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "nome",
                    .HeaderText = "Nome",
                    .DataPropertyName = "nome"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "grupo",
                    .HeaderText = "Grupo",
                    .DataPropertyName = "grupo"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "custo",
                    .HeaderText = "Custo",
                    .DataPropertyName = "custo"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "venda",
                    .HeaderText = "Venda",
                    .DataPropertyName = "venda"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "tpo_etq",
                    .HeaderText = "Tipo estoque",
                    .DataPropertyName = "tipo_etq"
                })

                    DgvProdutos.Columns.Add(New DataGridViewTextBoxColumn() With {
                    .Name = "qtde_estoque",
                    .HeaderText = "Estoque",
                    .DataPropertyName = "qtde_estoque"
                })
                End If

                DgvProdutos.DataSource = dt

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar produtos: " & ex.Message)
        End Try

    End Sub

    Private Sub ExcluirProduto(codigo As Integer)
        If Not TestarConexao() Then Exit Sub
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()
                Dim sql As String =
            "DELETE FROM produtos
             WHERE codigo = @codigo"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Produto excluído com sucesso!")
            LimparDados()
            CarregarCmbProdutos()
            CarregarDgvProdutos()

        Catch ex As Exception
            MessageBox.Show("Erro ao excluir produto: " & ex.Message)
        End Try
    End Sub

    Private Sub LimparDados()

        CmbProdutos.SelectedIndex = -1
        TxtCodProd.Text = ""
        TxtCusto.Text = ""
        TxtQtdeProd.Text = ""
        TxtVenda.Text = ""
        CmbTipoEtq.SelectedIndex = -1
        CmbGrupos.SelectedIndex = -1
        PicProds.Image = Nothing
        PicProds.Tag = Nothing

    End Sub

    Private Sub ImprimirProdutos()

        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Arquivo PDF (*.pdf)|*.pdf"
            sfd.FileName = "produtos_" & DateTime.Now.ToString("ddMMyyyy_HHmm") & ".pdf"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim caminho As String = sfd.FileName

            If DgvProdutos.Columns.Count = 0 Then Exit Sub
            If DgvProdutos.Rows.Count = 0 Then Exit Sub

            Dim writer As New PdfWriter(caminho)
            Dim pdf As New PdfDocument(writer)
            Dim doc As New Document(pdf)

            doc.SetMargins(20, 20, 20, 20)

            Dim tabela As New Table(DgvProdutos.Columns.Count)
            tabela.SetWidth(iText.Layout.Properties.UnitValue.CreatePercentValue(100))
            tabela.SetFixedLayout()

            For Each col As DataGridViewColumn In DgvProdutos.Columns
                Dim header As New Cell()
                header.Add(New Paragraph(col.HeaderText))
                tabela.AddHeaderCell(header)
            Next

            For Each row As DataGridViewRow In DgvProdutos.Rows
                If Not row.IsNewRow Then

                    For Each cell As DataGridViewCell In row.Cells

                        Dim valor As String = ""

                        If cell.Value IsNot Nothing AndAlso Not IsDBNull(cell.Value) Then
                            valor = cell.Value.ToString()
                        End If

                        If valor.Length > 100 Then
                            valor = valor.Substring(0, 100)
                        End If

                        Dim c As New Cell()
                        c.Add(New Paragraph(valor))

                        tabela.AddCell(c)

                    Next

                End If
            Next

            doc.Add(New Paragraph("RELATÓRIO DE PRODUTOS"))
            doc.Add(New Paragraph("Data: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm")))
            doc.Add(New Paragraph(" "))

            doc.Add(tabela)

            doc.Close()

            MessageBox.Show("PDF gerado com sucesso!")

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

#End Region



#Region "Validações"

    Private Function ConfirmaCodigo() As Boolean
        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim sql As String =
            "SELECT COUNT(*) FROM produtos WHERE codigo = @codigo"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", TxtCodProd.Text)

                    Dim resultado As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    Return resultado > 0
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao verificar produto: " & ex.Message)
            Return False
        End Try
    End Function
    Private Sub Valide()

        If Not IsNumeric(TxtCodProd.Text) Then
            If ConfirmaCodigo() = True Then

            End If
            MessageBox.Show("Código deve ser numérico.")
        End If

    End Sub

    Private Sub CmbTipoEtq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTipoEtq.SelectedIndexChanged
        If CmbTipoEtq.Text = "KG" Then
            FormatarDecimal(TxtQtdeProd, "KG")
        ElseIf CmbTipoEtq.Text = "Unidade" Then
            FormataInteiros(TxtQtdeProd)
        End If
    End Sub

    Private Function ValidarCampos() As Boolean


        If TxtCodProd.Text = "" Or TxtCusto.Text = "" Or TxtVenda.Text = "" Or TxtQtdeProd.Text = "" Or CmbGrupos.SelectedIndex = -1 Or CmbTipoEtq.Text = "" Then
            MessageBox.Show("Preencha todos os campos antes de gravar")
            Return False
            Exit Function
        End If

        Return True

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click

    End Sub

#End Region


#End Region


End Class