var itemFormManager = new ItemFormManager();

$(function () {

    itemFormManager.Init();
});

function ItemFormManager() {

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
            Formula: $('#Formula'),
            ddlLocation: $('#LocationId'),
            PiecesInPaking: $('#PiecesInPaking'),
            PurchasePrice: $('#PurchasePrice'),
            SalePrice: $('#SalePrice'),
            hfItemId: $('#hfItemId')
        }
    }

    function Initialization() { 
    }

    function ResetForm() {
        domElement.Name.val("");

        domElement.Formula.val("");

        domElement.PiecesInPaking.val("");
        domElement.PurchasePrice.val(0);
        domElement.SalePrice.val(0);
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