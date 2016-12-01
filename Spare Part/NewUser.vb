Imports System.Data.SqlClient
Public Class NewUser
    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Cnstr As String = FrmDataSql
        Dim cn As SqlConnection = New SqlConnection(Cnstr)
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT DISTINCT OnAir FROM Location ", cn) '表名
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "mytable")
        ComboBox1.DataSource = ds.Tables("mytable")
        ComboBox1.DisplayMember = "OnAir"
        ComboBox1.Text = ""
        cn.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("请输入自己的工号", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("请输入名字", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf ComboBox1.Text = "" Then
            MessageBox.Show("请选择部门", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            Dim cnStr As String = FrmDataSql
            Dim cn As SqlConnection = New SqlConnection(cnStr)
            Dim da As New SqlDataAdapter("select * from Login", cn)
            Dim ds As New DataSet()
            da.Fill(ds, "mytable")
            Dim drow As DataRow
            drow = ds.Tables("mytable").NewRow
            Try
                drow("Employee") = TextBox1.Text
                drow("Phone") = TextBox3.Text
                drow("UserName") = TextBox2.Text
                drow("Department") = ComboBox1.Text

                ds.Tables("mytable").Rows.Add(drow)
                Dim cmdb As New SqlCommandBuilder(da)
                da.Update(ds, "mytable")

                MessageBox.Show("用户注册成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)


            Catch ex As Exception
                MessageBox.Show("注册时数据连接报错", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            cn.Close()
            Userloginfrm.Show()
            Me.Close()

        End If


    End Sub
End Class