$(function () {
    try {
        debugger;
        BMS.SiteScript.Init();
    } catch (e) {
    }
});

BMS.SiteScript = {
    Init: function () {

        //Turn off cache in ajax calls
        $.ajaxSetup({ cache: false });

        //Setting kendo culture
        //kendo.culture("en-US");
        this.EnableBootstrapTooltip();
        //Disable autocomplete option in all textboxes
        this.SwitchAutoCompleteOnTextBoxes(true);

        this.ShowNotificationMessage();

        //this.AddRemoveCssClasses();
        //this.AjaxErrorHandler();
        //this.ApplyKendoTooltip();
        this.ShowHideWebsiteSections();

        // Run this function for all validation error messages
        this.ApplyQtipOnValidationMessages();

      
        this.BindEvents();

        //Detect browser and show message if browser is not supported or version is old
         
        this.ShowHTMLContent();

        this.MenuOpen();
 
    },


    BindEvents: function () {

        //Bind side navigation panel button click event
         
        //Stop zooming browser screen by shortcut keys
        $(document).keydown(function (event) {
            //debugger;

            if (event.ctrlKey == true && (event.which == '61' || event.which == '107' || event.which == '173' || event.which == '109' || event.which == '187' || event.which == '189')) {
                event.preventDefault();
            }
        });

        //stop zooming browser screen by ctrl + mousewheel
        $(window).bind('mousewheel DOMMouseScroll', function (event) {
            //debugger;

            if (event.ctrlKey == true) {
                event.preventDefault();
            }
        });
         
    },

    MenuOpen: function () {
        debugger;

        var url = window.location;

        // for treeview
        //$('ul.treeview-menu a').filter(function () {
        //    return this.href == url;
        //}).parentsUntil(".sidebar-menu > .treeview-menu").addClass('active');

        //// for sidebar menu entirely but not cover treeview
        //$('ul.sidebar-menu a').filter(function () {
        //    return this.href == url;
        //}).parent().addClass('active');

        var action = window.location.pathname.split('/')[2];

        // If there's no action, highlight the first menu item
        if (action == "") {
            $('ul.nav li:first').addClass('active');
        } else {
            // Highlight current menu
            $('ul.sidebar-menu a[href="/' + action + '"]').parent().addClass('active');

            // Highlight parent menu item
            $('ul.sidebar-menu a[href="/' + action + '"]').parents('li').addClass('active');
        }

       
    },

     
    AdjustContainersWidthAndHeight: function () {

        //Adjust height of navigation content in site menu
        var windowHeight = $(window).outerHeight();
        var windowWidth = $(window).outerWidth();

        var siteNavMenuContainer = $('.site-nav-menu-container');
        var siteNavMenuContainerHeight = siteNavMenuContainer.outerHeight();
        var logoHeight = siteNavMenuContainer.find('.BMS-logo').outerHeight();
        var copyrightHeight = siteNavMenuContainer.find('.copyright-bar').outerHeight();
        var loginButtonBarHeight = siteNavMenuContainer.find('.login-button-bar').outerHeight();

        var siteNavMenu = siteNavMenuContainer.find('.site-nav-menu');
        var pageNavMenu = siteNavMenuContainer.find('.page-nav-menu');
        var calculatedHeight = 0;

        if (windowHeight < 768) {
            calculatedHeight = siteNavMenuContainerHeight - logoHeight - copyrightHeight - 13;
            pageNavMenu.height(calculatedHeight);
            siteNavMenu.height(calculatedHeight - loginButtonBarHeight);
        } else {
            pageNavMenu.css('height', '');
            siteNavMenu.css('height', '');
        }

    },

    ShowNotificationMessage: function () {

        var messageJsonString = $('#hdnMessage').val();
        if (messageJsonString && BMS.UtilityFunctions.IsJsonString(messageJsonString)) {
            this.ShowMessage(JSON.parse(messageJsonString));
        }
    },

    AddRemoveCssClasses: function () {
        //Add css class to kendo UI dropdown list
        $("span.k-dropdown").removeClass('kendo-dropdown').addClass('kendo-dropdown');
        $("span.k-dropdown-wrap").removeClass('kendo-dropdown-wrap').addClass('kendo-dropdown-wrap');

        $(".k-editor span.k-dropdown").removeClass('kendo-dropdown');
        $("k-editor span.k-dropdown-wrap").removeClass('kendo-dropdown-wrap');

        //Add css class to kendo UI multiselect
        $("div.k-multiselect").removeClass('kendo-multiselect').addClass('kendo-multiselect');
    },

    //Capture ajax error globally 
    AjaxErrorHandler: function () {

        $(document).ajaxError(function (event, request, options) {

            //Redirect to access denied page if request is Forbidden
            if (request.status && request.status === 403) {
                window.location.replace(BMS.AppConstants.URL.Action.AccessDenied);
            }
        });
    },

    //apply tooltip on each element having title attribute
    ApplyKendoTooltip: function () {

        //Kendo UI tooltip
        $('body').find('*[title]').kendoTooltip({
            width: 130,
            position: "top",
            animation: {
                open: {
                    effects: "fade:in"
                },
                close: {
                    effects: "fade:out",
                    duration: 0
                }
            }
        }).show();

        //bootstrap tooltip
        //$('body').find('*[title]').tooltip('toggle');
    },

    //Show notification message box for specific interval of time. 
    //take two param, message and type. type could be, success,error,info,warning
    ShowMessage: function (objMessage) {
        if (objMessage == null)
            return;

        var className = '', messageBody = '', heading = '';

        switch (objMessage.Type) {

            case BMS.AppConstants.MessageType.Success:
                className = 'bg-success';
                heading = 'Success';
                break;
            case BMS.AppConstants.MessageType.Info:
                className = 'bg-info';
                heading = 'Info';
                break;
            case BMS.AppConstants.MessageType.Error:
                className = 'bg-danger';
                heading = 'Error';
                break;
            case BMS.AppConstants.MessageType.Warning:
                className = 'bg-warning';
                heading = 'Warning';
                break;

            default:
                className = 'alert-info';
                heading = 'Info';
        }
        debugger;
        //Notification Messages        
        $('#notificationMessageContainer').empty();
        var $messagebox = $('<div/>');
        messageBody = '<button type="button" class="close" data-dismiss="alert">&times;</button><strong><h4>' + heading + '!</h4></strong>' + objMessage.Message;
        className = 'message-box alert ' + className;
        $messagebox.appendTo('#notificationMessageContainer').removeClass().addClass(className).html(messageBody).show().delay(5000).fadeOut(500);
    },

    MessageBox: {

        ShowSuccess: function (message) {
            BMS.SiteScript.ShowMessage({ Message: message, Type: 'Success' });
        },
        ShowError: function (message) {
            BMS.SiteScript.ShowMessage({ Message: message, Type: 'Error' });
        },
        ShowWarning: function (message) {
            BMS.SiteScript.ShowMessage({ Message: message, Type: 'Warning' });
        },
        ShowInfo: function (message) {
            BMS.SiteScript.ShowMessage({ Message: message, Type: 'Info' });
        }
    },

    ShowHideBusyOverlay: function (isShow) {
        //Notification Messages
        if (isShow) {
            $('.overlay-busy').show();
        } else {
            $('.overlay-busy').hide();
        }
    },

    AjaxFormEventHandler: {
        onAjaxError: function (xhr, status, entityName) {
            if (xhr.statusText == BMS.AppConstants.HttpStatusCode.Forbidden) {
                BMS.SiteScript.MessageBox.ShowError(BMS.AppConstants.HttpStatusCodeMessage.Forbidden);
            }
        },
    },

    KendoEventHandler: {
        //------------------- Kendo UI Tabstrip control event handler -----------------------------

        //this event fucntion will be called after data has been loaded using kendoui tabstrip control
        //This function is actually used for validating dynamic input fields
        onContentLoad: function (e) {

            var frm = $('form');
            frm.data("unobtrusiveValidation", null);
            frm.data("validator", null);
            $.validator.unobtrusive.parse(frm);

            //calling this function after loading dynamic content from tabstrip control
            BMS.SiteScript.AddRemoveCssClasses();

            //apply tooltip on each element having title attribute
            BMS.SiteScript.ApplyKendoTooltip();

            //show/hide section on loading dynamic contents
            BMS.SiteScript.ShowHideWebsiteSections();

            //Disable autocomplete option in all textboxes
            BMS.SiteScript.SwitchAutoCompleteOnTextBoxes(true);
        },

        //this event will be called each time when tab is selected on clicked. 
        //it will reload tab window and refresh contents each time.
        onTabSelect: function (e) {
            //assign current tabstrip control
            tbStrip = e.sender;

            var index = $(tbStrip.items());
            tbStrip.reload(index);
        },

        //Show loading image as tab is selected in kendo ui tabstrip control
        ShowTabLoading: function (e) {
            var tabstripCtrl = e.sender;

            window.setTimeout(function () {
                kendo.ui.progress(tabstripCtrl.element, true);
            });
        },

        //Hide loading image 
        HideTabLoading: function (e) {
            var tabstrip = e.sender;

            window.setTimeout(function () {
                kendo.ui.progress(tabstrip.element, false);
            });
        },

        //------------------- End Kendo UI Tabstrip control event handler --------------------------

        //------------------- Kendo UI Grid event handlers --------------------------        

        onGridError: function (e) {
            //redirect to access denied page if request is forbidden
            if (e.errorThrown && e.errorThrown.toString().toLowerCase() === BMS.AppConstants.HttpStatusCode.Forbidden.toLowerCase() || (e.xhr && e.xhr.status && e.xhr.status == 403)) {

                //Redirecting to access denied page
                window.location.replace(BMS.AppConstants.URL.Action.AccessDenied);
            }
            if (e.errorThrown && e.errorThrown.toString().toLowerCase() === BMS.AppConstants.HttpStatusCode.InternalServerError.toLowerCase() || (e.xhr && e.xhr.status && e.xhr.status == 500)) {
                BMS.SiteScript.MessageBox.ShowError(e.errorThrown);

                //Roleback Changes
                $('.k-grid').each(function (item) {
                    var grid = $(this).data("kendoGrid");
                    if (e.sender === grid.dataSource) {
                        grid.cancelChanges();
                    }
                });
            }

            if (e.errors && e.errors && e.errors["ERROR"] && e.errors["ERROR"].errors[0]) {

                //show notification message
                BMS.SiteScript.MessageBox.ShowError('An error has occured in this operation');
                //BMS.SiteScript.MessageBox.ShowError(BMS.SiteScript.GetApplicationMessage(BMS.AppConstants.ApplicationMessageKey.Operation_Error));
            }
        },

        onGridEdit: function (e) {

            //Add css class to input[type=email] control in kendo popup window to make consistency
            $("div.k-edit-form-container input[type=email]").removeClass('text-box').addClass('k-textbox');
        },

        onGridDataBound: function (e) {
            $('.k-pager-sizes').contents().filter(function () {
                return this.nodeType === 3;
            }).remove();
        }
        //------------------- End Kendo UI Grid event handlers --------------------------
    },

    //----------------------- Kendo In line Grid Commands ------------------------------
    KendoGridCommands: {

        onEdit: function (e) {
            debugger;
            $(e.container).find("td:last").html("<a href='javascript: void(0)'   onclick='BMS.SiteScript.KendoGridCommands.UpdateRow(this)' title='update'><span class='icon-grid icon-update'></a> " +
                "<a href='javascript: void(0)'   onclick='BMS.SiteScript.KendoGridCommands.CancelRow(this)' title='cancel'><span class='icon-grid icon-cancel'></a>");
        },
        onSave: function (e) {
            debugger;

        },
        CreateRow: function () {

        },

        EditRow: function (e) {
            grid = $(e).closest(".k-grid").data("kendoGrid");//$("#Grid").data("kendoGrid");
            grid.editRow($(e).closest("tr"));
        },

        UpdateRow: function (e) {
            grid = $(e).closest(".k-grid").data("kendoGrid");//$("#Grid").data("kendoGrid");
            grid.saveRow();
        },

        DeleteRow: function (e) {
            grid = $(e).closest(".k-grid").data("kendoGrid");//$("#Grid").data("kendoGrid");
            grid.removeRow($(e).closest("tr"));
        },

        CancelRow: function (e) {
            grid = $(e).closest(".k-grid").data("kendoGrid");//$("#Grid").data("kendoGrid");
            grid.cancelRow();
        }
    },
    //Hide Spinner and show html content
    ShowHTMLContent: function () {
        $('.spinner').hide();
        $('#pageContent').show();
    },
    //Show/hide web site areas based on user's access level
    ShowHideWebsiteSections: function () {
        //debugger;

        //Close navigation menu on page load if it was not pinned
        if (!BMS.AppVar.AppSetting.IsNavigationMenuPinned) {
            $('.site-nav-body').hide('swing');
            $('.site-nav-menu-container').addClass('nav-menu-close');
        }

        //var userRole = AppVar.UserRole;

        //if (userRole == "Student") {

        //    //hide and empty footer bar section at bottom of form pages.
        //    $('.button-bar-footer').hide().html('');

        //    //hide and empty button bar section at top of list pages and where ever it is used in the site.
        //    $('.button-bar').hide().html('');
        //}

        //Show selected sub navigation link 
        $('.site-nav-selected').closest('ul').show();
    },

    //Print DOM element
    printDiv: function (divId, addCurrentPageStyleReferences, customStyle) {

        var cssLinks = '';
        if (addCurrentPageStyleReferences) {

            var cssLinksCollection = $.map($('head').find('link'), function (link, index) {
                return link.outerHTML;
            });

            cssLinks = cssLinksCollection.join('');
        }

        if (customStyle == null) {
            customStyle = '';
        }

        ////Get the HTML of div having printable contents
        var divElements = $('#' + divId).html();
        var printWindow = window.open('', '_blank', 'height=500,width=700,top=100,left=100');
        printWindow.document.writeln('<html><head><title></title>' + cssLinks + customStyle + '</head><body>');
        //printWindow.document.writeln('</head><body<div class="print-cntr">');
        printWindow.document.writeln(divElements);
        printWindow.document.writeln('</body></html>');
        printWindow.document.close();
        printWindow.focus();

        setTimeout(function () { // necessary for Chrome
            printWindow.print();
            printWindow.close();
        }, 300);

        return true;
    },

    //Call this function from report success
    onReportSuccess: function () {
        $('#loading-cntr').hide();

        //Clear report column filter multiselect
        this.ClearMultiSelect('msReportColumnFilter');
    },

    onReportFailure: function () {
        $('#loading-cntr').hide();

        //Clear report column filter multiselect
        this.ClearMultiSelect('msReportColumnFilter');
    },

    CreateMultiSelect: function (id) {

        var idSelector = '#' + id;

        //Create multiselect for report column filter
        $(idSelector).kendoMultiSelect({
            autoClose: true,
            change: function (e) {

                //this.wrapper.context.options[2].text

                //array of all values in list
                var arrAllValues = [];

                $(this.element).find('option').each(function (index) {
                    arrAllValues.push(this.value);
                });

                //array of selected values in list
                var arrSelectedValues = this.value();

                //newly selected value
                var value = arrSelectedValues[arrSelectedValues.length - 1];

                //Show all columns           
                $.each(arrAllValues, function (index, value) {
                    this.ShowHideColumn(value, true);
                });

                //Hide selected columns            
                $.each(arrSelectedValues, function (index, value) {
                    this.ShowHideColumn(value, false);
                });

            }
        });
    },

    ClearMultiSelect: function (id) {
        var idSelector = '#' + id;
        var ml = $(idSelector).data('kendoMultiSelect');

        if (ml) {
            //clear values
            ml.value({});
        }

    },

    ShowHideColumn: function (columnId, isShow) {
        if (columnId && columnId.length > 0) {

            var columnSelector = 'th#' + columnId;
            var tbl = $(columnSelector).closest('table');
            var columnIndex = tbl.find(columnSelector).index() + 1;
            var columnBody = 'tbody tr td:nth-child(' + columnIndex + ')';

            if (isShow) {
                $(columnSelector).show();
                tbl.find(columnBody).show();
            } else {
                $(columnSelector).hide();
                tbl.find(columnBody).hide();
            }
        }
    },

    ApplyQtipOnValidationMessages: function () {
        debugger;
        // Run this function for all validation error messages
        $('.field-validation-error').each(function () {
            // Get the name of the element the error message is intended for
            // Note: ASP.NET MVC replaces the '[', ']', and '.' characters with an
            // underscore but the data-valmsg-for value will have the original characters
            var inputElem = '#' + $(this).attr('data-valmsg-for').replace('.', '_').replace('[', '_').replace(']', '_');

            var corners = ['left center', 'right center'];
            var flipIt = $(inputElem).parents('span.right').length > 0;

            // Hide the default validation error
            $(this).hide();

            // Show the validation error using qTip
            $(inputElem).filter(':not(.valid)').qtip({
                content: { text: $(this).text() }, // Set the content to be the error message
                position: {
                    my: corners[flipIt ? 0 : 1],
                    at: corners[flipIt ? 1 : 0],
                    viewport: $(window)
                },
                show: { ready: true },
                hide: false,
                style: { classes: 'ui-tooltip-red' }
            });
        });
    },

    CustomConfirmationBox: function (message, onOkCallback, onCancelCallback, customData) {

        debugger;


        var kendoWindow = $("<div />").kendoWindow({
            title: "Confirm",
            resizable: false,
            modal: true
        });

        kendoWindow.data("kendoWindow")
            .content($("#confirmationBox").html())
            .center().open();


        // Set confirmation message 

        $(".confirmation-message").text(message);

        // return kendoWindow;


        kendoWindow
            .find(".confirmation-ok-button,.confirmation-cancel-button")
            .click(function () {
                debugger;
                if ($(this).hasClass("confirmation-ok-button")) {
                    // alert("Deleting record...");

                    if (onOkCallback) {
                        onOkCallback(customData);
                    }

                }
                else {
                    if (onCancelCallback) {
                        onCancelCallback();
                    }
                }

                kendoWindow.data("kendoWindow").close();

            });

    },

    SwitchAutoCompleteOnTextBoxes: function (isOff) {
        var state = isOff ? "off" : "on";
        $("input[type=text]").attr("autocomplete", state);
    },

    EnableBootstrapTooltip: function () {
        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });
    },

    GetMessage: function (messageKey) {


    },

    GetApplicationMessage: function (messageKey) {

        var appMessages = localStorage.getItem(BMS.AppConstants.LocalStorageKey.AppMessages);

        if (appMessages == null) {
            var actionUrl = BMS.AppConstants.URL.Action.Common.GetApplicationMessages;
            ServiceManager.Get(actionUrl, false, GetApplicationMessagesCallBack, null, false, false, null);
            debugger;
            appMessages = localStorage.getItem(BMS.AppConstants.LocalStorageKey.AppMessages)
        }

        if (JSON.parse(appMessages)[messageKey] != undefined) {
            return JSON.parse(appMessages)[messageKey].Message;
        }

        return 'Message not found in database';
    },
}


function GetApplicationMessagesCallBack(response, param) {
    debugger;

    if (response && response.length > 0 && response[0]) {
        appMessages = response[1];

        localStorage.setItem(BMS.AppConstants.LocalStorageKey.AppMessages, appMessages);
    }
}
 