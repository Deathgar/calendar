

function ChangeEventInfo(info) {


	$('#addEvent').css("display", "none");
	$('#changeEvent').css("display", "inline-block");

	$('#hiddenInfo').val(info.id);
	$('#hiddenDate').val(info.start._i);

	$('#imageDay').css("display", "inline-block");

	var token = localStorage.getItem("token");
	var id = info.id;
	var date = info.start._i;

	$.ajax({
        url: urlGetEvent,
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
            $('#description').val(request.Description);
            if (request.Image != null) {
	            $('#imageDay').attr("src", request.Image.Url);
            }
		}
	});

}