
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