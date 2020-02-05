@ModelType DemoWebApp2.Models.ProjectEmployee
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>ProjectEmployee</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.ProjectID)

        @Html.HiddenFor(Function(model) model.EmployeeID)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Role, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EnumDropDownListFor(Function(model) model.Role, htmlAttributes:=New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.Role, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.JoinDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.JoinDate, New With {.htmlAttributes = New With {.class = "form-control datepicker"}})
                @Html.ValidationMessageFor(Function(model) model.JoinDate, "", New With {.class = "text-danger"})
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
