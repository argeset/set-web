$(function () {
    $('#btnSaveFeedback').click(function () {
        var message = $("#FeedbackMessage").val();
        var fbRetMsg = $("#feedbackReturnMessage");
        if (message.length < 1) { fbRetMsg.html('<label class="error">*</label>'); return; }

        fbRetMsg.html(null);

        $.post('/Feedback/New', { message: message }, function (result) {
            if (result && result.IsOk) {
                fbRetMsg.html('<div class="alert alert-success alert-dismissable"><span>Thanks for feedback.</span></div>');
                setTimeout(function () {
                    $("#modalFeedback").modal('hide');
                    $("#FeedbackMessage").val(null);
                }, 2000);
            } else {
                fbRetMsg.html('<div class="alert alert-warning alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                    '<strong>Ups! </strong> ' + result.Msg + '</div>');
            }
        });
    });
    $('#modalFeedback').on('hidden.bs.modal', function () { $("feedbackReturnMessage").html(null); $("#Feedback").val(''); $("label.error").remove(); });
});



var textBtnDanger = "btn-danger";
var textBtnSuccess = "btn-success";
var textId = "id";
var textIsActive = "isactive";

var allLanguages = [{ id: 'tr', text: 'Türkçe' },
    { id: 'en', text: 'English' },
    { id: 'sp', text: 'Español' },
    { id: 'cn', text: '中文 (zhōngwén)' },
    { id: 'ru', text: 'Русский язык' },
    { id: 'fr', text: 'Français' },
    { id: 'gr', text: 'Deutsch' },
    { id: 'it', text: 'Italiano' },
    { id: 'az', text: 'Azərbaycan dili' },
    { id: 'tk', text: 'түркmенче (türkmençe)' },
    { id: 'kz', text: 'Қазақ тілі' }];

$(function () {
    $(".btnAction").click(function () {
        var textBtn = "#btnModalAction";

        var id = $(this).data(textId);
        var isActive = $(this).data(textIsActive);

        $(textBtn).removeClass(textBtnDanger).removeClass(textBtnSuccess);
        if (isActive == "True") {
            $(textBtn).addClass(textBtnDanger);
        } else {
            $(textBtn).addClass(textBtnSuccess);
        }

        $(textBtn).data(textId, id).data(textIsActive, isActive);
    });
});