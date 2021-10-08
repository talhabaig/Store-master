var manufacturerGridManager = new ManufacturerGridManager();

$(function () {
    manufacturerGridManager.Init();
});


function ManufacturerGridManager() {

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
            ManufacturerGrid: $("#ManufacturerGrid"),
            txtName: $("#txtName"),
            txtAlias: $('#txtAlias'),
            txtPhone: $('#txtPhone'), 
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
        var grid = domElement.ManufacturerGrid.data("kendoGrid");

        var Name = domElement.txtName.val();
        var Alias = domElement.txtAlias.val();
        var phone = domElement.txtPhone.val();

        if (Name != "") {
            filters.push({ field: "Name", operator: "contains", value: Name });
        }

        if (Alias != "") {
            filters.push({ field: "Alias", operator: "contains", value: Alias });
        }

        if (phone != "") {
            filters.push({ field: "Phone", operator: "contains", value: phone });
        }

        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });

        //e.preventDefault();
        localStorage["kendo-Manufacturergrid-options"] = kendo.stringify(grid.getOptions());
    }

    function ResetGrid() {

        debugger;

        var filters = [];

        domElement.txtName.val('');

        domElement.txtAlias.val('');

        domElement.txtPhone.val('');


        var grid = domElement.ManufacturerGrid.data("kendoGrid");


        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });
        localStorage.removeItem("kendo-Manufacturergrid-options");
    }


    function LoadGridState() {

        var grid = domElement.ManufacturerGrid.data("kendoGrid");
        var gridDatasourceUrl = grid.dataSource.options.transport.read.url;
        var options = localStorage["kendo-Manufacturergrid-options"];

        if (options) {
            var search = JSON.parse(options);
            var searchDatasourceUrl = search.dataSource.transport.read.url;

            if (gridDatasourceUrl == searchDatasourceUrl) {
                var objFilters = search.dataSource.filter;
                var objAppliedfilters = objFilters.filters;

                objAppliedfilters.forEach(function (obj) {
                    //    alert(obj.field + ' - ' + obj.value);

                    domElement.SetSearchCriteria.find('input[type=text][data-field=' + obj.field + ']').val(obj.value);
                });

                grid.setOptions(JSON.parse(options));
            } 
        }
    }

    this.onChangeManufacturerStatus = function (source, id) {
        debugger;
        BMS.SiteScript.CustomConfirmationBox( "Are you sure, you want to change status?" , onChangeManufacturerStatusOkCallback, null, { source: source, id: id });
        //"Are you sure, you want to change program status?"
    };

    function onChangeManufacturerStatusOkCallback(objInfo) {
        debugger;
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Manufacturer.ChangeManufacturerStatus + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, ChangeManufacturerStatusCallBack, objInfo.source);
        }
    }


    function ChangeManufacturerStatusCallBack(response, anchorTagElement) {
        debugger;
        if (response && response.length > 0) {

            var responseModel = response[1];

            if (response[0] == true && responseModel.MessageModel.Type == BMS.AppConstants.MessageType.Success) {

                $(anchorTagElement).text(responseModel.Status);
                BMS.SiteScript.MessageBox.ShowSuccess(responseModel.MessageModel.Message);
                SearchGrid();
            }
            else {
                if (response[2] == BMS.AppConstants.HttpStatusCode.Forbidden) {
                    BMS.SiteScript.MessageBox.ShowError(BMS.AppConstants.HttpStatusCodeMessage.Forbidden);
                }
            }
        }
    } 
}