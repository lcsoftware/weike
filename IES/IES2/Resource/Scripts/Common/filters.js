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

appFilter.filter('parentID', function () {
    return function (inputArray, v) {
        var array = [];
        if (inputArray != undefined) {
            for (var i = 0; i < inputArray.length; i++) {
                if (inputArray[i].ParentID == v) {
                    array.push(inputArray[i]);
                }
            }
        }
        return array;
    }
});

appFilter.filter('keyReplace', function () {
    return function (inputValue) {
        if (inputValue != undefined) {
            return inputValue.replace(/wshgkjqbwhfbxlfrh/g, ' ');
        }
        return '';
    }
});

