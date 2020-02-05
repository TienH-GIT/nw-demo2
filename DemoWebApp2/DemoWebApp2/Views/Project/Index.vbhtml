@ModelType IEnumerable(Of DemoWebApp2.Models.Project)
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
            @Html.DisplayNameFor(Function(model) model.Department.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ScopeType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Scale)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Duration)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PlanStartDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PlanEndDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.EndDate)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Department.Code)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Code)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Description)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ScopeType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Scale)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Duration)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PlanStartDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PlanEndDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EndDate)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ProjectID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ProjectID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ProjectID })
        </td>
    </tr>
Next

</table>
