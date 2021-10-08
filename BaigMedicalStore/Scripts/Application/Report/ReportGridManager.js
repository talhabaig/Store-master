var reportGridManager = new ReportGridManager();

$(function () {
    reportGridManager.Init();
});


function ReportGridManager() {

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
            ReportGrid: $("#ReportGrid"),
            FromDate: $("#FromDate"),
            ToDate: $("#ToDate"),
            btnSearch: $('#btnSearch'),
            btnReset: $('#btnReset'),
            SearchPanelInput: $('.search-panel-input input[type=text]'),

            SetSearchCriteria: $('.search-panel-input')
        }
    }


    function Initialization() {
/*        LoadGridState();*/
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
        var grid = domElement.ReportGrid.data("kendoGrid");

        var FromDate = domElement.FromDate.data('kendoDatePicker').value();
        var ToDate = domElement.ToDate.data('kendoDatePicker').value(); 

        if (FromDate !== "") {
            filters.push({ field: "FromDate", operator: "contains", value: FromDate });
        }

        if (ToDate !== "") {
            filters.push({ field: "ToDate", operator: "contains", value: ToDate });
        }



        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });

        //e.preventDefault();
        localStorage["kendo-ReportGrid-options"] = kendo.stringify(grid.getOptions());
    }

    function ResetGrid() {

        var filters = [];
        domElement.FromDate.data('kendoDatePicker').value("");
        domElement.ToDate.data('kendoDatePicker').value("");


        var grid = domElement.ReportGrid.data("kendoGrid");
        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });
        localStorage.removeItem("kendo-ReportGrid-options");
    } 
     
    
}