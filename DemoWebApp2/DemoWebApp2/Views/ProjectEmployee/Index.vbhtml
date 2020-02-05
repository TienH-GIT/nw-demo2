@ModelType IEnumerable(Of DemoWebApp2.Models.ProjectEmployee)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Employee.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Project.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Role)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.JoinDate)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Employee.Code)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Project.Code)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Role)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.JoinDate)
        </td>
        <td>
            @*@Html.ActionLink("Edit", "Edit", New With {.id = item.PrimaryKey}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.PrimaryKey}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.PrimaryKey})*@
        </td>
    </tr>
Next

</table>
