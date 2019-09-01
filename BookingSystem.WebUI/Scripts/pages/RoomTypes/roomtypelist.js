var RoomTypeList = function () {
    var that = this;
    var pageInitObject = [];

    var fillTable = function () {
        var table = $('#tableRoomTypeList');

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
                            var selectedRow = dt.rows({ selected: true }).data()[0];
                            var selectedId = !jQuery.isEmptyObject(selectedRow) ? selectedRow.Id : null;
                            openAddEditDialog(selectedId);
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
                    var req = Core.createModel(".box-body.filter");
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
                { "data": "Title", "name": "Title", "bSortable": false, "Width": "10" },
                { "data": "Description", "name": "Description", "bSortable": false, "Width": "10" },
                { "data": "IsActive", "name": "Active", "bSortable": true, "Width": "5" },
                { "data": "[]", "name": "#", "bSortable": true, "Width": "5" }

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
                    "targets": [1],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [2, 3],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return (data === true) ? '<span class="fa fa-check"></span>' : '<span class="fa fa-close"></span>';
                    },
                    "targets": [4],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        var actionsDelete = '<a style="margin-left:2px;" class="btn btn-danger btnDelete" data-id="' + row.Id + '" type="button"><i class="fa fa-delete"></i>' + " Delete " + ' </button>';
                        return actionsDelete;
                    },
                    "targets": 5,
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

    var refreshTable = function () {
        var oTable = $('#tableRoomTypeList').DataTable();
        oTable.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var openAddEditDialog = function (selectedId) {
        var req = { id: selectedId };
        //var getUrl = that.pageInitObject.Urls.addEditPartialUrl + "?id=" + req.id;

        $.ajax({
            url: that.pageInitObject.Urls.addEditPartialUrl,
            dataType: 'html',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(req),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                debugger;
                $("#modal-default .modal-body").html(data);
                $('#modal-default').modal({ cache: false }, 'show');
            },
            error: function (xhr) {
            }
        });
    };

    var handleEvents = function () {
        $('#modal-default').on('show.bs.modal', function () {
        });

        $('#modal-default').on('hidden.bs.modal', function () {
            Core.clearForm();
        });

        $('#tableRoomTypeList').DataTable().on('select', function (e, dt, type, indexes) {
            $(".dt-AddButton").html(that.pageInitObject.Languages.BtnEditValue);
            $(".dt-AddButton").attr("data-action", "edit");
        });

        $('#tableRoomTypeList').DataTable().on('deselect', function (e, dt, type, indexes) {
            $(".dt-AddButton").html(that.pageInitObject.Languages.BtnAddValue);
            $(".dt-AddButton").attr("data-action", "add");
        });

        $(document).on('click', '.btnDelete', function () {
            var id = $(this).attr("data-id");
            var reqObj = { id: id };

            $.ajax({
                url: that.pageInitObject.Urls.DeleteUrlAction,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(reqObj),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.ResultType == Core.responseStatus.Success) {
                        Core.showNotify("<b>Complate Successfully</b>", "", "success");
                        refreshTable();
                    }
                    else {
                        Core.showNotify("<b>Warning..</b>", data.Message, "warning");
                        return;
                    }
                },
                error: function (xhr) {
                    Core.showNotify("<b>Get an Error</b>", "", "error");
                }
            });
        });

        $(document).on('click', '.btnSave', function () {
            var req = Core.createModel("#modal-default");
            // Manuel Validations
            if (req.Title == "") {
                Core.showNotify("<b>Warning</b>", "Name must be requried", "warning");
                return;
            }

            if (req.Description == "") {
                Core.showNotify("<b>Warning</b>", "Description must be requried", "warning");
                return;
            }

            // add - or - edit
            var isSave = (parseInt(req.Id) || 0) <= 0;

            if (!jQuery.isEmptyObject(req)) {
                $.ajax({
                    url: isSave ? that.pageInitObject.Urls.SaveUrlAction
                        : that.pageInitObject.Urls.UpdateUrlAction,
                    dataType: "json",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(req),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (data) {
                        if (data.ResultType == Core.responseStatus.Success) {
                            Core.showNotify("<b>Complate Successfully</b>", "", "success");
                            $('#modal-default').modal('toggle');
                            refreshTable();
                        }
                        else {
                            Core.showNotify("<b>Warning..</b>", data.Message, "warning");
                            return;
                        }
                    },
                    error: function (xhr) {
                        Core.showNotify("<b>Get an Error</b>", "", "error");
                    }
                });
            }
        });

        $(document).on('click', '.btnClear', function () {
            Core.clearForm();
            refreshTable();
            // $('#tableRoomTypeList').DataTable().rows('.selected').deselect();
        });

        $(document).on('click', '.btnSearch', function () {
            refreshTable();
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