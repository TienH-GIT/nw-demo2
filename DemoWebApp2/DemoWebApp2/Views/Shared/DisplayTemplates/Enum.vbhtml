@If EnumHelper.IsValidForEnumHelper(ViewData.ModelMetadata) Then
    Dim displayname As String = ""
    For Each item As SelectListItem In EnumHelper.GetSelectList(ViewData.ModelMetadata, DirectCast(Model, [Enum]))
        If item.Selected Then
            If Not IsNothing(item.Text) Then
                displayname = item.Text
            Else
                displayname = item.Value

            End If
        End If
    Next

    If String.IsNullOrEmpty(displayname) Then
        If Model Is Nothing Then
            displayname = String.Empty
        Else
            displayname = Model.ToString()
        End If

    End If

    @Html.DisplayFor(Function(model) displayname)

Else
    @Html.DisplayTextFor(Function(model) model)
End If