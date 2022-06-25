$(document).on('click', '#download-ppt', function (event) {
    var eventId = $(this).val();
    $.get({
        url: '/Events/DownloadFile',
        data: { eventId: eventId }
    }).done(function () {
        alert("Downloaded File");
    });
});

$(document).ready(function () {

    //var element = document.getElementById('upcoming-events');
    //element.scrollIntoView();
    //$('body').scrollTo(60);
    //element.focus();

    $([document.documentElement, document.body]).animate({
        scrollTop: $("#upcoming-events").offset().top-110
    }, 2000);

});


function myFunction(eventId) {
    var dots = document.getElementById("dots" + eventId);
    var moreText = document.getElementById("more" + eventId);
    var btnText = document.getElementById("myBtn" + eventId);

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "Read more";
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "Read less";
        moreText.style.display = "inline";
    }
}
