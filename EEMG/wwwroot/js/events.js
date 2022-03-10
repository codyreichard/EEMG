$(document).on('click', '#download-ppt', function(event){
    var eventId = $(this).val();
    $.get({
        url: '/Events/DownloadFile',
        data: { eventId: eventId }
    }).done(function () {
        alert("Downloaded File");
    });
});