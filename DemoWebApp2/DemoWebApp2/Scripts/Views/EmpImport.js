(function ($, window, myApp) {
    'use strict';

    var isRefreshPage = false;

    //-------------------------//
    //      Event handler      //
    //-------------------------//
    $(document).on('click', '.file-select', function () {
        $('#InputFile').click();
    });

    $(document).on('change', ':file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.parent().find('input:text').val(label);
    });

    $(document).on('click', '#btnDoImport', function (e) {
        // Undisplay alert
        hideAlert();

        // Take action Import
        doImport();
    });

    $("#mdlImport").on("hidden.bs.modal", function () {
        if (isRefreshPage) {
            // refresh page
            location.reload();
        }
    });

    //-------------------------//
    //      Logic function     //
    //-------------------------//
    function doImport() {
        var form = $('#__AjaxForm');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        var fileUpload = $("#InputFile").get(0);
        var files = fileUpload.files;

        var formData = new FormData();

        if (files.length <= 0) return;

        // Add VerificationToken
        formData.append("__RequestVerificationToken", token);

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            console.log('(files[i].name:' + files[i].name);
            formData.append('UploadFile', files[i]);
        }

        var url = myApp.doImportURL;
        $.ajax({
            method: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                // Display result
                displayResult(data);
            },
            error: function (xhr, status, error) {
                displayAlert('Unnkow error.');
            }
        });
    }

    function displayResult(data) {
        var container = $("#warnContent");
        var args = data.message;

        switch (data.success) {
            case 0:
                // successful case
                showSuccess(args);
                break;
            case 1:
                showErrType1(container, args);
                break;
            case 2:
                showErrType2(container, args);
                break;
            case 3:
                showErrType3(container, args);
                break;
            default:
                displayAlert('Unnkow error.');
        }
    }

    function showSuccess(args) {
        var options = { "backdrop": "static", keyboard: true };
        $('#mdlOKContent').html(args);
        $('#mdlOK').modal(options);
        $('#mdlOK').modal('show');

        // catch close dialog event
        $("#mdlOK").on("hidden.bs.modal", function () {
            // clse import dialog
            isRefreshPage = true;
            //$('#mdlImport').modal('hide');

            // refresh page
            location.reload();
        });
    }

    function showErrType1(container, args) {
        var t = '';
        $.each(args, function (i, v) {
            $.each(v, function (index, value) {
                t += '<li>' + value.ErrorMessage + '</li>';
            })
        })
        const $ul = $('<ul>', { class: "mylist" }).append(t);
        container.append($ul);

        $(".alert").show();
    }

    function showErrType2(container, args) {
        const $ul = $('<ul>', { class: "mylist" }).append(
            args.map(msg =>
                $("<li>").append(msg)
            )
        );
        container.append($ul);

        $(".alert").show();
    }

    function showErrType3(container, args) {
        const $ul = $('<ul>', { class: "mylist" }).append(
            $("<li>").append(args)
        );
        container.append($ul);

        $(".alert").show();
    }

    function displayAlert(msg) {
        $("#warnContent").innerHTML = msg;
        $(".alert").show();
    }

    function hideAlert() {
        $("#warnContent").children().remove();
        $(".alert").hide();
    }

    var dlg = $('#dialog').dialog({
        title: 'Info',
        resizable: true,
        autoOpen: false,
        modal: true,
        hide: 'fade',
        width: 600,
        height: 285,
        close: function (event, ui) {
            location.reload();
        }
    });

    var createSublist = function (container, args) {
        var ul = document.createElement('ul');

        for (var j = 0; j < args.length; j++) {
            var row = args[j];

            var li = document.createElement('li');
            li.innerText = row.text;

            ul.appendChild(li);
        }

        container.appendChild(ul);
    };
})(jQuery, window, myApp);