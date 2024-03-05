Imports System.Configuration
Imports System.Data.Common
Imports System.Data.OleDb
Public Class Form1
    Dim connection As OleDbConnection
    Dim ds As DataSet
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection_string As String = ConfigurationManager.ConnectionStrings("c_string").ConnectionString
        connection = New OleDbConnection(connection_string)
        filldatagrid()

    End Sub

    Private Sub filldatagrid()
        ds = New DataSet
        Dim dtp As New OleDbDataAdapter("select * from vbr", connection)
        dtp.Fill(ds, "m_table")
        DataGridView1.DataSource = ds.Tables("m_table")

    End Sub

    Private Sub insbtn_Click(sender As Object, e As EventArgs) Handles insbtn.Click
        Try
            connection.Open()
            Dim qry As String
            qry = "insert into vbr values" & "(?,?,to_date(?,'YYYY-MM-DD'))"
            Dim command As New OleDbCommand(qry, connection)
            command.Parameters.AddWithValue("?", CInt(TextBox1.Text))
            command.Parameters.AddWithValue("?", TextBox2.Text)
            command.Parameters.AddWithValue("?", DateTimePicker1.Value.ToString("yyyy-MM-dd"))

            Dim insrow = command.ExecuteNonQuery()
            connection.Close()
            If (insrow >= 1) Then
                MessageBox.Show("row inserted bro")
                filldatagrid()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
