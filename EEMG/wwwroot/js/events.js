$(document).on('click', '#download-ppt', function(event){

    $.get({
        url: '../../Controllers/EventsController/DownloadPowerPoint',
        data: { id: -5 }
    }).done(function () {
        alert('Added');
    });
});