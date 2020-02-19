@ModelType DemoWebApp2.Models.CsvFile
@Code
    Layout = Nothing
End Code

<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal">&times;</button>
    <h4 class="modal-title" id="myModalLabel">Import CSV</h4>
</div>

<div class="modal-body">
    <div class="panel panel-default">
        @Using (Html.BeginForm("Import", "Employee", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
            @Html.AntiForgeryToken()

            @<div class="panel-body">
                <div>
                    @Html.LabelFor(Function(model) model.UploadFile, New With {.htmlAttributes = New With {.for = "InputFile"}})
                    @Html.TextBox("UploadFile", "", New With {.id = "InputFile", .Type = "file"})
                    @Html.ValidationMessageFor(Function(model) model.UploadFile, "", New With {.class = "text-danger"})
                </div>
                <br />
                <input type="submit" value="インポート" Class="btn btn-primary" />
            </div>
        End Using
    </div>
</div>