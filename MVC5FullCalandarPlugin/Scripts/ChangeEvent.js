
function addEvent(date) {
    $('#myLoginModal').modal('show');
    $('#hiddenDate').val(date);
    $('#deleteEvent').css("display", "none");
    $('#addEvent').css("display", "inline-block");
    $('#changeEvent').css("display", "none");
    $('#imageDay').css("display", "none");
}

function addImage() {
    var image = $('#imageButton')[0].files[0];
    var formData = new FormData();
    formData.append("img", image);
    formData.append("id", "dawdwad");

	$.ajax({
        url: "/DayEvents/AddImage",
        dataType: 'json',
        contentType: false,
        processData: false,
		type: "POST",
		data: formData,
	});
};


$('#addEvent').click(function () {

    var title = $('#title').val();
    var time = $('#time').val();
    var description = $('#description').val();
    var token = localStorage.getItem("token");
    var date = $('#hiddenDate').val();

    var image;

    console.log($('#imageButton')[0].files[0]);


    if (title && !isNaN(time) && time < 24 && time > 0) {

        $.ajax({
            url: "/DayEvents/AddTimeAndEvent",
            type: "POST",
            dataType: 'json',
            data:
            {
                "title": title,
                "time": time,
                "date": date,
                "description": description,
                "token": token,
            },

            success: function (request) {

                var event = {
                    title: title,
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
                            if (req !== "0") {
                                if (req > 8) {
                                    var eventColor =
                                    {
                                        start: date,
                                        end: date,
                                        rendering: 'background',
                                        color: 'red'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                                if (req < 4) {
                                    var eventColor =
                                    {
                                        start: date,
                                        end: date,
                                        rendering: 'background',
                                        color: 'green'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                            }
                        }

                });

                $('#calendar').fullCalendar('renderEvent', event, true);
            }
        });
    }
    $('#myLoginModal').modal('hide');


    if ($('#imageButton').val() !== "") {
	    addImage();
    }
});


$(function() {
	$('#myLoginModal').on('hidden.bs.modal',
		function() {

			$('#time').val("");
			$('#title').val('');
            $('#description').text("");
            $('#deleteEvent').css("display", "inline-block");

		});
});


function ChangeEventInfo(info) {

	$('#addEvent').css("display", "none");
	$('#changeEvent').css("display", "inline-block");

    $('#hiddenInfo').val(info.id);
    $('#hiddenDate').val(info.start._i);

	var token = localStorage.getItem("token");
    var id = info.id;
    var date = info.start._i;

	$.ajax({
		url: "/DayEvents/GetEvent",
		type: "GET",
		data:
		{
            "date": date,
			"token": token,
			"id": id
		},
        success: function (request) {

			$('#myLoginModal').modal('show');
            
			$('#title').val(request.Title);
            $('#time').val(request.Time);
            $('#description').text(request.Description);
            console.log(request.Image.Url);
            $('#imageDay').attr("src", request.Image.Url);
        }
	});
}

/*
 * string title, string description, string time, string date, string token, string id, string image = null)
 */
$('#changeEvent').click(function () {

    var date = $('#hiddenDate').val();
    var token = localStorage.getItem("token");

    if ($('#imageButton').val()) {
	    image = "null";
    } else {
	    image = $('#imageButton').val();
    }

	$.ajax({
		url: "/DayEvents/ChangeTimeAndEvent",
		type: "POST",
		data:
		{
			"title": $('#title').val(),
			"description": $('#description').val(),
			"time": $('#time').val(),
			"date": date,
			"token": token,
            "id": $('#hiddenInfo').val(),
            "image": image
		},
        success: function () {
			console.log("pppp");
		}
    });
	RenderingDay(date, token);
	$('#myLoginModal').modal('hide');
});

$('#deleteEvent').click(function () {

    var date = $('#hiddenDate').val();
    var id = $('#hiddenInfo').val();
    console.log(date);
    console.log(id);
	var token = localStorage.getItem("token");


	$.ajax({
		url: "/DayEvents/Delete",
		type: "POST",
		data: {
			'id': id,
			'date': date,
			'token': token
		},
		success: function (req) {
			$("#calendar").fullCalendar('removeEvents', req);
			RenderingDay(date, token);
		}

	});

	$('#myLoginModal').modal('hide');
    swal("", "Good job", "success");

});

function RenderingDay(date, token) {

    $.ajax({
        url: '/DayEvents/GetAllTimeInDay',
        type: "GET",
        data:
        {
            'date': date,
            'token': token
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

                if (req !== "0") {
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
            }

    });
}