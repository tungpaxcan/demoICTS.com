jQuery(function($) {

    var script = function(){

    var win = $(window);
    var html = $('html');
    var body = $('body');

    var mMenu = function(){
        var menu = $('.m-nav');
        var ct = menu.find('.nav-ct');
        var open = $('.nav-open');
        var close = $('.nav-close');

        ct.append($('.main-nav').clone());
        ct.append($('.search #search').clone()); 
        ct.append($('.h-phone').clone());    
        ct.append($('.h-address').clone());    
        // ct.append($('.h-facebook').clone());    
        

        

        open.click(function(e) {
            e.preventDefault();
            if(win.width()<1199){
                menu.addClass('act');
            }           
        });
        close.click(function(e) {
            e.preventDefault();
            if(win.width()<1199){
                menu.removeClass('act');
            }
        });


        var sidebar = $('.sb-nav');
        var ct2 = sidebar.find('.nav-ct');
        var open2 = $('.sb-open');
        var close2 = $('.sb-close');
        // ct2.append($('.main-nav').clone());
        open2.click(function(e) {
            e.preventDefault();
            if(win.width()<1199){
                sidebar.addClass('act');
            }           
        });
        close2.click(function(e) {
            e.preventDefault();
            if(win.width()<1199){
                sidebar.removeClass('act');
            }
        });

        win.click(function(e) {
            if(menu.has(e.target).length == 0 && !menu.is(e.target) && open.has(e.target).length == 0 && !open.is(e.target)){
                menu.removeClass('act');
            }
            if(sidebar.has(e.target).length == 0 && !sidebar.is(e.target) && open2.has(e.target).length == 0 && !open2.is(e.target)){
                sidebar.removeClass('act');
            }
        });


        nav = menu.find('.main-nav');
        nav.find("ul li").each(function() {
            if($(this).find("#menusanphamct>li").length > 0){
                $(this).prepend('<i class="nav-drop"></i>');
            }
        });

        $(".nav-drop").click(function(){
            var ul = $(this).nextAll("ul");
            if(ul.is(":hidden") === true){
                ul.parent('li').parent().children().children('ul').slideUp(200);
                ul.parent('li').parent().children().children('.nav-drop').removeClass("act");
                $(this).addClass("act");
                ul.slideDown(200);
            }
            else{
                ul.slideUp(200);
                $(this).removeClass("act");
            }
        });
    }

    return {

        uiInit: function($fun){
            switch ($fun) {
                case 'mMenu':
                mMenu();
                break;

                default:
                mMenu();
            }
        }
    }

}();

    script.uiInit();


});

$('.slider-cas').slick({
  dots: true,
  infinite: true,
  speed: 300,
  slidesToShow: 1,
  arrows: true,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 3000,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        dots: true,
        arrows: false,
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});

$('.slider-cas-cc').slick({
  dots: true,
  infinite: true,
  speed: 300,
  slidesToShow: 6,
  arrows: false,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 3000,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 1,
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,
        dots: false,
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});

$('.slider-cas-chn').slick({
  dots: true,
  infinite: true,
  speed: 300,
  slidesToShow: 4,
  arrows: false,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 3000,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 1,
        dots:false,
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,
        dots: false,
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});

$('.slider-cas-tt').slick({
  dots: false,
  infinite: true,
  speed: 300,
  slidesToShow: 6,
  arrows: false,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 3000,
  rows: 2,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 1,
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,
        dots: false,
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});

$(window).scroll(function() {    
    var scroll = $(window).scrollTop();

    if (scroll >= 200) {
        $(".nav-wp").addClass("darkHeader fadeInDown");
        $("body").addClass("aaa");
    } else {
        $(".nav-wp").removeClass("darkHeader fadeInDown");
        $("body").removeClass("aaa");
    }
});

/* menu left */

(function($){
$(document).ready(function(){

$('#cssmenu li.active').addClass('open').children('ul').show();
  $('#cssmenu li.has-sub>a').on('click', function(){
    $(this).removeAttr('href');
    var element = $(this).parent('li');
    if (element.hasClass('open')) {
      element.removeClass('open');
      element.find('li').removeClass('open');
      element.find('ul').slideUp(200);
    }
    else {
      element.addClass('open');
      element.children('ul').slideDown(200);
      element.siblings('li').children('ul').slideUp(200);
      element.siblings('li').removeClass('open');
      element.siblings('li').find('li').removeClass('open');
      element.siblings('li').find('ul').slideUp(200);
    }
  });

});
})(jQuery);

$(document).ready(function(){
  $('.previews a').click(function(){
    var largeImage = $(this).attr('data-full');
    $('.selected').removeClass();
    $(this).addClass('selected');
    $('.full img').hide();
    $('.full img').attr('src', largeImage);
    $('.full img').fadeIn();


  }); // closing the listening on a click
  $('.full img').on('click', function(){
    var modalImage = $(this);//.attr('src');
    $.fancybox.open(modalImage);
  });
}); //closing our doc ready

$('[data-fancybox="gallery"]').fancybox({
  // Options will go here
});

