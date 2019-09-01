var AuditLogList = function () {
    var that = this;
    var pageInitObject = [];

    var fillTable = function () {
        var table = $('#tableAuditLogList');

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
            buttons: [
            ],
            "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",
            "ajax": {
                "url": that.pageInitObject.urls.gridLoadUrl,
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
                { "data": "Id", "name": "Id", "bSortable": false, "sWidth": "0" },
                { "data": "SessionId", "name": "SessionId", "bSortable": false, "Width": "10" }
            ],
            "columnDefs": [

            ]
        });
    };

    var refreshTable = function () {
        var oTable = $('#tableAuditLogList').DataTable();
        oTable.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var handleEvents = function () {
        $(document).on('click', '.btnClear', function () {
            Core.clearForm();
            refreshTable();
            // $('#tableAuditLogList').DataTable().rows('.selected').deselect();
        });
    };

    return {
        init: function (initObject) {
            // diğer js 'ler de url'leri direk nesne olarak göndermiştik. Model gibi düşünebilirsiniz.
            // Burada ise Array objeler halinde gönderildi.
            // List veya array gibi düşünülebilir.
            // ChromeDev Console üzerinden değişkeni çağırıp inceleyiniz.
            that.pageInitObject = initObject;

            if (!jQuery().dataTable) {
                return;
            }
            fillTable();
            handleEvents();
            handleStartup();
        }
    };
}();