$('#All').click(function() {
	AllLines();
});


function AllLines() {
	console.log("dawdawd");
    var dates = [];
    var ggz = [];

    $.each($(".diagButton"),
        function (v, item) {

            var k = {
                title: $('#' + item.id).val(),
                dates: $('#' + item.id).data('info').Dates,
                times: $('#' + item.id).data('info').Times
            }
            ggz.push(k);

            $.each($('#' + item.id).data('info').Dates,
                function (v, item) {
                    dates.push(item);
                });
        });

    dates.sort((a, b) => moment(a, 'YYYY-MM-DD') - moment(b, 'YYYY-MM-DD'));

    console.log(dates);
    var tempDate = [];
    for (var i = 0; i < dates.length - 1; i++) {
        var dateArray = getDates(dates[i], dates[i + 1]);
        tempDate = tempDate.concat(dateArray);
    }

    var newDates = uniq(tempDate);

    var all = [];

    $.each(ggz,
        function (v, item) {
            var tempTime = [];
            var isEquals;


            for (var i = 0; i < newDates.length; i++) {
                isEquals = false;
                for (var j = 0; j < item.dates.length; j++) {

                    if (newDates[i] === item.dates[j]) {
                        isEquals = true;
                        tempTime.push(item.times[j]);
                        break;
                    }
                }
                if (!isEquals) {
                    tempTime.push(0);
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
                backgroundColor: 'transparent',
                pointBorderColor: color,
                pointBackgroundColor: 'transparent'
            });

        });

    var speedCanvas = $('#speedChart');

    Chart.defaults.global.defaultFontFamily = 'Lato';
    Chart.defaults.global.defaultFontSize = 18;

    var speedData = {
        labels: newDates,
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
};