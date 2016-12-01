Imports System.Data.SqlClient
Public Class Userloginfrm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cnStr As String = FrmDataSql
        Dim cn As SqlConnection = New SqlConnection(cnStr)
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from login", cn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "Login_User")
        Dim dv As New DataView(ds.Tables("Login_User"), "", "Employee", DataViewRowState.CurrentRows)
        Dim rowIndex As Integer = dv.Find(TextBoxUser.Text)

        If rowIndex = -1 Then


            MessageBox.Show("第一次使用，请注册信息", "用户不存在", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            NewUser.Show()
            Me.Close()
            Exit Sub
        Else

            Uservar = Trim(dv(rowIndex)("UserName").ToString)
            DesVar = Trim(dv(rowIndex)("Department").ToString)


            If OpenID = "IN" Then
                FrmIn.Show()
            ElseIf openid = "OUT" Then
                FrmOut.Show()

            End If


        End If
        cn.Close()


        Me.Close()



    End Sub
    Private Sub TextBoxuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxUser.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)

        End If
    End Sub


    Private Sub Userloginfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class