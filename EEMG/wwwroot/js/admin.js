$(document).on('click', '#change-membership', function (event) {
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

$(document).on('click', '#add-to-mailing-list', function (event) {
    var email = $(this).val();
    $.post({
        url: '/Admin/AddUserToMailingList',
        data: { email: email}
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

$(document).on('click', '#remove-from-mailing-list', function (event) {
    var email = $(this).val();
    $.post({
        url: '/Admin/RemoveUserFromMailingList',
        data: { email: email }
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

$(document).on('click', '#reset-all-memberships', function (event) {
    $.post({
        url: '/Admin/ResetAllMemberships',
    })
        .done(function (event) {
            console.log('should reload');
            window.location.reload();
            document.location.reload();
        })
        .fail(function (event) {
            console.log('Failed to Reset all memberships!');
        });
});