
Imports System.ComponentModel
Imports System.Data.SqlClient
Public Class inoutlogfrm
    Private Sub inoutlogfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim CnStr As String = FrmDataSql
        Dim cn1 As SqlConnection = New SqlConnection(CnStr)
        cn1.Open()
        Dim a As String = "SELECT * FROM Inandout where RandomID ='" & Randomvar & "'"

        Dim cmd1 As SqlCommand = New SqlCommand(a, cn1)
        Dim dr1 As SqlDataReader : dr1 = cmd1.ExecuteReader()

        If dr1.HasRows = 0 Then
        Else
            Dim i As Integer = "-1"


            While dr1.Read()

                i = i + 1



                DataGridView1.Rows.Add(1)
                Dim inout As String = Trim(dr1("status").ToString + Space(5))
                If inout = "IN" Then
                    DataGridView1("Column1", i).Value = ImageList1.Images(0)

                ElseIf inout = "OUT" Then
                    DataGridView1("Column1", i).Value = ImageList1.Images(1)
                End If



                DataGridView1("Column2", i).Value = Trim(dr1("Owner").ToString + Space(5))
                DataGridView1("Column3", i).Value = Trim(dr1("DatePrint"))
                DataGridView1("Column4", i).Value = Trim(dr1("Quantity").ToString + Space(5)) & " PCS"







            End While
            DataGridView1.Sort(DataGridView1.Columns("column3"), ListSortDirection.Descending)
        End If


    End Sub



    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class