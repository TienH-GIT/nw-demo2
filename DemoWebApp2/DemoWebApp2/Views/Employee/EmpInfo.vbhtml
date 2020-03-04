@ModelType DemoWebApp2.Models.Employee
@Code
    Layout = Nothing
End Code

<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal">&times;</button>
    <h4 class="modal-title" id="myModalLabel">@Html.DisplayFor(Function(model) model.Code)</h4>
</div>

<div class="modal-body">
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.FullName)
        </dt>
        <dd>
            @Html.DisplayFor(Function(model) model.FullName)
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
