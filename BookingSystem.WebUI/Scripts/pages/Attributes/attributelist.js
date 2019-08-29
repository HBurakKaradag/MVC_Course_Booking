var AttributeList = function () {
    var that = this;
    var pageInitObject = [];

    var fillTable = function () {
        var table = $('#tableAttributeList');

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
                            
                            var btnActionType = node.data("action");
                            if (btnActionType == "edit") {
                                var selectedRow = dt.rows({ selected: true }).data()[0];
                                Core.fillForm("#modal-default", selectedRow);
                            }
                            $('#modal-default').modal({ cache: false }, 'show')
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
                { "data": "[]", "name": "#", "bSortable": false },
                { "data": "Id", "name": "Id", "bSortable": false, "Width": "0" },
                { "data": "Name", "name": "Name", "bSortable": false, "Width": "10" },
                { "data": "Description", "name": "Description", "bSortable": false, "Width": "10" },
                { "data": "AttributeType", "name": "Type", "bSortable": false, "Width": "10" },
                { "data": "IsActive", "name": "Active", "bSortable": true, "Width": "5" }

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
                        
                        var attributeTypeList = that.pageInitObject.AttributeTypeList;

                        return data == attributeTypeList.Hotel ? "Hotel" : data == attributeTypeList.Room ? "Room" : "-";
                    },
                    "targets": [4],
                    "class": "text-center"
                },
                {
                    "render": function (data, type, row) {
                        return (data === true) ? '<span class="fa fa-check"></span>' : '<span class="fa fa-close"></span>';
                    },
                    "targets": [5],
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
        var oTable = $('#tableAttributeList').DataTable();
        oTable.ajax.reload();
    }

    var handleStartup = function () {
        $('.box.box-default').boxWidget('toggle');
    };

    var handleEvents = function () {
        //$('#modal-default').on('show.bs.modal', function () {
        //});

        $('#modal-default').on('hidden.bs.modal', function () {
            Core.clearForm();
        });

        $('#tableAttributeList').DataTable().on('select', function (e, dt, type, indexes) {
            $(".dt-AddButton").html(that.pageInitObject.Languages.BtnEditValue);
            $(".dt-AddButton").attr("data-action", "edit");
        });

        $('#tableAttributeList').DataTable().on('deselect', function (e, dt, type, indexes) {
            $(".dt-AddButton").html(that.pageInitObject.Languages.BtnAddValue);
            $(".dt-AddButton").attr("data-action", "add");
        });

        $(document).on('click', '.btnSave', function () {
            
            
            var req = Core.createModel("#modal-default");
            // Manuel Validations
            if (req.Name == "") {
                Core.showNotify("<b>Warning</b>", "Name must be requried", "warning");
                return;
            }

            if (req.Description == "") {
                Core.showNotify("<b>Warning</b>", "Description must be requried", "warning");
                return;
            }

            if (req.AttributeType == "") {
                Core.showNotify("<b>Warning</b>", "Attribute Type must be requried", "warning");
                return;
            }

            if (!jQuery.isEmptyObject(req)) {
                $.ajax({
                    url: that.pageInitObject.Urls.SaveUrlAction,
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
            // $('#tableAttributeList').DataTable().rows('.selected').deselect();
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