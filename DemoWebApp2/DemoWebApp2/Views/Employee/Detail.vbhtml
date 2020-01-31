@ModelType DemoWebApp2.Models.Employee
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Employee Info</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Code)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Code)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FirstName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LastName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Gender)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Gender)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Birthday)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Birthday)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Age)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Age)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Address)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Address)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Status)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Status)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.Detail.Hobby)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.Detail.Hobby)
        </dd>
    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}) |
    @Html.ActionLink("Back to List", "Index")
</p>
