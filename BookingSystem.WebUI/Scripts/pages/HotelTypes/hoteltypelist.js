var HotelTypelist = function () {
    var fillTable = function () {
        var table = $('#tableHotelTypeList');

        if (!jQuery().DataTable) {
            return;
        }

        table.dataTable({
            "searching": false,
            "responsive": true,
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
                            window.location.href = $("#hotelTypeAddUrl").val();
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
                "url": "/Hotel/GetHotelTypeList",
                "contentType": "application/json",
                "datatype": "json",
                "type": "POST",
                "data": function (data) {
                    var req = Core.createModel();
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
                { "data": "Id", "name": "Id", "bSortable": false },
                { "data": "Title", "name": "Title", "bSortable": false, "width": "15%" },
                { "data": "Description", "name": "Description", "bSortable": false, "width": "15%" },
                { "data": "IsActive", "name": "IsActive", "bSortable": true, "width": "10%" },
                { "data": "IsDeleted", "name": "IsDeleted", "bSortable": false, "width": "10%" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [3, 4],
                    "class": "text-center"
                },
                //{
                //    "render": function (data, type, row) {
                //        return moment(data).format('DD-MM-YYYY HH:mm:ss');
                //    },
                //    "targets": [1],
                //    "sWidth": "10%",
                //    "class": "td-center"
                //},
                {
                    "render": function (data, type, row) {
                        return (data === true) ? '<span class="fa fa-check"></span>' : '<span class="fa fa-close"></span>';
                    },
                    "targets": [3, 4],
                    "class": "td-center"
                }
            ]
        });
    };

    var refreshTable = function () {
        var oTable = $('#tableHotelTypeList').DataTable();
        oTable.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var handleEvents = function () {
        $(document).on('click', '.btnClear', function () {
            Core.clearForm();
            refreshTable();
            // $('#tableHotelTypeList').DataTable().rows('.selected').deselect();
        });

        $(document).on('click', '.btnSearch', function () {
            refreshTable();
        });
    };

    return {
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }
            fillTable();
            handleEvents();
            handleStartup();
        }
    };
}();

jQuery(document).ready(function () {
    HotelTypelist.init();
});