var url = '/DayEvents/GetAllTimeInDay';

function RenderingDay(date, token) {

	$.ajax({
		url: url,
		type: "GET",
		data:
		{
			'date': date,
			'token': token
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
						var eventColor = {
							start: date,
							end: date,
							rendering: 'background',
							color: 'red'
						}
						$('#calendar').fullCalendar('renderEvent', eventColor, true);
					}
					if (req < 4) {
						var eventColor = {
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
}