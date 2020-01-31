Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Employee
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
    End Class
End Namespace
