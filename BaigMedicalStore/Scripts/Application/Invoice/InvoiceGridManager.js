var invoiceGridManager = new InvoiceGridManager();

$(function () {
    invoiceGridManager.Init();
});


function InvoiceGridManager() {

    var that = this;

    var globalVar = {

    };

    var domElement = {}

    this.Init = function () {
        InitializeVariables();
        Initialization();
        BindEvents();
    }

    function InitializeVariables() {
        domElement = {
            ItemGrid: $("#ItemGrid"),
            txtName: $("#txtName"),
            lblTotalAmount: $('#lblTotalAmount'),
            lblTotalDiscount: $('#lblTotalDiscount'),
            txtCode: $("#txtCode"),
            txtInvoice: $("#txtInvoice"),
            btnAdd: $('#btnAdd'),
            btnSearch: $('#btnSearch'),
            btnPrint: $('#btnPrint'),
            tbodyOrderItems: $("#tbodyOrderItems"),
            btnReset: $('#btnReset'),
            orderItemTemplate: $('#orderItemTemplate'),
            Quantity: $('#Quantity'),
            SearchPanelInput: $('.search-panel-input input[type=text]'),
            SetSearchCriteria: $('.search-panel-input')
        }
    }

    function Initialization() {
        LoadGridState();
    }

    function BindEvents() {

        domElement.btnAdd.off();
        domElement.btnAdd.on('click', function () {
            AddGrid();
        });

        domElement.btnSearch.off();
        domElement.btnSearch.on('click', function () {
            SearchGrid();
        });

        domElement.btnReset.off();
        domElement.btnReset.on('click', function () {
            ResetGrid();
        });

        domElement.btnPrint.off();
        domElement.btnPrint.on('click', function () {
            PrintGrid();
        });

        domElement.SearchPanelInput.off();
        domElement.SearchPanelInput.on('keyup', function (event) {
            if (event.keyCode == 13) {
                AddGrid();
            }
            else {
                prefixCall();
            }
        });
    }
    function PrintGrid() {
        var InvoiceViewModel = {
            TotalPrice: parseInt(domElement.lblTotalAmount.text()),
            Discount: parseInt(domElement.lblTotalDiscount.text()),
            NoOfItems: $('.tbodyOrderItems tr').length,
            InvDetList: []
        };
        $('#OrderItems').find('tr').each(function (index, obj) {
            debugger
            if ($(obj).find('.total').text()) {
                element = $(obj);
                var data = element.data();
                var item = {
                    ItemId: data.id,
                    ItemName: data.name,
                    Quantity: parseInt($(obj).find('.quantity').val()),
                    Discount: parseFloat($(obj).find('.total').text()) * (parseFloat($(obj).find('.discount').val()) / 100),
                    UnitPrice: data.unitprice,
                    code: data.code,
                    TotalPrice: parseFloat($(obj).find('.total').text())
                };
                InvoiceViewModel.InvDetList.push(item);
            }
        });
        var actionUrl = BMS.AppConstants.URL.Action.Invoice.saveData;
        ServiceManager.Post(actionUrl, JSON.stringify(InvoiceViewModel), true, SaveInvoiceItemCallBack, null, null, true, false, true, false);

    }
    this.changeQuanitiy = function (e) {
        debugger
        var element = e;
        var dataunit = element.closest('tr').getAttribute("data-unitprice");
        var quantity = parseInt($(element).closest('tr').find('.quantity').val());
        var total = quantity * parseInt(dataunit);
        var discount = parseInt($(element).closest('tr').find('.discount').val());
        var discounttotal = total - (total * (discount / 100))
        $(element).closest('tr').find('.total').text(discounttotal);
        CalculateNetTotal();
    };
    function SaveInvoiceItemCallBack(response) {
        debugger
        window.location.href = response[1].redirectUrl
    }

    function AddGrid() {

        var GetData = new Object();
        GetData.Name = domElement.txtName.val();
        GetData.Code = domElement.txtCode.val();

        if (GetData != null) {
            $.ajax({
                type: "GET",
                url: "/Invoice/getData",
                data: { Name: domElement.txtName.val(), Code: domElement.txtCode.val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger
                    if (response != null) {
                        var isExist = false;
                        $('#OrderItems').find('tr').each(function (index, obj) {
                            debugger
                            if ($(obj).find('.itemId').text() && parseInt($(obj).find('.itemId').text()) == response[0].ItemId) {
                                isExist = true;
                                if (isExist) {
                                    $(obj).find('.total').text(parseInt($(obj).find('.total').text()) + (response[0].UnitPrice * response[0].Quantity))
                                    $(obj).find('.quantity').val(parseInt($(obj).find('.quantity').val()) + 1)
                                }
                            };
                        })
                        if (!isExist) {
                            $('#orderItemTemplate').tmpl(response).appendTo($('#OrderItems'));
                        }
                    } else {
                        alert("Something went wrong");
                    }
                    CalculateNetTotal()
                    ResetGrid()
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });

        }
    }

    function SearchGrid() {

        var val = domElement.txtInvoice.val(); 
        window.location.href = "Invoice/PrintInvoice?invoiceId=" + val ;
        //var actionUrl = BMS.AppConstants.URL.Action.Invoice.PrintInvoice + "?invoiceId=" + val;
        //ServiceManager.Get(actionUrl, null, false, );
        //$.ajax({
        //    type: "GET",
        //    url: "/Invoice/getInvoiceData",
        //    data: { InvoiceId: domElement.txtInvoice.val() },
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        if (response != null) {
        //            var isExist = false;
        //            $('#OrderItems').find('tr').each(function (index, obj) {
        //                if ($(obj).find('.itemId').text() && parseInt($(obj).find('.itemId').text()) == response[0].ItemId) {
        //                    isExist = true;
        //                    $(obj).find('.total').text(parseInt($(obj).find('.total').text()) + (response[0].UnitPrice * response[0].Quantity))
        //                    $(obj).find('.quantity').val(parseInt($(obj).find('.quantity').val()) + 1)
        //                };
        //            })
        //            if (!isExist) {
        //                $('#orderItemTemplate').tmpl(response).appendTo($('#OrderItems'));
        //            }

        //        } else {
        //            alert("Something went wrong");
        //        }
        //        CalculateNetTotal()
        //    },
        //    failure: function (response) {
        //        alert(response.responseText);
        //    },
        //    error: function (response) {
        //        alert(response.responseText);
        //    }
        //});


    }

    function prefixCall() {
        domElement.txtName.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Invoice/MedName",
                    type: "GET",
                    dataType: "json",
                    data: { Prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.Name, value: item.Name };
                        }))
                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        })

        //grid.dataSource.filter({
        //    logic: "and",
        //    filters: filters
        //});

        /* localStorage["kendo-Itemgrid-options"] = kendo.stringify(grid.getOptions());*/
    }

    function ResetGrid() {

        var filters = [];

        domElement.txtName.val('');

        domElement.txtCode.val('');

        //var grid = domElement.ItemGrid.data("kendoGrid");


        //grid.dataSource.filter({
        //    logic: "and",
        //    filters: filters
        //});
        //localStorage.removeItem("kendo-Itemgrid-options");
    }

    function LoadGridState() {

        //var grid = domElement.ItemGrid.data("kendoGrid");
        //var gridDatasourceUrl = grid.dataSource.options.transport.read.url;
        //var options = localStorage["kendo-Itemgrid-options"];

        //if (options) {
        //    var search = JSON.parse(options);
        //    var searchDatasourceUrl = search.dataSource.transport.read.url;

        //    if (gridDatasourceUrl == searchDatasourceUrl) {
        //        var objFilters = search.dataSource.filter;
        //        var objAppliedfilters = objFilters.filters;

        //        objAppliedfilters.forEach(function (obj) {

        //            domElement.SetSearchCriteria.find('input[type=text][data-field=' + obj.field + ']').val(obj.value);
        //        });

        //        grid.setOptions(JSON.parse(options));
        //    }
        //}
    }



    this.DeleteRow = function (e) {
        var element = e;
        element.closest('tr').remove()
        CalculateNetTotal();
    };
    this.onChangeItemStatus = function (source, id) {
        BMS.SiteScript.CustomConfirmationBox("Are you sure, you want to change status?", onChangeItemStatusOkCallback, null, { source: source, id: id });
    };

    function onChangeItemStatusOkCallback(objInfo) {
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Item.ChangeItemStatus + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, ChangeItemStatusCallBack, objInfo.source);
        }
    }

    function ChangeItemStatusCallBack(response, anchorTagElement) {
        if (response && response.length > 0) {
            var responseModel = response[1];
            if (response[0] == true && responseModel.MessageModel.Type == BMS.AppConstants.MessageType.Success) {
                $(anchorTagElement).text(responseModel.Status);
                BMS.SiteScript.MessageBox.ShowSuccess(responseModel.MessageModel.Message);
            }
            else {
                if (response[2] == BMS.AppConstants.HttpStatusCode.Forbidden) {
                    BMS.SiteScript.MessageBox.ShowError(BMS.AppConstants.HttpStatusCodeMessage.Forbidden);
                }
            }
        }
    }

    function CalculateNetTotal() {
        var netTotal = 0;
        var totalvalue = 0;
        var netDiscount = 0.00;
        $('#OrderItems').find('tr').each(function (index, obj) {
            if ($(obj).find('.total').text()) {
                netTotal += parseFloat($(obj).find('.total').text())
            }
            if ($(obj).find('.discount').val() && $(obj).find('.discount').val() != '0') {
                debugger
                var total = 0;
                var disc = 0;
                total = ($(obj).find('.quantity').val() * $(obj).data().unitprice)
                disc = parseInt($(obj).find('.discount').val());
                netDiscount += total * (disc / 100)
            }
        });
        domElement.lblTotalAmount.text(netTotal);
        var totalDiscount = parseFloat(netDiscount).toFixed(2)
        domElement.lblTotalDiscount.text(totalDiscount);
    }

    this.addToOrder = function (source, id) {
        BMS.SiteScript.CustomConfirmationBox("Are you sure, you want to add this to Order?", addToOrderOkCallback, null, { source: source, id: id });
        //"Are you sure, you want to change program status?"
    };

    function addToOrderOkCallback(objInfo) {
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Item.AddItemToOrder + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, AddtoOrderStatusCallBack, objInfo.source);
        }
    }

    function AddtoOrderStatusCallBack(response, anchorTagElement) {
        if (response && response.length > 0) {
            var responseModel = response[1];
            if (response[0] == true && responseModel.MessageModel.Type == BMS.AppConstants.MessageType.Success) {
                $(anchorTagElement).text(responseModel.Status);
                BMS.SiteScript.MessageBox.ShowSuccess(responseModel.MessageModel.Message);
            }
            else {
                if (response[2] == BMS.AppConstants.HttpStatusCode.Forbidden) {
                    BMS.SiteScript.MessageBox.ShowError(BMS.AppConstants.HttpStatusCodeMessage.Forbidden);
                }
            }
        }
    }


}