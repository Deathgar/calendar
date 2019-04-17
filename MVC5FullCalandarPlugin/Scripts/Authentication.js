$(document).ready(function() {
    if (localStorage.getItem("token") == "null" || localStorage.getItem("token") == null || localStorage.getItem('token') === "") {
	    $('.authorize').css("display", "none");
        $('.noneAuthorize').css("display", "block");
    } else {
        $('.authorize').css("display", "block");
        $('.noneAuthorize').css("display", "none");
        $('#emailName').text(localStorage.getItem("email"));
        
    }
});

$("#logOut").click(function() {
    
    $.ajax({
	    url: "/Authentication/LogOut",
	    type: "POST",
	    data:
	    {
		    "token": localStorage.getItem("token")
        },
        success: function() {
            var url = '/Home/Index';
            window.location.href = url;
            localStorage.setItem("token", "null");
        }

    });
});

$("#loginButton").click(function () {
	var vEmail = $('#emailLog').val();
    var vPassword = $('#passwordLog').val();

    if ((vEmail != null || vEmail != "null") && (vPassword != null || vPassword != "null")) {

	    $.ajax({
		    url: "/Authentication/Login",
		    type: "POST",
		    data:
		    {
			    "email": vEmail,
			    "password": vPassword
		    },
		    success: function(request) {
			    localStorage.setItem("token", request);
			    localStorage.setItem("email", vEmail);
			    var url = '/Home/Index';
			    window.location.href = url;

		    }
	    });
    }
});

$("#registerButton").click(function() {
	var vEmail = $('#email').val();
	var vFirstName = $('#firstName').val();
	var vPassword = $('#password').val();

	if (vPassword != $('#password_confirmation').val()) {
		console.log("d");
		return;
	}

	$.ajax({
		url: "/Authentication/Registration",
		type: "POST",
		data:
		{
			"firstName": vFirstName,
			"email": vEmail,
			"password": vPassword
		},
		success: function(request) {
			console.log(request);

			localStorage.setItem("token", request);

			var url = '/Home/Index';
			window.location.href = url;
		}
	});
});

