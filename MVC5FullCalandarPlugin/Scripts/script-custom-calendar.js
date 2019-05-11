
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
	      
            addEvent(moment(info._d).format('YYYY-MM-DD'));

        },

        eventClick: function (info) {
            console.log($(this));
	        ChangeEventInfo(info);
        },
       


        events: function (start, end, timezone, callback) {
	        console.log(localStorage.getItem("token"));
            var token = localStorage.getItem("token");
            
	            $.ajax({
		            url: '/Home/GetCalendarData',
		            type: "GET",
		            data:
		            {
			            token: token
		            },

		            success: function(result) {
			            var events = [];

			            //$('[data-date="2019-04-16"]').filter('.fc-day').html('<div style="max-height: 100%; height: 100%; max-width: 100%; text-align: right; margin-top: 0;"><div style="margin-top: 10%;">8</div></div>');
			            $.each(result,
				            function(i, data) {

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
						            function(i, newData) {

                                        var color;

                                        switch (newData.Status) {
                                        case "done":
                                            color = "green";
                                            break;
                                        case "new":
                                            color = "greenyellow";
                                            break;
                                        case "inProgress":
                                            color = "blue";
                                            break;
                                        case "end":
                                            color = "grey";
                                            break;
                                        case "checking":
                                            color = "orange";
                                            break;
                                        default:
                                            color = "blue";
                                            break;
                                        }

                                        events.push(
								            {
									            title: newData.Title,
                                                description: newData.Description,
									            start: moment(newData.Start_Date).format('YYYY-MM-DD'),
									            end: moment(newData.End_Date).format('YYYY-MM-DD'),
									            allDay: true,
									            backgroundColor: color,
									            borderColor: "#494949",
                                                id: newData.Id
                                            });
							            

						            });


				            });
			            // console.log(events);
                        callback(events);
		            }
	            });
            
        },

        eventRender: function (event, element) {
	        element[0].id = event.id;
            element.qtip(
                {
                    content: event.description
                });
     
        },

        eventAfterRender: function (event, elemet) {
	        $("#" + event.id).data("jopa", event);
            console.log($("#" + event.id).data("jopa"));
        },

        editable: false
        });
    

});