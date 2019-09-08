var HotelRoomAdd = function () {
    var that = this;
    var pageInitObject = [];

    var handleEvents = function () {

        $("#hotelRoomAddPage").submit(function (e) {
            e.preventDefault();
            debugger;


            var formData = $('#hotelRoomAddPage')[0];
            var dataParams = new FormData(formData);


            //for (var dPars of dataParams.entries()) {
            //    console.log(dPars[0] + ', ' + dPars[1]);
            //}

            debugger;
            $.ajax({
                url: that.pageInitObject.Urls.HotelSaveUrlAction,
                //dataType: "json",
                type: "POST",
                contentType: false,
                data: dataParams,
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    /*
                     Bu kısımlarda data dan dönen obje içerisndeki verileri kullanıp daha anlamlı mesajlar veya farklı aksiyonlar alabilirsiniz.
                     */

                    if (data.ResultType == Core.responseStatus.Success) {
                        Core.showNotify("<b>Complate Successfully</b>", "", "success");
                    }
                    else {
                        Core.showNotify("<b>Warning..</b>", data.Message, "warning");
                        return;
                    }
                },
                error: function (xhr) {
                    /*
                     Bu kısımlarda xhr dan dönen obje içerisndeki verileri kullanıp daha anlamlı mesajlar veya farklı aksiyonlar alabilirsiniz.
                     */
                    Core.showNotify("<b>Get an Error</b>", "", "error");
                }
            });
            return false;
        });

    }

    var handleStartup = function () {
    };

    return {
        init: function (initObject) {
            that.pageInitObject = initObject;
            handleEvents();
            handleStartup();
        }
    };
}();