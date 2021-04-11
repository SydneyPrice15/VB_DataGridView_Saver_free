Imports System.Drawing.Printing
Imports System.Data.DataTable
Imports excel = Microsoft.office.interop.Excel
Imports Microsoft.office.interop
Imports System.IO





Public Class Form1

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnext_Click(sender As Object, e As EventArgs) Handles btnext.Click
        MeExit()
    End Sub
    Private Sub MeExit()
        Dim iExit As DialogResult
        iExit = MessageBox.Show("confrim if you want to exsit", "Datagridview System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iExit = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        MeExit()
    End Sub

    Private Sub FeildToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FeildToolStripMenuItem.Click

    End Sub

    Private Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)

        Next
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Dim txt
        For Each txt In Me.Controls
            If TypeOf txt Is TextBox Then
                txt.text = ""
            End If
        Next txt
    End Sub

    Private Sub btnAddnew_Click(sender As Object, e As EventArgs) Handles btnAddnew.Click
        DataGridView1.Rows.Add(txtSid.Text, txtFn.Text, txtSn.Text, txtdob.Text, txtadress.Text, txtemail.Text, cmbgen.Text, txtmobile.Text)
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        DataGridView1.Rows.Add(txtSid.Text, txtFn.Text, txtSn.Text, txtdob.Text, txtadress.Text, txtemail.Text, cmbgen.Text, txtmobile.Text)
    End Sub
    Private bitmap As Bitmap
    Private Sub iPrint()
        Dim height As Integer = DataGridView1.Height
        DataGridView1.Height = DataGridView1.RowCount * DataGridView1.RowTemplate.Height
        bitmap = New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(bitmap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()
        DataGridView1.Height = height
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        iPrint()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        iPrint()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(bitmap, 0, 0)

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveToExcel()

    End Sub
    Private Sub SaveToExcel()
        Dim excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = excel.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
        Try
            worksheet = workbook.ActiveSheet

            worksheet.Name = "ExportedFromDataGrid"

            Dim cellrowindex As Integer = 1
            Dim cellcolumindex As Integer = 1


            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                worksheet.Cells(cellrowindex, cellcolumindex) = DataGridView1.Columns(j).HeaderText
                cellcolumindex += 1

            Next
            cellcolumindex = 1
            cellrowindex += 1

            For i As Integer = 0 To DataGridView1.Rows.Count - 2

            Next
            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                worksheet.Cells(cellrowindex, cellcolumindex) = DataGridView1.Rows(i).Cells(j).Value.ToString()
                cellcolumindex += 1
            Next
            cellcolumindex = 1
            cellrowindex += 1

            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All Files(*.*)|*.*"
            saveDialog.FilterIndex = 2

            If saveDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                workbook.SaveAs(saveDialog.FileName)
                MessageBox.Show("export successful")

            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        Finally
            excel.Quit()
            workbook = Nothing
            excel = Nothing

        End Try

    End Sub

    Private Function i() As Integer
        Throw New NotImplementedException
    End Function

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveToExcel()

    End Sub
End Class
