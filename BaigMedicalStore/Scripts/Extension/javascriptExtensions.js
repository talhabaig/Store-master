/*** String Extension Functions ***/

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
};

RegExp.escape = function(text) {
    return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
};
String.prototype.replaceAll = function (search, replace) {
    return this.replace(new RegExp(RegExp.escape(search), 'g'), replace);
};

/*** Array Extension Functions ***/

Array.prototype.swapItems = function (a, b) {
    this[a] = this.splice(b, 1, this[a])[0];
    return this;
};
Array.prototype.remove = function (value) {
    for (var i = this.length - 1; i >= 0; i--) {
        if (this[i] == value) {
            this.splice(i, 1);
        }
    }
};
Array.prototype.contains = function (value) {
    for (var i = this.length - 1; i >= 0; i--) {
        if (this[i] == value) {
            return true;
        }
    }

    return false;
};
Array.prototype.distinct = function () {
    var u = {}, a = [];
    for (var i = 0, l = this.length; i < l; ++i) {
        if (u.hasOwnProperty(this[i])) {
            continue;
        }
        a.push(this[i]);
        u[this[i]] = 1;
    }
    return a;
}