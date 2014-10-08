'use strict';

var filterApp = angular.module('app.filters', [])

filterApp.filter('interpolate', ['version', function (version) {
        return function (text) {
            return String(text).replace(/\%VERSION\%/mg, version);
        }
    }]);

filterApp.filter('filterFuncByParent', function () {
    return function (inputArray, v) {
        var array = [];
        if (inputArray != undefined) {
            for (var i = 0; i < inputArray.length; i++) {
                if (inputArray[i].FuncParent == v) {
                    array.push(inputArray[i]);
                }
            }
        }
        return array;
    }
}); 