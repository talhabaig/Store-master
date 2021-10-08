BMS.UtilityFunctions = {
    //Compares two string values. By default comparison is not case sensitive.
    isEqual: function (value1, value2, isCaseSensitive) {
        if (value1 == null && value2 == null) return true;
        if (value1 == null || value2 == null) return false;

        return (isCaseSensitive == true) ? (value1 == value2) : (value1.toString().toLowerCase() == value2.toString().toLowerCase());
    },
    findIntersectors: function (targetSelector, intersectorsSelector) {
        var intersectors = [];

        var $target = $(targetSelector);
        var tAxis = $target.offset();
        var t_x = [tAxis.left, tAxis.left + $target.outerWidth()];
        var t_y = [tAxis.top, tAxis.top + $target.outerHeight()];

        $(intersectorsSelector).each(function () {
            var $this = $(this);
            var thisPos = $this.offset();
            var i_x = [thisPos.left, thisPos.left + $this.outerWidth()]
            var i_y = [thisPos.top, thisPos.top + $this.outerHeight()];

            if (t_x[0] < i_x[1] && t_x[1] > i_x[0] &&
                t_y[0] < i_y[1] && t_y[1] > i_y[0]) {
                intersectors.push($this);
            }

        });
        return intersectors;
    },
    //Finds the item in array.
    findItemInArray: function (valueToMatch, arrayList, isCaseSensitive) {

        var itemFound = {
            index: -1,
            item: null
        };
        try {
            if (arrayList != null && arrayList.length > 0) {
                for (var i = 0; i < arrayList.length; i++) {
                    if (this.isEqual(arrayList[i].toString(), valueToMatch.toString(), isCaseSensitive)) {
                        itemFound.index = i;
                        itemFound.item = arrayList[i];
                        break;
                    }
                }
            }
        } catch (ex) {
        }

        return itemFound;
    },

    //Finds the item in objects list. It compares the value with property name (passed to function) of objects in list.
    findItemInObjectList: function (valueToMatch, propertyName, objectsList) {

        var itemFound = {
            index: -1,
            item: null
        };
        var propertiesList = propertyName.split('.');

        try {
            if (objectsList != null && objectsList.length > 0) {

                for (var i = 0; i < objectsList.length; i++) {

                    if (propertiesList.length > 1) {

                        if (this.isEqual(objectsList[i][propertiesList[0]][propertiesList[1]].toString(), valueToMatch.toString())) {
                            itemFound.index = i;
                            itemFound.item = objectsList[i];
                            break;
                        }
                    } else {

                        if (this.isEqual(objectsList[i][propertyName].toString(), valueToMatch.toString())) {
                            itemFound.index = i;
                            itemFound.item = objectsList[i];
                            break;
                        }
                    }
                }
            }
        } catch (ex) {
        }

        return itemFound;
    },

    // Creating Deep Copy for any model/object
    getDeepCopy: function getDeepCopy(object) {
        //return jQuery.extend(true, {}, object);
        return JSON.parse(JSON.stringify(object));
    },

    //Get query string variable value. name is any variable in query string and is case insensitive.
    getUrlVar: function (name) {
        return $.getUrlVar(name);
    },

    //get all variables from query string.
    getUrlVars: function () {
        return $.getUrlVars();
    },

    //sort the json object on some specific key
    sortJSON: function (data, key, sortBy) {
        return data.sort(function (a, b) {
            var x = a[key];
            var y = b[key];
            if (sortBy.toLowerCase() === 'asc') {
                return ((x < y) ? -1 : ((x > y) ? 1 : 0));
            }
            if (sortBy.toLowerCase() === 'desc') {
                return ((x > y) ? -1 : ((x < y) ? 1 : 0));
            }
        });
    },

    //Format numeric value by comma separater
    getCommaFormattedNumber: function (amount) {

        if (!isNaN(amount)) {
            amount = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        return amount;
    },

    setData: function (elementId, key, data) {
        $('#' + elementId).data(key, data);
    },

    getData: function (elementId, key) {
        $('#' + elementId).data(key);
    },

    IsJsonString: function (str) {
        try {
            JSON.parse(str);
        } catch (e) {
            return false;
        }
        return true;
    },

    ValidateForm: function (form) {
        //Validating dynamic input controls in popup
        form.data("unobtrusiveValidation", null);
        form.data("validator", null);
        $.validator.unobtrusive.parse(form);
    },

    //Get element distance from top of the screen.
    GetElementDistanceFromTop: function (element) {
        var scrollTop = $(window).scrollTop();
        var elementOffset = element.offset().top;
        var distance = (elementOffset - scrollTop);
        return distance;
    },

    ScrollToElement: function (element, duration) {

        if (!element) {
            element = 0;
        } else {
            element = element.offset().top;
        }

        if (!duration) {
            duration = 800;
        }

        $('html, body').animate({ scrollTop: element }, duration);
    },

    IgnoreDefaultValidationRule: function () {

        $('form').each(function (index, element) {
            var validator = $(element).data('validator');
            if (validator) {
                validator.settings.ignore = "";
            }
        });
    },

    RebindUnobtrusiveValidation: function () {
        $('form').each(function (index, element) {
            var form = $(element);
            form.removeData("validator");
            form.removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse(form);
        });
    },

    FocusOnErrorControls: function (formSelector) {
        $(formSelector).find('.input-validation-error:eq(0)').each(function (index, element) {

            if ($(element).data("kendoDropDownList") != undefined) {
                $(element).data("kendoDropDownList").focus();
            } else if ($(element).data("kendoEditor") != undefined) {
                $(element).data("kendoEditor").focus();
            } else if ($(element).data("kendoMultiSelect") != undefined) {
                $(element).data("kendoMultiSelect").focus();
            } else if ($(element).data("kendoNumericTextBox") != undefined) {
                $(element).data("kendoNumericTextBox").focus();
            } else {
                $(element).focus();
            }

            //Scroll to error control
            $('html, body').animate({
                'scrollTop': $(element).offset().top - 45
            });
        });
    },

    EnableBootStrapToolTip: function () {
        $('[data-toggle="tooltip"]').tooltip();
    },

    ApplyDangerTooltip: function (element, errorMessage) {
        $(element).tooltip({
            'placement': 'bottom',
            show: { duration: 500 },
            template: '<div class="tooltip tooltip-bottom-left5 kbottomleft5"><div class="tooltip-arrow tooltip-arrow-left"></div><div class="tooltip-inner redcustom"></div></div>'
        })
             .attr('data-tooltip', 'tooltip-danger')
             .attr('data-toggle', 'tooltip')
             .attr('data-original-title', errorMessage);

        $(element).trigger('mouseenter');
    },


    HasArabicCharacters: function (text) {
        var regex = new RegExp("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
        return regex.test(text);
    },

    ApplyJNNFont : function(text) {
        debugger;
        if (BMS.UtilityFunctions.HasArabicCharacters(text)) {
            text = '<span class="jnnfont">' + text + '</span>';
        }

        return text;
    },

    DownloadContentFormAWS : function (e, fileName,folderName) {
        var url = BMS.AppConstants.URL.Action.Download.DownloadFromAmazonS3 + "?fileName=" + fileName + "&folder=" + folderName;
        window.open(url, '_blank');
        //window.location.href = url;
    }
};