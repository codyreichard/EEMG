$(document).on('click', '#change-membership', function (event) {
    var userId = $(this).val();
    console.log('in change memebrship');
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
        console.log('fail');
    })
    .always(function () {
            console.log('always');
    });
});