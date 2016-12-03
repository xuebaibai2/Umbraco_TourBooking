﻿$(document).ready(function () {
    var nameList = [];
    $.ajax({
        type: 'POST',
        url: baseUrl + 'umbraco/Surface/JSONData/GetSchoolNameList',
        success: function (result) {
            $("#Name").autocomplete({
                source: result,
                autoFocus: true,
                delay: 500
            });
        }
    });
})

