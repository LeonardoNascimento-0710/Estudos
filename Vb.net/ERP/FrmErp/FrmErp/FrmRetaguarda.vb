Imports System.Windows.Forms.VisualStyles
Imports MySql.Data.MySqlClient

Public Class FrmRetaguarda

    Private Sub FrmRetaguarda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarCmbProdutos()
        CarregarCmbGrupos()
    End Sub

#Region "Produtos"


#Region "Design e containers"
    Private Sub BtnGravar_Click(sender As Object, e As EventArgs) Handles BtnGravar.Click

        If TxtCodProd.Text = "" Or TxtCusto.Text = "" Or TxtVenda.Text = "" Or TxtQtdeProd.Text = "" Or CmbGrupos.SelectedIndex = -1 Then
            MessageBox.Show("Preencha todos os campos antes de gravar")
            Exit Sub
        End If
        If ConfirmaCodigo() = True Then
            Atualizardados()
        Else
            SalvarDados()
        End If

        LimparDados()

    End Sub

    Private Sub CmbFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFiltro.SelectedIndexChanged

        If CmbFiltro.Text = "Grupo" Then
            LblFiltro.Text = "Grupo:"
            CarregarCmbFiltroGrupos()
            CmbFiltroGrp.Visible = True
            TxtFiltro.Visible = False
        End If

        If CmbFiltro.Text = "Custo" Then
            LblFiltro.Text = "Custo:"
            CmbFiltroGrp.Visible = False
            TxtFiltro.Enabled = True
            TxtFiltro.Visible = True
        End If

        If CmbFiltro.Text = "Venda" Then
            LblFiltro.Text = "Venda:"
            CmbFiltroGrp.Visible = False
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

        CarregarProdutoPorCodigo()

    End Sub
    Private Sub BtnExcluirProd_Click(sender As Object, e As EventArgs) Handles BtnExcluirProd.Click

        If TxtCodProd.Text = "" Then
            MessageBox.Show("Selecione um produto para excluir.")
            Exit Sub
        End If
        ExcluirProduto(TxtCodProd.Text)

    End Sub

#End Region



#Region "Funções"

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

                Using da As New MySqlDataAdapter("SELECT grupo FROM produtos ORDER BY grupo", conn)
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

                Using da As New MySqlDataAdapter("SELECT grupo FROM produtos ORDER BY grupo", conn)
                    da.Fill(dt)
                End Using

                CmbFiltroGrp.DataSource = dt
                CmbFiltroGrp.DisplayMember = "grupo"
                CmbFiltroGrp.ValueMember = "grupo"

                CmbFiltroGrp.SelectedIndex = -1

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar os produtos:" & vbCrLf & ex.Message)
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
            "SELECT codigo, custo, venda, qtde_estoque, grupo
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
                        End If
                    End Using
                End Using

            End Using

            DgvProdutos.DataSource = Nothing

        Catch ex As Exception
            MessageBox.Show("Erro ao carregar produto: " & ex.Message)
        End Try
    End Sub

    Private Sub SalvarDados()

        If Not TestarConexao() Then Exit Sub

        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim sql As String =
            "INSERT INTO produtos
            (codigo, nome, custo, venda, qtde_estoque, grupo)
            VALUES
            (@codigo, @nome, @custo, @venda, @qtde_estoque, @grupo)"

                Using cmd As New MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@codigo", TxtCodProd.Text)
                    cmd.Parameters.AddWithValue("@nome", CmbProdutos.Text)
                    cmd.Parameters.AddWithValue("@custo", TxtCusto.Text)
                    cmd.Parameters.AddWithValue("@venda", TxtVenda.Text)
                    cmd.Parameters.AddWithValue("@qtde_estoque", TxtQtdeProd.Text)
                    cmd.Parameters.AddWithValue("@grupo", CmbGrupos.Text)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Registro salvo com sucesso!")
            LimparDados()
            CarregarCmbProdutos()

        Catch ex As Exception
            MessageBox.Show("Erro ao salvar:" & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub AtualizarDados()

        If Not TestarConexao() Then Exit Sub

        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim sql As String =
            "UPDATE produtos SET
                nome = @nome,
                custo = @custo,
                venda = @venda,
                qtde_estoque = @qtde_estoque,
                grupo = @grupo
             WHERE codigo = @codigo"

                Using cmd As New MySqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@codigo", TxtCodProd.Text)
                    cmd.Parameters.AddWithValue("@nome", CmbProdutos.Text)
                    cmd.Parameters.AddWithValue("@custo", TxtCusto.Text)
                    cmd.Parameters.AddWithValue("@venda", TxtVenda.Text)
                    cmd.Parameters.AddWithValue("@qtde_estoque", TxtQtdeProd.Text)
                    cmd.Parameters.AddWithValue("@grupo", CmbGrupos.Text)

                    Dim linhasAfetadas As Integer = cmd.ExecuteNonQuery()

                    If linhasAfetadas > 0 Then
                        MessageBox.Show("Registro atualizado com sucesso!")
                    Else
                        MessageBox.Show("Nenhum registro encontrado para atualizar.")
                    End If

                End Using
            End Using

            LimparDados()
            CarregarCmbProdutos()

        Catch ex As Exception
            MessageBox.Show("Erro ao atualizar:" & vbCrLf & ex.Message)
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
                    grupo = @grupo
                 WHERE codigo = @codigo"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", codigo)
                    cmd.Parameters.AddWithValue("@nome", nome)
                    cmd.Parameters.AddWithValue("@custo", custo)
                    cmd.Parameters.AddWithValue("@venda", venda)
                    cmd.Parameters.AddWithValue("@qtde_estoque", qtde_estoque)
                    cmd.Parameters.AddWithValue("@grupo", grupo)

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

        Dim venda As String = TxtFiltro.Text.Trim()
        Dim custo As String = TxtFiltro.Text.Trim()
        Dim Grupo As String = CmbFiltroGrp.Text.Trim()

        Dim sql As String = "SELECT codigo, nome, custo, venda, qtde_estoque, grupo FROM produtos WHERE 1=1"

        Try
            Using conn As New MySqlConnection(
            "server=localhost;database=retaguarda_orbit;user id=root;password=root;"
        )
                conn.Open()

                Dim cmd As New MySqlCommand()
                cmd.Connection = conn

                If CmbFiltro.Text = "Venda" Then
                    sql &= " AND venda LIKE @venda"
                    cmd.Parameters.AddWithValue("@venda", "%" & venda & "%")
                End If

                If CmbFiltro.Text = "Custo" Then
                    sql &= " AND custo LIKE @custo"
                    cmd.Parameters.AddWithValue("@custo", "%" & custo & "%")
                End If

                If CmbFiltro.Text = "Grupo" Then
                    sql &= " AND grupo = @grupo"
                    cmd.Parameters.AddWithValue("@grupo", Grupo)
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
                    .Name = "qtde_estoque",
                    .HeaderText = "Estoque",
                    .DataPropertyName = "qtde_estoque"
                })
                End If

                DgvProdutos.DataSource = dt

            End Using

            CmbFiltroGrp.SelectedIndex = -1
            TxtFiltro.Text = ""

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
        CmbGrupos.SelectedIndex = -1

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

#End Region


#End Region


End Class