$(document).ready(function () {
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


    $('#right-angle>div>a:first-child').click(function () {
        $(this).parent().parent().remove();
        setCookie('CK_RIGHT_ANGLE', 1, 1);
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
        url: "/Ajax/KhoaHocInsert",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagRegister = false;
            if (e.status == -1) {
                alert(e.error);
                return false;
            } else {
                alert('Bạn đã đăng ký tư vấn thành công. Chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất.');
                $('#txtContentRG').val('');
                $('#txtEmailRG').val('');
                $('#txtPhoneNumberRG').val('');
                $('#txtFullNameRG').val('');
                return false;
            }
        }
    });
    return false;
}

var flagRegisterService = false;
function RegisterService() {
    debugger;
    if (flagRegisterService) return;
    flagRegisterService = true;
    $('#ajaxloading').show();

    var data = GetAllFormData('#frmRegisterService');
    $.ajax({
        type: "POST",
        cache: false,
        data: data,
        url: "/Ajax/KhoaHocServiceInsert",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagRegister = false;
            if (e.status == -1) {
                alert(e.error);
                return false;
            } else {
                alert('Bạn đã đăng ký dịch vụ thành công. Chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất.');
                $('#txtContentSV').val('');
                $('#txtEmailSV').val('');
                $('#txtPhoneNumberSV').val('');
                $('#txtFullNameSV').val('');
                $.magnificPopup.close();
                return false;
            }
        }
    });
    return false;
}

var flagInsert = false;
function LienHeInsert() {
    debugger;
    if (flagInsert) return;
    flagInsert = true;
    $('#ajaxloading').show();

    var data = GetAllFormData('#frmLienHe');
    $.ajax({
        type: "POST",
        cache: false,
        data: data,
        url: "/Ajax/LienHeInsert",
        success: function (e) {
            $('#ajaxloading').fadeOut();
            flagInsert = false;
            if (e.status == -1) {
                alert(e.error);
                return false;
            } else {
                alert('Gửi yêu cầu liên hệ thành công.Chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất!!!');
                $('input[name=txtFullName]').val('');
                $('input[name=txtEmail]').val('');
                $('input[name=txtPhone]').val('');
                $('textarea[name=txtContent]').val('');
                $.colorbox.close();
                return false;
            }
        }
    });
    return false;
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

function setCookie(cname, cvalue, exhous) {
    var d = new Date();
    d.setTime(d.getTime() + (exhous * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}





$('.rm-content').readmore({
    speed: 500,
    collapsedHeight: 75,
    moreLink: '<a href="#">Xem tiếp</a>',
    lessLink: '<a href="#">Thu gọn</a>'

});

$("li[data-sub='is-sub']").parents(".menu ").addClass("menu-2cap");

var isMobile = false;

if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
    isMobile = true;
}

if (isMobile) {
    if ($('#right-angle')[0] != undefined) {
        $('#right-angle').css("position", "inherit");
        $('#right-angle').addClass("zoom-anim-dialog");
        $('#right-angle').addClass("mfp-hide all-popup");
        $('#right-angle-close').hide();
        openPopup('right-angle');
    }
}