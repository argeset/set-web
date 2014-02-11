$(function () {
    $('#btnSaveFeedback').click(function () {
        var message = $("#FeedbackMessage").val();
        if (message.length < 1) { $("#FeedbackMessage").parent().append('<label class="error">*</label>'); return; }

        $("div#wrnFeedback").html('');

        $.post('/Feedback/New', { message: message }, function (result) {
            if (result && result.IsOk) {
                $("#modalFeedback").modal('hide');
            } else {
                $("div#wrnFeedback").append('<div class="alert alert-warning alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                    '<strong>Ups! </strong> ' + result.Msg + '</div>');
            }
        });
    });
    $('#modalFeedback').on('hidden.bs.modal', function () { $("div#wrnFeedback").html(''); $("#Feedback").val(''); $("label.error").remove(); });
});