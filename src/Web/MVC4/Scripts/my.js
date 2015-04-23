function IsInDocument(el) {
    var html = document.body.parentNode;
    while (el) {
        if (el === html) {
            return true;
        }
        el = el.parentNode;
    }
    return false;
}

function FindAndTranslateToZh(rootId) {
    $('#' + rootId).find('input[enfor],textarea[enfor]').Translate({ 'from': 'en', 'to': 'zh-CN' });
};

function FindAndTranslateToEn(rootId) {
    $('#' + rootId).find('input[zhfor],textarea[zhfor]').Translate({ 'from': 'zh-CN', 'to': 'en' });
};

jQuery.fn.ShowLoading = function (minWidth, minHeight) {
    t = this;
    var w = minWidth || 280;
    var h = minHeight || 120;
    var loadingDiv = $('<div id="loading"></div>');

    if ($.contains($('body'), loadingDiv[0])) {
        loadingDiv.css({
            top: t.position().top,
            left: t.position().left
        });
    }

    loadingDiv.css({
        width: Math.max(t.width(), w),
        height: Math.max(t.height(), h),
        position: t.height() > 0 ? 'absolute' : 'static'
    });
    t.append(loadingDiv.fadeIn(1000));
    //The rest is handled by CSS
    return t;
};

jQuery.fn.RefreshUnobtrusiveValidation = function () {
    var t = this;
    t.removeData("validator");
    t.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(t[0]);
    return t;
}

jQuery.fn.LoadData = function () {
    var t = this;
    if (t.attr('data-source') !== "") {
        t.ShowLoading();
        t.load(t.attr('data-source'));
    }
    return t;
};

jQuery.fn.ConfirmDialog = function (options) {
    t = this;
    var settings = {
        title: t.attr('title'),
        content: t.html(),
        confirm: function () { }
    };

    if (options) {
        $.extend(settings, options);
    }

    var dialogId = "confirmDialog";
    if ($('#' + dialogId).length === 0) {
        $('body').append('<div id="' + dialogId + '"></div>');
    }
    var dialogDiv = $('#' + dialogId);

    dialogDiv.html(settings.content);

    dialogDiv.dialog({
        title: settings.title,
        modal: true,
        buttons: [{
            text: local.Confirm,
            click: function () {
                settings.confirm();
                dialogDiv.remove();
            }
        }, {
            text: local.Cancel,
            click: function () {
                dialogDiv.remove();
            }
        }],
        width: 'auto',
        height: 'auto'
    });

    return t;
};

jQuery.fn.InfoDialog = function (title, content) {
    t = this;

    var dialogId = "infoDialog";
    if ($('#' + dialogId).length === 0) {
        $('body').append('<div id="' + dialogId + '"></div>');
    }
    var dialogDiv = $('#' + dialogId);

    dialogDiv.html(content);

    dialogDiv.dialog({
        title: title,
        modal: true,
        buttons: [{
            text: local.Confirm,
            click: function () {
                dialogDiv.remove();
            }
        }],
        width: 'auto',
        height: 'auto'
    });

    return t;
};

jQuery.fn.CrudDialog = function (options) {
    t = this;
    var settings = {
        title: t.text(),
        opType: t.attr('operation-type').toLowerCase(), // create, edit, delete or details
        updateTarget: t.attr('update-target'), // reload the target element
        urlForGet: t.attr('href'),
        urlForPost: t.attr('href') // MVC use same Url for get and post
    };

    if (options) {
        $.extend(settings, options);
    }

    //dialog buttons
    var dialogButtons = {};
    var btn1Text = "";
    var btn2Text = "";
    switch (settings.opType) {
        case "create":
        case "edit":
            //local is defined in _scripts.cshtml in shared views folder
            btn1Text = local.Save;
            btn2Text = local.Cancel;
            break;
        case "delete":
            btn1Text = local.Confirm;
            btn2Text = local.Cancel;
            break;
        case "details":
            break;
        default:
            btn2Text = local.Close;
    }

    var dialogId = "dialogFor" + settings.updateTarget;
    if ($('#' + dialogId).length === 0) {
        $('body').append('<div id="' + dialogId + '" class="ui-crud-dialog"></div>');
    }
    var dialogDiv = $('#' + dialogId);

    // add loading gif for initialization
    dialogDiv.html($('<div></div>').ShowLoading());

    if (btn1Text !== "") {
        dialogButtons[btn1Text] = function () {
            var form = dialogDiv.find('form');
            if (form.length <= 0) {
                dialogDiv.remove();
                return;
            }
            //if (form.RefreshUnobtrusiveValidation().valid()) {
            dialogDiv.parent().find('.ui-dialog-buttonpane button').attr('disabled', 'disabled');
            $.post(settings.urlForPost, form.serialize(), function (data) {
                if (data === consts.AjaxSuccess) {
                    $('#' + settings.updateTarget).LoadData();
                    dialogDiv.remove();
                }
                else {
                    // server side validation fail, show the partial view
                    dialogDiv.html(data);
                    dialogDiv.parent().find('.ui-dialog-buttonpane button').removeAttr('disabled');
                }
            }, 'html');
            //}
        };
    }

    if (btn2Text !== "") {
        dialogButtons[btn2Text] = function () {
            dialogDiv.remove();
        };
    }

    // open dialog
    dialogDiv.dialog({
        title: settings.title,
        modal: true,
        open: function () {
            $.get(settings.urlForGet, function (data) {
                dialogDiv.html(data)
                .dialog('option', { 'position': 'center', 'width': 'auto', 'height': 'auto' });
                if (settings.opType == "delete" || settings.opType == "details") {
                    dialogDiv.find('input').attr('readonly', 'readonly');
                }
                else {
                    dialogDiv.find('input').first().focus();
                }
            }, 'html');
        },
        close: function () {
            //$(this).dialog('close');
            dialogDiv.remove(); // close faster than above statement
        },
        buttons: dialogButtons,
        //show: 'fade',
        //closeOnEscape: false,
        //position: 'center',
        width: 'auto',
        height: 'auto'
    });

    return t;
};

