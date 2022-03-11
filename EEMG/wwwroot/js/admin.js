$(document).on('click', '#change-membership', function (event) {
    var userId = $(this).val();
    $.post({
        url: '/Admin/ChangeUserToMember',
        data: { userId: userId }
    }).done(function () {
    });
});