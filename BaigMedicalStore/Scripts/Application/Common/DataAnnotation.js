 
$.validator.setDefaults({
    showErrors: function (errorMap, errorList) {
        this.defaultShowErrors();

        var controll = $("." + this.settings.validClass);
         
        debugger;
        $(controll).parent().attr('data-original-title', '');
        $(controll).closest('table').attr('data-original-title', '');
        $(controll).attr('data-original-title', '');


        $(controll).each(function (index, element) {
            $(element).closest('.kendo-validation-error').removeClass('kendo-validation-error');
        });

        var isFirstControll = true;
        // add/update tooltips 
        for (var i = 0; i < errorList.length; i++) {
            var error = errorList[i];

            var id = '#' + error.element.id;
            var isInModal = $(id).parents('.modal').length > 0;

            if ($(id).data("kendoMultiSelect") != undefined
                || $(id).data("kendoDropDownList") != undefined
                //|| $(id).data("kendoMaskedTextBox") != undefined
                || $(id).data("kendoNumericTextBox") != undefined
            ) {

                BMS.UtilityFunctions.ApplyDangerTooltip($(id).parent(), error.message)
                $(id).parent().addClass('kendo-validation-error')

            } else if ($(id).data("kendoEditor") != undefined) {
                var editor = $(id).closest('table');
                BMS.UtilityFunctions.ApplyDangerTooltip(editor, error.message)
                editor.addClass('kendo-validation-error')
            } else {
                BMS.UtilityFunctions.ApplyDangerTooltip($(id), error.message)
            }

            if (isFirstControll) {
                var submitButton = $(id).closest("form").find('input[type="submit"]');
                var clickEvent = $(submitButton).data('events')

                if (clickEvent == undefined) {
                    $(submitButton).off('click')
                    $(submitButton).on('click', function (e) {
                        BMS.UtilityFunctions.FocusOnErrorControls($(this).closest('form'));
                    });//.trigger('click');

                    BMS.UtilityFunctions.FocusOnErrorControls($(this).closest('form'));
                }

                isFirstControll = false;
            }
        }
    },
    //submitHandler: function(form) { alert('submitHandler');form.submit();   },
    //highlight: function (element, errorClass, validClass) { alert('highlight'); },
    //unhighlight: function (element, errorClass, validClass) { alert('unhighlight'); },
    //onclick: function (element, event) { alert('onclick'); },
    //errorPlacement: function (element, event) { alert('errorPlacement'); },
});



//ValidateIf Custom Attribute Javascript
$.validator.unobtrusive.adapters.add(
    "requiredif",
    ['dependentupon', 'dependentuponvalue'],
    function (options) {
        //var dependentupon = options.params.dependentupon; // shall equal 'value1'
        //var dependentuponvalue = options.params.dependentuponvalue; // shall equal 'value2'
        // TODO: use those custom parameters to define the client rules

        // simply pass the options.params here
        options.rules['requiredif'] = options.params;
        options.messages['requiredif'] = options.message;
    });
$.validator.addMethod("requiredif", function (value, element, params) {
    var dependentupon = params.dependentupon;
    var dependentuponvalue = params.dependentuponvalue;

    if ($('#' + dependentupon).val() == dependentuponvalue) {
        if (value) {
            return true;
        }

        return false;
    } else if ((dependentuponvalue || '') == '' && $('#' + dependentupon).val()) {
        if (value) {
            return true;
        }

        return false;
    }

    return true;
});

//ValidateGreaterThan Custom Attribute Javascript
$.validator.unobtrusive.adapters.add(
    "requiredgreaterthan",
    ['dependentupon', 'attributevalue', 'attributedatatype'],
    function (options) {
        //var dependentupon = options.params.dependentupon; // shall equal 'value1'
        //var attributevalue = options.params.attributevalue; // shall equal 'value2'
        // TODO: use those custom parameters to define the client rules

        // simply pass the options.params here
        options.rules['requiredgreaterthan'] = options.params;
        options.messages['requiredgreaterthan'] = options.message;
    });
$.validator.addMethod("requiredgreaterthan", function (value, element, params) {

    var dependentupon = params.dependentupon;
    var dependentUponValue = $('#' + dependentupon).val() | 0;
    var attributevalue = params.attributevalue || value;;
    var attributedatatype = params.attributedatatype;
    if (dependentUponValue > attributevalue) {
        if ((dependentUponValue != 0 && attributevalue == 0) || (dependentUponValue != 0 && (dependentUponValue > attributevalue))) {
            if (value) {
                return true;
            }
        }

        return false;
    }

    return true;
});


