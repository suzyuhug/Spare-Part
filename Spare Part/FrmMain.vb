Imports System.Data.SqlClient
Imports System.Text
Public Class FrmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenID = "IN"
        Userloginfrm.Show()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FrmShow.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenID = "OUT"
        Userloginfrm.Show()


    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        Try

            FrmDataSql = "Data Source=.\SQLEXPRESS;Initial Catalog=SparePart;Integrated Security=False;User ID=sa;Password=Aa123456;"
        Catch ex As Exception

            MessageBox.Show("数据库连接失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        End

    End Sub

End Class