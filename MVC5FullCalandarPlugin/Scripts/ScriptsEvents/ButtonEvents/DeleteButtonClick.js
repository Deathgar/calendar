

$('#deleteEvent').click(function () {
	var url = "/DayEvents/Delete";

	var date = $('#hiddenDate').val();
	var id = $('#hiddenInfo').val();
	console.log(date);
	console.log(id);
	var token = localStorage.getItem("token");


	$.ajax({
		url: url,
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