@ModelType IEnumerable(Of DemoWebApp2.Models.Employee)
@Code
    ViewData("Title") = "List View"
End Code

<h2>@ViewData("Title").</h2>
<h3>@ViewData("Message")</h3>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table" id="EmpList">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </th>
        <th>
            Age
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td class="ViewCell">
                @Html.HiddenFor(Function(modelItem) item.ID, New With {.class = "EmpID"})
                @Html.DisplayFor(Function(modelItem) item.Code)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.FirstName)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.LastName)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Detail.Age)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", New With {.id = item.ID}) |
                @Html.ActionLink("Detail", "Detail", New With {.id = item.ID}) |
                @Html.ActionLink("Delete", "Delete", New With {.id = item.ID})
            </td>
        </tr>
    Next

</table>

@Section Scripts
    <script type="text/javascript">
        var myApp = myApp || {};
        myApp.empUrl = '@Url.Action("Emp", "api")';
    </script>

    <script type="text/javascript" src="@Url.Content("/Scripts/Views/EmpIndex.js")"></script>
End Section