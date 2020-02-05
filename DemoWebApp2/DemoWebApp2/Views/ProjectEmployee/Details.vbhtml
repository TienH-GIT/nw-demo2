@ModelType DemoWebApp2.Models.ProjectEmployee
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

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
</div>
<p>
    @*@Html.ActionLink("Edit", "Edit", New With {.id = Model.PrimaryKey}) |*@
    @Html.ActionLink("Back to List", "Index")
</p>
