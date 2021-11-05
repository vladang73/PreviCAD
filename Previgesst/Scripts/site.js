//Culture pour la validation


$.when(
    $.getJSON(CONFIGPATH + "supplemental/likelySubtags.json"),
    $.getJSON(CONFIGPATH + "main/fr-ca/numbers.json"),
    $.getJSON(CONFIGPATH + "supplemental/numberingSystems.json"),
    $.getJSON(CONFIGPATH + "main/fr-ca/ca-gregorian.json"),
    $.getJSON(CONFIGPATH+ "main/fr-ca/timeZoneNames.json"),
    $.getJSON(CONFIGPATH + "supplemental/timeData.json"),
    $.getJSON(CONFIGPATH + "supplemental/weekData.json")
    ).then(function () {
        return [].slice.apply(arguments, [0]).map(function (result) {
            return result[0];
        });
    }).then(Globalize.load).then(function () {
        Globalize.locale("fr-CA");
});

//Culture pour Kendo
kendo.culture("fr-CA");

//Valider tous les contrôles (même les hidden pour Kendo)
$.validator.setDefaults({
    ignore: ''
});

//Permet d'activer la validation avant les appels AJAX
// selector : balise englobant tous les formulaires à rafraîchir la validation AJAX.
function UpdateValidators(selector) {
    $(selector + " form").removeData("validator");
    $(selector + " form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(selector + " form");
}

function RemoveAjaxModal() {
    $('.modal-backdrop').remove();
    var $body = $('body').get(0);
    while ($body.attributes.length > 0)
        $body.removeAttribute($body.attributes[0].name);
}

function setAjaxData(selector, data) {
    $(selector).html(data);
    UpdateValidators(selector);
}

//Notifications
function ShowNotification(title, message, template) {
    var notification = $('#notification').data('kendoNotification');
    notification.show({
        title: title,
        message: message,
    }, template);
}

//AntiForgeryToken dans les Grid
function sendAntiForgeryToken() {
    return { "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val() }
}

//Popup générique de suppression
function deleteGenericResult(result) {
    $('#delete-generic-popup .delete-generic-result').val(result);
}

function ShowDeleteConfirm(elementToDelete, callback) {
    $('#delete-generic-popup .element-to-delete').text(elementToDelete);
    $('#delete-generic-popup .delete-generic-result').val(false);
    $('#delete-generic-popup').modal('show');

    $('#delete-generic-popup').on('hidden.bs.modal', function (e) {
        $('#delete-generic-popup').off('hidden.bs.modal');
        var args = {
            confirmed: $('#delete-generic-popup .delete-generic-result').val() == 'true'
        };
        callback(args);
    });
}

// TODO MAX

//Modifier le form action au runtime du client
$(document).on('click', '[type="submit"][data-form-action]', function (event) {
    var $this = $(this);
    var formAction = $this.attr('data-form-action');
    $this.closest('form').attr('action', formAction);
});

$(document).ready(function () {
    $('#delete-generic-popup').modal({
        show: false
    });
});

function ShowGridError(title, selecteurGrid, args) {
    console.log('There was some error in ' + selecteurGrid, args);
    ShowNotification(title, args.xhr.statusText, "errorTemplate");

    var grid = $(selecteurGrid).data("kendoGrid");
    grid.dataSource.read();

    $('#generic-popup').modal('show');
    $('#generic-popup #generic-title').text(args.xhr.statusText);
    $('#generic-popup .text-danger').text('');
    $('#generic-popup .text-danger').append(args.xhr.responseText);
}