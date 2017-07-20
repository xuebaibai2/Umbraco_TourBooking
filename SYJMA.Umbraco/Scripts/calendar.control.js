$(document).ready(function () {
    
    calendar.onTourCellHoverIn = function ($currentCell) {
        $currentCell.addClass("enable");
    }
    calendar.onTourCellHoverOut = function ($currentCell) {
        $currentCell.removeClass("enable");
    }
    calendar.onTourClick = function (tourObj, $currentCell) {
        removeerrorMSGDiv();
        $('#externalLink').hide();
        $('#selectionConfirm').show();
        $('#selectionConfirm').html(
            'You have chosen <b>' + tourObj.title + '</b> from ' + moment(tourObj.start).format('hh:mma') + ' until ' + moment(tourObj.end).format('hh:mma') + ' on ' + moment(tourObj.start).format('DD MMMM YYYY') + '.  Click Next to confirm.'
        );

        $('#selectedEventId').val(tourObj.id);
        $('#selectedEventTitle').val(tourObj.title);
        $('#selectedEventStart').val(moment(tourObj.start).format());
        $('#selectedEventEnd').val(moment(tourObj.end).format());
        $('#isInvoiceOnly').val(tourObj.IsInvoiceOnly);
    }
    $('#calendarForm input').on('change', function () {
        //removeEvents();
        removeerrorMSGDiv();
        addEvents($('input[name=Program]:checked', '#calendarForm').val());
        $('#selectionConfirm').hide();
        $('#externalLink').show();
    });
});

function addSelectionClass(parentNode, targetElement, className) {
    $(parentNode).find($(targetElement).get(0).tagName.toLowerCase()).each(function () {
        if ($(this).is('.' + className)) {
            $(this).removeClass(className);
        }
    });
    $(targetElement).addClass(className);
}

function addEvents(eventname) {
    $.ajax({
        type: 'POST',
        url: baseUrl + 'umbraco/Surface/JSONData/GetJsonData_Event?eventName=' + eventname + '&category=' + category,
        success: function (result) {
            calendar.addEventSource(result);
        }
    });
}

function removeerrorMSGDiv() {
    $('span[id^="errorMSG"]').remove();
}

function validationForm() {
    if ($('#selectedEventId').val() == '') {
        $('span[id^="errorMSG"]').remove();
        $('#externalLink').append('<span id="errorMSG" style="color:red;">Please select one event before continue</span>');
        return false;
    }
    return true;
}