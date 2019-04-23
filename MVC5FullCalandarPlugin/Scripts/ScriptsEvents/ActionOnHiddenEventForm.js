$(function () {
	$('#myLoginModal').on('hidden.bs.modal',
		function () {
			$('#time').val('');
			$('#title').val('');
			$('#description').val('');
			$('#deleteEvent').css("display", "inline-block");

		});
});
