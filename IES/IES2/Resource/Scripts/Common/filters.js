'use strict';

var appFilter = angular.module('app.filters', []);

appFilter.filter('interpolate', ['version', function (version) {
    return function (text) {
        return String(text).replace(/\%VERSION\%/mg, version);
    }
}]);