$(document).ready(function () {
    // ajax setup
    $.ajaxSetup({
        cache: false, // Disable caching of AJAX responses
        beforeSend: function () {
            // $('body').addClass('processing');
        },
        complete: function () {
            // $('body').removeClass('processing');
            $('.ui-crud-dialog').dialog('option', { 'position': 'center', 'width': 'auto', 'height': 'auto' });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        }
    });

    // jQuery dataTable plugin setup
    $('table.client-side-table').livequery(function () {
        oDT = $(this).dataTable({
            'bJQueryUI': true,
            'sPaginationType': 'full_numbers',
            'iDisplayLength': 25,
            'bStateSave': true,
            'oLanguage': { 'sUrl': local.jQueryDataTableLanguageUrl },
            'fnDrawCallback': function (oSettings) {
                /* Need to redo the counters if filtered or sorted */
                if (oSettings.bSorted || oSettings.bFiltered) {
                    for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                        $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                    }
                }
            },
            'aoColumnDefs': [
                { 'bSortable': false, 'aTargets': [0] }
            ],
            'aaSorting': [[1, 'asc']]
        });
    });

    $('div[data-source]').livequery(function () {
        $(this).LoadData();
    });

    $('a[dialog-type=crud-dialog]').live('click', function () {
        $(this).CrudDialog();
        return false;
    });

    $('div.info-dialog-content').livequery(function () {
        $(this).dialog({ autoOpen: false });
    });
    $('a[dialog-type=info-dialog]').live('click', function () {
        $('#' + $(this).attr("target-content")).dialog("open");
        return false;
    });

    /////////////////////CSS//////////////////////
    // ui resizable
    $('textarea.resizable').livequery(function () {
        $(this).resizable({
            handles: 'se'
        });
    });

    //ui button
    $('.button, button').livequery(function () {
        $(this).button();
    });

    $('div.my-small-icon').live('mouseenter', function () {
        $(this).addClass('ui-state-hover');
    }).live('mouseleave', function () {
        $(this).removeClass('ui-state-hover');
    });
    /////////////////////CSS//////////////////////

    $('tbody tr').live('click', function () {
        if ($(this).hasClass('row_selected')) {
            $(this).removeClass('row_selected');
        } else {
            $(this).parent().find('tr.row_selected').removeClass('row_selected');
            $(this).addClass('row_selected');
        }
    });

    //    $('tbody tr').live('click', function () {
    //        var selectAllCheckBox = $('input:checkbox.select-all');
    //        if ($(this).hasClass('row_selected')) {
    //            $(this).removeClass('row_selected');
    //            selectAllCheckBox.removeAttr('checked');
    //        } else {
    //            $(this).addClass('row_selected');
    //            if ($(this).parent().find('tr.row_selected').length == $(this).parent().find('tr').length) {
    //                selectAllCheckBox.attr('checked', 'checked');
    //            }
    //        }
    //    });

    //    $('a.delete-selected').live('click', function (e) {
    //        e.preventDefault();
    //        var t = $('#' + $(this).attr('update-target'));
    //        var tr = t.find('tbody tr.row_selected');
    //        if (tr.length == 0) {
    //            return;
    //        }

    //        var ids = [];
    //        tr.each(function () {
    //            ids.push($(this).attr('row-id'));
    //        });

    //        var postUrl = $(this).attr('href');

    //        $(this).ConfirmDialog({
    //            title: local.Confirm,
    //            content: '<div class="ui-state-highlight ui-corner-all">' +
    //                        '<span class="ui-icon ui-icon-info"></span>' +
    //                        local.DeleteConfirm +
    //                     '</div>',
    //            confirm: function () {
    //                $.ajax({
    //                    type: 'POST',
    //                    url: postUrl,
    //                    data: { 'ids': ids },
    //                    success: function (data) {
    //                        if (data === consts.AjaxSuccess) {
    //                            t.LoadData();
    //                        }
    //                        else {
    //                            // server side failure
    //                            $(this).InfoDialog("", data)
    //                        }
    //                    },
    //                    dataType: 'html',
    //                    traditional: true
    //                });
    //            }
    //        });

    //        return $(this);
    //    });

    //    $('input:checkbox.select-all').live('click', function () {
    //        var tr = $('#' + $(this).attr('update-target')).find('tbody tr');
    //        if ($(this).is(':checked')) {
    //            tr.addClass('row_selected');
    //        } else {
    //            tr.removeClass('row_selected');
    //        }
    //    });
});