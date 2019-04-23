$(document).ready(function () {
	if (localStorage.getItem("token") == "null" || localStorage.getItem("token") == null || localStorage.getItem('token') === "") {
		$('.authorize').css("display", "none");
		$('.noneAuthorize').css("display", "block");
	} else {
		$('.authorize').css("display", "block");
		$('.noneAuthorize').css("display", "none");
		$('#emailName').text(localStorage.getItem("email"));

	}
});