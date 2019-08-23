var HotelTypeAdd = function () {
    var handleEvents = function () {

        $(".btnSave").click(function () {
          
            var req = Core.createModel();

            debugger;
            var x = 5;

        });
    }

    return {
        init: function () {
            handleEvents();
        }
    };
}();

jQuery(document).ready(function () {
    HotelTypeAdd.init();
});