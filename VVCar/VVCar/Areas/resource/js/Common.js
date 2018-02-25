/// <reference path="../js/jquery-1.8.2.intellisense.js" />
/// <reference path="../js/jquery-1.8.2.js" />


function showMessage(parStr) {
    $("#hudmessage").text(parStr);
    var $iosDialog2 = $('#iosDialog2');
    $iosDialog2.fadeIn(200);
}

function showMessage(parStr, callback) {
    $("#hudmessage").text(parStr);
    var $iosDialog2 = $('#iosDialog2');
    $iosDialog2.fadeIn(200);
    $("#btnknow").click(function () {
        $("#iosDialog2").fadeOut(200, callback);

    });
}

function showConfrim(str, callback) {
    $("#confirmMessage").text(str);
    var $iosDialog1 = $('#iosDialog1');
    $iosDialog1.fadeIn(200);

    $("#btnOK").click(function () {
        $("#iosDialog1").fadeOut(200, callback);
    });

    $("#cancel").click(function () {
        $("#iosDialog1").fadeOut(200);
    });

}

function showLoading() {
    var $loadingToast = $('#loadingToast');
    if ($loadingToast.css('display') !== 'none') return;
    $loadingToast.fadeIn(100);
}

function hideLoading() {
    var $loadingToast = $('#loadingToast');
    $loadingToast.fadeOut(100);
}


function showToast(strParam) {
    var toastcontent = $('#toast');
    $(".weui-toast__content").text(strParam);
    if (toastcontent.css('display') != 'none') {
        return;
    }
    $(toastcontent).fadeIn(100);
    setTimeout(function () {
        $(toastcontent).fadeOut(100);
    }, 3000);
}


function showToast(strParam, callback) {
    var toastcontent = $('#toast');
    $(".weui-toast__content").text(strParam);
    if (toastcontent.css('display') != 'none') {
        return;
    }
    $(toastcontent).fadeIn(100);
    setTimeout(function () {
        $(toastcontent).fadeOut(100, callback);
    }, 3000);


}

$(document).ready(function () {
    $("#btnknow").click(function () {
        $("#iosDialog2").fadeOut(200);
    });
});




