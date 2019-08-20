var HotelTypelist = function () {
    var responseStatus = { Success: 1, Info: 2, Warning: 3, Error: 4 };

    var isRefresh = false;

    var fillTable = function () {
        var table = $('#tableHotelTypeList');

        if (!jQuery().DataTable) {
            return;
        }

        table.dataTable({
            "iDisplayLength": 10,
            "bLengthChange": false,
            "searching": false,
            "select": true,
            "processing": true,
            "serverSide": true,
            buttons: [

            ],
            "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",
            "ajax": {
                "url": "/Hotel/GetHotelTypeList",
                "contentType": "application/json",
                "type": "POST",
                "data": function (data) {
                    //var req = Core.createModel();
                    //data.FilterRequest = req;

                    var request = {
                        model: data
                    };
                    return JSON.stringify(request);
                },
                "dataFilter": function (data) {
                    var json = jQuery.parseJSON(data);
                    return json.d;
                }
            },
            "columns": [
                { "data": "ID", "bSortable": false, "width": 0 },
                { "data": "Title", "bSortable": false, "width": 150 },
                { "data": "Description", "bSortable": false },
                { "data": "Active", "bSortable": true },
                { "data": "Deleted", "bSortable": false }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
                //,
                //{
                //    "targets": [0, 1, 2, 3, 4, 5],
                //    "class": "td-center"
                //},
                //{
                //    "render": function (data, type, row) {
                //        return moment(data).format('DD-MM-YYYY HH:mm:ss');
                //    },
                //    "targets": [1],
                //    "sWidth": "10%",
                //    "class": "td-center"
                //},
                //{
                //    "render": function (data, type, row) {
                //        var actions = "<a href=" + data + " target='_blank'> <img src='../../assets/img/documentIcon.png' style='width: 32px; height: 32px;'/> </a>";
                //        return actions;
                //    },
                //    "targets": [5],
                //    "class": "td-center"
                //}
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
        $('#tableDocumentList').on('select.dt', function (e, dt, type, indexes) {
            var data = dt.rows(indexes).data();
            if (!jQuery.isEmptyObject(data) && !jQuery.isEmptyObject(data[0])) {
                var url = data[0].Url;
                if (url != null || url != '' || url != 'undefined') {
                    $('#pdfViewer_frame').attr('src', url);
                }
            }
        });

        $(document).on("keypress", ".numericTextBox", function (evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        });

        $(document).on('click', '.btn-clear', function () {
            Core.clearForm();
            $('#pdfViewer_frame').attr('src', "images/noPreview.png");

            $('#tableHotelTypeList').DataTable().rows('.selected').deselect();
        });

        $(document).on('click', '#btnSearch', function () {
            if (isRefresh) {
                refreshTable();
            } else {
                fillTable();
                isRefresh = true;
            }
        });

        //$('.date-picker').datepicker({
        //    autoclose: true,
        //    language: 'tr',
        //    format: 'dd.mm.yyyy'
        //});
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