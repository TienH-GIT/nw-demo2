Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models
    Public Class Branch
        Implements IValidatableObject

        <Display(Name:="支店ID")>
        Public Property BranchID As Integer

        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        <Display(Name:="支店コード")>
        Public Property Code As String

        <Required>
        <MaxLength(200)>
        <Display(Name:="支店名")>
        Public Property Name As String

        <MaxLength(200)>
        <Display(Name:="支店名（カタ）")>
        Public Property KataName As String

        <MaxLength(300)>
        <Display(Name:="住所")>
        Public Property Address As String

        <MaxLength(500)>
        <Display(Name:="説明")>
        Public Property Description As String

#Region "Validation"
        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate
            Dim results As List(Of ValidationResult) = New List(Of ValidationResult)
            Dim db As New EmployeeContext

            Dim validateCode = db.Branches.FirstOrDefault(Function(x) x.Code.Equals(Code) And x.BranchID <> BranchID)
            If validateCode IsNot Nothing Then
                results.Add(New ValidationResult("コードは既に存在します。", {"Code"}))

            Else
                results.Add(ValidationResult.Success)
            End If
            Return results
        End Function
#End Region
    End Class
End Namespace
