var swiper = new Swiper('.slider1', {
    pagination: '.swiper-pagination',
    paginationClickable: true,
    autoplay: 5000,
    autoplayDisableOnInteraction: false
});


var swiper = new Swiper('.slider2', {
    pagination: '.swiper-pagination',
    paginationClickable: true,
    autoplay: 5000,
    autoplayDisableOnInteraction: false,
    nextButton: '.swiper-button-next',
    prevButton: '.swiper-button-prev',
});

var swiper = new Swiper('.tintuc1', {
    paginationClickable: true,

    autoplayDisableOnInteraction: false,
    nextButton: '.swiper-button-next',
    prevButton: '.swiper-button-prev',
});

var swiper = new Swiper('.tintuc2', {
    pagination: '.swiper-pagination',
    paginationClickable: true,
    autoplayDisableOnInteraction: false,

});

 var swiper = new Swiper('.sliderFeature .swiper-container', {
  
        slidesPerView: 4,
        spaceBetween: 30,
        autoplayDisableOnInteraction: false,
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        breakpoints: {
            1024: {
                slidesPerView: 4,
                spaceBetween: 20
            },
            768: {
                slidesPerView: 3,
                spaceBetween: 20
            },
            640: {
                slidesPerView: 2,
                spaceBetween: 20
            },
            320: {
                slidesPerView: 1,
                spaceBetween: 10
            }
        }
    });


    $(window).scroll(function(){
  if ($(this).scrollTop() > 150) { // If the scroll equal 150px
    $(".gotop").css({width:"40px",borderRadius:"0"}); // Show the button by give it (width 40px and border-radius 0px)
  } else { // else (if the scroll <= 150px )
    $(".gotop").css({width:"0",borderRadius:"50% 0 0 50%"}); // Return button style to default
  }
});
$('.gotop').click(function () { // When user click on the button
  $("body").animate({ scrollTop: "0" },  500); // Return scroll to 0
  $("body").css({paddingTop:"20px"});
  // After .5s (when window scroll equal 0)
  setTimeout(function(){
    $("body").animate({ 
      'padding-top' : 0,
    }, "fast");
  }, 500);
});

//$('.popup-with-zoom-anim').magnificPopup({
//    type: 'inline',
//    preloader: false,

//    midClick: true,
//    removalDelay: 300,
//    mainClass: 'my-mfp-slide-bottom',
//    callbacks: {
//        beforeOpen: function () {
//            if ($(window).width() < 700) {
//                this.st.focus = false;
//            } else {
//                this.st.focus = '#name';
//            }
//        }
//    }
//});

$('.popup-with-zoom-anim').magnificPopup({
    type: 'inline',

    fixedContentPos: false,
    fixedBgPos: true,

    overflowY: 'auto',

    closeBtnInside: true,
    preloader: false,

    midClick: true,
    removalDelay: 300,
    mainClass: 'my-mfp-zoom-in',

    callbacks: {
                beforeOpen: function () {
                    if ($(window).width() < 700) {
                        this.st.focus = false;
                    } else {
                        this.st.focus = '#name';
                    }
                }
            }
});


function openPopup(hmm) { //hmm la id popup

    if (typeof hmm === 'undefined') {
        console.log("Nothing");
    } else {
        $.magnificPopup.open({
            items: {
                src: $('#' + hmm)
            },
            type: 'inline',
            closeBtnInside: true,
            preloader: false,

            midClick: true,
            removalDelay: 300,
            mainClass: 'my-mfp-zoom-in',
            callbacks: {
                open: function () {
                    $.magnificPopup.instance.close = function () {
                        setCookie('CK_RIGHT_ANGLE', 1, 1);
                        $.magnificPopup.proto.close.call(this);
                    };
                }
            }
        }, 0);
    }

}

