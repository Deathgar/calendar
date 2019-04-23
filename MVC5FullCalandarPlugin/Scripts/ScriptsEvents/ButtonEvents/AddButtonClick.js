

$('#addEvent').click(function () {
    
    var title = $('#title').val();
    var time = $('#time').val();
    var description = $('#description').val();
    var token = localStorage.getItem("token");
    var date = $('#hiddenDate').val();

    var image = $('#imageButton')[0].files[0];

    var formData = new FormData();

    if (title && !isNaN(time) && time < 24 && time > 0) {

        formData.append("img", image);
        formData.append("title", title);
        formData.append("time", time);
        formData.append("description", description);
        formData.append("token", token);
        formData.append("date", date);


        $.ajax({
            url: urlAddTimeEvent,
            dataType: 'json',
            contentType: false,
            processData: false,
            type: "POST",
            data: formData,

            success: function (request) {

                var event = {
                    title: title,
                    description: description,
                    start: date,
                    allDay: true,
                    backgroundColor: "#9501fc",
                    borderColor: "#494949",
                    id: request
                };

                $.ajax({
                    url: urlGetAllTimeInDay,
                    type: "GET",
                    data:
                    {
                        'date': date,
                        'token': localStorage.getItem('token')
                    },
                    success:
                        function (req) {

                            var eventColor = {
                                start: date,
                                end: date,
                                rendering: 'background',
                                color: 'white'
                            }
                            for (var i = 0; i < 10; i++) {
                                $('#calendar').fullCalendar('renderEvent', eventColor, true);
                            }
                            if (req !== "0") {
                                if (req > 8) {
                                    var eventColor =
                                    {
                                        start: date,
                                        end: date,
                                        rendering: 'background',
                                        color: 'red'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                                if (req < 4) {
                                    var eventColor =
                                    {
                                        start: date,
                                        end: date,
                                        rendering: 'background',
                                        color: 'green'
                                    }
                                    $('#calendar').fullCalendar('renderEvent', eventColor, true);
                                }
                            }
                        }

                });

                $('#calendar').fullCalendar('renderEvent', event, true);
            }
        });
    }
    $('#myLoginModal').modal('hide');

});