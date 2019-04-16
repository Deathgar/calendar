
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

            console.log(info);
            Event(info.year() + "-" + ((info.month() < 9) ? ("0" + (info.month()+1)) : info.month()) + "-" + ((info.date() < 10) ? ("0" + info.date()) : info.date()));
           // $('#hiddenInfo').val(info.data);
           //sessionStorage.setItem("ww", "wwwww");
            //console.log(sessionStorage.getItem("ww"));
            //localStorage.setItem("ww", "ee");
           
        },

        eventClick: function(info) {
	        GetEventInfo(info);
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
                 
                    //$('[data-date="2019-04-16"]').filter('.fc-day').html('<div style="max-height: 100%; height: 100%; max-width: 100%; text-align: right; margin-top: 0;"><div style="margin-top: 10%;">8</div></div>');
                    $.each(result, function (i, data) {

                        if (data.AllTime < 4) {
	                        events.push(
		                        {
			                        start: moment(data.Date).format('YYYY-MM-DD'),
                                    end: moment(data.Date).format('YYYY-MM-DD'),
			                        rendering: 'background',
			                        color: 'green'
		                        });
                        }
                        if (data.AllTime > 7) {
	                        events.push(
		                        {
                                    start: moment(data.Date).format('YYYY-MM-DD'),
                                    end: moment(data.Date).format('YYYY-MM-DD'),
			                        rendering: 'background',
			                        color: 'red'
		                        });
                        }

                        $.each(data.PublicHolidays,
                            function (i, newData) {
	                            
	                            events.push(
			                        {
				                        title: newData.Title,
				                        description: newData.Desc,
                                        start: moment(newData.Start_Date).format('YYYY-MM-DD'),
                                        end: moment(newData.End_Date).format('YYYY-MM-DD'),
                                        allDay: true,
				                        backgroundColor: "#9501fc",
                                        borderColor: "#494949",
                                        id: newData.Id
			                        });

	                        });
                       
                        
                    });
                    console.log(events);
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

function GetEventInfo(info) {
    console.log();
    $.ajax({
	    url: "/DayEvents/GetEvent",
	    type: "GET",
	    data:
	    {
		    "date": info.start._i,
		    "token": localStorage.getItem('token'),
		    "id": info._id
	    },
	    success: function(request) {

		    console.log(request);

            var title = request.Title;
		    var time = request.Time;
            var description = request.Description;

            swal({
		            showCancelButton: true,
		            closeOnConfirm: false,
		            html: true,
		            title: 'Event',
		            text: 'Title <input id="title" class="form-control" style="margin-left: 18%" placeholder="Title" value="'+ title +'"><br/>' +
                        'Time <input id="time" class="form-control"  style="margin-left: 18%" placeholder="Time" value="' + time +'"><br/>' +
			            'Description<input id="idEvent" style="display:none"><br/>' +
                        ' <textarea id="description" name="message" rows="3" cols="30" placeholder="Description">' + description +'</textarea>'
	            },
	            function(isConfirm) {
                    if (!isConfirm) return false;

                    if ($('#title').val() && !isNaN($('#time').val()) && $('#time').val() < 24 && $('#time').val() > 0) {
                        var date = info.start._i;
                        var titleName = $('#title').val();
                        var description = $('#description').val();

                        $.ajax({
	                        url: "/DayEvents/ChangeTimeAndEvent",
	                        type: "POST",
	                        data:
	                        {
		                        "title": titleName,
		                        "time": $('#time').val(),
		                        "date": date,
		                        "description": description,
                                "token": localStorage.getItem('token'),
                                "id": info._id
                            },
                            success: function (request) {
                                swal("", "Good job", "success");

                                var event = {
                                    title: titleName,
                                    description: description,
                                    start: date,
                                    allDay: true,
                                    backgroundColor: "#9501fc",
                                    borderColor: "#494949",
                                    id: request
                                };

                                $.ajax({
                                    url: '/DayEvents/GetAllTimeInDay',
                                    type: "GET",
                                    data:
                                    {
                                        'date': date,
                                        'token': localStorage.getItem('token')
                                    },
                                    success:
                                        function (req) {

                                            var eventColor = {
                                                start: date,
                                                end: date,
                                                rendering: 'background',
                                                color: 'white'
                                            }
                                            for (var i = 0; i < 10; i++) {
                                                $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                            }


                                            if (req > 8) {
                                                var eventColor = {
                                                    start: date,
                                                    end: date,
                                                    rendering: 'background',
                                                    color: 'red'
                                                }
                                                $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                            }
                                            if (req < 4) {
                                                var eventColor = {
                                                    start: date,
                                                    end: date,
                                                    rendering: 'background',
                                                    color: 'green'
                                                }
                                                $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                            }
                                        }

                                });

                                $('#calendar').fullCalendar('renderEvent', event, true);

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
    });
}

function Event(date) {
    swal({
        showCancelButton: true,
        closeOnConfirm: false,
        html: true,
        title: 'Event',
        text: 'Title <input id="event" class="form-control" style="margin-left: 18%" placeholder="Title"><br/>' +
            'Time <input id="hour" class="form-control"  style="margin-left: 18%" placeholder="Time"><br/>' +
            'Description<input id="idEvent" style="display:none"><br/>' +
            ' <textarea id="description" name="message" rows="3" cols="30" placeholder="Description"></textarea>'
    }, function (isConfirm) {
        if (!isConfirm) return false;

        if ($('#event').val() && !isNaN($('#hour').val()) && $('#hour').val() < 24 && $('#hour').val() > 0) 
        {
            var eventName = $('#event').val();
            var description = $('#description').val();

	        $.ajax({
		        url: "/DayEvents/AddTimeAndEvent",
		        type: "POST",
		        data:
		        {
			        "events": eventName,
			        "hour": $('#hour').val(),
                    "date": date,
			        "description": description,
                    "token": localStorage.getItem('token')
                    
        },
                success: function (request) {
                    swal("", "Good job", "success");

                    var event = {
	                    title: eventName,
	                    description: description,
	                    start: date,
	                    allDay: true,
	                    backgroundColor: "#9501fc",
	                    borderColor: "#494949",
	                    id: request
                };

                    $.ajax({
                        url: '/DayEvents/GetAllTimeInDay',
	                    type: "GET",
	                    data:
	                    {
		                    'date': date,
		                    'token': localStorage.getItem('token')
	                    },
                        success:
                            function (req) {

	                            var eventColor = {
		                            start: date,
		                            end: date,
		                            rendering: 'background',
                                    color: 'white'
                                }
	                            for (var i = 0; i < 10; i++) {
		                            $('#calendar').fullCalendar('renderEvent', eventColor, true);
	                            }
	                          

                                if (req > 8) {
	                                var eventColor = {
                                        start: date,
                                        end: date,
		                                rendering: 'background',
		                                color: 'red'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                                if (req < 4) {
	                                var eventColor = {
                                        start: date,
                                        end: date,
		                                rendering: 'background',
		                                color: 'green'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                            }
                
                    });
                  

                    //$('[data-date="2019-04-16"]').css('backgroundColor', 'yellow');
                    
                    $('#calendar').fullCalendar('renderEvent', event, true);
                    console.log(event);
                   
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
