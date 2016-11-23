$(document).ready(function () {
    var nameList = [];
    $.ajax({
        type: 'POST',
        url: '/umbraco/Surface/JSONData/GetSchoolNameList',
        success: function (result) {
            //nameList = result;
            $("#Name").autocomplete({
                source: result,
                autoFocus: true
            });
        }
    });
})

