var manufacturerFormManager = new ManufacturerFormManager();

$(function () {

    manufacturerFormManager.Init();
});

function ManufacturerFormManager() {

    var that = this;

    var globalVar = {

    };

    var domElement = {}

    this.Init = function () {
        InitializeVariables();
        Initialization();
    }

    function InitializeVariables() {

        domElement = {

            btnSubmitForm: $('#btnSubmitForm'),

            Name: $('#Name'),
            Alias: $('#Alias'),
            Phone: $('#Phone'),
            Address: $('#Address'),
            City: $('#City'),
            Country: $('#Country'),
            hfManufactId: $('#hfManufactId')
        }
    }

    function Initialization() {
        debugger;

    }

    function ResetForm() {
        domElement.Name.val("");

        domElement.Alias.val("");

    }

    this.OnAjaxSuccessResponse = function (xhr, status, entityName) {
        debugger;
        var messageModel = JSON.parse(xhr.responseText);
        ResetForm();
        BMS.SiteScript.ShowMessage(messageModel.messageModel); 
    }

    this.OnAjaxResponse = function (xhr, status, entityName) {
        var messageModel = JSON.parse(xhr.responseText);
        ResetForm();
        BMS.SiteScript.ShowMessage(messageModel.messageModel);

        domElement.btnSave.attr('disabled', false);
    }

    this.DisableSubmitButton = function (xhr, status, entityName) {
        debugger;
        domElement.btnSave.attr('disabled', true);
    }

}