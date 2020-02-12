@ModelType IEnumerable(Of DemoWebApp2.Models.Employee)
@Code
    ViewData("Title") = "List View"
End Code

<h2>@ViewData("Title").</h2>
<h3>@ViewData("Message")</h3>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<div class="table-custom">
    <div class="header">Employees</div>
    
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
                <td class="view-cell">
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

</div>

<div id='myModal' class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div id='myModalContent'></div>
        </div>
    </div>
</div>

<div id="dialog" style="display: none">
</div>

@Html.Raw(File.ReadAllText(Server.MapPath("~/Views/Shared/Modal/EmpInfoModal.vbhtml")))

@section Styles
    <link href="@Url.Content("/Content/Views/EmpIndex.css")" rel="stylesheet" type="text/css" />
End Section

@Section Scripts
    <script type="text/javascript">
    var myApp = myApp || {};
    myApp.empAPI = '@Url.Action("Emp", "api")';
    myApp.empUrl = '@Url.Action("EmpInfo", "Employee")';
    </script>

    <script type="text/javascript" src="@Url.Content("/Scripts/Views/EmpIndex.js")"></script>
End Section
