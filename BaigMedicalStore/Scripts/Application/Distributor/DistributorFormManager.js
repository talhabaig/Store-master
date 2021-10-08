var distributorFormManager = new DistributorFormManager();

$(function () {

    distributorFormManager.Init();

});

function DistributorFormManager() {

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
            Company: $('#Company'),
            Phone: $('#Phone'),
            Address: $('#Address'),
            City: $('#City'),
            ManufacturerList: $('#ManufacturerIds'),
            hfDstributorId: $('#hfDstributorId')
        }
    }

    function Initialization() {
        debugger;

    }

    function ResetForm() {
        domElement.Name.val("");
        domElement.ManufacturerList.data("kendoMultiSelect").value([]);
        domElement.Company.val("");
        domElement.Phone.val("");
        domElement.City.val(""); 
    }

    this.OnAjaxResponse = function (xhr, status, entityName) {
        debugger;
        var respnse = JSON.parse(xhr.responseText);
        var programFormViewModel = respnse.viewModel;
        var messageModel = respnse.messageModel;

        if (messageModel.Type == BMS.AppConstants.MessageType.Success) {
            ResetForm();
        }

        BMS.SiteScript.ShowMessage(messageModel);
        domElement.btnSubmitForm.attr('disabled', false);
    }


    this.DisableSubmitButton = function (xhr, status, entityName) {
        domElement.btnSubmitForm.attr('disabled', true);
    }


}