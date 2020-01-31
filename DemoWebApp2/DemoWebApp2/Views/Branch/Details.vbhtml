@ModelType DemoWebApp2.Models.Branch
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Branch</h4>
    <hr />
    <dl class="dl-horizontal">
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
            @Html.DisplayNameFor(Function(model) model.KataName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.KataName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Address)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Address)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Description)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.BranchID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
