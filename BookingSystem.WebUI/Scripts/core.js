var Core = function () {
    "use strict";

    var responseStatus = { Success: 1, Info: 2, Warning: 3, Error: 4 };

    var createModel = function (param) {
        if (param) {
            if (typeof (param) === "object") return createModelMain(param, false);
            else if (typeof (param) === "string") return createModelMain($(param, false));
        }
        return createModelMain($(document), false);
    }

    var createModelMain = function (object, upper) {
        var model = {};
        $(object).find("[data-model]").each(function () {
            var $this = $(this);
            var key, value;
            var $el = $this,
                type = $this.attr('type'),
                dataType = $this.attr('data-type');

            key = $(this).attr("data-model");
            switch (type) {
                case 'checkbox':
                    value = $(this).prop('checked');
                    break;
                case 'radio':
                    value = $(this).prop('checked');
                    //value = $el.filter('[value="' + val + '"]:checked').val();
                    break;
                case 'number':
                    value = $(this).val();
                    if (isNaN(value)) {
                        value = 0;
                    }
                    else if (value % 1 === 0) {
                        value = parseInt(value);
                    } else {
                        value = parseFloat(value);
                    }
                    break;
                default:
                    value = upper && ($(this).data("type") != "email") ? $(this).val().toUpperCase() : $(this).val();
            }
         
            model[key] = value;
        });
        return model;
    }

    var clearForm = function (param) {
        if (param) {
            if (typeof (param) === "object") return clearFormMain(param);
            else if (typeof (param) === "string") return clearFormMain($(param));
        }
        return clearFormMain($(document));
    }

    var clearFormMain = function (object) {
        $(object).find("[data-model]").each(function () {
            var $this = $(this);
            var type = $this.attr('type');
            debugger;
            switch (type) {
                case 'checkbox':
                    $this.removeAttr('checked');
                    $this.prop("checked", false);
                    break;
                case 'radio':
                    $this.filter('[value=""]').removeAttr('checked');
                    break;
                default:
                    $this.val('');
            }

        });
    }

    var fillForm = function (data) {
        fillFormMain($(document), data);
    }

    var fillFormMain = function (object, data) {
        $.each(data, function (name, val) {
            var $el = $(object).find('[data-model="' + name + '"]'),
                type = $el.attr('type'),
                dataType = $el.attr('data-type');

          
          
            switch (type) {
                case 'checkbox':
                    $el.prop('checked', val);
                    break;
                case 'radio':
                    $el.filter('[value="' + val + '"]').attr('checked', 'checked');
                    break;
                default:
                    $el.val(val);
            }
            if (dataType == 'label') {
                $el.html(val);
            }
        });
    }

    return {
        createModel: createModel,
        clearForm: clearForm,
        fillForm: fillForm,
        responseStatus: responseStatus,
        init: function () {
        }
    }
}();