function addEvent(date) {
	$('#myLoginModal').modal('show');
	$('#hiddenDate').val(date);
	$('#deleteEvent').css("display", "none");
	$('#addEvent').css("display", "inline-block");
	$('#changeEvent').css("display", "none");
	$('#imageDay').css("display", "none");
}