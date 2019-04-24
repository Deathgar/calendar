
$('#addEvent').click(function () {

	var title = $('#title').val();
	var time = $('#time').val();
	var description = $('#description').val();
	var token = localStorage.getItem("token");
	var date = $('#hiddenDate').val();

	var image = $('#imageButton')[0].files[0];

	var formData = new FormData();

	if (title && !isNaN(time) && time < 24 && time > 0) {

		formData.append("img", image);
		formData.append("title", title);
		formData.append("time", time);
		formData.append("description", description);
		formData.append("token", token);
		formData.append("date", date);


		$.ajax({
			url: urlAddTimeEvent,
			dataType: 'json',
			contentType: false,
			processData: false,
			type: "POST",
			data: formData,

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

				RenderingDay(date, token);



				$('#calendar').fullCalendar('renderEvent', event, true);
			}
		});
	}
	$('#myLoginModal').modal('hide');

});




$('#changeEvent').click(function () {

	var date = $('#hiddenDate').val();
	var token = localStorage.getItem("token");
	var title = $('#title').val();
	var description = $('#description').val();
	var time = $('#time').val();
	var id = $('#hiddenInfo').val();

	var image = $('#imageButton')[0].files[0];

	var formData = new FormData();


	formData.append("img", image);
	formData.append("title", title);
	formData.append("time", time);
	formData.append("description", description);
	formData.append("token", token);
	formData.append("date", date);
	formData.append("id", id);


	$.ajax({
		url: urlChangeTimeAndEvent,
		dataType: 'json',
		contentType: false,
		processData: false,
		type: "POST",
		data: formData,
		success: function () {

			$('#calendar').fullCalendar('removeEvents', id);

			var event = {
				title: title,
				description: description,
				start: date,
				allDay: true,
				backgroundColor: "#9501fc",
				borderColor: "#494949",
				id: id
			};

			$('#calendar').fullCalendar('renderEvent', event, true);

			RenderingDay(date, token);
		}

    });

	$('#myLoginModal').modal('hide');
});



$('#deleteEvent').click(function () {

	var date = $('#hiddenDate').val();
	var id = $('#hiddenInfo').val();
	console.log(date);
	console.log(id);
	var token = localStorage.getItem("token");


	$.ajax({
		url: urlDelete,
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