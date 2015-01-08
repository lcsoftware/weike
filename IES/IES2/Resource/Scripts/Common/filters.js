'use strict';

var appFilter = angular.module('app.filters', []);

appFilter.filter('interpolate', ['version', function (version) {
    return function (text) {
        return String(text).replace(/\%VERSION\%/mg, version);
    }
}]);


appFilter.filter('dateFormat', [function () {
    return function (v) {
        var re = /-?\d+/;
        var m = re.exec(v);
        var d = new Date(parseInt(m[0]));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd");

    }
}]);