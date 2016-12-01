Imports System.Data.SqlClient
Imports System.IO
Public Class FrmIn
    Dim cnstr As String
    Dim FrmRandomID As Integer


    Private Sub FrmIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cnstr = FrmDataSql
        Dim cn As SqlConnection = New SqlConnection(Cnstr)
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT DISTINCT LocationMark FROM Location WHERE OnAir ='" & DesVar & "'", cn) '表名
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "mytable")
        ComLoc.DataSource = ds.Tables("mytable")
        ComLoc.DisplayMember = "LocationMark"
        ComLoc.Text = ""
        cn.Close()
        TextBox1.Text = Uservar
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ComLoc.Text = "" Then
            MessageBox.Show("请选择一个库位！", "库位选择", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComLoc.Focus()
        ElseIf TextBoxDes.Text = "" Then
            MessageBox.Show("请输入物料的描述，偏于查找！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf IsNumeric(TextBoxQty.Text) = False Then
            MessageBox.Show("数量必须为整数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBoxQty.Clear() : TextBoxQty.Focus()
        ElseIf CInt(TextBoxQty.Text) <> TextBoxQty.Text Then
            MessageBox.Show("数量必须为整数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBoxQty.Clear() : TextBoxQty.Focus()
        Else
            Dim cn1 As SqlConnection = New SqlConnection(cnstr)
            Dim da1 As SqlDataAdapter = New SqlDataAdapter("select * from randomID", cn1) '表名
            Dim ds1 As DataSet = New DataSet() : da1.Fill(ds1, "mytable")
            Dim dv1 As New DataView(ds1.Tables("mytable"), "", "Mark", DataViewRowState.CurrentRows)
            Dim rowIndex1 As Integer = dv1.Find("ID")
            If rowIndex1 = -1 Then
            Else
                FrmRandomID = Trim(dv1(rowIndex1)("ID").ToString)

                Dim cn As SqlConnection = New SqlConnection(cnstr)
                Dim cn2 As SqlConnection = New SqlConnection(cnstr)
                Dim cn3 As SqlConnection = New SqlConnection(cnstr)
                Dim da As New SqlDataAdapter("select * from SparePartList", cn)
                Dim da2 As New SqlDataAdapter("select * from RandomID", cn2)
                Dim da3 As New SqlDataAdapter("select * from Inandout", cn3)
                Dim ds As New DataSet() : da.Fill(ds, "mytable")
                Dim ds2 As New DataSet() : da2.Fill(ds2, "mytable2")
                Dim ds3 As New DataSet() ： da3.Fill(ds3, "mytable3")
                Dim dv As New DataView(ds.Tables("mytable"), "", "RandomID", DataViewRowState.CurrentRows)
                Dim rowIndex As Integer = dv.Find(FrmRandomID)
                Dim drow3 As DataRow ： drow3 = ds3.Tables("mytable3").NewRow
                Dim drow2 As DataRow
                ds2.Tables("mytable2").PrimaryKey = New DataColumn() {ds2.Tables("mytable2").Columns("Mark")}
                drow2 = ds2.Tables("mytable2").Rows.Find("ID")
                If rowIndex = -1 Then
                    Dim drow As DataRow
                    drow = ds.Tables("mytable").NewRow
                    Try
                        drow("RandomID") = FrmRandomID
                        drow("Location") = ComLoc.Text
                        drow("Description") = TextBoxDes.Text
                        drow("PartNumber") = TextBoxPN.Text
                        drow("SerialNumber") = TextBoxSN.Text
                        drow("Quantity") = TextBoxQty.Text
                        drow("Department") = DesVar
                        drow("StoreDate") = Format(Now, "yyyy/MM/dd")
                        drow("Owner") = Uservar
                        drow("InOutLog") = "1"
                        ds.Tables("mytable").Rows.Add(drow)
                        drow3("RandomID") = FrmRandomID
                        drow3("Status") = "IN"
                        drow3("Owner") = Uservar
                        drow3("DatePrint") = Format(Now, "yyyy/MM/dd")
                        drow3("TimePrint") = Format(Now, "hh:mm:ss")
                        drow3("Quantity") = TextBoxQty.Text
                        ds3.Tables("mytable3").Rows.Add(drow3)
                        drow2("ID") = FrmRandomID + 1

                        Dim cmdb As New SqlCommandBuilder(da)
                        Dim cmdb2 As New SqlCommandBuilder(da2)
                        Dim cmdb3 As New SqlCommandBuilder(da3)
                        da.Update(ds, "mytable")
                        da2.Update(ds2, "mytable2")
                        da3.Update(ds3, "mytable3")

                        MessageBox.Show("信息保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        ComLoc.Text = "" : TextBoxDes.Clear() : TextBoxPN.Clear() : TextBoxDes.Clear() : TextBoxSN.Clear() : TextBoxQty.Clear()

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                Else
                    MessageBox.Show("出错了，随机ID重复！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                cn.Close()
                cn2.Close()
                cn3.Close()

            End If
            cn1.Close()

        End If



    End Sub
End Class