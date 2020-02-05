(function ($, window, myApp) {
    'use strict';

    $('#EmpList').on('dblclick', 'tbody tr td:first-child', function (e) {
        // Require obj ID
        var id = $(this).find('input').val();
        // Get detail info
        getEmpDetail(id);
    });

    function getEmpDetail(id) {
        var url = myApp.empUrl + "/" + id;
        $.ajax({
            method: 'GET',
            url: url,
            success: function (data) {
                // DO SOMETHING HERE
                alert(data);
            },
            error: function (xhr, status, error) { }
        });
    }

    function getEmpDetail2($obj) {
        var id = $obj.find('input').val();
        var url = window.location.origin + "/api/Emp/" + id;
        $.getJSON(url)
            .done(function (data) {
                alert(data);
            })
            .fail(function (data) {
                alert("failed!");
            });
    }
})(jQuery, window, myApp);