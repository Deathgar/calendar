$("#registerButton").click(function () {

	var vEmail = $('#email').val();
	var vFirstName = $('#firstName').val();
	var vPassword = $('#password').val();

	if (vPassword != $('#password_confirmation').val()) {
		console.log("d");
		return;
	}

	$.ajax({
        url: urlRegistration,
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
