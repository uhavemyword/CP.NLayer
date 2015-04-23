/// <reference path="jquery-1.6.4-vsdoc.js" />
(function ($) {
    $.fn.Translate = function (options) {
        var settings = $.extend({
            from: 'zh-CN',
            to: 'en',
            contentType: 'text/html'
        }, options);

        var key = '8B2BAF7DA8222EEB87FEE16182F265A870B76378';

        return this.each(function () {
            var t = $(this);
            var text = t.val();
            var id = t.attr(settings.from.substring(0, 2) + 'for');
            var target = $('[' + settings.to.substring(0, 2) + 'for=' + id + ']');
            if (text && id && target) {
                $.ajax({
                    url: "http://api.microsofttranslator.com/V2/Ajax.svc/Translate",
                    dataType: "jsonp",
                    jsonp: "oncomplete",
                    crossDomain: true,
                    //context: this,
                    data: { appId: key, from: settings.from, to: settings.to, contentType: settings.contentType, text: text },
                    success: function (data) {
                        target.val(data);
                    }
                });
            }
        });
    };
})(jQuery);