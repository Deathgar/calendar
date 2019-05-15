var lineChart;

function uniq(array) {
	var newArray = new Array();
	$.each(array, function (i, el) {
		if ($.inArray(el, newArray) === -1) newArray.push(el);
	});

	return newArray;
}

function renderCanvas(title, dates, times) {
    var speedCanvas = $('#speedChart');

    var newArr = [];

    Chart.defaults.global.defaultFontFamily = 'Lato';
    Chart.defaults.global.defaultFontSize = 21;

    for (var i = 0; i < dates.length; i++) {
        var p = {
            date: dates[i],
            time: times[i]
        };
        newArr.push(p);
    }

    newArr.sort((a, b) => moment(a.date, 'YYYY-MM-DD') - moment(b.date, 'YYYY-MM-DD'));

    var sortTimes = [];
    var sortDates = [];

    if (newArr.length > 1) {
        var datePrev = newArr[1].date;

        for (var i = 0; i < newArr.length - 1; i++) {
            var dateArray = getDates(newArr[i].date, newArr[i + 1].date);
            var tempTime = [];
            for (var j = 0; j < dateArray.length; j++) {
                if (j === 0) {
                    if (datePrev !== newArr[i].date) {
                        tempTime.push(newArr[i].time);
                    }

                }
                else {
                    if (j === dateArray.length - 1) {
                        tempTime.push(newArr[i + 1].time);
                    } else {
                        tempTime.push(null);
                    }
                }
            }
            datePrev = newArr[i + 1].date;
            sortTimes = sortTimes.concat(tempTime);
            sortDates = sortDates.concat(dateArray);
        }
    }

    sortDates = uniq(sortDates);
    console.log("z:");
    console.log(sortDates);
    console.log("z:");
    console.log(sortTimes);

    var r = Math.floor(Math.random() * 255);
    var g = Math.floor(Math.random() * 255);
    var b = Math.floor(Math.random() * 255);
    var color = "rgb(" + r + "," + g + "," + b + ")";

    var speedData = {
        labels: sortDates,
        datasets: [{
            label: title,
            data: sortTimes,
            borderColor: color,
            backgroundColor: transparize(color, 0.8),
            pointRadius: 8,
            pointHoverRadius: 10,
            pointBorderColor: color,
            pointBackgroundColor: color
}]
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
                pointStyle: "rectRot"
            }
        }
    };
    lineChart.destroy();
    lineChart = new Chart(speedCanvas, {
        type: 'line',
        data: speedData,
        options: chartOptions
    });
}

function getDates(startDate, stopDate) {
	var dateArray = [];
	var currentDate = moment(startDate);
	var stopDate = moment(stopDate);
	while (currentDate <= stopDate) {
		dateArray.push(moment(currentDate).format('YYYY-MM-DD'));
		currentDate = moment(currentDate).add(1, 'days');
	}
	return dateArray;
}

function transparize(color, opacity) {
    var alpha = opacity === undefined ? 0.5 : 1 - opacity;
    return Color(color).alpha(alpha).rgbString();
}