var itemGridManager = new ItemGridManager();

$(function () {
    itemGridManager.Init();
});


function ItemGridManager() {

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
            txtCode: $("#txtCode"),
             txtLocation: $('#txtLocation'),
             btnSearch: $('#btnSearch'),
            btnReset: $('#btnReset'),
            SearchPanelInput: $('.search-panel-input input[type=text]'),

            SetSearchCriteria: $('.search-panel-input')
        }
    }


    function Initialization() {
        LoadGridState();
    }

    function BindEvents() {

        domElement.btnSearch.off();
        domElement.btnSearch.on('click', function () {
            SearchGrid();
        });

        domElement.btnReset.off();
        domElement.btnReset.on('click', function () {
            ResetGrid();
        });

        domElement.SearchPanelInput.off();
        domElement.SearchPanelInput.on('keyup', function (event) {
            if (event.keyCode == 13) {
                SearchGrid();
            }

        });
    }

    function SearchGrid() {

        debugger;

        var filters = [];
        var grid = domElement.ItemGrid.data("kendoGrid");

        var Name = domElement.txtName.val();
        var Code = domElement.txtCode.val();
       
        var Location = domElement.txtLocation.val();
       

        if (Name != "") {
            filters.push({ field: "Name", operator: "contains", value: Name });
        }

        if (Code != "") {
            filters.push({ field: "Code", operator: "contains", value: Code });
        }

       

        if (Location != "") {
            filters.push({ field: "Location", operator: "contains", value: Location });
        }
 


        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });

        //e.preventDefault();
        localStorage["kendo-Itemgrid-options"] = kendo.stringify(grid.getOptions());
    }

    function ResetGrid() {
         
        var filters = [];

        domElement.txtName.val('');

        domElement.txtCode.val('');
         
        domElement.txtLocation.val('');

        
        var grid = domElement.ItemGrid.data("kendoGrid");


        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });
        localStorage.removeItem("kendo-Itemgrid-options");
    }


    function LoadGridState() {

        var grid = domElement.ItemGrid.data("kendoGrid");
        var gridDatasourceUrl = grid.dataSource.options.transport.read.url;
        var options = localStorage["kendo-Itemgrid-options"];

        if (options) {
            var search = JSON.parse(options);
            var searchDatasourceUrl = search.dataSource.transport.read.url;

            if (gridDatasourceUrl == searchDatasourceUrl) {
                var objFilters = search.dataSource.filter;
                var objAppliedfilters = objFilters.filters;

                objAppliedfilters.forEach(function (obj) {
                   
                    domElement.SetSearchCriteria.find('input[type=text][data-field=' + obj.field + ']').val(obj.value);
                });

                grid.setOptions(JSON.parse(options));
            }
        }
    }

    this.onChangeItemStatus = function (source, id) {
        debugger;
        BMS.SiteScript.CustomConfirmationBox("Are you sure, you want to change status?", onChangeItemStatusOkCallback, null, { source: source, id: id }); 
    };

    function onChangeItemStatusOkCallback(objInfo) {
        debugger;
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Item.ChangeItemStatus + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, ChangeItemStatusCallBack, objInfo.source);
        }
    }


    function ChangeItemStatusCallBack(response, anchorTagElement) {
        debugger;
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




    this.addToOrder = function (source, id) {
        debugger;
        BMS.SiteScript.CustomConfirmationBox("Are you sure, you want to add this to Order?", addToOrderOkCallback, null, { source: source, id: id });
        //"Are you sure, you want to change program status?"
    };

    function addToOrderOkCallback(objInfo) {
        debugger;
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Item.AddItemToOrder + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, AddtoOrderStatusCallBack, objInfo.source);
        }
    }


    function AddtoOrderStatusCallBack(response, anchorTagElement) {
        debugger;
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