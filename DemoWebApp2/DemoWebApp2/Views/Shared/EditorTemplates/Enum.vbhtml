@model Enum

@If (EnumHelper.IsValidForEnumHelper(ViewData.ModelMetadata)) Then
    @Html.EnumDropDownListFor(Function(Model) Model, New With {.htmlAttributes = New With {.class = "form-control"}})
Else
    @Html.TextBoxFor(Function(Model) Model, New With {.htmlAttributes = New With {.class = "form-control"}})
End If
