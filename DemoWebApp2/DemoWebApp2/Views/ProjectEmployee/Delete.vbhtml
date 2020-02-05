@ModelType DemoWebApp2.Models.ProjectEmployee
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>ProjectEmployee</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Employee.Code)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Employee.Code)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Project.Code)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Project.Code)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Role)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Role)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.JoinDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.JoinDate)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
