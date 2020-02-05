Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DemoWebApp2.DAL

Namespace Models

    Public Enum ProjectRoleEnum
        PM = 1
        BSE
        Leader
        Member
        Support
    End Enum

    Public Class ProjectEmployee
        Public Property ProjectID As Integer
        Public Property EmployeeID As Integer
        Public Property Role As ProjectRoleEnum?
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property JoinDate As Date?

        Public Overridable Property Project As Project
        Public Overridable Property Employee As Employee
    End Class
End Namespace
