$(function () {
    $.extend({
        getUrlVars: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0].toLowerCase());
                vars[hash[0].toLowerCase()] = hash[1];
            }
            return vars;
        },
        getUrlVar: function (name) {
            return $.getUrlVars()[name.toLowerCase()];
        },
        compare: function (arrayA, arrayB) {
            if (arrayA.length != arrayB.length) { return false; }
            // sort modifies original array
            // (which are passed by reference to our method!)
            // so clone the arrays before sorting
            var a = jQuery.extend(true, [], arrayA);
            var b = jQuery.extend(true, [], arrayB);
            a.sort();
            b.sort();
            for (var i = 0, l = a.length; i < l; i++) {
                if (a[i] !== b[i]) {
                    return false;
                }
            }
            return true;
        }
    });

    jQuery.fn.center = function(animationSpeed) {
        var top = Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
            $(window).scrollTop()) + "px";
        var left = Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
            $(window).scrollLeft()) + "px";
        this.css("position", "absolute");
        this.animate({ top: top, left: left }, animationSpeed);
        return this;
    };

    serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
});