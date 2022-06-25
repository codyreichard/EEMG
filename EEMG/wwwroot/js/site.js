// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    $("ul.navbar-nav > li > a").on("click", function (e) {

        e.preventDefault();

        var link = $(this);

        var listItems = $("ul.navbar-nav > li"); //.removeClass("active");

        //link.parent().addClass("active");

        // Redirect to URL
        window.location.href = link.attr("href");

    });
});


