(function ($, window, myApp) {
    'use strict';

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        title: "View Details"
    });

    $('#EmpList').on('click', 'tbody tr td:first-child', function (e) {
        // Acquire ID
        var id = $(this).find('input').val();
        // Get detail info
        getEmpDetail(id);
    });

    function getEmpDetail(id) {
        var options = { "backdrop": "static", keyboard: true };
        //var url = myApp.empAPI + "/" + id;
        var url = myApp.empUrl + "/" + id;
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
                $('#myModalContent').html(data);
                //$('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function (xhr, status, error) { }
        });
    }
})(jQuery, window, myApp);