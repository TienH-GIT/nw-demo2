Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models
    Public Enum ScopeType
        <Display(Name:="Small (＜10)")>
        Small = 1
        <Display(Name:="Medium (10～50)")>
        Medium
        <Display(Name:="Large (51～200)")>
        Large
        <Display(Name:="Very large (＞200)")>
        VeryLarge
    End Enum
    Public Class Project
        Implements IValidatableObject
        Public Property ProjectID As Integer
        <Required>
        <MaxLength(20)>
        <Index("Ix_Code", Order:=1, IsUnique:=True)>
        Public Property Code As String
        <Required>
        <MaxLength(200)>
        Public Property Name As String
        <MaxLength(500)>
        Public Property Description As String
        Public Property ScopeType As ScopeType?
        Public Property Scale As Integer?
        Public Property Duration As String
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property PlanStartDate As Date?
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property PlanEndDate As Date?
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property StartDate As Date?
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property EndDate As Date?

        Public Property DepartmentID As Integer
        Public Overridable Property Department As Department

        Public Overridable Property ProjectEmployees As ICollection(Of ProjectEmployee)

#Region "Validation"
        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate
            Dim results As List(Of ValidationResult) = New List(Of ValidationResult)
            Dim db As New EmployeeContext

            Dim validateCode = db.Projects.FirstOrDefault(Function(x) x.Code.Equals(Code) And x.ProjectID <> ProjectID)
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
