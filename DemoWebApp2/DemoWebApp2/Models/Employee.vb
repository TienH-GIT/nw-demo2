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
        <Display(Name:="従業員コード")>
        Public Property Code As String

        <Required>
        <MaxLength(100)>
        <Display(Name:="名")>
        Public Property FirstName As String

        <MaxLength(100)>
        <Display(Name:="苗字")>
        Public Property LastName As String

        <NotMapped>
        <Display(Name:="姓名")>
        Public ReadOnly Property FullName As String
            Get
                Return FirstName + " " + LastName
            End Get
        End Property

        <DefaultSettingValue("GETDATE()")>
        <Display(Name:="入社日")>
        Public Property StartDate As Date?

        <Display(Name:="退職日")>
        Public Property RetireDate As Date?

        <DefaultSettingValue("False")>
        <Display(Name:="有効性")>
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
