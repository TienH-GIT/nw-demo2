Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models
    Public Class Employee
        Implements IValidatableObject
        Public Property ID As Integer
        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        Public Property Code As String
        <Required>
        <MaxLength(100)>
        Public Property FirstName As String
        <MaxLength(100)>
        Public Property LastName As String
        <DefaultSettingValue("GETDATE()")>
        Public Property StartDate As Date?
        Public Property RetireDate As Date?

        <DefaultSettingValue("False")>
        Public Property IsActive As Boolean

        Public Overridable Property Detail As PersonalInfo

        Public Property JobTitleID As Integer
        Public Overridable Property JobTitle As JobTitle

        Public Property BranchID As Integer
        Public Overridable Property Branch As Branch

        Public Overridable Property ProjectEmployees As ICollection(Of ProjectEmployee)

#Region "Validation"
        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate
            Dim results As List(Of ValidationResult) = New List(Of ValidationResult)
            Dim db As New EmployeeContext

            Dim validateCode = db.Employees.FirstOrDefault(Function(x) x.Code.Equals(Code) And x.ID <> ID)
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
