

$('#changeEvent').click(function () {
    
	var date = $('#hiddenDate').val();
	var token = localStorage.getItem("token");
	var title = $('#title').val();
	var description = $('#description').val();
	var time = $('#time').val();
	var id = $('#hiddenInfo').val();

	var image = $('#imageButton')[0].files[0];

	var formData = new FormData();

	formData.append("img", image);
	formData.append("title", title);
	formData.append("time", time);
	formData.append("description", description);
	formData.append("token", token);
	formData.append("date", date);
	formData.append("id", id);


	$.ajax({
        url: urlChangeTimeAndEvent,
		dataType: 'json',
		contentType: false,
		processData: false,
		type: "POST",
        data: formData,
        success: function () {
	        var z = 0;
	        RenderingDay(date, token);
        }
       
    });


	
	$('#myLoginModal').modal('hide');
});