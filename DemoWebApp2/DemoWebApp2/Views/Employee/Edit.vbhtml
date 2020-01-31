@Imports DemoWebApp2.Models
@ModelType DemoWebApp2.Models.Employee
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Employee</h4>
        <hr />
        @Html.ValidationSummary(True)
        @Html.HiddenFor(Function(model) model.ID)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Code, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Code, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Code, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.FirstName, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.FirstName, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.FirstName, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.LastName, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.LastName, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.LastName, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Gender, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.RadioButtonFor(Function(model) model.Detail.Gender, "Male", New With {.id = "male"})
                @Html.Label("male", "Male", New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.RadioButtonFor(Function(model) model.Detail.Gender, "Female", New With {.id = "female"})
                @Html.Label("female", "Female", New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Detail.Gender, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Birthday, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Detail.Birthday, New With {.htmlAttributes = New With {.class = "form-control datepicker"}})
                @Html.ValidationMessageFor(Function(model) model.Detail.Birthday, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Age, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Detail.Age, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Detail.Age, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Address, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Detail.Address, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Detail.Address, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Status, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownListFor(Function(model) model.Detail.Status, EnumHelper.GetSelectList(GetType(StatusEnum)), "-- SELECT --", New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.Detail.Status, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Detail.Hobby, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Detail.Hobby, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Detail.Hobby, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.BranchID, htmlAttributes:=New With {.class = "control-label col-sm-2"})
            <div class="col-sm-10">
                @Html.DropDownListFor(Function(model) model.BranchID, DirectCast(ViewBag.model1, IEnumerable(Of SelectListItem)), "-- SELECT --", New With {.class = "form-control"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.JobTitleID, htmlAttributes:=New With {.class = "control-label col-sm-2"})
            <div class="col-sm-10">
                @Html.DropDownListFor(Function(model) model.JobTitleID, DirectCast(ViewBag.model2, IEnumerable(Of SelectListItem)), "-- SELECT --", New With {.class = "form-control"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")

    <script type="text/javascript">
        $(function () { // will trigger when the document is ready
            $('.datepicker').datepicker({
                language: "ja",
                todayHighlight: true
            }); //Initialise any date pickers
        });
    </script>
End Section
