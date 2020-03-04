Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class CsvFile
        <Display(Name:="CSV ファイル")>
        <UploadFile(Extensions:="csv", MaxLength:=65536)>
        Public Property UploadFile As HttpPostedFileBase
    End Class
End Namespace
