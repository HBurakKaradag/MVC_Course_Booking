var HotelRoomList = function () {
    var that = this;
    var pageInitObject = [];

    var fillGrid = function () {
        var table = $('#tableHotelRoomList');

        if (!jQuery().DataTable) {
            return;
        }

        table.dataTable({
            "select": true,
            "searching": false,
            "responsive": false,
            "paging": true,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 10,
            buttons: {
                buttons: [
                    {
                        text: "New Record",
                        action: function (e, dt, node, config) {
                            var req = Core.buildModel(".box-body.filter");
                            debugger;
                            window.location.href = that.pageInitObject.Urls.AddHotelRoomPageUrl + "?HotelId=" + req.HotelId;
                        }
                    }
                ],
                dom: {
                    button: {
                        tag: "button",
                        className: "btn btn-default dt-AddButton"
                    },
                    buttonLiner: {
                        tag: null
                    }
                }
            },
            "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", 
            "ajax": {
                "url": that.pageInitObject.Urls.gridLoadUrl,
                "contentType": "application/json",
                "datatype": "json",
                "type": "POST",
                "data": function (data) {
                    var req = Core.buildModel(".box-body.filter");
                    data.FilterRequest = req;

                    var request = {
                        model: data
                    };
                    return JSON.stringify(request);
                },
                "dataFilter": function (data) {
                    return data;
                }
            },
            "columns": [
                { "data": "[]", "name": "", "bSortable": false },
                { "data": "Id", "name": "Id", "bSortable": false, "Width": "0" },
                { "data": "HotelId", "name": "HotelId", "bSortable": false, "Width": "0" },
                { "data": "HotelName", "name": "HotelName", "bSortable": false, "Width": "15" },
                { "data": "RoomTypeId", "name": "RoomTypeId", "bSortable": false, "Width": "0" },
                { "data": "RoomName", "name": "RoomName", "bSortable": false, "Width": "15" },
                { "data": "RoomCapacity", "name": "RoomCapacity", "bSortable": false, "Width": "5" },
                { "data": "ImageUrl", "name": "ImageUrl", "bSortable": false, "Width": "5" },
                { "data": "Price", "name": "Price", "bSortable": false, "Width": "5" },
                { "data": "IsActive", "name": "IsActive", "bSortable": false, "Width": "5" }
            ],
            "columnDefs": [
                {
                    "sortable": false,
                    "searchable": false,
                    "className": 'select-checkbox',
                    "targets": [0]
                },

                {
                    "sortable": false,
                    "targets": [1, 2, 4],
                    "visible": false,
                    "searchable": false
                },
                {
                    "render": function (data, type, row) {
                        //if (row.ImageUrl == '')
                        return "No Image";
                        //else {
                        //
                        //    var imageUrlStr = '<img src="file:' + row.ImageUrl + '" slt="' + row.RoomName + '"  height="42" width="42">';
                        //    imageUrlStr = imageUrlStr.split('\\').join('/')

                        //    return imageUrlStr;
                        //}
                    },
                    "targets": 7,
                    "class": "text-center"
                }

            ]
            ,
            select: {
                style: 'os',
                selector: 'td:first-child'
            }
        });
    };

    var refreshGrid = function () {
        var tbl = $('#tableHotelRoomList').DataTable();
        tbl.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var handleEvents = function () {
        $('#HotelId').on("change", function (e) {
            refreshGrid();
        });

        $(document).on('click', '.btnClear', function () {
            Core.clearForm();
            refreshGrid();
        });

        $(document).on('click', '.btnSearch', function () {
            refreshGrid();
        });
    };

    return {
        init: function (initObject) {
            that.pageInitObject = initObject;

            if (!jQuery().dataTable) {
                return;
            }
            fillGrid();
            handleEvents();
            handleStartup();
        }
    };
}();