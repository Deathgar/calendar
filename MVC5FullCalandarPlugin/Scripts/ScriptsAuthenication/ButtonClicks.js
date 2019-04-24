$("#loginButton").click(function () {

	var vEmail = $('#emailLog').val();
	var vPassword = $('#passwordLog').val();

	if ((vEmail != null || vEmail != "null") && (vPassword != null || vPassword != "null")) {

		$.ajax({
			url: urlLogin,
			type: "POST",
			data:
			{
				"email": vEmail,
				"password": vPassword
			},
			success: function (request) {
				localStorage.setItem("token", request);
				localStorage.setItem("email", vEmail);
				window.location.href = homeUrl;

			}
		});
	}
});


$("#logOut").click(function () {

	var l = 0;

	$.ajax({
		url: urlLogOut,
		type: "POST",
		data:
		{
			"token": localStorage.getItem("token")
		},
		success: function () {
			window.location.href = homeUrl;
			localStorage.setItem("token", "null");
		}

	});
});

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

