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