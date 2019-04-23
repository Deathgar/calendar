$("#loginButton").click(function () {
    var url = '/Authentication/Login';
    var homeUrl = '/Home/Index';

	var vEmail = $('#emailLog').val();
	var vPassword = $('#passwordLog').val();

	if ((vEmail != null || vEmail != "null") && (vPassword != null || vPassword != "null")) {

		$.ajax({
			url: url,
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