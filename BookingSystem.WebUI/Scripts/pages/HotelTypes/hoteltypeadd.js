var HotelTypeAdd = function () {

    var jsUrlActions = {};

    var handleEvents = function () {
        $(".btnSave").click(function () {
            debugger;
            var req = Core.createModel();

            $.ajax({
                url: jsUrlActions.saveUrlAction,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(req),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    debugger;
                },
                error: function (xhr) {
                    debugger;
                }
            });
            
        });
    }

    return {
        init: function (params) {
            jsUrlActions = params;
            handleEvents();
        }
    };
}();

//jQuery(document).ready(function () {
//    HotelTypeAdd.init();
//});