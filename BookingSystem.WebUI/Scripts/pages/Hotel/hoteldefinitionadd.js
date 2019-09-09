var HotelDefinitionAdd = function () {
    var that = this;
    var pageInitObject = [];

    var handleEvents = function () {
        $(".btnSave").click(function () {
            // manuel js validation

            var req = Core.buildModel();

            if (req.Title == "") {
                // mesaj sonrasında return etmelisiniz.
                Core.showNotify("<b>Validation</b>", "Title field must be required", "warning");
                return;
            }

            if (req.HotelName == "") {
                // mesaj sonrasında return etmelisiniz.
                Core.showNotify("<b>Validation</b>", "Hotel Name field must be required", "warning");
                return;
            }

            if (req.HotelTypeId == "" || req.HotelTypeId == "-1") {
                // mesaj sonrasında return etmelisiniz.
                Core.showNotify("<b>Validation</b>", "Hotel Type field must be required", "warning");
                return;
            }

            if (req.CityId == "" || req.CityId == "-1") {
                // mesaj sonrasında return etmelisiniz.
                Core.showNotify("<b>Validation</b>", "Hotel Type field must be required", "warning");
                return;
            }

            //Address: ""
            //CityId: "-1"
            //DistrictId: "-1"
            //HotelAttributes: (5)[{ … }, { … }, { … }, { … }, { … }]

            var hotelAttributeArray = [];
            var hotelAttributeCount = $("#HotelAttributesCount").val();

            var index;
            for (index = 0; index < hotelAttributeCount; ++index) {
                var template = "Attributes_" + index + "__";
                var id = $("#" + template + "Id").val();
                var isSelected = $("#" + template + "IsSelected").prop("checked");
                var text = $('label[for="' + template + "IsSelected" + '"]').html()

                var obj = {
                    Id: id,
                    IsSelected: isSelected,
                    Text: text
                };
                hotelAttributeArray.push(obj);
            };

            req.Attributes = hotelAttributeArray;

            if (req.Description == "") {
                Core.showNotify("<b>Validation</b>", "Description field must be required", "warning");
                return;
            }

            $.ajax({
                url: that.pageInitObject.Urls.HotelSaveUrlAction,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(req),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    /*
                     Bu kısımlarda data dan dönen obje içerisndeki verileri kullanıp daha anlamlı mesajlar veya farklı aksiyonlar alabilirsiniz.
                     */

                    if (data.ResultType == Core.responseStatus.Success) {
                        Core.showNotify("<b>Complate Successfully</b>", "", "success");
                        Core.redirectPageAfterSecond(jsUrlActions.listPageUrlAction);
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
        });

        $('#CityId').on("change", function (e) {
            var selectedValue = $(this).val();
            var req = { cityId: selectedValue };

            // ServerSide DataBinding
            $.ajax({
                url: '/Hotel/GetDistricts',
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(req),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    var districtDOM = $("#DistrictId");
                    districtDOM.empty();

                    var opt = new Option("Select District", "-1", true, true);
                    districtDOM.append(opt);

                    $.each(data, function (i, v) {
                        var opt = new Option(v.Name, v.Id, false, false);
                        districtDOM.append(opt);
                    });
                },
                error: function (xhr) {
                    Core.showNotify("<b>Get an Error</b>", "", "error");
                }
            });

            /*

             LocalDataBinding

            var districtDOM = $('#DistrictId');
            districtDOM.empty();

            var selectedValue = $(this).val(); // $('#CityId').val();
            var filteredData = that.pageInitObject.DistrictsJson.filter(p => p.ParentValue == selectedValue);

            var opt = new Option("Select ...", "-1", true, false);
            districtDOM.append(opt);

            $.each(filteredData, function (index, data) {
                var opt = new Option(data.Text, data.Value, false, data.Selected);
                districtDOM.append(opt);
            });
            */
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