Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class JobTitle
        Public Property JobTitleID As Integer
        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        Public Property Code As String
        <Required>
        <MaxLength(200)>
        Public Property Name As String
        <MaxLength(500)>
        Public Property Description As String
    End Class
End Namespace
