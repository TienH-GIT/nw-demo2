Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    Public Enum GenderEnum
        <Display(Name:="男性")>
        Male
        <Display(Name:="女性")>
        Female
        <Display(Name:="不明")>
        Unknow
    End Enum

    Public Enum StatusEnum
        <Display(Name:="独身")>
        Singleness
        <Display(Name:="既婚")>
        Married
        <Display(Name:="不明")>
        Unknow
    End Enum

    Public Class PersonalInfo
        Public Property PersonalInfoID As Integer

        <Display(Name:="性別")>
        Public Property Gender As GenderEnum?

        <DisplayFormat(DataFormatString:="{0:yyyy/MM/dd}", ApplyFormatInEditMode:=True)>
        <Display(Name:="誕生日")>
        Public Property Birthday As Date?

        <Display(Name:="年齢")>
        Public Property Age As Integer?

        <MaxLength(300)>
        <Display(Name:="住所")>
        Public Property Address As String

        <Display(Name:="個人状態")>
        Public Property Status As StatusEnum?

        <MaxLength(200)>
        <Display(Name:="趣味")>
        Public Property Hobby As String


        Public Overridable Property Employee As Employee
    End Class
End Namespace
