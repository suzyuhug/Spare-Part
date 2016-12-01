Imports System.Data.SqlClient
Public Class FrmOut
    Dim ID As String
    Private Sub FrmOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim CnStr As String = FrmDataSql
        Dim Cn As SqlConnection = New SqlConnection(CnStr)
        Cn.Open()
        Dim SEL As String = "select * from SparePartList where InOutLog= '1'"
        Dim da As SqlDataAdapter = New SqlDataAdapter(SEL, Cn) '表名
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "mytable")
        DataGridView1.DataSource = ds.Tables("mytable")

        DataGridView1.RowHeadersVisible = False
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(9).Visible = False
        Cn.Close()
    End Sub

    Private Sub ComboBoxField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxField.SelectedIndexChanged
        If ComboBoxField.Text = "Description" Or ComboBoxField.Text = "SerialNumber" Then
            ComExpression.DataSource = Nothing
        Else
            Dim cnstr As String = FrmDataSql
            Dim cn As SqlConnection = New SqlConnection(cnstr)
            Dim sql As String = "SELECT DISTINCT " & ComboBoxField.Text & " FROM SparePartList"
            Dim da As SqlDataAdapter = New SqlDataAdapter(sql, cn) '表名
            Dim ds As DataSet = New DataSet()
            da.Fill(ds, "mytable")
            ComExpression.DataSource = ds.Tables("mytable")
            ComExpression.DisplayMember = ComboBoxField.Text
            cn.Close()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Columns.Clear()

        Dim CnStr As String = FrmDataSql
        Dim Cn As SqlConnection = New SqlConnection(CnStr)
        Cn.Open()
        Dim SEL As String = "select * from SparePartList where " & ComboBoxField.Text & " like " & "'%" & ComExpression.Text & "%' and InOutLog='1'"
        Dim da As SqlDataAdapter = New SqlDataAdapter(SEL, Cn) '表名
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "mytable")
        DataGridView1.DataSource = ds.Tables("mytable")

        DataGridView1.RowHeadersVisible = False
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(9).Visible = False
        Cn.Close()




    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim i As Integer = DataGridView1.CurrentRow.Index
        ID = DataGridView1.Item(0, i).Value.ToString()
        Loclabel.Text = DataGridView1.Item(1, i).Value.ToString()
        Deslabel.Text = DataGridView1.Item(2, i).Value.ToString()
        Pnlabel.Text = DataGridView1.Item(3, i).Value.ToString()
        SNlabel.Text = DataGridView1.Item(4, i).Value.ToString()
        Ownerlabel.Text = DataGridView1.Item(5, i).Value.ToString()
        SDlabel.Text = DataGridView1.Item(6, i).Value
        Qtylabel.Text = DataGridView1.Item(7, i).Value.ToString()




    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ID = "" Then
            MessageBox.Show("请选择你要拿出去的物料！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf IsNumeric(Qtytext.Text) = False Then
            MessageBox.Show("数量必须为整数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Qtytext.Clear()
        ElseIf CInt(Qtytext.Text) <> Qtytext.Text Then
            MessageBox.Show("数量必须为整数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Qtytext.Clear()
        ElseIf int(Qtytext.Text) > int(Qtylabel.Text) Then
            MessageBox.Show("出去的数量不可以大于库存数量！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Qtytext.Clear()
        Else




            Dim cnStr As String = FrmDataSql
            Dim cn As SqlConnection = New SqlConnection(cnStr)
            Dim cn1 As SqlConnection = New SqlConnection(cnStr)
            Dim da As New SqlDataAdapter("select * from Inandout", cn)
            Dim da1 As New SqlDataAdapter("select * from SparePartList", cn1)
            Dim ds As New DataSet() : da.Fill(ds, "mytable")
            Dim ds1 As New DataSet() : da1.Fill(ds1, "mytable")
            Dim drow As DataRow : drow = ds.Tables("mytable").NewRow
            Dim drow1 As DataRow
            ds1.Tables("mytable").PrimaryKey = New DataColumn() {ds1.Tables("mytable").Columns("RandomID")}
            drow1 = ds1.Tables("mytable").Rows.Find(ID)
            Try
                drow("RandomID") = ID
                drow("Status") = "OUT"
                drow("Owner") = Uservar
                drow("DatePrint") = Format(Now, "yyyy-MM-dd")
                drow("TimePrint") = Format(Now, "hh:mm:ss")
                drow("Quantity") = Qtytext.Text

                drow1("StoreDate") = Format(Now, "yyyy-MM-dd")
                Dim s As Integer = Int(Qtylabel.Text - Qtytext.Text)
                If s = 0 Then
                    drow1("Quantity") = "0"
                    drow1("InOutLog") = "0"
                Else
                    drow1("InOutLog") = "1"
                    drow1("Quantity") = s
                End If





                ds.Tables("mytable").Rows.Add(drow)
                Dim cmdb As New SqlCommandBuilder(da)
                Dim cmdb1 As New SqlCommandBuilder(da1)
                da1.Update(ds1, "mytable")
                da.Update(ds, "mytable")

                MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

                ID = ""
                Loclabel.Text = "" : Pnlabel.Text = "" : SNlabel.Text = "" : Deslabel.Text = "" : Ownerlabel.Text = "" : SDlabel.Text = "" : Qtylabel.Text = ""

                Qtytext.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            cn.Close()







        End If

    End Sub
End Class