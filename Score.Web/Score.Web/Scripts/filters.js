'use strict';

var aFilter = angular.module('app.filters', []);

aFilter.filter('interpolate', ['version', function (version) {
    return function (text) {
        return String(text).replace(/\%VERSION\%/mg, version);
    }
}]);