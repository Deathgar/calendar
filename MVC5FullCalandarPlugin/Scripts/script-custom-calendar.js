
$(document).ready(function () {
    //var calendarEl = document.getElementById('calendar');

    //var calendar = new FullCalendar.Calendar(calendarEl, {
    //    timeZone: 'UTC',
    //    plugins: ['dayGrid', 'timeGrid', 'list', 'interaction'],
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
    //    },
    //    navLinks: true, // can click day/week names to navigate views
    //    editable: true,
    //    eventLimit: true, // allow "more" link when too many events
    //    events: '/demo-events.json?overload-day' // TODO: use BASE_URL somehow
    //});
    var i = 0;

    $('#calendar').fullCalendar({
        plugins: ["interaction", "dayGrid", "timeGrid"],
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },
        navLinks: true,
        eventLimit: true,
        timeZone: 'local',
        selectable: true,
        views: {
            month: {
                eventLimit: 3
            }
        },

        dayClick: function (info) {

            console.log(info.year() + " : " + info.month());
            Event(info.year() + "-" + ((info.month() < 9) ? ("0" + (info.month()+1)) : info.month()) + "-" + ((info.date() < 10) ? ("0" + info.date()) : info.date()));
           // $('#hiddenInfo').val(info.data);
           //sessionStorage.setItem("ww", "wwwww");
            //console.log(sessionStorage.getItem("ww"));
            //localStorage.setItem("ww", "ee");
           
        },


        events: function (start, end, timezone, callback) {
            var token = localStorage.getItem("token");
	        $.ajax({
		        url: '/Home/GetCalendarData',
		        type: "GET",
		        data:
		        {
                    token: token
		        },

		        success: function (result) {
                    var events = [];
                    var datePrev = "null";
                    var timeTemp = +0;
                 
                    $('[data-date="2019-04-16"]').filter('.fc-day').html('<div style="max-height: 100%; height: 100%; max-width: 100%; text-align: right; margin-top: 0;"><div style="margin-top: 10%;">8</div></div>');
                    $.each(result, function (i, data) {

                        var date = moment(data.Start_Date).format('YYYY-MM-DD');
                        console.log(date + " : " + (datePrev != date));

                        if (date != datePrev) {

                            if (datePrev != "null") {
                                console.log("  addTime[" + datePrev + "] = " + timeTemp);
                                if(timeTemp > 8) {

	                                var dataDate = '[data-date="' + datePrev + '"]:not(.fc-day-top)';
                                    $(dataDate).css('backgroundColor', '#ff8383');
                                }
                                if (timeTemp < 4) {

	                                var dataDate = '[data-date="' + datePrev + '"]:not(.fc-day-top)';
                                    $(dataDate).css('backgroundColor', '#84e8ff');
                                }

	                            datePrev = date;
	                            timeTemp = data.Time;
	                            console.log("----------");
	                            console.log("  newTime = " + timeTemp);
                            } else {
                                datePrev = date;
                                timeTemp = +data.Time;
                            }
                        } else {

	                        timeTemp = +data.Time + +timeTemp;
                            console.log("  sumTime[" + datePrev + "] = " + timeTemp);
                        }

                        if (i == result.length - 1) {
                            console.log("  addTime[" + datePrev + "] = " + timeTemp);
                            if (timeTemp > 8) {

	                            var dataDate = '[data-date="' + datePrev + '"]:not(.fc-day-top)';
                                $(dataDate).css('backgroundColor', '#ff8383');
                            }
                            if (timeTemp < 4) {

	                            var dataDate = '[data-date="' + datePrev + '"]:not(.fc-day-top)';
                                $(dataDate).css('backgroundColor', '#84e8ff');
                            }
                        }

                        events.push(
                            {
                                title: data.Title,
                                description: data.Desc,
                                start: moment(data.Start_Date).format('YYYY-MM-DD'),
                                end: moment(data.End_Date).format('YYYY-MM-DD'),
                                allDay: true,
                                backgroundColor: "#9501fc",
                                borderColor: "#494949"
                            });
                    });

                    callback(events);
                }
            });
        },

        eventRender: function (event, element) {
            
            element.qtip(
                {
                    content: event.description
                });
        },

        editable: false
    });

    
});


function Event(date) {
    swal({
        showCancelButton: true,
        closeOnConfirm: false,
        html: true,
        title: 'Event',
        text: 'Event <input id="event" class="form-control" style="margin-left: 18%"><br/>' +
            'Time <input id="hour" class="form-control"  style="margin-left: 18%">' +
            '<input id="hiddenInfo" style="display:none"></div>'
    }, function (isConfirm) {
        if (!isConfirm) return false;

        if ($('#event').val() && !isNaN($('#hour').val()) && $('#hour').val() < 24 && $('#hour').val() > 0) 
        {
            var eventName = $('#event').val();

	        $.ajax({
		        url: "/DayEvents/SetTimeAndEvent",
		        type: "POST",
		        data:
		        {
			        "events": eventName,
			        "hour": $('#hour').val(),
			        "date": date,
			        "token": localStorage.getItem('token')
        },
                success: function (request) {
                    swal("", "Good job", "success");

                    var event = {
                        title: eventName,
                        description: eventName,
                        start: date,
	                    allDay: true,
	                    backgroundColor: "#9501fc",
	                    borderColor: "#494949"
                    };

                    $('[data-date="2019-04-16"]').css('backgroundColor', 'yellow');
                    
                    $('#calendar').fullCalendar('renderEvent', event, true);
                   
                    var token = localStorage.getItem("token");

                }
            }).fail(function () {
                swal("Warning!", "Something wrong with web.", "warning");

            })

        }
        else {
            swal("Warning!", "Input is incorrect.", "warning");
            return false;
        }
    });
}
