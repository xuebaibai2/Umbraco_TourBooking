$(document).ready(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev agendaWeek',
            center: 'title',
            right: 'month next'
        },
        defaultView: 'agendaWeek',
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        allDaySlot: false,
        slotDuration: '01:00:00',
        minTime: '9:00',
        maxTime: '17:00',
        firstDay: 1,
        weekends: false,
        height: 250,
        timezone: 'local',
        titleFormat: 'D MMM YYYY',
        eventStartEditable: false,
        eventDurationEditable: false,
        eventClick: function (calEvent, jsEvent, view) {
            removeerrorMSGDiv();
            $('#externalLink').hide();
            $('#selectionConfirm').show();
            $('#selectionConfirm').html(
                'You have chosen <b>' + calEvent.title + '</b> from ' + calEvent.start.format('hha') + ' until ' + calEvent.end.format('hha') + ' on ' + calEvent.start.format('DD MMMM YYYY') + '.  Click Next to confirm.'
        );
            $('#selectedEventId').val(calEvent.id);
            $('#selectedEventTitle').val(calEvent.title);
            $('#selectedEventStart').val(calEvent.start.format());
            $('#selectedEventEnd').val(calEvent.end.format());
        }
    });

    $('#calendarForm input').on('change', function () {
        removeEvents();
        removeerrorMSGDiv();
        addEvents($('input[name=Program]:checked', '#calendarForm').val());
        $('#selectionConfirm').hide();
        $('#externalLink').show();
    });
});

function addEvents(url) {
    $.ajax({
        type: 'POST',
        url: baseUrl + 'umbraco/Surface/JSONData/GetJsonData_Event?eventName=' + url,
        success: function (result) {
            removeEvents();
            addEvent(result);
        }
    });
}

function removeEvents() {
    $('#calendar').fullCalendar('removeEvents')
}

function addEvent(events) {
    $('#calendar').fullCalendar('addEventSource', events);
}

function removeerrorMSGDiv() {
    $('span[id^="errorMSG"]').remove();
}

function validationForm() {
    if ($('#selectedEventId').val() == '') {
        $('#externalLink').append('<span id="errorMSG" style="color:red;">Please select one event before continue</span>');
        return false;
    }
    return true;
}