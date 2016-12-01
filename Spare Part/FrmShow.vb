Imports System.Data.SqlClient
Imports System.IO
Public Class FrmShow
    Private Sub FrmShow_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Label1.Left = (Me.Width / 2) - (Label1.Width / 2)
        Label2.Left = Me.Width - Label2.Width - 10

    End Sub
    Private Sub FrmShow_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        up_Click(sender, e)




    End Sub



    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        If e.ColumnIndex = 9 Then

            Dim i As Integer = DataGridView1.CurrentRow.Index

            Randomvar = DataGridView1.Item(10, i).Value.ToString()



            inoutlogfrm.Show()




        End If

    End Sub

    Public Sub up_Click(sender As Object, e As EventArgs)
        Try
            Dim CnStr As String = FrmDataSql
            'Dim Cn As SqlConnection = New SqlConnection(CnStr)
            'Cn.Open()
            'Dim SEL As String = "SELECT DISTINCT OnAir FROM Location"
            'Dim Cmd As SqlCommand = New SqlCommand(SEL, Cn) '表名
            'Dim Dr As SqlDataReader : Dr = Cmd.ExecuteReader()
            ' If Dr.HasRows = 0 Then
            ' Else
            'While Dr.Read()
            'TabLoc.TabPages.Add(Trim(Dr("OnAir").ToString))
            'End While
            'End If
            ' Cn.Close()



            Dim cn1 As SqlConnection = New SqlConnection(CnStr)
            cn1.Open()
            Dim a As String = "SELECT * FROM SparePartList  where InOutLog ='1'"
            'Dim a As String = "SELECT * FROM SparePartList WHERE Department ='EM'"
            Dim cmd1 As SqlCommand = New SqlCommand(a, cn1) '表名
            Dim dr1 As SqlDataReader : dr1 = cmd1.ExecuteReader()

            If dr1.HasRows = 0 Then
            Else
                Dim i As Integer = "-1"
                Dim dtStart As DateTime
                Dim dtEnd As DateTime = Format(Now, "yyyy/MM/dd")
                Dim s As Integer


                While dr1.Read()

                    i = i + 1
                    dtStart = Trim(dr1("StoreDate"))


                    DataGridView1.Rows.Add(1)

                    DataGridView1("Column1", i).Value = Trim(dr1("Location").ToString + Space(5))
                    DataGridView1("Column2", i).Value = Trim(dr1("Description").ToString + Space(5))
                    DataGridView1("Column3", i).Value = Trim(dr1("PartNumber").ToString + Space(5))
                    DataGridView1("Column4", i).Value = Trim(dr1("SerialNumber").ToString + Space(5))
                    DataGridView1("Column5", i).Value = Trim(dr1("Quantity").ToString + Space(5)) & " PCS"
                    DataGridView1("Column6", i).Value = Trim(dr1("Owner").ToString + Space(5))
                    DataGridView1("Column7", i).Value = Trim(dr1("StoreDate"))
                    DataGridView1("Column9", i).Value = "view"
                    DataGridView1("Column11", i).Value = Trim(dr1("RandomID").ToString + Space(5))
                    s = DateDiff(DateInterval.Day, dtStart, dtEnd)
                    DataGridView1("Column8", i).Value = "已存放 " & s & " 天"


                    Select Case s
                        Case Is >= 90
                            DataGridView1("Column10", i).Value = ImageList1.Images(3)
                        Case 60 To 90
                            DataGridView1("Column10", i).Value = ImageList1.Images(2)
                        Case 30 To 60
                            DataGridView1("Column10", i).Value = ImageList1.Images(1)
                        Case 0 To 30
                            DataGridView1("Column10", i).Value = ImageList1.Images(0)

                    End Select



                End While
            End If
            cn1.Close()



        Catch ex As Exception

        End Try

    End Sub



End Class