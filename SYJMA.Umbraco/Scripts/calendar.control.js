$(document).ready(function () {

    $('#calendar').fullCalendar({
        header: {
            left: 'prev',
            center: 'title',
            right: 'next'
        },
        defaultView: 'agendaWeek',
        defaultDate: new Date(), // Selected date
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        //events: {
        //	url: 'php/get-events.php',
        //	error: function() {
        //		$('#script-warning').show();
        //	}
        //},
        //loading: function(bool) {
        //	$('#loading').toggle(bool);
        //},
        allDaySlot: false,
        minTime: '9:00',
        maxTime: '17:00',
        events: 'http://demo2054938.mockable.io/1',
        firstDay: 1,
        weekends: false,
        contentHeight: 200,
        timezone: 'local',
        titleFormat: 'D MMM YYYY',
        eventStartEditable: false,
        eventDurationEditable: false,
        eventClick: function (calEvent, jsEvent, view) {
            alert('Event: ' + calEvent.title + ' \nTime: ' + calEvent.start.toString('dd-mm-yyyy') + '\nID: ' + calEvent.id);
        }

    });

});