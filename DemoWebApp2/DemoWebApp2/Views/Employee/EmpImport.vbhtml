@ModelType DemoWebApp2.Models.CsvFile
@Code
    Layout = Nothing
End Code

<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal">&times;</button>
    <h4 class="modal-title" id="myModalLabel">Import CSV</h4>
</div>

<div class="modal-body">
    <div class="alert alert-warning">
        <strong>警告!</strong> エラーは以下になります。
        <div id="warnContent"></div>
    </div>
    <div class="panel panel-default">
        @Using (Html.BeginForm("Import", "Employee", FormMethod.Post, New With {.id = "__AjaxForm", .enctype = "multipart/form-data"}))
            @Html.AntiForgeryToken()

            @<div class="panel-body">
                <div>
                    @Html.LabelFor(Function(model) model.UploadFile)
                    <div class="custom-file">
                        @Html.TextBox("UploadFile", "", New With {.id = "InputFile", .class = "custom-file-input", .Type = "file", .style = "display:none;"})
                        @Html.Label("ファイルを選ぶ", "", New With {.class = "custom-file-label", .for = "InputFile"})
                        <div class="input-group file-select">
                            <span class="input-group-btn">
                                <button type="button" id="file_select_icon" class="btn btn-info"><span class="glyphicon glyphicon-folder-open" aria-hidden="true"></span></button>
                            </span>
                            <input type="text" id="file_name" class="form-control" placeholder="ファイルを選ぶ。。。" readonly>
                        </div>
                    </div>
                    @Html.ValidationMessageFor(Function(model) model.UploadFile, "", New With {.class = "text-danger"})
                </div>
                <br />
                <!-- <input type="submit" value="インポート" class="btn btn-primary" /> -->
                <input type="button" id="btnDoImport" value="インポート" class="btn btn-primary" />
            </div>
        End Using
    </div>
</div>