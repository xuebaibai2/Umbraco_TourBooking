$(document).ready(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev agendaWeek',
            center: 'title',
            right: 'month next'
        },
        defaultView: 'agendaWeek',
        //defaultDate: new Date(), // Selected date
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        allDaySlot: false,
        minTime: '9:00',
        maxTime: '17:00',
        firstDay: 1,
        weekends: false,
        height: 428,
        timezone: 'local',
        titleFormat: 'D MMM YYYY',
        eventStartEditable: false,
        eventDurationEditable: false,
        eventClick: function (calEvent, jsEvent, view) {
            
            $('#selectionConfirm').html(
                'You have chosen <b>' + calEvent.title + '</b> from ' + calEvent.start.format('hha') + ' until ' + calEvent.end.format('hha') + ' on ' + calEvent.start.format('DD MMMM YYYY') + '.  Click Next to confirm.'
        );
            $('#selectedEventId').val(calEvent.id);
            $('#selectedEventTitle').val(calEvent.title);
            //$('#selectedEventStart').val(calEvent.start.format('DD-MM-YYYY kk:mm'));
            //$('#selectedEventEnd').val(calEvent.end.format('DD-MM-YYYY kk:mm'));
            $('#selectedEventStart').val(calEvent.start.format());
            $('#selectedEventEnd').val(calEvent.end.format());
            $('#selectedEventStudentPrice').val(calEvent.studentPrice);
        }
    });

    $('#calendarForm input').on('change', function () {
        addEvents($('input[name=Program]:checked', '#calendarForm').val());
        $('#selectionConfirm').html('');
    });
});

function addEvents(url) {
    $.ajax({
        type: 'POST',
        url: '/umbraco/Surface/JSONData/GetJsonData?eventName=' + url,
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