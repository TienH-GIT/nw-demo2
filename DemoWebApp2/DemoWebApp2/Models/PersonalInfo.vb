Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Enum GenderEnum
        Male
        Female
        Unknow
    End Enum

    Public Enum StatusEnum
        <Display(Name:="Single")>
        Singleness
        <Display(Name:="Married")>
        Married
        <Display(Name:="Unknow")>
        Unknow
    End Enum

    Public Class PersonalInfo
        Public Property PersonalInfoID As Integer
        Public Property Gender As GenderEnum?
        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        Public Property Birthday As Date?
        Public Property Age As Integer?
        <MaxLength(300)>
        Public Property Address As String
        Public Property Status As StatusEnum?
        <MaxLength(200)>
        Public Property Hobby As String

        Public Overridable Property Employee As Employee
    End Class
End Namespace
