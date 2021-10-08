var distributorGridManager = new DistributorGridManager();

$(function () {
    distributorGridManager.Init();
});


function DistributorGridManager() {

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
            DistributorGrid: $("#DistributorGrid"),
            txtName: $("#txtName"),
            txtCompany: $('#txtCompany'),
            txtPhone: $('#txtPhone'),
            ddlTestType: $('#TestType'),
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
        var grid = domElement.DistributorGrid.data("kendoGrid");

        var Name = domElement.txtName.val();
        var company = domElement.txtCompany.val();
        var phone = domElement.txtPhone.val();

        if (Name != "") {
            filters.push({ field: "Name", operator: "contains", value: Name });
        }

        if (company != "") {
            filters.push({ field: "Company", operator: "contains", value: company });
        }

        if (phone != "") {
            filters.push({ field: "Phone", operator: "contains", value: phone });
        }

        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });

        //e.preventDefault();
        localStorage["kendo-Distributorgrid-options"] = kendo.stringify(grid.getOptions());
    }

    function ResetGrid() {

        debugger;

        var filters = [];

        domElement.txtName.val('');

        domElement.txtCompany.val('');

        domElement.txtPhone.val('');


        var grid = domElement.DistributorGrid.data("kendoGrid");


        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });
        localStorage.removeItem("kendo-Distributorgrid-options");
    }

   

    function LoadGridState() {

        var grid = domElement.DistributorGrid.data("kendoGrid");
        var gridDatasourceUrl = grid.dataSource.options.transport.read.url;
        var options = localStorage["kendo-Distributorgrid-options"];

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

    this.onChangeDistributorStatus = function (source, id) {
        debugger;
        BMS.SiteScript.CustomConfirmationBox("Are you sure, you want to change status?", onChangeDistributorStatusOkCallback, null, { source: source, id: id });
        //"Are you sure, you want to change program status?"
    };

    function onChangeDistributorStatusOkCallback(objInfo) {
        debugger;
        if (objInfo != null) {
            var actionUrl = BMS.AppConstants.URL.Action.Distributor.ChangeDistributorStatus + "?id=" + objInfo.id;
            ServiceManager.Put(actionUrl, null, true, ChangeDistributorStatusCallBack, objInfo.source);
        }
    }


    function ChangeDistributorStatusCallBack(response, anchorTagElement) {
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