var datePattern = /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/;
var numberPattern = /^\d+$/;;

function validationForm() {
    if ($("#Name")[0]) {
        if ($('#Name').val() == '') {
            return validateInput('#Name-Validation', 'Please enter school name');
        } else {
            $('#Name-Validation').text('');
        }
    }
    if ($("#AdultGroupName")[0]) {
        if ($('#AdultGroupName').val() == '') {
            return validateInput('#Name-Validation', 'Please enter group name');
        } else {
            $('#Name-Validation').text('');
        }
    }
    if ($("#CampusName")[0]) {
        if ($('#CampusName').val() == '') {
            return validateInput('#CampusName-Validation', 'Please enter campus name');
        } else {
            $('#CampusName-Validation').text('');
        }
    }
    if ($("#datepicker")[0]) {
        if ($('#datepicker').val() == '') {
            return validateInput('#datepicker-Validation', 'Please select one preferred date');
        } else {
            if (!datePattern.test($('#datepicker').val())) {
                $('#datepicker-Validation').text('Please enter correct date format DD/MM/YYYY');
                return false;
            } else {
                $('#datepicker-Validation').text('');
            }
        }
    }
    if ($("#studentNo")[0]) {
        if ($('#studentNo').val() == '') {
            return validateInput('#studentNo-Validation', 'Please enter student number');
        } else {
            if (!numberPattern.test($('#studentNo').val())) {
                $('#studentNo-Validation').text('Please enter number only');
                return false;
            } else {
                $('#studentNo-Validation').text('');
            }
        }
    }
    if ($("#adultNo")[0]) {
        if ($('#adultNo').val() == '') {
            return validateInput('#adultNo-Validation', 'Please enter adult number');
        } else {
            if (!numberPattern.test($('#adultNo').val())) {
                $('#adultNo-Validation').text('Please enter number only');
                return false;
            } else {
                $('#adultNo-Validation').text('');
            }
        }
    }
    if ($("#staffNo")[0]) {
        if ($('#staffNo').val() == '') {
            $('#staffNo').val('0');
            $('#staffNo-Validation').text('');
        } else {
            if (!numberPattern.test($('#staffNo').val())) {
                $('#staffNo-Validation').text('Please enter number only');
                return false;
            } else {
                $('#staffNo-Validation').text('');
            }
        }
    }
    return true;
}
function validateInput(spanID, message) {
    $(spanID).text(message);
    return false;
}