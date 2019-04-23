$("#registerButton").click(function () {
    var url = '/Authentication/Registration';
    var homeUrl = '/Home/Index';

	var vEmail = $('#email').val();
	var vFirstName = $('#firstName').val();
	var vPassword = $('#password').val();

	if (vPassword != $('#password_confirmation').val()) {
		console.log("d");
		return;
	}

	$.ajax({
		url: url,
		type: "POST",
		data:
		{
			"firstName": vFirstName,
			"email": vEmail,
			"password": vPassword
		},
		success: function (request) {
			localStorage.setItem("token", request);
			window.location.href = homeUrl;
		}
	});
});
