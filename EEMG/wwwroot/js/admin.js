﻿$(document).on('click', '#change-membership', function (event) {
    var userId = $(this).val();
    $.post({
        url: '/Admin/ChangeUserToMember',
        data: { userId: userId }
    })
        .done(function (event) {
            console.log('should reload');
            window.location.reload();
            document.location.reload();
        })
        .fail(function (event) {
            console.log('Failed to Changed Member to a user!');
        });
});


$(document).on('click', '#add-event', function (event) {
    var userId = $(this).val();
    $.get({
        url: '/Admin/AddEvent',
        data: {}
    })
        .done(function (event) {
        })
        .fail(function (event) {
        });
});