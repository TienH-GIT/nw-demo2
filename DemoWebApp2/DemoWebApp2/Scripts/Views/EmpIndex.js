(function ($, window, myApp) {
    'use strict';

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        title: "View Details"
    });

    //-------------------------//
    //      Event handler      //
    //-------------------------//
    $('#EmpList').on('click', 'tbody tr td:first-child', function (e) {
        // Acquire ID
        var id = $(this).find('input').val();
        // Get detail info
        getEmpDetail(id);
    });

    $('#btnImport').on('click', function (e) {
        // Open Import dialog
        gotoImportDialog();
    });

    //-------------------------//
    //      Logic function     //
    //-------------------------//
    function getEmpDetail(id) {
        var options = { "backdrop": "true", keyboard: false };
        //var url = myApp.empAPI + "/" + id;
        var url = myApp.empInfoURL + "/" + id;
        $.ajax({
            method: 'GET',
            url: url,
            success: function (data) {
                // DO SOMETHING HERE
                //alert(JSON.stringify(data));

                // Using base modal
                //$('#modalEmpInfo').modal('show');

                // Using Form dialog
                //$('#dialog').html(data);
                //$('#dialog').dialog('open');

                // Using Form modal
                $('#mdlDetailContent').html(data);
                $('#mdlDetail').modal(options);
                $('#mdlDetail').modal('show');
            },
            error: function (xhr, status, error) { }
        });
    }

    function gotoImportDialog() {
        var options = { "backdrop": "static", keyboard: true };
        var url = myApp.empImportURL;
        $.ajax({
            method: 'GET',
            url: url,
            success: function (data) {
                // Using Form modal
                $('#mdlImportContent').html(data);
                $('#mdlImport').modal(options);
                $('#mdlImport').modal('show');

                // Undisplay alert as default
                $("#warnContent").innerHTML = "";
                $(".alert").hide();
            },
            error: function (xhr, status, error) { }
        });
    }
})(jQuery, window, myApp);