//ValidateLessThan Custom Attribute Javascript
$.validator.unobtrusive.adapters.add(
    "requiredlessthan",
    ['dependentupon', 'attributevalue', 'attributedatatype'],
    function (options) {
        //var dependentupon = options.params.dependentupon; // shall equal 'value1'
        //var attributevalue = options.params.attributevalue; // shall equal 'value2'
        // TODO: use those custom parameters to define the client rules

        // simply pass the options.params here
        options.rules['requiredlessthan'] = options.params;
        options.messages['requiredlessthan'] = options.message;
    });
$.validator.addMethod("requiredlessthan", function (value, element, params) {

    var dependentupon = params.dependentupon;
    var dependentUponValue = $('#' + dependentupon).val() | 0;
    var attributevalue = params.attributevalue || value;
    var attributedatatype = params.attributedatatype;
    if (dependentUponValue < attributevalue) {
        if ((dependentUponValue != 0 && attributevalue == 0) || (dependentUponValue != 0 && (dependentUponValue < attributevalue))) {
            if (value) {
                return true;
            }
        }

        return false;
    }

    return true;
});



//LessThanAndEqualTo Custom Attribute Javascript
$.validator.unobtrusive.adapters.add(
    "lessthanandequalto",
    ['dependentupon', 'attributevalue', 'attributedatatype'],
    function (options) {
        debugger;
        //var dependentupon = options.params.dependentupon; // shall equal 'value1'
        //var attributevalue = options.params.attributevalue; // shall equal 'value2'
        // TODO: use those custom parameters to define the client rules

        // simply pass the options.params here
        options.rules['lessthanandequalto'] = options.params;
        options.messages['lessthanandequalto'] = options.message;
    });
$.validator.addMethod("lessthanandequalto", function (value, element, params) {

    var isValid = false;
    var continueLoop = true;

    var dependentupon = params.dependentupon;
    $.each(dependentupon.split(','), function (index, dep) {
        dependentproperty = myValidationNamespace.getDependantProperyID(element, dep.trim());
        if (continueLoop) {
            var dependentUponValue = $('#' + dependentproperty).val() | 0;
            var attributevalue = value;
            var attributedatatype = params.attributedatatype;
            if (dependentUponValue < attributevalue) {
                //if ((dependentUponValue != 0 && attributevalue == 0) || (dependentUponValue != 0 && (dependentUponValue < attributevalue))) {
                //    if (value) {
                //        return true;
                //    }
                //}

                isValid = false;
                continueLoop = false;
            } else {
                isValid = true;
            }
        }
    });

    return isValid;
});


//GreaterThanEqualToSumOf Custom Attribute Javascript
$.validator.unobtrusive.adapters.add(
    "greaterthanequaltosumof",
    ['dependentupon', 'attributevalue', 'attributedatatype', 'errormessages'],
    function (options) {
        debugger;
        //var dependentupon = options.params.dependentupon; // shall equal 'value1'
        //var attributevalue = options.params.attributevalue; // shall equal 'value2'
        // TODO: use those custom parameters to define the client rules

        // simply pass the options.params here
        options.rules['greaterthanequaltosumof'] = options.params;
        options.messages['greaterthanequaltosumof'] = options.message;
    });
$.validator.addMethod("greaterthanequaltosumof", function (value, element, params) {
    debugger;
    var isValid = false;
    var dependentUponValue = 0;
    var dependentupon = params.dependentupon;
    $.each(dependentupon.split(','), function (index, dep) {
        dependentproperty = myValidationNamespace.getDependantProperyID(element, dep.trim());

        dependentUponValue = (dependentUponValue | 0) + ($('#' + dependentproperty).val() | 0);
    });

    var attributevalue = value;
    var attributedatatype = params.attributedatatype;
    if (dependentUponValue > attributevalue) {

        isValid = false;
    } else {

        isValid = true;
    }

    return isValid;
});
//$.validator.unobtrusive.adapters.addBool('greaterthanequaltosumof');

myValidationNamespace = {
    getDependantProperyID: function (validationElement, dependantProperty) {
        if (document.getElementById(dependantProperty)) {
            return dependantProperty;
        }
        var name = validationElement.name;
        var index = name.lastIndexOf(".") + 1;
        dependantProperty = (name.substr(0, index) + dependantProperty).replace(/[\.\[\]]/g, "_");
        if (document.getElementById(dependantProperty)) {
            return dependantProperty;
        }
        return null;
    }
} 