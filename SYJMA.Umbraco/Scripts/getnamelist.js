$(document).ready(function () {
    var nameList = [];
    if (category == 'School') {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'umbraco/Surface/JSONData/GetJsonData_SchoolNameList',
            success: function (result) {
                $("#Name").autocomplete({
                    source: result,
                    autoFocus: true,
                    delay: 300
                });
            }
        });
    } else if (category == 'University') {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'umbraco/Surface/JSONData/GetJsonData_UniNameList',
            success: function (result) {
                $("#Name").autocomplete({
                    source: result,
                    autoFocus: true,
                    delay: 300
                });
            }
        });
    }
    
})

