@ModelType DemoWebApp2.Models.Project
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Project</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Department.Code)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Department.Code)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Code)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Code)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Description)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ScopeType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ScopeType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Scale)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Scale)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Duration)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Duration)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PlanStartDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PlanStartDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PlanEndDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PlanEndDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EndDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EndDate)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ProjectID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
