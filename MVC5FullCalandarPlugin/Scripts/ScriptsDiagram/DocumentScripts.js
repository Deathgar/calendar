var urlLines;
var urlLinesUser;

function StartPage(url, urlUser) {

    urlLines = url;
    urlLinesUser = urlUser;

	$.ajax({
        url: urlLines,
		data:
		{
			"token": localStorage.getItem("token")
		},
		success: function (req) {

            FillButtonContainer(false,req);
			AllLines(false);
		}
	});

};

function FillButtonContainer(isUser, req) {


    if (!isUser) {
        $.each(req.Events,
            function (i, item) {

                $(".diagramButtons").append('<input id="' +
                    item.Id +
                    '" class="diagButton' +
                    ' btn btn-default btn-register " type="button" value="' +
                    i +
                    '" ></input><br>');
                $('#' + item.Id).data('info', item);
            });
    } else {

        var id = 0;
        $.each(req,
            function(i, item) {

                $(".diagramButtons").append('<input id="' +
                    id +
                    '" class="diagButtonUser btn btn-default btn-register" type="button" value="' +
                    i +
                    '" ></input><br>');
                $('#' + id).data('info', item.Events);
                id++;
            });
    }
}

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

$(document).on('click', '.diagButtonUser',
    function () {
        var title = $(this).val();
        var arrays = $('#' + this.id).data('info');
        var dates = new Array();
        var newArray = new Array();

        var all = [];
        var tempDate = [];
      //  arrays.sort((a, b) => moment(a.Date, 'YYYY-MM-DD') - moment(b.Date, 'YYYY-MM-DD'));

        console.log("Z:");

        $.each(arrays,
            function (i, item) {

                var temp = {
                    title: i,
                    dates: item.Dates,
                    times: item.Times
                }
                newArray.push(temp);

                $.each(item.Dates,
                    function(j, item2) {
                        dates.push(item2);
                    });
            });

        dates.sort((a, b) => moment(a, 'YYYY-MM-DD') - moment(b, 'YYYY-MM-DD'));

       

        for (var i = 0; i < dates.length - 1; i++) {
            var dateArray = getDates(dates[i], dates[i + 1]);
            tempDate = tempDate.concat(dateArray);
        }

        tempDate = uniq(tempDate);

        $.each(newArray,
            function (v, item) {
                var tempTime = [];
                var isEquals;

                for (var i = 0; i < tempDate.length; i++) {
                    isEquals = false;
                    for (var j = 0; j < item.dates.length; j++) {

                        if (tempDate[i] === item.dates[j]) {
                            isEquals = true;
                            tempTime.push(item.times[j]);
                            break;
                        }
                    }
                    if (!isEquals) {
                        tempTime.push(null);
                    }
                }



                var r = Math.floor(Math.random() * 255);
                var g = Math.floor(Math.random() * 255);
                var b = Math.floor(Math.random() * 255);
                var color = "rgb(" + r + "," + g + "," + b + ")";

                all.push({
                    "label": item.title,
                    "data": tempTime,
                    borderColor: color,
                    backgroundColor: transparize(color, 0.8),
                    pointRadius: 8,
                    pointHoverRadius: 10,
                    pointBorderColor: color,
                    pointBackgroundColor: color,
                    lineTension: 0
                });

            });

        var speedCanvas = $('#speedChart');

        Chart.defaults.global.defaultFontFamily = 'Lato';
        Chart.defaults.global.defaultFontSize = 18;

        var speedData = {
            labels: tempDate,
            datasets: all
        };

        var chartOptions = {
            legend: {
                display: true,
                position: 'top',
                labels: {
                    boxWidth: 1,
                    fontColor: 'black'
                }
            },
            elements: {
                point: {
                    pointStyle: 'rectRot'
                }
            },
            scales: {
                yAxes: [
                    {
                        ticks: {
                            min: 0
                        }
                    }
                ]
            }
        };

        if (!!lineChart) {
            lineChart.destroy();
        }
        lineChart = new Chart(speedCanvas,
            {
                type: 'line',
                data: speedData,
                options: chartOptions
            });
    });