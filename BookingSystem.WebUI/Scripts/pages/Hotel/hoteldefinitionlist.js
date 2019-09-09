var HotelDefinitionlist = function () {
    var that = this;
    var pageInitObject = [];

    var fillGrid = function () {
        var table = $('#tableHotelDefinitionList');

        if (!jQuery().DataTable) {
            return;
        }

        table.dataTable({
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
                            window.location.href = that.pageInitObject.Urls.HotelAddEditActionUrl;
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
                "url": that.pageInitObject.Urls.GridLoadUrl,
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
                    // var json = JSON.stringify jQuery.parseJSON(data);
                    // return json.data;
                }
            },
            "columns": [
                { "data": "Id", "name": "Id", "bSortable": false, "sWidth": "0" },
                { "data": "HotelName", "name": "HotelName", "bSortable": false, "Width": "10" },
                { "data": "Title", "name": "Title", "bSortable": false, "Width": "10" },
                { "data": "Url", "name": "Url", "bSortable": false, "Width": "10" },
                { "data": "HotelTypeId", "name": "HotelTypeId", "bSortable": true, "Width": "5" },
                { "data": "IsActive", "name": "IsActive", "bSortable": false, "Width": "5" },
                { "data": "IsDeleted", "name": "IsDeleted", "bSortable": false, "Width": "5" },
                { "data": "#", "name": "#", "bSortable": false }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [1, 2, 3],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        var fData = that.pageInitObject.HotelTypesJSon.filter(x => x.Value == row.HotelTypeId);

                        if (!jQuery.isEmptyObject(fData)) {
                            return fData[0].Text;
                        }
                        else {
                            return "-";
                        }
                    },
                    "targets": [4],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return (data === true) ? '<span class="fa fa-check"></span>' : '<span class="fa fa-close"></span>';
                    },
                    "targets": [5, 6],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        var editUrl = that.pageInitObject.Urls.HotelAddEditActionUrl + '?hotelId=' + row.Id;
                        var actionsEdit = '<a style="margin-right:2px;" href="' + editUrl + '" class="btn btn-info" data-id="' + row.Id + '" type="button"><i class="fa fa-edit"></i>' + " Edit " + ' </button>';
                        return actionsEdit;
                    },
                    "targets": 7,
                    "class": "text-center"
                }
            ]
        });
    };

    var refreshGrid = function () {
        var tbl = $('#tableHotelDefinitionList').DataTable();
        tbl.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var handleEvents = function () {
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