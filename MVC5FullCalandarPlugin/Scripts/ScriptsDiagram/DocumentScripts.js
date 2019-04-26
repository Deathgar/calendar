function StartPage (url) {

	$.ajax({
        url: url,
		data:
		{
			"token": localStorage.getItem("token")
		},
		success: function (request) {

			$.each(request.Events,
				function (i, item) {
					$(".diagramButtons").append('<input id="' + item.Id + '" class="btn btn-default btn-register diagButton" type="button" value="' + i + '" ></input><br>');
					$('#' + item.Id).data('info', item);
                });
			AllLines();
		}
	});

};

$(document).on('click', '.diagButton',
	function () {
		var title = $(this).val();
		var arrays = $('#' + this.id).data('info');

		var z = {
			dates: arrays.Dates,
			times: arrays.Times
		}

		renderCanvas(title, arrays.Dates, arrays.Times);
	});