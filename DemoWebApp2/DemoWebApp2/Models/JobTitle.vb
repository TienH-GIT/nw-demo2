Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models
    Public Class JobTitle
        Implements IValidatableObject
        Public Property JobTitleID As Integer
        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        <Display(Name:="役職コード")>
        Public Property Code As String

        <Required>
        <MaxLength(200)>
        <Display(Name:="役職名")>
        Public Property Name As String

        <MaxLength(500)>
        <Display(Name:="説明")>
        Public Property Description As String

#Region "Validation"
        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate
            Dim results As List(Of ValidationResult) = New List(Of ValidationResult)
            Dim db As New EmployeeContext

            Dim validateCode = db.JobTitles.FirstOrDefault(Function(x) x.Code.Equals(Code) And x.JobTitleID <> JobTitleID)
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
