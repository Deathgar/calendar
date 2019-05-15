$(function () {

    $('#deleteAllEvent').click(function () {
        var modal = $('#EventPanelInfo');
        var id = modal.find('.id').data('id');

        $.ajax({
            url: '/AllEvents/Delete',
            type: "POST",
            data:
            {
                id: id,
                date: modal.find("#date").val(),
                email: modal.find('.user').data('class')
            },
            success: function() {
                $('#' + id).remove();
                modal.modal('hide');
            }

    });
    });

    $('.addNewEventAll').on('click',
        function () {
            $('#changeAllEvent').css({ 'display': 'none' });
            $('#addNewAllEvent').css({ 'display': 'inline-block' });
            $("#status").css({ 'display': 'none' });
            var modal = $('#EventPanelInfo');


            modal.modal('show');
            modal.find('#title').val("");
            modal.find('#time').val("");
            modal.find('#description').val("");
            modal.find('#date').val("");
            modal.find('#imageDay').removeAttr('src').replaceWith(modal.find('#imageDay').clone());
            modal.find('.id').data('id', "");
            modal.find('.user').data('class', "");
        });

    $('.EventPanel').on('click',
        function () {
            var modal = $('#EventPanelInfo');
            console.log(document.getElementById("emailName"));
            $('#changeAllEvent').css({ 'display': 'inline-block' });
            $('#addNewAllEvent').css({ 'display': 'none' });
            $("#status").css({ 'display': 'inline-block' });

            modal.modal('show');
            modal.find('#title').val($(this).find(".title").text());
            modal.find('#time').val($(this).find(".time").text());
            modal.find('#description').val($(this).find(".description").text());
            modal.find('#date').val($(this).find(".date").text());
            modal.find('#imageDay').attr('src', $(this).find(".img").attr('src'));
            modal.find('.id').data('id', this.id);
            modal.find('.user').data('class', $(this).data('user'));

        });

    $('#addNewAllEvent').click(function () {



        var date = $('#date').val();
        var title = $('#title').val();
        var description = $('#description').val();
        var time = $('#time').val();
        var user = "nobody@admin.com";
        var status = "new";

        console.log(date);
        console.log(title);
        console.log(description);
        console.log(time);
        console.log(user);

        var image = $('#imageButton')[0].files[0];

        var formData = new FormData();


        formData.append("img", image);
        formData.append("title", title);
        formData.append("time", time);
        formData.append("description", description);
        formData.append("email", user);
        formData.append("date", date);
        formData.append("status", status);

        $.ajax({
            url: '/AllEvents/AddTimeAndEvent',
            dataType: 'json',
            contentType: false,
            processData: false,
            type: "POST",
            data: formData,
            success: function (event) {

                var html;

                html = '<div class="EventPanel" id="' +
                    event.Id +
                    '" data-user="' +
                    user +
                    '" style="border: greenyellow solid 1px; border-left: greenyellow solid 15px;">';

                if ((typeof (event.image) !== "undefined") && event.image !== null) {
                    html +=
                        '<img class="img" id="imageDay" src="' +
                        event.image.Url +
                        '" style="height: 45%; object-fit: cover;"/><br/>';
                } else {
                    html += '<img class="img" id="imageDay" src=' +
                        '"https://webref.ru/assets/images/html5-css3/img-04.jpg" ' +
                        'style="height: 45%; object-fit: cover;"/><br/>';
                }

                html += '<div class="panelWithLabels">'
                    + '<label class="labels" > Date: </label > <br />'
                    + '<div class="textLabels date">' + date + '</div>'
                    + '<label class="labels"> Title: </label> <br/>'
                    + '<div class="textLabels title">' + title + '</div>'
                    + '<label class="labels"> Time: </label> <br/>'
                    + '<div class="textLabels time">' + time + '</div>'
                    + '<label class="labels"> Description: </label> <br/>'
                    + '<div class="textLabels description">' + description + '</div>'
                    + '</div></div>';

                var container = $('.eventContainer');
                container.append(html);

                $('#EventPanelInfo').modal('hide');
            }

        });

    });

    $("#changeAllEvent").click(
        function () {
            var date = $('#date').val();
            var title = $('#title').val();
            var description = $('#description').val();
            var time = $('#time').val();
            var id = $('.id').data('id');
            var user = $('.user').data('class');
            var status = $('#status').val();

            console.log(date);
            console.log(title);
            console.log(description);
            console.log(time);
            console.log(id);
            console.log(user);

            var image = $('#imageButton')[0].files[0];

            var formData = new FormData();


            formData.append("img", image);
            formData.append("title", title);
            formData.append("time", time);
            formData.append("description", description);
            formData.append("email", user);
            formData.append("date", date);
            formData.append("id", id);
            formData.append("status", status);


            $.ajax({
                url: '/AllEvents/ChangeTimeAndEvent',
                dataType: 'json',
                contentType: false,
                processData: false,
                type: "POST",
                data: formData,
                success: function (url) {
                    console.log(url);
                    var panel = $("#" + id);
                    panel.find('.date').text(date);
                    panel.find('.time').text(time);
                    panel.find('.description').text(description);
                    panel.find('.title').text(title);

                    panel.find('#imageDay').attr('src', url);


                    switch (status) {
                        case 'done':
                            panel.css({ 'border': 'green solid 1px', 'border-left': 'green solid 15px' });
                            break;
                        case 'new':
                            panel.css({ 'border': 'greenyellow solid 1px', 'border-left': 'greenyellow solid 15px' });
                            break;
                        case 'inProgress':
                            panel.css({ 'border': 'blue solid 1px', 'border-left': 'blue solid 15px' });
                            break;
                        case 'end':
                            panel.css({ 'border': 'gray solid 1px', 'border-left': 'grey solid 15px' });
                            break;
                        case 'checking':
                            panel.css({ 'border': 'orange solid 1px', 'border-left': 'orange solid 15px' });
                            break;

                        default:
                    }

                    $('#EventPanelInfo').modal('hide');
                }

            });
        });
});