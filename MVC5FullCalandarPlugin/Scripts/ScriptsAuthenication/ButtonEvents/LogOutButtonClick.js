
$("#logOut").click(function () {
	var url = '/Authentication/LogOut';
	var homeUrl = '/Home/Index';

	$.ajax({
		url: url,
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