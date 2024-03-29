﻿var calendar = {
    model: {
        date: {},
        eventSourceArray: [],
        displayEvents: { am: {}, pm: {} },
        middayHour: "13:00:00.000",
        midNight: "23:59:59.999"
    },
    init: function (date) {
        this.cacheDom();
        this.generateCalendarTbl();
        this.generateVerticalCalendarTbl();
        this.displayCalendar();
        this.momentJsSetting();
        this.fillDateInfoToTbl(date);
        this.fillDataInfoToVerticalTbl(date);
    },
    cacheDom: function () {
        this.$mainDiv = $("#syjma_calendar");
        this.$navBtnDiv = $("<div/>", { clss: "col-lg-12 col-md-12 col-sm-12 col-xs-12" }).appendTo(this.$mainDiv);
        this.$preWeekBtn = $("<a />", { id: "preWeekBtn", text: "Previous Week", click: this.eventPreBtnClicked.bind(this) }).appendTo(this.$navBtnDiv);
        this.$nextWeekBtn = $("<a />", { id: "nextWeekBtn", text: "Next Week", click: this.eventNextBtnClicked.bind(this) }).appendTo(this.$navBtnDiv);
        this.$calendarTbl = $("<table />", { class: "calendarTbl" }).appendTo(this.$mainDiv);
        this.$verticalCalendarTbl = $("<table />", { class: "verticalCalendarTbl" }).appendTo(this.$mainDiv);
        this.$calendarTblHeader = $("<tr>", { id: "calendar_header" }).appendTo(this.$calendarTbl);

    },
    displayCalendar: function () {
        var self = this;
        if ($(window).width() > 830) {
            $(".calendarTbl").show();
            $(".verticalCalendarTbl").hide();
            this.$mainDiv.css("max-width", this.$calendarTbl.outerWidth());
        } else {
            $(".calendarTbl").hide();
            $(".verticalCalendarTbl").show();
            this.$mainDiv.css("max-width", this.$verticalCalendarTbl.outerWidth());
        }
        $(window).resize(function () {
            if ($(window).width() > 830) {
                $(".calendarTbl").show();
                $(".verticalCalendarTbl").hide();
                self.$mainDiv.css("max-width", self.$calendarTbl.outerWidth());
            } else {
                $(".calendarTbl").hide();
                $(".verticalCalendarTbl").show();
                self.$mainDiv.css("max-width", self.$verticalCalendarTbl.outerWidth());
            }
        });
    },
    generateCalendarTbl: function () {
        //Table header
        for (var i = 0; i < 5; i++) {
            var $dayTh = $("<th>", { id: this.tableHeaderConvertor[i] });
            this.$calendarTblHeader.append($dayTh);
        }
        this.$headerTrs = $("#calendar_header th");
        //Table body
        for (var i = 0; i < 2; i++) {
            var $dayTr = $("<tr>", { id: this.dayConvertor[i] });
            this.$calendarTbl.append($dayTr);
            for (var j = 0; j < 5; j++) {
                var $dateTd = $("<td>", { id: this.weekConvertor[j] + "-" + this.dayConvertor[i] });
                $dayTr.append($dateTd);
            }
        }
    },
    generateVerticalCalendarTbl: function () {
        //Table body
        for (var i = 0; i < 5; i++) {
            var $eventSessionTr = $("<tr>", { id: this.weekConvertor[i] + "-tr" });
            this.$verticalCalendarTbl.append($eventSessionTr);
            for (var j = -1; j < 2; j++) {
                if (j !== -1) {
                    var $dateTd = $("<td>", { id: this.weekConvertor[i] + "-" + this.dayConvertor[j] + "-v" });
                } else {
                    var $dateTd = $("<td>", { id: this.weekConvertor[i] + "-v" })
                }
                $eventSessionTr.append($dateTd);
            }
        }
    },
    addEventSource: function (eventSourceArray) {
        this.resetCalendar();
        this.resetVerticalCalendar();
        this.model.eventSourceArray = eventSourceArray;
        this.filterStagingEvents();
        this.renderEvents();
        this.renderVerticalCalendarEvents();
    },
    resetCalendar: function () {
        this.model.displayEvents = { am: {}, pm: {} };
        var $amTr = $("#am td");
        var $pmTr = $("#pm td");
        for (var i = 0; i < 2; i++) {
            var $currentRow = i === 0 ? $amTr : $pmTr;
            for (var j = 0; j < 5; j++) {
                var $tempTd = $($currentRow[j]);
                $tempTd.removeClass();
                $tempTd.off("click");
                $tempTd.unbind("mouseenter mouseleave");
            }
        }
    },
    resetVerticalCalendar: function () {
        for (var i = 0; i < 5; i++) {
            for (var j = 0; j < 2; j++) {
                var $tempTd = $("#" + this.weekConvertor[i] + "-" + this.dayConvertor[j] + "-v");
                $tempTd.removeClass();
                $tempTd.off("click");
                $tempTd.unbind("mouseenter mouseleave");
            }
        }
    },
    filterStagingEvents: function () {
        var startDate = this.model.date.weekday(0).format();
        var endDate = this.model.date.weekday(5).format();
        var self = this;
        var tempDispalyArray = [];
        this.model.eventSourceArray.map(function (tour) {
            if (moment(tour.start).isBetween(startDate, endDate, null, '[)')) {
                tempDispalyArray.push(tour);
            }
        });

        for (var i = 0; i <= 5; i++) {
            var tempBegin_Today = this.model.date.weekday(i).format("YYYY-MM-DD HH:mm:ss");
            var tempMidday = this.model.date.weekday(i).format("YYYY-MM-DD") + " " + this.model.middayHour;
            var tempEnd_Today = this.model.date.weekday(i).format("YYYY-MM-DD") + " " + this.model.midNight;
            tempDispalyArray.map(function (tour) {
                if (self.isDateTimeBetween(tour.start, tempBegin_Today, tempMidday)) {
                    if (self.isDateTimeBetween(tour.end, tempMidday, tempEnd_Today)) {
                        tour.isContinuous = true;
                        var morninghalf = $.extend(true, {}, tour);
                        var afternonHalf = $.extend(true, {}, tour);
                        morninghalf.end = tempMidday;
                        afternonHalf.start = tempMidday;
                        self.model.displayEvents.am[morninghalf.id] = morninghalf;
                        self.model.displayEvents.pm[afternonHalf.id] = afternonHalf;
                    } else {
                        self.model.displayEvents.am[tour.id] = tour;
                    }
                } else if (self.isDateTimeBetween(tour.start, tempMidday, tempEnd_Today)) {
                    self.model.displayEvents.pm[tour.id] = tour;
                }
            });
        }
    },
    isDateTimeBetween: function (date, from_date, end_date) {
        return moment(date).isBetween(from_date, end_date, null, '[)');
    },
    isSameDateTime: function (date1, date2) {
        return moment(date1).isSame(date2);
    },
    renderEvents: function () {
        var self = this;
        var $amTr = $("#am td");
        var $pmTr = $("#pm td");
        for (var i = 0; i < 2; i++) {
            var $insertRow = i === 0 ? $amTr : $pmTr;
            var displayTourGroup = i === 0 ? self.model.displayEvents.am : self.model.displayEvents.pm;
            for (var j = 0; j < 5; j++) {
                var $tempTd = $($insertRow[j]);
                $tempTd.text("");
                $tempTd.removeClass();
                var tempDate = this.model.date.weekday(j).format("YYYY-MM-DD");
                for (var tour in displayTourGroup) {
                    var tempTour = displayTourGroup[tour];
                    var tempTourDate = moment(tempTour.start).format("YYYY-MM-DD");
                    if (self.isSameDateTime(tempDate, tempTourDate)) {
                        var renderText = moment(tempTour.start).format("h:mm a") + " - " + moment(tempTour.end).format("h:mm a");
                        //var renderText = tempTour.title;
                        $tempTd.text(renderText);
                        $tempTd.addClass(tempTour.id);
                        $tempTd.off("click").on("click", this.eventTourClick.bind(this));
                        $tempTd.hover(this.eventTourCellHoverIn.bind(this), this.eventTourCellHoverOut.bind(this));
                    }
                }
            }
        }
    },
    renderVerticalCalendarEvents: function () {
        var self = this;
        for (var i = 0; i < 5; i++) {
            for (var j = 0; j < 2; j++) {
                var tempDate = this.model.date.weekday(i).format("YYYY-MM-DD");
                var displayTourGroup = j === 0 ? self.model.displayEvents.am : self.model.displayEvents.pm;
                var $tempTd = $("#" + this.weekConvertor[i] + "-" + this.dayConvertor[j] + "-v");
                $tempTd.text("");
                $tempTd.removeClass();
                for (var tour in displayTourGroup) {
                    var tempTour = displayTourGroup[tour];
                    var tempTourDate = moment(tempTour.start).format("YYYY-MM-DD");
                    if (self.isSameDateTime(tempDate, tempTourDate)) {
                        var renderText = moment(tempTour.start).format("h:mm a") + " - " + moment(tempTour.end).format("h:mm a");
                        $tempTd.text(renderText);
                        $tempTd.addClass(tempTour.id);
                        $tempTd.off("click").on("click", this.eventTourClick.bind(this));
                        $tempTd.hover(this.eventTourCellHoverIn.bind(this), this.eventTourCellHoverOut.bind(this));
                    }
                }
            }
        }
    },
    eventTourClick: function (e) {
        var $self = $(e.currentTarget);
        $("td.selected").removeClass("selected");
        var idElements = $self.attr("id").split("-"); // "am" "pm" or "v" if vertical calendar
        var lastIdElements = idElements[idElements.length - 1];
        var $correspondingOtherTd = lastIdElements === "v" ?
            $("#" + idElements.slice(0, -1).join("-")) :
            $("#" + idElements.join("-") + "-v");
        $self.addClass("selected");
        $correspondingOtherTd.addClass("selected");
        for (var i = 0; i < this.model.eventSourceArray.length; i++) {
            if ($self.hasClass(this.model.eventSourceArray[i].id)) {
                this.onTourClick(this.model.eventSourceArray[i], $self)
            }
        }
    },
    eventTourCellHoverIn: function (e) {
        var $self = $(e.currentTarget);
        this.onTourCellHoverIn($self);
    },
    eventTourCellHoverOut: function (e) {
        var $self = $(e.currentTarget);
        this.onTourCellHoverOut($self);
    },
    eventPreBtnClicked: function () {
        this.resetCalendar();
        this.resetVerticalCalendar();
        this.model.date = this.model.date.weekday(-7);
        this.updateCalendarHeader();
        this.updateVerticalCalendarFirstTd();
        this.filterStagingEvents();
        this.renderEvents();
        this.renderVerticalCalendarEvents();
        return false;
    },
    eventNextBtnClicked: function () {
        this.resetCalendar();
        this.resetVerticalCalendar();
        this.model.date = this.model.date.weekday(7);
        this.updateCalendarHeader();
        this.updateVerticalCalendarFirstTd();
        this.filterStagingEvents();
        this.renderEvents();
        this.renderVerticalCalendarEvents();
        return false;
    },
    onTourClick: function (tourObj, $currentCell) {
    },
    onTourCellHoverIn: function ($currentCell) {
    },
    onTourCellHoverOut: function ($currentCell) {
    },
    generateWeekNavBtn: function () {

    },
    fillDateInfoToTbl: function (date) {
        this.model.date = moment(date);//"2017-07-20"
        this.updateCalendarHeader();
    },
    fillDataInfoToVerticalTbl: function (data) {
        this.updateVerticalCalendarFirstTd();
    },
    momentJsSetting: function () {
        moment.locale("en-AU", {
            week: {
                dow: 1
            }
        });
    },
    updateCalendarHeader: function () {
        for (var i = 0; i < this.$headerTrs.length; i++) {
            this.$headerTrs[i].innerText = this.model.date.weekday(i).format("ddd DD/MM");
        }
    },
    updateVerticalCalendarFirstTd: function () {
        for (var i = 0; i < 5; i++) {
            $("#" + this.weekConvertor[i] + "-v").html("");
            $("#" + this.weekConvertor[i] + "-v").append(this.model.date.weekday(i).format("ddd DD/MM"));
        }
    },
    weekConvertor: {
        0: "mon",
        1: "tue",
        2: "wed",
        3: "thu",
        4: "fri",
        5: "sat",
        6: "sun"
    },
    dayConvertor: {
        0: "am",
        1: "pm"
    },
    tableHeaderConvertor: {
        0: "h_mon",
        1: "h_tue",
        2: "h_wed",
        3: "h_thu",
        4: "h_fri",
        5: "h_sat",
        6: "h_sun"
    }
}