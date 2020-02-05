Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models
    Public Class Branch
        Implements IValidatableObject
        Public Property BranchID As Integer
        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        Public Property Code As String
        <Required>
        <MaxLength(200)>
        Public Property Name As String
        <MaxLength(200)>
        Public Property KataName As String
        <MaxLength(300)>
        Public Property Address As String
        <MaxLength(500)>
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
