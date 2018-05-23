$(function () {
    $.parser = function (target) {
        target = target || $("body")
        
       
        $('input.date_picker', target).datetimepicker({
            showSecond: true,
            timeFormat: 'hh:mm:ss',
            stepHour: 1,
            stepMinute: 1,
            stepSecond: 1
        });

        $('input.date_picker', target).keyup(function () {
            $(this).val("");
        });
        $('div[data-lazy="true"]', target).each(function () {
            var self = $(this);
            var url = self.data("url");
            self.jload(url)
        });
    }
    $.parser();
});
$.fn.jload = function (url, params, callback) {
    var $this = this;
    if (jQuery.isFunction(params)) {
        callback = params;
        params = undefined;

    }
    $this.mask("载入中...");
    $this.load(url, params, function (data, status, xhr) {
        $.parser($this);
        $this.unmask();
        if (callback) {
            callback.call(this, data, status, xhr);
        }
    });
    return this;
}


$(function () {

    var formSubmitHandler = function (e) {
        var $form = $(this);
        $form.attr("data-submit-complete", "form.onComplete");
    };

    var loadAndShowDialog = function (link, url) {
        var separator = url.indexOf('?') >= 0 ? '&' : '?';
        var loadingDlg = $('<div class="loadingDlg">Loading...</div>')
                           .dialog({
                               modal: true,
                               title: "请稍后...",
                               resizable: false,
                               draggable: true,
                               close: function () {
                                   $(this).dialog("destroy").remove();
                               }
                           })
        $.get(url + separator + "__swhg=" + new Date().getTime())
            .done(function (content) {
                loadingDlg.dialog("close")
                var dialog = $('<div class="modal-popup">' + content + '</div>')
                    .hide()
                    .appendTo(document.body)
                    .filter('div')
                    .dialog({
                        title: link.data('dialog-title') || '信息窗口',
                        modal: true,
                        resizable: false,
                        draggable: true,
                        width: "auto",
                        close: function () {
                            $(this).dialog("destroy").remove();
                        }
                    })
                    .find('form') // Attach logic on forms
                        .submit(formSubmitHandler)
                    .end();
                $.parser(dialog);
            });
    };


    $(document).on("click", "a[data-dialog=true]", function (evt) {
        evt.preventDefault();
        var url = this.href,
            link = $(this);
        loadAndShowDialog(link, url);
    })
});
var form = {
    onComplete: function (el, div, data) {
        var errors = $(data).find(".validation-summary-errors");
        if (errors.length == 0) {
            $(el).parents(".modal-popup").dialog("close");
            return true;
        }
        $(el).html($(data).html());
        return false;
    }
}