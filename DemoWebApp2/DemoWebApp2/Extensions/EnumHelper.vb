Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Module ModuleExtension

    <Extension()>
    Public Function DisplayName(ByVal value As System.Enum) As String

        Dim enumType As Type = value.GetType()
        Dim enumValue = System.Enum.GetName(enumType, value)
        Dim member As MemberInfo = enumType.GetMember(enumValue)(0)

        Dim attrs = member.GetCustomAttributes(GetType(DisplayAttribute), False)
        Dim outString = CType(attrs(0), DisplayAttribute).Name

        If (CType(attrs(0), DisplayAttribute).ResourceType IsNot Nothing) Then
            outString = CType(attrs(0), DisplayAttribute).GetName()
        End If

        Return outString
    End Function

End Module
