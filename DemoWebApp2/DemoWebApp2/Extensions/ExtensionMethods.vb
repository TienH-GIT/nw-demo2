Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Class ExtensionMethods
    Public Shared Function GetDisplayName(Of TModel, TProperty)(ByVal model As TModel, ByVal expression As Expression(Of Func(Of TModel, TProperty))) As String
        Dim type As Type = GetType(TModel)
        Dim memberExpression As MemberExpression = CType(expression.Body, MemberExpression)
        Dim propertyName As String = If((TypeOf memberExpression.Member Is PropertyInfo), memberExpression.Member.Name, Nothing)

        ' First look into attributes on a type and it's parents
        Dim attr As DisplayAttribute
        attr = CType(type.GetProperty(CStr(propertyName)).GetCustomAttributes(CType(GetType(DisplayAttribute), Type), CBool(True)).SingleOrDefault(), DisplayAttribute)


        ' Look for [MetadataType] attribute in type hierarchy
        ' http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
        If attr Is Nothing Then
            Dim metadataType As MetadataTypeAttribute = CType(type.GetCustomAttributes(CType(GetType(MetadataTypeAttribute), Type), CBool(True)).FirstOrDefault(), MetadataTypeAttribute)

            If metadataType IsNot Nothing Then
                Dim [property] = metadataType.MetadataClassType.GetProperty(propertyName)

                If [property] IsNot Nothing Then
                    attr = CType([property].GetCustomAttributes(GetType(DisplayNameAttribute), True).SingleOrDefault(), DisplayAttribute)
                End If
            End If
        End If

        Return If(attr IsNot Nothing, attr.Name, String.Empty)
    End Function

    Public Shared Function GetDisplayName(Of TModel)(ByVal expression As Expression(Of Func(Of TModel, Object))) As String
        Dim type As Type = GetType(TModel)
        Dim propertyName As String = Nothing
        Dim properties As String() = Nothing
        Dim propertyList As IEnumerable(Of String)

        'unless it's a root property the expression NodeType will always be Convert
        Select Case expression.Body.NodeType
            Case ExpressionType.Convert, ExpressionType.ConvertChecked
                Dim ue = TryCast(expression.Body, UnaryExpression)
                propertyList = ue?.Operand.ToString().Split(".".ToCharArray()).Skip(1) 'don't use the root property
            Case Else
                propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1)
        End Select


        'the propert name is what we're after
        propertyName = propertyList.Last()
        'list of properties - the last property name
        properties = propertyList.Take(propertyList.Count() - 1).ToArray() 'grab all the parent properties
        'Dim expr As Expression = Nothing

        For Each [property] As String In properties
            Dim propertyInfo As PropertyInfo = type.GetProperty([property])
            'expr = expression.Property(expr, type.GetProperty([property]))
            type = propertyInfo.PropertyType
        Next

        Dim attr As DisplayAttribute
        attr = CType(type.GetProperty(CStr(propertyName)).GetCustomAttributes(CType(GetType(DisplayAttribute), Type), CBool(True)).SingleOrDefault(), DisplayAttribute)


        ' Look for [MetadataType] attribute in type hierarchy
        ' http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
        If attr Is Nothing Then
            Dim metadataType As MetadataTypeAttribute = CType(type.GetCustomAttributes(CType(GetType(MetadataTypeAttribute), Type), CBool(True)).FirstOrDefault(), MetadataTypeAttribute)

            If metadataType IsNot Nothing Then
                Dim [property] = metadataType.MetadataClassType.GetProperty(propertyName)

                If [property] IsNot Nothing Then
                    attr = CType([property].GetCustomAttributes(GetType(DisplayNameAttribute), True).SingleOrDefault(), DisplayAttribute)
                End If
            End If
        End If

        Return If(attr IsNot Nothing, attr.Name, String.Empty)
    End Function
End Class
