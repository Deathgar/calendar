$('#All').click(function() {
	AllLines();
});

$('#Your').click(function() {
    $('#AllLinesUsers').css({ 'display': 'none' });
    $('#All').css({ 'display': 'inline-block' });

    $.ajax({
        url: urlLines,
        data:
        {
            "token": localStorage.getItem("token")
        },
        success: function (req) {

            $(".diagramButtons").empty();
            FillButtonContainer(false, req);
            AllLines(false);
        }
    });

    

});

$('#AllLinesUsers').click(function() {
    AllLines(true);
});

$('#Users').click(function() {

    $('#All').css({ 'display': 'none' });
    $('#AllLinesUsers').css({ 'display': 'inline-block' });

    $.ajax({
        url: urlLinesUser,
        success:
            function(req) {

                $(".diagramButtons").empty();
                FillButtonContainer(true, req);
                AllLines(true);
            }
    });

});

function AllLines(isUser) {
    var dates = [];
    var ggz = [];

    if (!isUser) {
        $.each($(".diagButton"),
            function(v, item) {

                var k = {
                    title: $('#' + item.id).val(),
                    dates: $('#' + item.id).data('info').Dates,
                    times: $('#' + item.id).data('info').Times
                }
                ggz.push(k);

                $.each($('#' + item.id).data('info').Dates,
                    function(v, item) {
                        dates.push(item);
                    });
            });
    } else {
        $.each($(".diagButtonUser"),
            function(v, item) {


                var arrEvents = $('#' + item.id).data('info');

                $.each(arrEvents,
                    function(j, event) {
                        var k = {
                            title: j,
                            dates: event.Dates,
                            times: event.Times
                        }

                        ggz.push(k);

                        $.each(event.Dates,
                            function(v, date) {
                                dates.push(date);
                            });
                    });
            });

    }

    dates.sort((a, b) => moment(a, 'YYYY-MM-DD') - moment(b, 'YYYY-MM-DD'));

    var tempDate = [];

    console.log("dates:");
    console.log(ggz);
    console.log(dates);

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
                pointBackgroundColor: color
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
        },
        elements: {
            point: {
                pointStyle: 'rectRot'
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