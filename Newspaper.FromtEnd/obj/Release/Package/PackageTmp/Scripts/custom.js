$(document).ready(function () {
    ImgLazyLoad();
    ScrollToTop();
    Menu();
    TabDetail();
    DefaultEvent();
});
function DefaultEvent()
{
    $('.iframe').click(function () {
        var iframe = $(this).data('iframe');
        $(this).replaceWith(iframe);
    });

    $('.dt-qa>div>b').click(function () {
        $(this).parent().find('div').slideToggle();
    });

    $("#owl-slider").owlCarousel({
        items: 1,
        lazyLoad: true,
        navigation: true,
        navigationText: ["", ""],
        paginationNumbers: true,
        autoPlay: true
    });

    $('#right-angle>div>a:first-child').click(function () {
        $(this).parent().parent().remove();
        //setCookie('CK_RIGHT_ANGLE', 1, 0.5);
    });
}
function ImgLazyLoad() {
    $("img.lazy").lazyload({
        effect: "fadeIn"
    });
}
function ScrollToTop() {
    $(window).scroll(function () {
        var scrollTopCurrent = $(this).scrollTop();
        if (scrollTopCurrent != 0) {
            $('#btn-top').fadeIn();
        } else {
            $('#btn-top').fadeOut();
        }

        if (scrollTopCurrent > 100) {
            $('nav.menudek').addClass('fixed');
        } else {
            $('nav.menudek').removeClass('fixed');
        }

        Slider();
    });

    $('#btn-top').click(function () { $('body,html').animate({ scrollTop: 0 }, 800); });
}
function GetAllFormData(form) {
    var unindexed_array = $(form).serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });
    return indexed_array;
}
function AddColorBox(idResult, objectId, ajaxResult) {
    if (typeof (ajaxResult) == 'object') {
        if (ajaxResult.status == -1) {
            alert(ajaxResult.error);
        }
    } else {
        if (ajaxResult == null || ajaxResult == '') return;
        if ($(idResult).length == 0) {
            $('body').addClass('hidden').append(ajaxResult);
            $('section, footer').addClass('fixed');
            $(objectId).colorbox({
                inline: true, closeButton: false, maxHeight: '90%',
                onClosed: function () {
                    $(idResult).remove();
                    $('body').removeClass('hidden');
                    $('section, footer').removeClass('fixed');
                }
            });
            $(objectId).trigger('click').remove();
        } else {
            $(idResult).replaceWith(ajaxResult);
            $.colorbox.resize({ width: $(idResult).width() + 'px' });
            $(objectId).remove();
        }
    }
}
function Menu()
{
    $('.menu').click(function () {
        if ($(this).hasClass('open')) {
            $(this).removeClass('open');
            $('nav').removeAttr('style');
            $('.h1, section, footer').removeClass('fixed');
        } else {
            $(this).addClass('open');
            $('nav').show();
            $('.h1, section, footer').addClass('fixed');
        }
        $('.header.mobile nav').css('max-height', (screen.height - 185) + 'px'); 
    });
    $('nav b').click(function (e) {       
        if ($(this).hasClass('op')) {
            $(this).removeClass('op');
            $(this).parent().parent().removeClass('active');
        } else {
            $(this).addClass('op');
            $(this).parent().parent().addClass('active');
        }
        e.preventDefault();
    });
}

var flagPopupRegister = false;
function PopupRegister()
{
    if (flagPopupRegister) return;
    flagPopupRegister = true;
    $('#ajaxloading').show();

    var data = GetAllFormData('#frmRegister');
    $.ajax({
        type: "POST",
        cache: false,
        data: data,
        url: "/Ajax/PopupRegister",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagPopupRegister = false;
            AddColorBox('#register', '#lregister', e);
        }
    });
}
var flagRegister = false;
function Register()
{
    if (flagRegister) return;
    flagRegister = true;
    $('#ajaxloading').show();

    var data = GetAllFormData('#frmRegister');
    $.ajax({
        type: "POST",
        cache: false,
        data: data,
        url: "/Ajax/RegisterInsert",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagRegister = false;
            if (e.status == -1) {
                alert(e.error);
                return false;
            } else {
                alert('Bạn đã đăng ký tư vấn dịch vụ thành công.');
                $.colorbox.close();
                return false;
            }
        }
    });
    return false;
}

