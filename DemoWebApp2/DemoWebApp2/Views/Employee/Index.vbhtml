@Imports DemoWebApp2.Models
@ModelType DemoWebApp2.Models.EmpViewModel
@Code
    ViewData("Title") = "List View"
End Code

<h2>@ViewData("Title").</h2>
<h3>@ViewData("Message")</h3>

<div class="row">
    <div class="container">
        <div class="col-md-2">
            @Html.ActionLink("Create New", "Create")
        </div>
        <div class="col-md-3 col-md-offset-2">
            @Using (Html.BeginForm("Export", "Employee", FormMethod.Post))
                @Html.AntiForgeryToken()

                @<div class="form-horizontal">
                    <input type="submit" value="エクスポート" Class="btn btn-primary" />
                    <input id="btnImport" type="button" value="インポート" Class="btn btn-primary" />
                </div>
            End Using
        </div>
    </div>
</div>
<hr />
<div class="table-custom">
    <div class="header">Employees</div>

    <table class="table" id="EmpList">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.Employees.FirstOrDefault().Code)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Employees.FirstOrDefault().FullName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Employees.FirstOrDefault().Detail.Gender)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Employees.FirstOrDefault().Detail.Age)
            </th>
            <th></th>
        </tr>

        @For Each item In Model.Employees
            @<tr>
                <td class="view-cell">
                    @Html.HiddenFor(Function(modelItem) item.ID, New With {.class = "EmpID"})
                    @Html.DisplayFor(Function(modelItem) item.Code)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.FullName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Detail.Gender)
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


<div id='mdlDetail' class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div id='mdlDetailContent'></div>
        </div>
    </div>
</div>

<div id='mdlImport' class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div id='mdlImportContent'></div>
        </div>
    </div>
</div>

<div id="mdlOK" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div id='mdlOKContent'></div>
            </div>
            <div class="modal-footer">
                <button type="button" data-dismiss="modal" class="btn btn-primary">Ok</button>
            </div>
        </div>
    </div>
</div>


<div id="dialog" class="divider" style="display: none">
</div>

@Html.Raw(File.ReadAllText(Server.MapPath("~/Views/Shared/Modal/EmpInfoModal.vbhtml")))

@section Styles
    <link href="@Url.Content("/Content/Views/EmpIndex.css")" rel="stylesheet" type="text/css" />
End Section

@Section Scripts
    <script type="text/javascript">
        var myApp = myApp || {};
        myApp.empAPI = '@Url.Action("Emp", "api")';
        myApp.empInfoURL = '@Url.Action("EmpInfo", "Employee")';
        myApp.empImportURL = '@Url.Action("Import", "Employee")';
        myApp.doImportAPI = '@Url.Action("Emp/Import", "api")';
        myApp.doImportURL = '@Url.Action("ImportCSV", "Employee")';
    </script>

    <script type="text/javascript" src="@Url.Content("/Scripts/Views/EmpIndex.js")"></script>
    <script type="text/javascript" src="@Url.Content("/Scripts/Views/EmpImport.js")"></script>
End Section
