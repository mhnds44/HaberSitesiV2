$(document).ready(function () {
    $("#slider .gizli").first().addClass("resim_link");
    $("#slider_nav .button").first().addClass("aktif");

    $("#slider_nav .button").hover(function () {
        var index = $(this).index();
        $("#slider .gizli").eq(index).addClass("resim_link").siblings().removeClass("resim_link");
    });

    $("#slider_nav .button").hover(function () {
        $(this).addClass("aktif").siblings().removeClass("aktif");
    });
});