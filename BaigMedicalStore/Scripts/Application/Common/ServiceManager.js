// Service Manager for Restfull api calls.
var ServiceManager = {

    DataType: {
        JSON: 'json',
        Form: 'form'
    },

    RequestType: {
        Get: 'GET',
        Put: 'PUT',
        Post: 'POST',
        Delete: 'DELETE'
    },

    // Sends a restfull GET request to a specified URL.
    Get: function (url, async, callback, callbackParam, showOverlay, showLoaderImage, loaderImageTargetElementSelector) {

        var returnValue;

        if (async == undefined) {
            async = false;
        }

        if (async && showOverlay) {
            $('.overlay-busy').show();
        } else if (async && showLoaderImage) {
            kendo.ui.progress($(loaderImageTargetElementSelector), true);
        }

        $.ajax({
            url: url,
            type: ServiceManager.RequestType.Get,
            contentType: 'application/json; charset=utf-8',
            cache: false,
            async: async,
            success: function (result) {
                returnValue = [true, result];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            error: function (req, status, error) {
                returnValue = [false, req.responseText];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            complete: function () {
                $('.overlay-busy').hide();

                if ($(loaderImageTargetElementSelector).length) {
                    kendo.ui.progress($(loaderImageTargetElementSelector), false);
                }
            },
            ajaxComplete: function () {
                $('.overlay-busy').hide();

                if ($(loaderImageTargetElementSelector).length) {
                    kendo.ui.progress($(loaderImageTargetElementSelector), false);
                }
            }
        });

        return returnValue;
    },

    // Sends a restfull POST request to a specified URL with the specified record/data.
    Post: function (url, objData, async, callback, callbackParam, dataType, showOverlay, showLoaderImage, loaderImageTargetElementSelector) {

        var returnValue;
        var _contentType = 'application/json; charset=utf-8';

        if (async == undefined) {
            async = false;
        }

        if (dataType && dataType == ServiceManager.DataType.Form) {
            var _contentType = 'application/x-www-form-urlencoded; charset=UTF-8';
        }

        if (async && showOverlay) {
            $('.overlay-busy').show();
        } else if (async && showLoaderImage) {
            kendo.ui.progress($(loaderImageTargetElementSelector), true);
        }

        $.ajax({
            url: url,
            type: ServiceManager.RequestType.Post,
            data: objData,
            contentType: _contentType,
            dataType: ServiceManager.DataType.JSON,
            cache: false,
            async: async,

            success: function (response, status, XHR) {
                debugger;
                returnValue = [true, response, status, XHR];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            error: function (req, status, error) {
                debugger;
                returnValue = [false, req.responseText, error];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            complete: function () {
                $('.overlay-busy').hide();

                if($(loaderImageTargetElementSelector).length){
                    kendo.ui.progress($(loaderImageTargetElementSelector), false);
                }
            },
            ajaxComplete: function () {
                $('.overlay-busy').hide();

                if ($(loaderImageTargetElementSelector).length) {
                    kendo.ui.progress($(loaderImageTargetElementSelector), false);
                }
            }

        });

        return returnValue;
    },

    // Sends a restfull PUT request to a specified URL with the specified record/data.
    Put: function (url, objData, async, callback, callbackParam, dataType, showOverlay) {
        debugger;

        var returnValue;
        var _contentType = 'application/json; charset=utf-8';

        if (async == undefined) {
            async = false;
        }

        if (dataType && dataType == ServiceManager.DataType.Form) {
            var _contentType = 'application/x-www-form-urlencoded; charset=UTF-8';
        }

        if (async && showOverlay) {
            $('.overlay-busy').show();
        }

        $.ajax({
            url: url,
            type: ServiceManager.RequestType.Put,
            data: objData,
            contentType: _contentType,
            dataType: ServiceManager.DataType.JSON,
            cache: false,
            async: async,

            success: function (response, status, XHR) {
                debugger;
                returnValue = [true, response, status, XHR];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            error: function (req, status, error) {
                debugger;
                returnValue = [false, req.responseText, error];
                if (callback != undefined) {
                    callback(returnValue, callbackParam);
                }
            },
            complete: function () {
                $('.overlay-busy').hide();
            },
            ajaxComplete: function () {
                $('.overlay-busy').hide();
            }

        });

        return returnValue;
    },

    // Sends a restfull DELETE request to the specified URL.
    Delete: function (url) {
        debugger;
        var returnValue = [true, ""];

        $.ajax({
            url: url,
            type: ServiceManager.RequestType.Delete,
            dataType: ServiceManager.DataType.JSON,
            cache: false,
            async: false,
            success: function (user) {
                returnValue = [true, ""];
            },
            error: function (req, status, error) {
                returnValue = [false, req.responseText];
            }

        });

        return returnValue;
    }
};