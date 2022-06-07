// sticky menu

$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop()) {
            $('.navbar.fix').addClass('sticky');
        } else {
            $('.navbar.fix').removeClass('sticky');
        }
    })
})
//slide