var flagSlider = false;
function Slider()
{
    if (flagSlider) return;
    flagSlider = true;

    $("#owl-picture").owlCarousel({
        lazyLoad: true,
        navigation: true,
        navigationText: ["", ""],
        paginationNumbers: true,
        autoPlay: true,
        itemsCustom: [[1199, 2], [979, 2], [640, 2], [480, 2], [414, 1], [375, 1], [360, 1], [320, 1]]
    });

    $("#owl-customer").owlCarousel({
        lazyLoad: true,
        navigation: true,
        navigationText: ["", ""],
        paginationNumbers: true,
        autoPlay: true,
        itemsCustom: [[1199, 4], [979, 4], [640, 3], [480, 2], [414, 1], [375, 1], [360, 1], [320, 1]]
    });
}
var flagTabDetail = false;
function TabDetail()
{
    if (flagTabDetail) return;
    flagTabDetail = true;

    $('.dt-tab>a').click(function () {
        $('.dt-tab>a.active').removeClass('active');
        $(this).addClass('active');
        var id = $(this).attr('itemid');
        $('.detail>.tab').hide();

        var $item = $('.detail>.tab' + id);
        var imgSrc = $item.find('img').attr('data-src');
        $item.find('img').attr('src', imgSrc);
        
        if (id == 4) {
            if ($item.attr('load') == '1') {
                LoadCustomerReview($item);
            } else {
                $item.fadeIn();
            }
        } else if (id == 6) {
            if ($item.find('div') != undefined) {
                var iframe = $item.find('div').data('iframe');
                $item.html(iframe);
            }
            $item.fadeIn();
        } else if (id == 8) {
            if ($item.attr('load') == '1') {
                LoadQuestionAnswer($item);
            } else {
                $item.fadeIn();
            }
        } else {
            $item.fadeIn();
        }
    });

    flagTabDetail = false;
}

var flagLoadCustomerReview = false;
function LoadCustomerReview($item)
{
    if (flagLoadCustomerReview) return;
    flagLoadCustomerReview = true;
    $('#ajaxloading').show();
    $.ajax({
        type: "POST",
        cache: false,
        data: { articleId: $('#hdArticleId').val() },
        url: "/Ajax/CustomerReview",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagLoadCustomerReview = false;
            $item.append(e).fadeIn();
            $item.attr('load', '0');
            InitRating();
        }
    });
}
var flagLoadQuestionAnswer = false;
function LoadQuestionAnswer($item) {
    if (flagLoadQuestionAnswer) return;
    flagLoadQuestionAnswer = true;
    $('#ajaxloading').show();
    $.ajax({
        type: "POST",
        cache: false,
        data: { articleId: $('#hdArticleId').val() },
        url: "/Ajax/QuestionAnswer",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagLoadQuestionAnswer = false;
            $item.html(e).fadeIn();
            $item.attr('load', '0');
            $('.dt-qa>div>b').click(function () {
                $(this).parent().find('div').slideToggle();
            });
        }
    });
}
var flagViewPrice = false;
function ViewPrice(bannerId)
{
    if (flagViewPrice) return;
    flagViewPrice = true;
    $('#ajaxloading').show();
    $.ajax({
        type: "POST",
        cache: false,
        data: { bannerId: bannerId },
        url: "/Ajax/ViewPrice",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagViewPrice = false;
            $('#img').attr('src', e);
        }
    });
}

var flagAddRating = false;
function AddRating()
{
    if (flagAddRating) return;
    flagAddRating = true;

    $('#ajaxloading').show();
    $('#btnRating').addClass('disable');

    var itemId = $('#txtItemId').val();
    var itemType = $('#txtItemType').val();
    var price = GetStar('.rating-price');
    var quality = GetStar('.rating-quality');
    var doctor = GetStar('.rating-doctor');
    var attitude = GetStar('.rating-attitude');
    var facility = GetStar('.rating-facility');
    var customer = GetStar('.rating-customer');

    $.ajax({
        type: "POST",
        url: "/Ajax/AddRating",
        data: {
            itemId: itemId,
            itemType: itemType,
            price: price,
            quality: quality,
            doctor: doctor,
            attitude: attitude,
            facility: facility,
            customer: customer
        },
        success: function (e) {
            flagAddRating = false;
            $('#ajaxloading').fadeOut();
            $('#btnRating').removeClass('disable');
            $('#rating>div.fl').html($(e).find('div.fl').html());
            InitRating();
        }
    });
}
function InitRating() {
    $('.ra-vote a').mouseover(function () {
        ShowStar($(this), false);
    }).mouseout(function () {
        HideStar($(this));
    }).click(function () {
        ShowStar($(this), true);
    });

    ShowRatting();
}
function ShowRatting() {
    $('.ra-current').mouseover(function () {
        $(this).css('display', 'none');
    }).mouseout(function () {
        $(this).css('display', 'block');
    });
}
function ShowStar(object, isClick) {
    var parentClass = "." + object.parent().parent().attr('class');
    $(parentClass + ' .ra-current').css('display', 'none');
    $(parentClass + ' .ra-vote a').removeClass('ra-hover');
    var star = object.attr('star');
    for (var i = 1; i <= star; i++) {
        $(parentClass + ' .ra-vote .star-' + i).addClass('ra-hover');
    }
    
    if (isClick) {
        $(parentClass).attr('click', '1');
        $(parentClass + ' .ra-vote').addClass('active');
    } else {
        $(parentClass).attr('click', '');
        $(parentClass + ' .ra-vote').removeClass('active');
    }
}
function HideStar(object) {
    var parentClass = "." + object.parent().parent().attr('class');
    $(parentClass + ' .ra-current').css('display', 'block');
    if ($(parentClass).attr('click') == undefined || $(parentClass).attr('click') == '') {
        $(parentClass + ' .ra-vote a').removeClass('ra-hover');
    }
}
function GetStar(className) {
    var star = 0;
    $(className + ' .ra-vote>a').each(function () {
        if ($(this).hasClass('ra-hover')) {
            star++;
        }
    });
    return star;
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}
